using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using IvmUtils;
using SamhwaInspection.Schemas;
using SamhwaInspection.UI.Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvUtils;

namespace SamhwaInspection.UI.Control
{
    public partial class SetVariables : XtraUserControl
    {
        public SetVariables() => InitializeComponent();
        private LocalizationInspection 번역 = new LocalizationInspection();

        public void Init()
        {
            this.GridView1.Init(this.barManager1);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            this.GridView1.AddEditSelectionMenuItem();
            this.GridView1.AddSelectPopMenuItems();
            this.GridControl1.DataSource = Global.비전마스터구동.글로벌변수제어;
            this.b도구설정.Click += 도구설정;
            this.b도구저장.Click += 도구저장;
            this.b설정적용.Click += 설정적용;
            Utils.Localization.SetColumnCaption(this.GridView1, typeof(VmVariable));
            this.b설정적용.Text = 번역.설정저장;
            this.GridView1.RefreshData();
        }

        public void VariableUpdate() => this.GridView1.RefreshData();

        public void Close() { }

        private void 도구설정(object sender, EventArgs e)
        {
            Teaching form = new Teaching();
            form.Show(Global.mainForm);
        }

        private void 도구저장(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm(번역.저장확인)) return;
            Global.비전마스터구동.Save();
            Global.정보로그("도구설정", "설정저장", 번역.저장완료, true);
        }

        private void 설정적용(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm(번역.적용확인)) return;
            Global.비전마스터구동.글로벌변수제어.Set();
        }

        private class LocalizationInspection
        {
            private enum Items
            {
                [Translation("Save", "설정저장")]
                설정저장,
                [Translation("It's saved.", "저장되었습니다.")]
                저장완료,
                [Translation("Save the inspection settings?", "검사 설정을 저장하시겠습니까?")]
                저장확인,
                [Translation("Do you want to apply the value of a global variable?", "Global 변수 값을 적용하시겠습니까?")]
                적용확인,
            }

            public String 설정저장 { get { return Localization.GetString(Items.설정저장); } }
            public String 저장완료 { get { return Localization.GetString(Items.저장완료); } }
            public String 저장확인 { get { return Localization.GetString(Items.저장확인); } }
            public String 적용확인 { get { return Localization.GetString(Items.적용확인); } }
        }
    }
}
