using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SamhwaInspection.Schemas;
using SamhwaInspection.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamhwaInspection.UI.Control
{
    public partial class User : DevExpress.XtraEditors.XtraUserControl
    {
        public User()
        {
            InitializeComponent();
        }

        public void Init()
        {
            //this.myGridView1.Init(this.barManager1);
            //this.myGridView1.OptionsBehavior.Editable = true;
            //this.myGridView1.AddDeleteMenuItem(유저삭제_Click);
            this.myGridControl1.DataSource = Global.유저자료;
            Localization.SetColumnCaption(this.myGridView1, typeof(유저정보));
            //this.b유저저장.Click += 유저저장_Click;
        }
        private void 유저저장_Click(object sender, EventArgs e)
        {
            if (!Utils.Utils.Confirm("사용자정보를 저장하시겠습니까?", Localization.확인.GetString())) return;
            Global.유저자료.Save();
            Global.정보로그("Users", "정보저장", "저장되었습니다", this.FindForm());
        }

        private void 유저삭제_Click(object sender, ItemClickEventArgs e)
        {
            유저정보 정보 = this.myGridView1.GetFocusedRow() as 유저정보;
            if (정보 == null) return;
            if (!Utils.Utils.Confirm($"[{정보.성명}] 선택 사용자를 삭제하시겠습니까?", Localization.확인.GetString())) return;
            if (Global.유저자료.Remove(정보)) Global.정보로그("Users", "사용자 삭제", $"[{정보.성명}] 삭제되었습니다", false);
        }
    }
}
