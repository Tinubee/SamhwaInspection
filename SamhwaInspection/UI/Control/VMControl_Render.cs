using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraEditors;
using GraphicsSetModuleCs;
using SamhwaInspection.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VM.Core;
using VM.PlatformSDKCS;

namespace SamhwaInspection.UI.Control
{
    public partial class VMControl_Render : DevExpress.XtraEditors.XtraUserControl
    {
        //private delegate void 검사완료보고대리자(AcquisitionData Data);
        //private delegate void 검사완료보고대리자(ImageBaseData Data);
        //private delegate void 검사완료보고대리자(GraphicsSetModuleTool graphicTool);
        //private delegate void 이미지그랩완료보고대리자(AcquisitionData Data);
        public VMControl_Render()
        {
            InitializeComponent();
        }

        //public void Init2(비전마스터플로우 Flow, int 순서)
        //{
        //    if (Flow == null || Flow.graphicsSetModuleTool_List.Count != 6) return;

        //    this.vmRenderControl1.ModuleSource = Flow.graphicsSetModuleTool_List[순서];

        //    this.vmRenderControl1.Update();
        //    this.vmRenderControl1.Refresh();
        //}

        public void Init(비전마스터플로우 Flow)
        {
            //if(Flow.구분 == Flow구분.표면검사앞)
            //{
            //    if (Flow == null || Flow.graphicsSetModuleTool_List == null) return;

            //    this.vmRenderControl1.ModuleSource = Flow.graphicsSetModuleTool_List[0];

            //    this.vmRenderControl1.Update();
            //    this.vmRenderControl1.Refresh();
            //}
            //else
            //{
            if (Flow == null || Flow.graphicsSetModuleTool == null) return;

            this.vmRenderControl1.ModuleSource = Flow.graphicsSetModuleTool;

            this.vmRenderControl1.Update();
            this.vmRenderControl1.Refresh();
            //}
        }

        //private void Flow_InspectionFinishedEvent(GraphicsSetModuleTool graphicTool)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.Invoke(new 검사완료보고대리자(Flow_InspectionFinishedEvent), graphicTool);
        //        return;
        //    }
        //}

        private void UpdateControl(GraphicsSetModuleTool graphicTool)
        {
            this.vmRenderControl1.ModuleSource = graphicTool;

            Debug.WriteLine($"RenderControl 업데이트완료");
        }

        public void SaveOriginalImage(String filePath)
        {
            this.vmRenderControl1.SaveOriginalImage(filePath);
        }
    }


}
