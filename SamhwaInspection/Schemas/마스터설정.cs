using IvmUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        _15부
    }

    public class 마스터설정 : List<마스터변수>
    {
        List<VmVariable> calValueList = new List<VmVariable>();

        public void Init()
        {
            base.Clear();
            calValueList = Global.비전마스터구동.글로벌변수제어.GetCalValue();

            foreach (VmVariable v in calValueList)
            {
                this.Add(new 마스터변수(v));
            }
        }

        public void Set()
        {
            foreach (var v in this)
            {
                Global.비전마스터구동.글로벌변수제어.InspectUseSet(v.검사명칭, v.보정값.ToString());
            }
        }
    }

    public class 마스터변수
    {
        [JsonProperty("name"), Description("검사명")]
        public String 검사명칭 { get; set; } = String.Empty;
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
            this.검사명칭 = v.Name; //v.Name.Replace("calValue", "보정값");
            this.보정값 = Convert.ToDouble(v.Value);
        }
    }
}
