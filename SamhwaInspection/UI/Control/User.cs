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
            this.GridView1.Init(this.barManager1);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.AddDeleteMenuItem(유저삭제_Click);
            this.GridControl1.DataSource = Global.유저자료;
            Localization.SetColumnCaption(this.GridView1, typeof(유저정보));
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
            유저정보 정보 = this.GridView1.GetFocusedRow() as 유저정보;
            if (정보 == null) return;
            if (!Utils.Utils.Confirm($"[{정보.성명}] 선택 사용자를 삭제하시겠습니까?", Localization.확인.GetString())) return;
            if (Global.유저자료.Remove(정보)) Global.정보로그("Users", "사용자 삭제", $"[{정보.성명}] 삭제되었습니다", false);
        }

        private class LocalizationUsers
        {
            private enum Items
            {
                [Translation("Save", "정보저장")]
                정보저장,
                [Translation("It's saved.", "저장되었습니다.")]
                저장완료,
                [Translation("Save users information?", "사용자정보를 저장하시겠습니까?")]
                저장확인,
                [Translation("Delete this selected user?", "선택 사용자를 삭제하시겠습니까?")]
                삭제확인,
                [Translation("Remove user", "사용자 삭제")]
                유저삭제,
                [Translation("Removed.", "삭제되었습니다.")]
                유저제거,
            }

            public String 정보저장 { get { return Localization.GetString(Items.정보저장); } }
            public String 저장완료 { get { return Localization.GetString(Items.저장완료); } }
            public String 저장확인 { get { return Localization.GetString(Items.저장확인); } }
            public String 삭제확인 { get { return Localization.GetString(Items.삭제확인); } }
            public String 유저삭제 { get { return Localization.GetString(Items.유저삭제); } }
            public String 유저제거 { get { return Localization.GetString(Items.유저제거); } }
            public String 유저저장 { get { return Localization.저장.GetString(); } }
        }

        private void g유저관리_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
