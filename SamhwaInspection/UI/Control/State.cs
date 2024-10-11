using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
using static SamhwaInspection.Schemas.환경설정;

namespace SamhwaInspection.UI.Control
{
    public partial class State : DevExpress.XtraEditors.XtraUserControl
    {
        private Int32 preValue_자동모드;
        private Int32 preValue_자동운전시작;
        private Int32 preValue_마스터모드;
        private Int32 preValue_Front지그;
        private Int32 preValue_Rear지그;

        private Int32 currentValue_자동모드;
        private Int32 currentValue_자동운전시작;
        private Int32 currentValue_마스터모드;
        private Int32 currentValue_Front지그;
        private Int32 currentValue_Rear지그;

        public State()
        {
            InitializeComponent();
          
        }

        private void TitleView1_DoubleClick(object sender, EventArgs e)
        {
            Global.그랩제어.GetItem(CameraType.Cam01).Ready();
        }

        private void 입출상태적용()
        {
            currentValue_자동모드 = Global.신호제어.자동모드여부;
            currentValue_자동운전시작 = Global.신호제어.운전시작여부;
            currentValue_마스터모드 = Global.신호제어.마스터모드여부;
            currentValue_Front지그 = Global.신호제어.Front지그;
            currentValue_Rear지그 = Global.신호제어.Rear지그;


            if (preValue_Front지그 != currentValue_Front지그)
            {
                if (preValue_Front지그 > 0)
                {
                    Debug.WriteLine("Front지그 On");
                }
                else
                {
                    Debug.WriteLine("Front지그 Off");
                }
                preValue_Front지그 = currentValue_Front지그;
            }

            if (preValue_Rear지그 != currentValue_Rear지그)
            {
                if (preValue_Rear지그 > 0)
                {
                    Debug.WriteLine("Rear지그 On");
                }
                else
                {
                    Debug.WriteLine("Rear지그 Off");
                }
                preValue_Rear지그 = currentValue_Rear지그;
            }



            if (preValue_자동모드 != currentValue_자동모드)
            {
                this.버튼UI변경(this.b운전모드, currentValue_자동모드, "AUTO", "MANUAL");
                preValue_자동모드 = currentValue_자동모드;

                if (currentValue_자동모드 > 0)
                {
                    //Global.조명제어.TurnOn(조명구분.BACK);
                    Debug.WriteLine("조명켬");
                }
                else
                {
                    //Global.조명제어.TurnOff(조명구분.후면검사조명);
                    //Global.조명제어.TurnOff(조명구분.상면검사조명);
                    Global.조명제어.TurnOff(조명구분.BACK);
                    Debug.WriteLine("조명끔");
                }

                Debug.WriteLine("운전모드 변경!");
            }
            if (preValue_자동운전시작 != currentValue_자동운전시작)
            {
                if (currentValue_자동운전시작 > 0)
                {
                    //Global.조명제어.TurnOn(조명구분.BACK);
                    Debug.WriteLine("조명켬");
                }
                else
                {
                    //Global.조명제어.TurnOff(조명구분.후면검사조명);
                    //Global.조명제어.TurnOff(조명구분.상면검사조명);
                    //Global.조명제어.TurnOff(조명구분.BACK);
                    Debug.WriteLine("조명끔");
                }

                this.버튼UI변경(this.b운전상태, currentValue_자동운전시작, "START", "STOP");
                preValue_자동운전시작 = currentValue_자동운전시작;
                Debug.WriteLine("운전상태 변경!");
            }

            if (preValue_마스터모드 != currentValue_마스터모드)
            {
                if (currentValue_마스터모드 > 0)
                {
                    //this.버튼UI변경(this.b운전모드, currentValue_마스터모드, "MASTER", "MANUAL");
                    //Global.조명제어.TurnOn(조명구분.BACK);
                    //this.GlobalVariableModuleTool.SetGlobalVar("마스터모드", "1");
                    Global.비전마스터구동.글로벌변수제어.InspectUseSet("마스터모드", "1");
                    Debug.WriteLine("마스터모드 On");
                }
                else
                {
                    //Global.조명제어.TurnOff(조명구분.BACK);
                    //this.GlobalVariableModuleTool.SetGlobalVar("마스터모드", "1");
                    Global.비전마스터구동.글로벌변수제어.InspectUseSet("마스터모드", "0");
                    Debug.WriteLine("마스터모드 Off");
                }
                preValue_마스터모드 = currentValue_마스터모드;
                Debug.WriteLine("마스터모드 상태 변경!");
            }
        }
        public void Init()
        {
            Global.신호제어.CompleteReceive += 입출상태적용;
            Global.환경설정.결과상태갱신알림 += 결과업데이트;

            this.b수량리셋.Click += 수량리셋;
            this.환경설정BindingSource.DataSource = Global.환경설정;
            this.b로그인.Click += B로그인_Click;

            모델변경알림(Global.환경설정.선택모델);
            Global.환경설정.모델변경알림 += 모델변경알림;
            this.e모델선택.Properties.DataSource = Global.모델자료;
            this.e모델선택.EditValue = Global.환경설정.선택모델;
            this.e모델선택.EditValueChanging += 모델변경;

            검사상태표현(결과구분.NO);
            유저상태표현(Global.환경설정.사용자명);
            this.e양품수율.BaseColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            this.e양품수량.BaseColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.e불량수량.BaseColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.e전체수량.BaseColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;

            titleView1.DoubleClick += TitleView1_DoubleClick;
        }

        public void 로그아웃상태()
        {
            Global.환경설정.사용자명 = string.Empty;
            Global.환경설정.사용권한 = 유저권한구분.없음;
            Global.환경설정.로그인상태 = false;
        }

        private void B로그인_Click(object sender, EventArgs e)
        {
            if (!Global.환경설정.로그인상태)
            {
                Login form = new Login();
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.No || result == DialogResult.Cancel)
                {
                    로그아웃상태();
                    return;
                }

                유저상태표현(Global.환경설정.사용자명);
                Global.환경설정.로그인상태 = true;
                b로그인.Text = "로그아웃";
            }
            else
            {
                if (!Utils.Utils.Confirm($"[{Global.환경설정.사용자명}] 로그아웃 하시겠습니까 ?", "Logout")) return;
                로그아웃상태();
                유저상태표현(Global.환경설정.사용자명);
                b로그인.Text = "로그인";
            }

        }

        public void 유저상태표현(string 사용자명)
        {
            lb로그인유저.Text = 사용자명 == string.Empty ? "Not Login" : 사용자명;
        }

        public void 수량리셋(object sender, EventArgs e)
        {
            if (!IvmUtils.Utils.Confirm("검사수량을 초기화하시겠습니까?")) return;
            Global.환경설정.수량리셋();
            this.환경설정BindingSource.ResetBindings(false);

            //이스터에그(추후 삭제)//********************************
            //Global.tactTimeChecker.Start();
            //Global.환경설정.메뉴얼검사확인 = true;
            //Global.그랩제어[0].SoftTrig();
        }

        private void 모델변경(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            // 0 : 4개짜리 1: 6개짜리
            if (Global.환경설정.선택모델 == IvmUtils.Utils.IntValue(e.NewValue)) return;
            if (!IvmUtils.Utils.Confirm("모델을 변경하시겠습니까?"))
            {
                e.Cancel = true;
                return;
            }
            Global.환경설정.모델변경요청(IvmUtils.Utils.IntValue(e.NewValue));
            Global.비전마스터구동.Init();
        }

        private void 결과업데이트()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => this.결과업데이트()));
                return;
            }

            검사상태표현(Global.환경설정.현재결과상태);
            this.환경설정BindingSource.ResetBindings(false);
            //Debug.WriteLine("결과업데이트 완료 in MainForm.");

        }

        private void 모델변경알림(Int32 모델번호)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => this.모델변경알림(모델번호)));
                return;
            }
            //this.검사상태표현(결과구분.NO);
            //this.검사현황BindingSource.DataSource = Global.모델자료.선택모델;
            //this.검사현황BindingSource.ResetBindings(false);
            if (IvmUtils.Utils.IntValue(this.e모델선택.EditValue) == 모델번호) return;
            this.e모델선택.EditValue = 모델번호;
            Global.모델자료.모델변경적용();
        }

        private void 버튼UI변경(SimpleButton 버튼, int value, String txtSignalOn, String txtSignalOff)
        {
            Boolean 상태 = value > 0;
            if (상태)
            {
                if (버튼.InvokeRequired) 버튼.BeginInvoke(new Action(() => { 버튼Text변경(버튼, txtSignalOn, DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information); }));
            }
            else
            {
                if (버튼.InvokeRequired) 버튼.BeginInvoke(new Action(() => { 버튼Text변경(버튼, txtSignalOff, DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText); }));
            }
        }

        private void 버튼Text변경(SimpleButton 버튼, String 표시값, System.Drawing.Color color)
        {
            버튼.Text = 표시값;
            버튼.Appearance.ForeColor = color;
        }

        private void 검사상태표현(결과구분 구분)
        {
            this.lbl판정결과.Text = IvmUtils.Utils.GetEnumDescription(구분);
            this.lbl판정결과.Appearance.ForeColor = 환경설정.ResultColor(구분);
        }
    }
}
