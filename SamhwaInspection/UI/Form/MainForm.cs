using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SamhwaInspection.Schemas;
using System.Diagnostics;
using SamhwaInspection.UI.Control;
using DevExpress.Utils.Extensions;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using DevExpress.XtraWaitForm;
using SamhwaInspection.Utils;

namespace SamhwaInspection
{
    public partial class MainForm : DevExpress.XtraBars.TabForm
    {

        private Boolean IsStarted = false;
        public ResultViewer_4 resultViewer_41 = new ResultViewer_4();
        public ResultViewer_6 resultViewer_61 = new ResultViewer_6();
        public ResultViewer_유무검사 ResultViewer_유무검사1 = new ResultViewer_유무검사();
        public ResultViewer_표면검사 ResultViewer_표면검사6_앞 = new ResultViewer_표면검사();
        public ResultViewer_표면검사 ResultViewer_표면검사6_뒤 = new ResultViewer_표면검사();

        private UI.Form.WaitForm WaitForm;

        public enum 검사면
        {
            앞면,
            뒷면,
        }

        public MainForm()
        {
            InitializeComponent();
            this.ShowWaitForm();
            //Global.Init();
            //Debug.WriteLine("Global Init Finished");
            this.StartPosition = FormStartPosition.Manual;
            Global.mainForm = this;
            this.tabFormControl1.SelectedPage = p비전검사;
            this.Shown += MainForm_Shown;
            this.FormClosing += MainForm_FormClosing;
        }

        private Boolean Init()
        {
            this.WindowState = FormWindowState.Maximized;
            this.state1.Init();
            this.settings1.Init();
            DisplaySetting(Global.모델자료.선택모델.모델번호);
            this.resultList1.Init();
            return true;
        }
        public void ShowWaitForm()
        {
            WaitForm = new UI.Form.WaitForm() { ShowOnTopMode = ShowFormOnTopMode.AboveAll };
            WaitForm.Show(this);
        }
        public void HideWaitForm()
        {
            WaitForm.Close();
        }
        public void DisplaySetting(int modelNumber)
        {
            BeginInvoke((Action)delegate
            {
                디스플레이변경(modelNumber);
                변수업데이트();//this.settings1.변수업데이트();
            });
        }

        public void 변수업데이트() => this.settings1.변수업데이트();

        public void 디스플레이변경(int modelNumber)
        {
            panel치수검사.Controls.Clear();
            if (modelNumber == 0)
            {
                panel치수검사.Controls.Add(this.resultViewer_41);
                this.resultViewer_41.Dock = DockStyle.Fill;
                this.resultViewer_41.Init();
            }
            if (modelNumber == 1)
            {
                panel치수검사.Controls.Add(this.resultViewer_61);
                this.resultViewer_61.Dock = DockStyle.Fill;
                this.resultViewer_61.Init();
            }

            if (!panel유무검사.Controls.Contains(ResultViewer_유무검사1))
            {
                panel유무검사.Controls.Add(this.ResultViewer_유무검사1);
                this.ResultViewer_유무검사1.Dock = DockStyle.Fill;
                this.ResultViewer_유무검사1.Init();
            }

            if (!panel표면검사_앞.Controls.Contains(ResultViewer_표면검사6_앞))
            {
                panel표면검사_앞.Controls.Add(this.ResultViewer_표면검사6_앞);
                this.ResultViewer_표면검사6_앞.Dock = DockStyle.Fill;
                this.ResultViewer_표면검사6_앞.Init(검사면.앞면);
            }

            if (!panel표면검사_뒤.Controls.Contains(ResultViewer_표면검사6_뒤))
            {
                panel표면검사_뒤.Controls.Add(this.ResultViewer_표면검사6_뒤);
                this.ResultViewer_표면검사6_뒤.Dock = DockStyle.Fill;
                this.ResultViewer_표면검사6_뒤.Init(검사면.뒷면);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Global.Initialized += GlobalInitialized;
                Task.Run(() => { Global.Init(); });
            }
            catch (Exception ex)
            {
                Global.오류로그("메인페이지", "프로그램 시작", "프로그램 시작 중 오류가 발생하였습니다.\n" + ex.Message, true);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.IsStarted) return;
            e.Cancel = !IvmUtils.Utils.Confirm("프로그램을 종료하시겠습나까?");
            if (!e.Cancel)
            {
                this.IsStarted = false;
                //this.e검사결과.Close();
                Global.Close();
            }
        }

        private void GlobalInitialized(object sender, Boolean e)
        {
            this.BeginInvoke(new Action(() => GlobalInitialized(e)));
        }

        private void GlobalInitialized(Boolean e)
        {
            Global.Initialized -= GlobalInitialized;
            if (!e) { this.Close(); return; }
            this.HideWaitForm();
            Common.SetForegroundWindow(this.Handle.ToInt32());

            if (this.Init())
            {
                Debug.WriteLine("MainForm Init Finished.");
                this.IsStarted = true;
                Global.Start();
            }
            else this.Close();
            //this.Init();
            //Global.Start();
        }
    }
}