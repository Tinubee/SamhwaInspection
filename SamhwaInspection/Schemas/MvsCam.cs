using DevExpress.Utils.Design;
using Euresys.MultiCam;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace SamhwaInspection.Schemas
{
    public class MvsCam : MvsCamConfig
    {
        public override MvsCameraType Camera { get; set; } = MvsCameraType.Camera2;

        public Int32 height;
        public Int32 width;

        public MvsCam(MvsCameraType camera, MvsCamAcquisitionMode acquisitionMode)
        {
            this.Camera = camera;
            this.AcquisitionMode = acquisitionMode;
        }
       
        public override void Init()
        {
            //추상클래스 Init
            base.Init();
        }

        public void Set()
        {

        }
    }
}