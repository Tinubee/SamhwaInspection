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
    public partial class CountViewer : DevExpress.XtraEditors.XtraUserControl
    {
        public CountViewer()
        {
            InitializeComponent();
        }

        [Bindable(true)]
        public string ValueText
        {
            get { return this.e현재값.Text; }
            set { this.e현재값.Text = value; }
        }

        public string Caption
        {
            get { return this.e타이틀.Text; }
            set { this.e타이틀.Text = value; }
        }

        public Color BaseColor
        {
            get { return this.e현재값.Appearance.ForeColor; }
            set
            {
                this.e현재값.Appearance.BackColor = Color.FromArgb(32, value);
            }
        }
    }
}
