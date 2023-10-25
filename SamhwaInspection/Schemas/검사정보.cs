using IvmUtils;
using IvLibs;
using IVMRectangle = IvLibs.Regions.Rectangle;
using SamhwaInspection.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils.Extensions;
using System.Drawing;
using OpenCvSharp;

namespace SamhwaInspection.Schemas
{
    public enum 결과구분
    {
        [Description("대기중")]
        NO = 0,
        [Description("검사중")]
        IN = 1,
        [Description("Error")]
        ER = 2,
        [Description("Pass")]
        PS = 3,
        [Description("NG")]
        NG = 5,
        [Description("OK")]
        OK = 7,
    }

    public enum InspectionType
    {
        [Description("Blob")]
        BLOB = 0,
        [Description("LineFind")]
        LINEFIND = 1,
    }

    public class UnitAttribute : Attribute
    {
        public String Unit = String.Empty;
        public UnitAttribute() { }
        public UnitAttribute(String unit) { this.Unit = unit; }
    }

    public class 검사정보
    {
        [JsonProperty("cam")]
        public CameraType 카메라구분 { get; set; } = CameraType.Cam01;
        [JsonProperty("ord")]
        public Int32 검사번호 { get; set; } = 0;
        [JsonProperty("name"), Description("검사명")]
        public String 검사명칭 { get; set; } = String.Empty;
        [JsonProperty("minv"), BatchEdit(true), Description("최소값")]
        public Double 최소 { get; set; } = 0;
        [JsonProperty("stdv"), BatchEdit(true), Description("적정값")]
        public Double 기준 { get; set; } = 0;
        [JsonProperty("maxv"), BatchEdit(true), Description("최대값")]
        public Double 최대 { get; set; } = 0;
        [JsonProperty("calb"), Description("교정값")] //(µm)
        public Double 교정 { get; set; } = 0;
        [JsonProperty("use"), BatchEdit(true), Description("검사여부")]
        public Boolean 사용 { get; set; } = true;
        [JsonIgnore, Description("측정값")]
        public Double 측정 { get; set; } = 0;
        [JsonIgnore, Description("실측값")] //(mm)
        public Double 실측 { get; set; } = 0;
        [JsonIgnore, Description("검사결과")]
        public 결과구분 판정 { get; set; } = 결과구분.NO;
        [JsonProperty("Tool")]
        public IVMRectangle rectangle { get; set; }
        public 검사정보() 
        {
            rectangle = new IVMRectangle();
        }
        public 검사정보(검사정보 정보) { this.Set(정보); }

        public void Set(검사정보 정보)
        {
            if (정보 == null) return;
            foreach (PropertyInfo p in typeof(검사정보).GetProperties())
            {
                if (!p.CanWrite) continue;
                Object v = p.GetValue(정보);
                if (v == null) continue;
                p.SetValue(this, v);
            }
        }
    }


}
