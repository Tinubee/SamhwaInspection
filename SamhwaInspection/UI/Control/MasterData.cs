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
using VM.Core;
using VM.PlatformSDKCS;

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
            b마스터값로드.Click += B마스터값로드_Click;
        }

        private void B마스터값로드_Click(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm(번역.마스터값로드)) return;
            List<IMVSGroup> tool = Global.비전마스터구동.GetItem(Flow구분.Flow1).Slot20PointGroupMouduleTool;
            List<String> slot_n_1Value = new List<String>();

            for (int lop = 0; lop < tool.Count; lop++)
            {
                foreach (var v in tool[lop].Outputs)
                {
                    if (v.Value == null) continue;
                    if (v.Name.Contains("Slot") && v.Name.Contains("-5"))
                    {
                        if (((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])v.Value).Count() == 1)
                        {
                            if(lop == tool.Count -1) MvUtils.Utils.SaveOK(번역.불러오기오류);
                            continue;
                        }

                        String resultString = ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])v.Value)[4].strValue;
                        slot_n_1Value.Add(resultString);
                    }
                }
            }

            Global.마스터데이터.비전데이터적용(slot_n_1Value);

            this.GridView1.RefreshData();
        }

        private void B설정적용_Click(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm(번역.적용확인)) return;

            Global.마스터데이터.Set();
            Global.마스터데이터.Save();
            Global.비전마스터구동.Save();

            this.GridView1.RefreshData();

            MvUtils.Utils.SaveOK(번역.저장완료);
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
                [Translation("Do you want to load master data values?", "마스터 데이터값을 불러오시겠습니까?")]
                마스터값로드,
                [Translation("No data", "비전 데이터가 없습니다. 제품을 지그에 놓고 수동으로 검사를 진행해 주세요.")]
                불러오기오류,
            }

            public String 설정저장 { get { return Localization.GetString(Items.설정저장); } }
            public String 저장완료 { get { return Localization.GetString(Items.저장완료); } }
            public String 저장확인 { get { return Localization.GetString(Items.저장확인); } }
            public String 적용확인 { get { return Localization.GetString(Items.적용확인); } }
            public String 마스터값로드 { get { return Localization.GetString(Items.마스터값로드); } }
            public String 불러오기오류 { get { return Localization.GetString(Items.불러오기오류); } }
        }
    }
}
