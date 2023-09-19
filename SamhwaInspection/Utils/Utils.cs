using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamhwaInspection.Utils
{
    public class Utils
    {
        public static Boolean Confirm(String message, String caption = "")
        {
            if (String.IsNullOrEmpty(caption)) caption = Localization.확인.GetString();
            return XtraMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}
