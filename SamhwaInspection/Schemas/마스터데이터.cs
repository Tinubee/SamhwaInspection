﻿using DevExpress.Office.Utils;
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
using static SamhwaInspection.Schemas.마스터데이터;
using static SamhwaInspection.UI.Control.MasterSetting;

namespace SamhwaInspection.Schemas
{
    public class 마스터데이터 : List<마스터데이터설정변수>
    {
        private string 마스터설정파일 { get { return Path.Combine(Global.환경설정.기본경로, "마스터데이터.json"); } }
        List<VmVariable> masterValueList = new List<VmVariable>();

        public void Init()
        {
            base.Clear();
            masterValueList = Global.비전마스터구동.글로벌변수제어.GetMasterInspectionValue();
            foreach (VmVariable v in masterValueList)
                this.Add(new 마스터데이터설정변수(v));

            List<마스터데이터설정변수> 자료 = Load();
        }

        public void 비전데이터적용(List<String> visionData)
        {
            for (int lop = 0; lop < visionData.Count; lop++)
            {
                this[lop].기준값 = Convert.ToSingle(visionData[lop]);
            }
        }

        private List<마스터데이터설정변수> Load()
        {
            if (!File.Exists(this.마스터설정파일)) return null;
            return JsonConvert.DeserializeObject<List<마스터데이터설정변수>>(File.ReadAllText(마스터설정파일), IvmUtils.Utils.JsonSetting());
        }

        public void Set()
        {
            int minmaxValueIndex = this.FindIndex(x => x.검사명칭.Contains("MinMaxValue"));

            foreach (var v in this)
            {
                v.최소값 = v.검사명칭.Contains("MinMaxValue") == false ? Math.Round(v.기준값 - this[minmaxValueIndex].기준값, 4) : 0;
                v.최대값 = v.검사명칭.Contains("MinMaxValue") == false ? Math.Round(v.기준값 + this[minmaxValueIndex].기준값, 4) : 0;
                Global.비전마스터구동.글로벌변수제어.InspectUseSet(v.검사명칭, v.기준값.ToString());
            }
        }

        public void Save()
        {
            if (!IvmUtils.Utils.WriteAllText(마스터설정파일, JsonConvert.SerializeObject(this, IvmUtils.Utils.JsonSetting())))
            {
                //Global.오류로그(로그영역, "환경설정 저장", "환경설정 저장에 실패하였습니다.", true);
            }
        }

        public class 마스터데이터설정변수
        {
            [JsonProperty("name"), Description("변수명")]
            public String 검사명칭 { get; set; } = String.Empty;
            [JsonProperty("minv"), BatchEdit(true), Description("최소값")]
            public Double 최소값 { get; set; } = 0;
            [JsonProperty("stdv"), BatchEdit(true), Description("기준값")]
            public Single 기준값 { get; set; } = 0;
            [JsonProperty("maxv"), BatchEdit(true), Description("최대값")]
            public Double 최대값 { get; set; } = 0;
            [JsonIgnore, Description("결과값")]
            public String 결과값 { get; set; } = String.Empty;
            [JsonIgnore, Description("검사결과")]
            public 결과구분 판정 { get; set; } = 결과구분.NO;

            public 마스터데이터설정변수(VmVariable v)
            {
                if (v != null)
                {
                    this.검사명칭 = v.Name;
                    this.기준값 = Convert.ToSingle(v.StringValue);
                    this.최소값 = this.검사명칭.Contains("MinMaxValue") == false ? Math.Round(this.기준값 - 0.005, 4) : 0;
                    this.최대값 = this.검사명칭.Contains("MinMaxValue") == false ? Math.Round(this.기준값 + 0.005, 4) : 0;
                }
            }
        }
    }
}
