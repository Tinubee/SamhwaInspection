using Euresys.MultiCam;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;

namespace SamhwaInspection.Schemas
{
    [Description("카메라설정")]
    public abstract class CamConfig
    {
        [Description("이미지 그랩 이벤트")]
        public delegate void AcquisitionFinished(AcquisitionData Data);

        public event AcquisitionFinished AcquisitionFinishedEvent;

        // Callback 함수 가비지 수집된 대리자에서 콜백이 발생했습니다. 오류 방지를 위해 반드시 해당 콜백 함수를 상단에 정의
        private MC.CALLBACK CamCallBack;

        private UInt32[] SurfaceTable;
        private Int32 SurfaceCount = 2;
        private Int32 PageIndex = 1;
        public ProductIndex ProductIndex = ProductIndex.PRODUCT_INDEX1;

        [Description("채널번호")]
        public UInt32 Channel;

        [Description("카메라 구분")]
        public abstract CameraType Camera { get; set; }

        [Description("카메라 설정 파일")]
        public abstract string CamFile { get; set; }

        [Description("그래버 보드 Index")]
        public abstract UInt32 DriverIndex { get; set; }

        [Description("Connector")]
        public virtual Connector Connector { get; set; } = Connector.M;

        [Description("Acquisition Mode")]
        public virtual AcquisitionMode AcquisitionMode { get; set; } = AcquisitionMode.PAGE;

        [Description("LineRateMode")]
        public virtual LineRateMode LineRateMode { get; set; } = LineRateMode.CAMERA;

        [Description("LineCaptureMode")]
        public virtual LineCaptureMode LineCaptureMode { get; set; } = LineCaptureMode.ALL;

        [Description("Trig Mode")]
        public virtual TrigMode TrigMode { get; set; } = TrigMode.SOFT;

        [Description("Next Trig Mode")]
        public virtual NextTrigMode NextTrigMode { get; set; } = NextTrigMode.SOFT;

        [Description("End Trig Mode")]
        public virtual EndTrigMode EndTrigMode { get; set; } = EndTrigMode.AUTO;

        [Description("Break Effect")]
        public virtual BreakEffect BreakEffect { get; set; } = BreakEffect.FINISH;

        [Description("Line Rate Hz")]
        public virtual Int32 LineRate_Hz { get; set; } = 40000;

        [Description("Seq Length")]
        public virtual Int32 SeqLength_Ln
        {
            get
            {
                Int32 seqLength = 0;
                MC.GetParam(this.Channel, "SeqLength_Ln", out seqLength);
                return seqLength;
            }
            set
            {
                Int32 seqLength = 0;

                //이전 값 확인
                MC.GetParam(this.Channel, "SeqLength_Ln", out seqLength);
                Debug.WriteLine($"Channel : {this.Channel}, PageLength : {seqLength}");

                //카메라 상태 Ready로 전환
                MC.SetParam(this.Channel, "ChannelState", ChannelState.IDLE);
                Debug.WriteLine("Idle!!!");

                //카메라 설정값 바뀐값으로 변환
                MC.SetParam(this.Channel, "SeqLength_Ln", value);

                //바뀐 값 확인
                MC.GetParam(this.Channel, "SeqLength_Ln", out seqLength);
                Debug.WriteLine($"Channel : {this.Channel}, SeqLength_Ln : {seqLength}");

                //카메라 상태 다시 Active로 변환
                this.Active();
                Debug.WriteLine("ACTIVE!!!");
            }
        }

        [Description("Page Length")]
        public virtual Int32 PageLength_Ln
        {
            get
            {
                Int32 PageLength = 0;
                MC.GetParam(this.Channel, "PageLength_Ln", out PageLength);
                return PageLength;
            }
            set
            {
                Int32 PageLength = 0;

                //이전 값 확인
                MC.GetParam(this.Channel, "PageLength_Ln", out PageLength);
                Debug.WriteLine($"Channel : {this.Channel}, PageLength : {PageLength}");

                //카메라 상태 Ready로 전환
                MC.SetParam(this.Channel, "ChannelState", ChannelState.IDLE);
                Debug.WriteLine("Idle!!!");

                //카메라 설정값 바뀐값으로 변환
                MC.SetParam(this.Channel, "PageLength_Ln", value);

                //바뀐 값 확인
                MC.GetParam(this.Channel, "PageLength_Ln", out PageLength);
                Debug.WriteLine($"Channel : {this.Channel}, PageLength : {PageLength}");

                //카메라 상태 다시 Active로 변환
                this.Active();
                Debug.WriteLine("ACTIVE!!!");
            }
        }

        [Description("Expose_us")]
        public virtual Int32 Expose_us
        {
            get
            {
                Int32 Expose;
                MC.GetParam(this.Channel, "Expose_us", out Expose);
                return Expose;
            }
            set
            {
                ////카메라 상태 Ready로 전환
                MC.SetParam(this.Channel, "ChannelState", ChannelState.IDLE);
                Debug.WriteLine("Idle!!!");

                MC.SetParam(this.Channel, "Expose_us", value);

                ////카메라 상태 다시 Active로 변rud
                this.Active();
                Debug.WriteLine("ACTIVE!!!");
            }
        }

        [Description("초기화")]
        public virtual void Init()
        {
            //this.sw = new MyWatch(this.Camera.ToString());
        }

        [Description("메모리 링 버퍼")]
        private void InitSurfaceTable()
        {
            //Debug.WriteLine($"SeqLength_Ln={this.SeqLength_Ln}, PageLength_Ln={this.PageLength_Ln}");
        }

        [Description("메모리 링 버퍼 해제")]
        private void FreeSufaceTable()
        {
            // FREE all surfaces
            for (int i = 0; i < SurfaceCount; i++)
                MC.SetParam(SurfaceTable[i], "SurfaceState", "FREE");
        }


        [Description("채널 활성화 준비")]
        public void Ready()
        {
            //this.InitSurfaceTable();
            // Callback 연결
            this.CamCallBack = new MC.CALLBACK(MultiCamCallback);
            MC.RegisterCallback(this.Channel, this.CamCallBack, this.Channel);
            // Enable the signals corresponding to the callback functions
            MC.SetParam(this.Channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
            MC.SetParam(this.Channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");
            // Prepare the channel in order to minimize the acquisition sequence startup latency
            MC.SetParam(this.Channel, "ChannelState", ChannelState.READY);

            Debug.WriteLine($"{this.Channel}, {this.CurrentState()}", "READY currentState");
        }

        [Description("Acquisition 시작")]
        public void Active()
        {
            //this.EncoderTickCount = 0;
            if (this.CurrentState() != ChannelState.ACTIVE)
                MC.SetParam(this.Channel, "ChannelState", ChannelState.ACTIVE);
            Debug.WriteLine("Set Active!");
        }

        [Description("채널 IDLE")]
        public void Idle()
        {
            if (this.CurrentState() != ChannelState.IDLE)
                MC.SetParam(this.Channel, "ChannelState", ChannelState.IDLE);
            this.FreeSufaceTable();
        }

        [Description("채널 Release")]
        public void Free()
        {
            MC.SetParam(this.Channel, "ChannelState", ChannelState.FREE);
        }

        [Description("Software Trig")]
        public void SoftTrig()
        {
            MC.SetParam(this.Channel, "ForceTrig", "TRIG");
        }

        [Description("채널 상태")]
        public string CurrentState()
        {
            String State;
            MC.GetParam(this.Channel, "ChannelState", out State);
            return State;
        }

        [Description("MultiCam CallBack Event")]
        private void MultiCamCallback(ref MC.SIGNALINFO signalInfo)
        {
            switch (signalInfo.Signal)
            {
                case MC.SIG_SURFACE_PROCESSING:
                    ProcessingCallback(signalInfo);
                    break;

                case MC.SIG_ACQUISITION_FAILURE:
                    AcqFailureCallback(signalInfo);
                    break;

                default:
                    Debug.WriteLine(signalInfo.Signal, "SIGNALINFO");
                    throw new Euresys.MultiCamException("Unknown signal");
            }
        }

        [Description("Acquisition Process")]
        private void ProcessingCallback(MC.SIGNALINFO signalInfo)
        {
            Debug.WriteLine("ProcessingCallback");
            try
            {
                UInt32 currentChannel = (UInt32)signalInfo.Context;
                Int32 ImageSizeX, ImageSizeY, BufferPitch;

                MC.GetParam(currentChannel, "ImageSizeX", out ImageSizeX);
                MC.GetParam(currentChannel, "ImageSizeY", out ImageSizeY);
                MC.GetParam(currentChannel, "BufferPitch", out BufferPitch);
                Debug.WriteLine($"{ImageSizeX}", "ImageSizeX");
                Debug.WriteLine($"{ImageSizeY}", "ImageSizeY");
                Debug.WriteLine($"{BufferPitch}", "BufferPitch");

                if (this.AcquisitionMode == AcquisitionMode.PAGE)
                {
                    this.ImageGrap(currentChannel, signalInfo.SignalInfo, ImageSizeX, ImageSizeY, BufferPitch);
                }
            }
            catch (Euresys.MultiCamException ex)
            {
                //IvmUtils.Utils.DebugException(ex, 3, "MultiCamException");
                this.AcquisitionFinishedEvent?.Invoke(new AcquisitionData(this.Camera, $"MultiCam Exception: {ex.Message}"));
            }
            catch (Exception ex)
            {
                //IvmUtils.Utils.DebugException(ex, 3, "MultiCamSystemException");
                this.AcquisitionFinishedEvent?.Invoke(new AcquisitionData(this.Camera, $"System Exception: {ex.Message}"));
            }
        }
        private void ImageGrap(UInt32 Channel, UInt32 SurfaceAddr, Int32 Width, Int32 Height, Int32 BufferPitch)
        {
            
            //AcquisitionData acq = new AcquisitionData(this.Camera, this.ProductIndex);
            AcquisitionData acq = new AcquisitionData(this.Camera, PageIndex);
            PageIndex += 1;
            if (PageIndex == 3) PageIndex = 1;

            try
            {
                IntPtr BufferAddress;
                MC.GetParam(SurfaceAddr, "SurfaceAddr", out BufferAddress);
               
                acq.SetImage(new OpenCvSharp.Mat(Height, Width, OpenCvSharp.MatType.CV_8U, BufferAddress));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "System Exception");
                acq.Dispose();
                acq.Error = ex.Message;
            }
            this.AcquisitionFinishedEvent?.Invoke(acq);
        }

        [Description("Acquisition Failed")]
        private void AcqFailureCallback(MC.SIGNALINFO signalInfo)
        {
            AcquisitionData Data = new AcquisitionData(this.Camera, "Acquisition Failure, Channel State: IDLE");
            this.AcquisitionFinishedEvent?.Invoke(Data);
        }
    }
}