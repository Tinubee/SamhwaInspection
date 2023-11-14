using IvmUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SamhwaInspection.UI.Control.MasterSetting;

namespace SamhwaInspection.Schemas
{
    public enum 검사항목
    {
        슬롯부20Point,
        슬롯부200Point,
        큰원치수,
        작은원치수,
        높이,
        너비,
        _50_5부,
        _33_94부,
        _15부,
        X축,
        Y축
    }

    public class 마스터설정 : List<마스터변수>
    {
        private string 마스터데이터파일 { get { return Path.Combine(Global.환경설정.기본경로, "마스터데이터.json"); } }
        List<VmVariable> calValueList = new List<VmVariable>();
        
        public void Init()
        {
            base.Clear();
            calValueList = Global.비전마스터구동.글로벌변수제어.GetCalValue();
            foreach (VmVariable v in calValueList)
                this.Add(new 마스터변수(v));

            Load();
        }

        public void Load()
        {
            if (File.Exists(마스터데이터파일))
            {
                try
                {
                    마스터변수 변수 = JsonConvert.DeserializeObject<마스터변수>(File.ReadAllText(마스터데이터파일, Encoding.UTF8));
                    foreach (PropertyInfo p in 변수.GetType().GetProperties())
                    {
                        if (!p.CanWrite) continue;
                        Object v = p.GetValue(변수);
                        if (v == null) continue;
                        p.SetValue(this, v);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    //Global.오류로그(로그영역, "환경설정 로드", ex.Message, true);
                }
            }
            else
            {
                this.Save();
            }
        }
        public void Save()
        {
            if (!IvmUtils.Utils.WriteAllText(마스터데이터파일, JsonConvert.SerializeObject(this, IvmUtils.Utils.JsonSetting())))
            {
                //Global.오류로그(로그영역, "환경설정 저장", "환경설정 저장에 실패하였습니다.", true);
            }
        }
        public void Set()
        {
            foreach (var v in this)
            {
                Global.비전마스터구동.글로벌변수제어.InspectUseSet(v.변수명칭, v.보정값.ToString());
            }
        }

        public void 보정값계산()
        {
            foreach (var item in this)
            {
                if(item.실측값 == 0 || item.비전측정값 == 0) continue;

                item.보정값 = Math.Round((item.실측값 / item.비전측정값),6);
                Debug.WriteLine($"{item.검사명칭} 의 보정값 : {item.보정값} ");
            }
        }


        public void 마스터비전결과값적용(List<VmVariable> 결과리스트, 지그위치 위치, Flow구분 플로우 )
        {
            foreach (var 결과 in 결과리스트)
            {
                if (결과.Name.Contains("-1"))
                {
                 
                }
            }
        }
    }

    public class 마스터변수
    {
        [JsonProperty("name"), Description("변수명")]
        public 검사항목 검사명칭 { get; set; } = 검사항목.슬롯부20Point;
        [JsonProperty("namecalv"), Description("변수명")]
        public String 변수명칭 { get; set; } = String.Empty;
        [JsonProperty("minv"), BatchEdit(true), Description("최소값")]
        public Double 최소값 { get; set; } = 0;
        [JsonProperty("stdv"), BatchEdit(true), Description("기준값")]
        public Double 기준값 { get; set; } = 0;
        [JsonProperty("maxv"), BatchEdit(true), Description("최대값")]
        public Double 최대값 { get; set; } = 0;
        [JsonIgnore, Description("실측값")] //(mm)
        public Double 실측값 { get; set; } = 0;
        [JsonProperty("calb"), Description("보정값")]
        public Double 보정값 { get; set; } = 0;
        [JsonIgnore, Description("비전측정값")]
        public Double 비전측정값 { get; set; } = 0;
        [JsonIgnore, Description("검사결과")]
        public 결과구분 판정 { get; set; } = 결과구분.NO;

        public 마스터변수(VmVariable v)
        {
            this.변수명칭 = v.Name; //v.Name.Replace("calValue", "보정값");
            this.보정값 = Convert.ToDouble(v.Value);
            검사명칭설정();
        }

        public void 검사명칭설정()
        {
            if (this.변수명칭.Contains("Slot"))
                this.검사명칭 = 검사항목.슬롯부20Point;
            else if (this.변수명칭.Contains("Height"))
                this.검사명칭 = 검사항목.높이;
            else if (this.변수명칭.Contains("Width"))
                this.검사명칭 = 검사항목.너비;
            else if (this.변수명칭.Contains("BigCircle"))
                this.검사명칭 = 검사항목.큰원치수;
            else if (this.변수명칭.Contains("SmallCircle"))
                this.검사명칭 = 검사항목.작은원치수;
            else if (this.변수명칭.Contains("S1") || this.변수명칭.Contains("S2") || this.변수명칭.Contains("S3"))
                this.검사명칭 = 검사항목._15부;
            else if (this.변수명칭.Contains("calValueX"))
                this.검사명칭 = 검사항목.X축;
            else if (this.변수명칭.Contains("calValueY"))
                this.검사명칭 = 검사항목.Y축;
        }
    }
}
