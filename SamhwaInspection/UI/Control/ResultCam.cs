using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SamhwaInspection.Schemas;
using SamhwaInspection.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SamhwaInspection.UI.Control
{
    public partial class ResultCam : DevExpress.XtraEditors.XtraUserControl
    {
        public ResultCam()
        {
            InitializeComponent();

        }

        private CameraType 카메라 = CameraType.None;
        private delegate void 이미지그랩완료보고대리자(AcquisitionData Data);
        private Cam cam;

        public void Init(CameraType 카메라)
        {
            //Global.cameraControl.grabCompleted += CameraControl_grabCompleted;
            this.카메라 = 카메라;
            this.b카메라명.Caption = $"Cam.{((Int32)this.카메라 + 1).ToString()} [{Common.GetEnumDescription(this.카메라)}]";
            cam = Global.그랩제어.GetItem(카메라);
            cam.AcquisitionFinishedEvent += Paint_camImage;

            //추후 로그인 기능 구현 후 수정
            if (true)
            {
                Debug.WriteLine("ResultCam Init");
                this.b스냅촬영.ItemClick += 스냅촬영;
            }
            this.cam.Active();
        }

        private void Paint_camImage(AcquisitionData Data)
        {
            if (Data.BmpImage == null) return;
            if (this.InvokeRequired)
            {
                this.Invoke(new 이미지그랩완료보고대리자(Paint_camImage), new object[] { Data });
                return;
            }
            if (Data.Camera == 카메라) this.e뷰어.Image = Data.BmpImage;
            Debug.WriteLine("뿌려짐");
        }

        private void 스냅촬영(object sender, ItemClickEventArgs e)
        {
            //if (this.그래버 == null || this.그래버.연속촬영여부) return;
            //this.검사도구.Run();
            Debug.WriteLine("SnapShot Click!");
            if (this.cam == null) return;
            if(this.cam.CurrentState() != ChannelState.ACTIVE)
            {
                this.cam.Active();
                Debug.WriteLine("Active Completed!");
            }
            Debug.WriteLine($"ChannelState : {this.cam.CurrentState()}");
            this.cam.SoftTrig();
            Debug.WriteLine("Trig Completed!");
            return;
        }

        //private void 연속촬영(object sender, ItemClickEventArgs e)
        //{
        //    BarCheckItem button = sender as BarCheckItem;
        //    if (button.Checked)
        //    {
        //        this.e뷰어.InteractiveGraphics.Clear();
        //        this.e뷰어.StaticGraphics.Clear();
        //        this.연속촬영시간.Restart();
        //        this.그래버?.StartAcquire();
        //    }
        //    else
        //    {
        //        this.연속촬영시간.Stop();
        //        this.그래버?.StopAcquire();
        //        GC.Collect();
        //    }
        //    상태색상변경(button);
        //}
    }
}
