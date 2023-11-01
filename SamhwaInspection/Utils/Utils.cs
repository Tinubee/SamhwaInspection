using DevExpress.XtraEditors;
using Microsoft.VisualBasic;
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

        public static Boolean IsNullValue(Object value) => value == null || Convert.IsDBNull(value);
        public static Boolean Confirm(String message, String caption = "")
        {
            if (String.IsNullOrEmpty(caption)) caption = Localization.확인.GetString();
            return XtraMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static String StrValue(Object value)
        {
            if (IsNullValue(value)) return String.Empty;
            return Convert.ToString(value).Trim(new char[] { Strings.Chr(32), ControlChars.Tab, ControlChars.Lf, ControlChars.Cr });
        }

        public static Boolean WarningMsg(String message, String caption = "")
        {
            if (String.IsNullOrEmpty(caption)) caption = Localization.경고.GetString();
            XtraMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
}
