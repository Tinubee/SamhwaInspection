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
            
            //this.GridView1.AddDeleteMenuItem()
            this.gridControl1.DataSource = Global.유저자료;
            //this.b유저저장.Click += B유저저장_Click;
            //this.GridView1.ValidateRow += GridView1_ValidateRow;
        }

    }
}
