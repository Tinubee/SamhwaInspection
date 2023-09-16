using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraEditors;
using SamhwaInspection.Schemas;
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
    public partial class IOControl : DevExpress.XtraEditors.XtraUserControl
    {
        private Color 동작색상 = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
        //private Color 동작색상배경 = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
        private Color 정지색상 = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        

        public IOControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Global.신호제어.CompleteReceive += 입출변경알림;
        }

        private void 버튼체크(SimpleButton 버튼, int value)
        {
            Boolean 상태 = value > 0;
            if (버튼 == b수동모드) 상태 = !상태;
            if (상태 && 버튼.Appearance.ForeColor == 동작색상) return;
            if (!상태 && 버튼.Appearance.ForeColor == 정지색상) return;
            if (버튼.InvokeRequired) 버튼.BeginInvoke(new Action(() => { 버튼상태(버튼, 상태); }));
            else 버튼상태(버튼, 상태);
        }

        private void 버튼상태(SimpleButton 버튼, Boolean 상태)
        {
            버튼.Appearance.Options.UseForeColor = true;
            //버튼.Appearance.Options.UseBackColor = true;
            if (상태) 
            {
                버튼.Appearance.ForeColor = 동작색상;
                //버튼.Appearance.BackColor = 동작색상배경;
            } 
            else 버튼.Appearance.ForeColor = 정지색상;


            if(버튼 == b검사결과요청)
            {
                
            }

        }

        private void 입출변경알림()
        {
            //Input영역
            this.버튼체크(this.b수동모드, Global.신호제어.자동모드여부);
            this.버튼체크(this.b자동모드, Global.신호제어.자동모드여부);
            this.버튼체크(this.b자동운전시작, Global.신호제어.운전시작여부);
            this.버튼체크(this.bPLC연결상태, Global.신호제어.Heartbit_PLC);

            this.버튼체크(this.b제품확인카메라, Global.신호제어.제품확인카메라트리거1);
            this.버튼체크(this.b치수검사카메라, Global.신호제어.F상부치수검사카메라트리거1);
            
            this.버튼체크(this.b상부평탄도센서, Global.신호제어.상부변위센서확인트리거);
            this.버튼체크(this.b하부평탄도센서, Global.신호제어.하부변위센서확인트리거);

            this.버튼체크(this.b검사결과요청, Global.신호제어.결과값요청트리거);
            //this.버튼체크(this.bNG신호, Global.신호제어.프로그램구동펄스);
        }

    }
}
