using System;
using System.Threading;
using System.Windows.Forms;

namespace SamhwaInspection
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        [Obsolete]
        private static void Main()
        {
            bool createdNew = false;
            Mutex mtx = new Mutex(true, Global.GetGuid(), out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("프로그램이 이미 실행중입니다.");
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            Application.Run(new MainForm());
        }

        //이거 넣어야지 그리드단에서 크로스 쓰레딩 발생안함.
        //DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
    }
}