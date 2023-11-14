using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MvUtils;
using SamhwaInspection.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VM.Core;
using VM.PlatformSDKCS;

namespace SamhwaInspection.UI.Control
{
    public partial class MasterSetting : XtraUserControl
    {
        private LocalizationInspection 번역 = new LocalizationInspection();
        public 지그위치 위치 { get; set; } = 지그위치.Front;

        public Flow구분 플로우 { get; set; } = Flow구분.Flow1;
        public enum 지그위치
        {
            Front,
            Rear,
        }
        public MasterSetting()
        {
            InitializeComponent();
        }
        public void Init()
        {
            this.GridView1.Init(this.barManager1);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            this.GridView1.AddEditSelectionMenuItem();
            this.GridView1.AddSelectPopMenuItems();
            this.GridControl1.DataSource = Global.보정값설정;

            b설정저장.Click += B설정저장_Click;
            b보정값계산.Click += B보정값계산_Click;
            b마스터값로드.Click += B마스터값로드_Click;

            c지그선택.SelectedIndexChanged += C지그선택_SelectedIndexChanged;
            c지그선택.Properties.Items.AddRange(Enum.GetValues(typeof(지그위치)));
            c지그선택.SelectedIndex = 0;

            c플로우선택.SelectedIndexChanged += C플로우선택_SelectedIndexChanged;
            c플로우선택.Properties.Items.AddRange(Enum.GetValues(typeof(Flow구분)));
            c플로우선택.SelectedIndex = 0;
        }

        private void C플로우선택_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.플로우 = (Flow구분)c플로우선택.SelectedIndex;
        }

        private void C지그선택_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.위치 = (지그위치)c지그선택.SelectedIndex;
        }

        private void B마스터값로드_Click(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm(번역.마스터값로드)) return;
            List<IMVSGroup> tool = Global.비전마스터구동.GetItem(this.플로우).Slot20PointGroupMouduleTool;
            List<String> slot_n_1Value = new List<String>();
            for (int lop = 0; lop < tool.Count; lop++)
            {
                foreach (var v in tool[lop].Outputs)
                {
                    if (v.Value == null) continue;
                    if (v.Name.Contains("Slot") && v.Name.Contains("-5"))
                    {
                        String resultString = ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])v.Value)[4].strValue;
                        slot_n_1Value.Add(resultString);
                    }
                }
            }

            Global.보정값설정.비전데이터적용(slot_n_1Value, this.플로우 ,this.위치);
           
            this.GridView1.RefreshData();
        }

        private void B보정값계산_Click(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm(번역.보정값계산)) return;

            Global.보정값설정.보정값계산();

            GridView1.RefreshData();
        }

        private void B설정저장_Click(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm(번역.적용확인)) return;

            Global.보정값설정.Set(); //글로벌변수값 셋팅
            Global.보정값설정.Save(); //JSON파일에 저장
            Global.비전마스터구동.Save(); //변경된 글로벌변수값이 적용된 Solution파일 저장.
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
                [Translation("Do you want to calculate correction values ​​based on current vision measurements?", "현재 비전측정값으로 보정값을 계산하시겠습니까?")]
                보정값계산,
                [Translation("Do you want to load master data values?", "마스터 데이터값을 불러오시겠습니까?")]
                마스터값로드,
            }

            public String 설정저장 { get { return Localization.GetString(Items.설정저장); } }
            public String 저장완료 { get { return Localization.GetString(Items.저장완료); } }
            public String 저장확인 { get { return Localization.GetString(Items.저장확인); } }
            public String 적용확인 { get { return Localization.GetString(Items.적용확인); } }
            public String 보정값계산 { get { return Localization.GetString(Items.보정값계산); } }
            public String 마스터값로드 { get { return Localization.GetString(Items.마스터값로드); } }
        }
    }
}
