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

namespace SamhwaInspection.UI
{
    public partial class Settings : DevExpress.XtraEditors.XtraUserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.e기본설정.Init();
            this.eIO컨트롤.Init();
            //this.e모델설정.Init();
            this.e변수설정.Init();
        }

        public void 변수업데이트()
        {
            this.e변수설정.VariableUpdate();
        }
    }
}
