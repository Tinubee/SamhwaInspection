﻿using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SamhwaInspection.Utils
{
    public static class Localization
    {

        public static TranslationAttribute 제목 = new TranslationAttribute("Sheet Flatness Inspection");
        public static TranslationAttribute 취소 = new TranslationAttribute("Cancle", "취소");
        public static TranslationAttribute 닫기 = new TranslationAttribute("Close", "닫기");
        public static TranslationAttribute 저장 = new TranslationAttribute("Save", "저장");
        public static TranslationAttribute 삭제 = new TranslationAttribute("Delete", "삭제");
        public static TranslationAttribute 확인 = new TranslationAttribute("Confirm", "확인");
        public static TranslationAttribute 정보 = new TranslationAttribute("Infomation", "정보");
        public static TranslationAttribute 경고 = new TranslationAttribute("Warning", "경고");
        public static TranslationAttribute 오류 = new TranslationAttribute("Error", "오류");
        public static TranslationAttribute 조회 = new TranslationAttribute("Search", "조회");

        public static TranslationAttribute 일자 = new TranslationAttribute("Day", "일자");
        public static TranslationAttribute 시간 = new TranslationAttribute("Time", "시간");

        public static Language CurrentLanguage { get { return (Language)Properties.Settings.Default.Language; } }
        public static String GetString(PropertyInfo prop) { return GetString(prop, CurrentLanguage); }
        public static String GetString(PropertyInfo prop, Language lang)
        {
            TranslationAttribute a = Common.GetAttribute<TranslationAttribute>(prop);
            if (a == null) return prop.Name;
            return a.GetString(lang);
        }
        public static String GetString(Enum num) { return GetString(num, CurrentLanguage); }
        public static String GetString(Enum num, Language lang)
        {
            TranslationAttribute a = Common.GetAttribute<TranslationAttribute>(num);
            if (a == null) return num.ToString();
            return a.GetString(lang);
        }

        //public static void SetColumnCaption(GridView view, Type source)
        //{
        //    foreach (GridColumn col in view.Columns)
        //    {
        //        try
        //        {
        //            PropertyInfo p = source.GetProperty(col.FieldName);
        //            if (p == null) continue;
        //            col.Caption = GetString(p);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine($"[{source.Name}, {col.FieldName}] {ex.Message}", "SetColumnCaption");
        //        }
        //    }
        //}

        //public static void SetColumnCaption(LookUpEdit edit, Type source)
        //{
        //    foreach (LookUpColumnInfo col in edit.Properties.Columns)
        //    {
        //        try
        //        {
        //            PropertyInfo p = source.GetProperty(col.FieldName);
        //            if (p == null) continue;
        //            col.Caption = GetString(p);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine($"[{source.Name}, {col.FieldName}] {ex.Message}", "SetLookupColumnCaption");
        //        }
        //    }
        //}
    }

    public enum Language
    {
        [Description("English")]
        EN = 0,
        [Description("한국어")]
        KO = 1,
        [Description("Slovenská")] //, ListBindable(false)
        SK = 2
    }

    public class TranslationAttribute : Attribute
    {
        public String KO = String.Empty;
        public String EN = String.Empty;
        public String SK = String.Empty;

        public TranslationAttribute(String en)
        {
            this.EN = en;
            this.KO = en;
            this.SK = en;
        }

        public TranslationAttribute(String en, String ko)
        {
            this.EN = en;
            this.KO = ko;
            this.SK = en;
        }

        public TranslationAttribute(String en, String ko, String sk)
        {
            this.EN = en;
            this.KO = ko;
            this.SK = sk;
        }

        public String GetString(Language lang)
        {
            if (lang == Language.EN) return this.EN;
            if (lang == Language.KO) return this.KO;
            if (lang == Language.SK) return this.SK;
            return this.EN;
        }

        public String GetString() { return GetString(Localization.CurrentLanguage); }
    }
}
