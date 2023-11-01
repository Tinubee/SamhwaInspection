using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamhwaInspection.UI.Form
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.e사용자명.Properties.Items.AddRange(Global.유저자료.사용자목록());
            if (!String.IsNullOrEmpty(Properties.Settings.Default.UserName) && Global.유저자료.GetItem(Properties.Settings.Default.UserName) != null)
                this.e사용자명.Text = Properties.Settings.Default.UserName;

            this.Shown += Login_Shown;
            this.b인증.Click += B인증_Click;
            this.b취소.Click += B취소_Click;
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.e사용자명.Text))
                this.e비밀번호.Focus();
        }

        private void B취소_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void B인증_Click(object sender, EventArgs e)
        {
            string 사용자명 = Utils.Utils.StrValue(this.e사용자명.Text);
            string 비밀번호 = Utils.Utils.StrValue(this.e비밀번호.Text);
            Global.유저자료.비밀번호확인(사용자명, 비밀번호);
            if (Global.환경설정.사용권한 == Schemas.유저권한구분.없음) this.DialogResult = DialogResult.No;
            else
            {
                Global.환경설정.시스템관리자인증(사용자명, 비밀번호);
                if (Global.환경설정.사용권한 == Schemas.유저권한구분.시스템 || Global.환경설정.사용권한 == Schemas.유저권한구분.관리자) this.DialogResult = DialogResult.OK;
                else
                {
                    //Global.정보로그(로그영역, 번역.로그인, $"[{사용자명}] {번역.인증오류}", false);
                    Utils.Utils.WarningMsg("인증오류", "Warning");
                    this.e비밀번호.Focus();
                }
            }
        }
    }
}