using DevExpress.Utils.Design;
using Euresys.MultiCam;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace SamhwaInspection.Schemas
{
    public class Cam : CamConfig
    {
        public override CameraType Camera { get; set; } = CameraType.Camera1;

        public override string CamFile { get; set; } = "LA-CM-16K05A_L16380SC.cam";

        public override UInt32 DriverIndex { get; set; } = 0;

        public Int32 height;
        public Int32 width;

        public Cam(CameraType camera, UInt32 driverIndex, Connector connector, AcquisitionMode acquisitionMode, LineRateMode lineRateMode)
        {
            this.Camera = camera;
            this.DriverIndex = driverIndex;
            this.Connector = connector;
            this.AcquisitionMode = acquisitionMode;
            this.LineRateMode = lineRateMode;
        }
       
        public override void Init()
        {
            //추상클래스 Init
            base.Init();

            Debug.WriteLine($"{this.DriverIndex}, {this.Connector}", this.Camera.ToString());

            MC.Create("CHANNEL", out this.Channel);
            Debug.WriteLine($"{this.Channel}", this.Channel.ToString());
            MC.SetParam(this.Channel, "DriverIndex", this.DriverIndex);
            MC.SetParam(this.Channel, "Connector", this.Connector.ToString());
            MC.SetParam(this.Channel, "CamFile", Path.Combine(Global.환경설정.기본경로, this.CamFile));

            MC.SetParam(this.Channel, "ColorFormat", "Y8");
            MC.SetParam(this.Channel, "TapConfiguration", "FULL_8T8");
            MC.SetParam(this.Channel, "AcquisitionMode", "LONGPAGE");
            MC.SetParam(this.Channel, "TrigMode", "SOFT");
            MC.SetParam(this.Channel, "NextTrigMode", "REPEAT");
            MC.SetParam(this.Channel, "PageLength_Ln", 60000);
            MC.SetParam(this.Channel, "EndTrigMode", "AUTO");
            MC.SetParam(this.Channel, "SeqLength_Ln", 120000);
            MC.SetParam(this.Channel, "BreakEffect", "FINISH");

            MC.GetParam(this.Channel, "ImageSizeY", out this.height);
            MC.GetParam(this.Channel, "ImageSizeX", out this.width);
        }

        public void Set()
        {

            Debug.WriteLine("Camera Setting Change");

            Int32 LineRate;
            Int32 PageLength;
            Int32 SeqLength;

            //이전 값 확인
            MC.GetParam(this.Channel, "LineRate_Hz", out LineRate);
            MC.GetParam(this.Channel, "PageLength_Ln", out PageLength);
            MC.GetParam(this.Channel, "SeqLength", out SeqLength);
            Debug.WriteLine($"Channel : {this.Channel}, PageLength : {PageLength}, SeqLength : {SeqLength}, LineRate_HZ : {LineRate} ");

            //카메라 상태 Ready로 전환
            MC.SetParam(this.Channel, "ChannelState", ChannelState.IDLE);
            Debug.WriteLine("Idle!!!");

            //카메라 설정값 바뀐값으로 변환
            MC.SetParam(this.Channel, "PageLength_Ln", this.PageLength_Ln);

            //바뀐 값 확인
            MC.GetParam(this.Channel, "PageLength_Ln", out PageLength);
            Debug.WriteLine($"Channel : {this.Channel}, PageLength : {PageLength} LineRate_HZ : {LineRate} ");

            //카메라 상태 다시 Active로 변견
            this.Active();
            Debug.WriteLine("ACTIVE!!!");
        }
    }
}