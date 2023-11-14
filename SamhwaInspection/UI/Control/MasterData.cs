using DevExpress.XtraEditors;
using System;
using SamhwaInspection.UI.Form;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SamhwaInspection.Utils;
using DevExpress.XtraGrid.Views.Grid;
using SamhwaInspection.Schemas;
using static SamhwaInspection.UI.Control.MasterSetting;

namespace SamhwaInspection.UI.Control
{
    public partial class MasterData : XtraUserControl
    {
        
        public MasterData() => InitializeComponent();

        private LocalizationInspection 번역 = new LocalizationInspection();

        public void Init()
        {
            this.GridView1.Init(this.barManager1);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            this.GridView1.AddEditSelectionMenuItem();
            this.GridView1.AddSelectPopMenuItems();
            this.GridControl1.DataSource = Global.마스터데이터;

            b설정적용.Click += B설정적용_Click;
        }

        private void B설정적용_Click(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm(번역.적용확인)) return;

            Global.마스터데이터.Set();
            Global.마스터데이터.Save();
            Global.비전마스터구동.Save();

            //this.GridControl1.DataSource = Global.마스터데이터;
            //this.GridControl1.Refresh();
            this.GridView1.RefreshData();
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
