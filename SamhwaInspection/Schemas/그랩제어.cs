﻿using DevExpress.CodeParser;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using Euresys.MultiCam;
using IvmUtils;
using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvUtils;


namespace SamhwaInspection.Schemas
{
    public enum CameraType
    {
        [Bindable(false)]
        None = -1,
        [Description("수치측정")]
        Cam01 = 0,
        [Description("유무검사")]
        Cam02 = 1,
        [Description("표면검사뒷면")]
        Cam03 = 2,
        [Description("표면검사앞면")]
        Cam04 = 3,
    }

    #region Enum Setting by LHD

    public enum ProductIndex
    {
        PRODUCT_INDEX1,
        PRODUCT_INDEX2,
        PRODUCT_INDEX3,
        PRODUCT_INDEX4,
        PRODUCT_INDEX5,
        PRODUCT_INDEX6,
    }

    public enum Connector
    {
        M,
        A,
        B,
    }

    public enum AcquisitionMode
    {
        PAGE = 0,
        LONGPAGE = 1,
        WEB = 2,
    }

    public enum LineRateMode
    {
        PULSE,
        CONVERT,
        PERIOD,
        EXPOSE,
        CAMERA,
    }

    public enum LineCaptureMode
    {
        ALL,
        PICK,
        TAG,
        ADR,
    }


    public enum TrigMode
    {
        IMMEDIATE,
        HARD,
        SOFT,
        COMBINED,
        OFF,
        ON
    }
    #endregion

    #region ConvertStringToVar
    public static class ChannelState
    {
        [Description("채널은 그래버를 소유하고 있지만 잠금상태는 아님.")]
        public const string IDLE = "IDLE";

        [Description("채널은 그래버를 사용합니다.")]
        public const string ACTIVE = "ACTIVE";

        [Description("채널에 그래버가 없습니다.")]
        public const string ORPHAN = "ORPHAN";

        [Description("채널은 그래버를 잠그고 acquisition sequence를 시작할 준비가 됨.")]
        public const string READY = "READY";

        [Description("채널의 상태를 ORPHAN으로 설정합니다.")]
        public const string FREE = "FREE";
    }
    #endregion ConvertStringToVar

    public class 그랩제어 : Dictionary<CameraType, 카메라장치>
    {
        //public delegate void 그랩완료대리자(카메라구분 구분, CogImage8Grey 이미지);
        public delegate void 그랩완료대리자(CameraType 구분, Mat 이미지);
        public delegate void 그랩완료대리자2(CameraType 구분, List<Mat> 이미지);

        public event 그랩완료대리자 그랩완료보고;
        public event 그랩완료대리자2 그랩완료보고2;

        [JsonIgnore]
        private const string 로그영역 = "카메라";
        [JsonIgnore]
        private string 저장파일 { get { return Path.Combine(Global.환경설정.기본경로, "카메라설정.json"); } }
        //[JsonIgnore]
        //public Boolean 정상여부 { get { return !this.Values.Any(e => !e.상태); } }

        public EuresysLink 카메라1 = null;
        public HikeGigE 카메라2 = null;
        public HikeGigE 카메라3 = null;
        public HikeGigE 카메라4 = null;

        public Boolean Init()
        {
            MC.OpenDriver();
            MC.SetParam(MC.CONFIGURATION, "ErrorLog", "error.log");

            this.카메라1 = new EuresysLink(CameraType.Cam01) { 코드 = "" }; //라인스캔카메라 치수검사
            this.카메라2 = new HikeGigE() { 구분 = CameraType.Cam02, 코드 = "K38332378" }; //공트레이 제품유무검사
            this.카메라3 = new HikeGigE() { 구분 = CameraType.Cam03, 코드 = "02DA0286475" }; //후면 표면검사
            this.카메라4 = new HikeGigE() { 구분 = CameraType.Cam04, 코드 = "02DA0286474" }; //상면 표면검사
            this.Add(CameraType.Cam01, this.카메라1);
            this.Add(CameraType.Cam02, this.카메라2);
            this.Add(CameraType.Cam03, this.카메라3);
            this.Add(CameraType.Cam04, this.카메라4);

            // 카메라 설정 저장정보 로드
            카메라장치 정보;
            List<카메라장치> 자료 = Load();
            if (자료 != null)
            {
                foreach (카메라장치 설정 in 자료)
                {
                    정보 = this.GetItem(설정.구분);
                    if (정보 == null) continue;
                    정보.Set(설정);
                }
            }

            List<CCameraInfo> 카메라들 = new List<CCameraInfo>();
            Int32 nRet = CSystem.EnumDevices(CSystem.MV_GIGE_DEVICE, ref 카메라들);// | CSystem.MV_USB_DEVICE
            if (!Validate("Enumerate devices fail!", nRet, true)) return false;

            for (int i = 0; i < 카메라들.Count; i++)
            {
                CGigECameraInfo gigeInfo = 카메라들[i] as CGigECameraInfo;
                HikeGigE gige = this.GetItem(gigeInfo.chSerialNumber) as HikeGigE;
                if (gige == null) continue;
                gige.Init(gigeInfo);
            }

            //Debug.WriteLine($"카메라 갯수: {this.Count}");
            GC.Collect();
            return true;
        }

        private List<카메라장치> Load()
        {
            if (!File.Exists(this.저장파일)) return null;
            return JsonConvert.DeserializeObject<List<카메라장치>>(File.ReadAllText(this.저장파일), IvmUtils.Utils.JsonSetting());
        }

        public void Save()
        {
            if (!IvmUtils.Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this.Values, IvmUtils.Utils.JsonSetting())))
                Global.오류로그(로그영역, "카메라 설정 저장", "카메라 설정 저장에 실패하였습니다.", true);
        }

        public void Close()
        {
            foreach (카메라장치 장치 in this.Values)
                장치?.Close();
            //this.Save();
        }

        public void Ready(CameraType 카메라) => this.GetItem(카메라)?.Ready();

        public void 그랩완료(CameraType 카메라, Mat 이미지)
        {
            if (카메라 == CameraType.Cam02)
            {
                Debug.WriteLine("공트레이 제품유무검사 이미지 그랩완료");
                //this.카메라2.Stop();
                new Thread(() =>
                {
                    Global.비전마스터구동.GetItem(Flow구분.유무검사).유무검사(이미지);
                }).Start();
            }
            this.그랩완료보고?.Invoke(카메라, 이미지);
        }

        public void 그랩완료(CameraType 카메라, List<Mat> 이미지)
        {
            if (카메라 == CameraType.Cam04) //상부표면검사
            {
                //Debug.WriteLine($"{카메라} 이미지획득 {this.카메라4.MatImage.Count}개 완료");
                Global.조명제어.TurnOff(조명구분.상면검사조명);

                if (Global.비전마스터구동.Count == 0 || Global.신호제어.마스터모드여부 == 1)
                    return;

                new Thread(() =>
                {
                    for (int lop = 0; lop < this.카메라4.MatImage.Count; lop++)
                    {
                        Debug.WriteLine($"--------------상면표면검사 For문 들어옴.-------------");
                        Global.비전마스터구동.글로벌변수제어.InspectUseSet("상면검사순서", lop.ToString());
                        Global.비전마스터구동.GetItem(Flow구분.표면검사앞).표면검사(이미지[lop], lop);
                    }
                }).Start();
            }
            else if (카메라 == CameraType.Cam03) //하부표면검사
            {
                Debug.WriteLine($"{카메라} 이미지획득 {this.카메라3.MatImage.Count}개 완료");
                Global.조명제어.TurnOff(조명구분.후면검사조명);

                if (Global.비전마스터구동.Count == 0 || Global.신호제어.마스터모드여부 == 1)
                    return;

                new Thread(() =>
                {
                    for (int lop = 0; lop < this.카메라3.MatImage.Count; lop++)
                    {
                        Global.비전마스터구동.글로벌변수제어.InspectUseSet("후면검사순서", lop.ToString());
                        Global.비전마스터구동.GetItem(Flow구분.표면검사뒤).표면검사(이미지[lop], lop);
                    }
                }).Start();
            }
            this.그랩완료보고2?.Invoke(카메라, 이미지);
        }

        public void ImageSave(List<Mat> 이미지, CameraType 카메라, Int32 검사번호, 결과구분 결과)
        {
            if (!Global.환경설정.사진저장OK && !Global.환경설정.사진저장NG) return;
            List<String> paths = new List<String> { Global.환경설정.이미지저장경로, IvmUtils.Utils.FormatDate(DateTime.Now, "{0:yyyy-MM-dd}"), Global.환경설정.선택모델.ToString(), 카메라.ToString() };
            String name = $"{검사번호.ToString("d4")}_{IvmUtils.Utils.FormatDate(DateTime.Now, "{0:HHmmss}")}.png";//_{결과.ToString()}
            String path = Utils.Common.CreateDirectory(paths);
            if (String.IsNullOrEmpty(path))
            {
                Global.오류로그(로그영역, "이미지 저장", $"[{Path.Combine(paths.ToArray())}] 디렉토리를 만들 수 없습니다.", true);
                return;
            }
            String file = Path.Combine(path, name);
            //Debug.WriteLine($"{이미지.Size()}: {file}", $"{카메라} 그랩완료");
            Task.Run(() =>
            {
                Int32 level = 3; // 0에서 9까지의 값 중 선택
                Int32[] @params = new[] { (Int32)ImwriteFlags.PngCompression, level };

                for (int lop = 0; lop < 이미지.Count; lop++)
                {
                    Cv2.ImWrite(file, 이미지[lop], @params);
                    이미지[lop].Dispose();
                }
            });
        }

        public 카메라장치 GetItem(CameraType 구분)
        {
            if (this.ContainsKey(구분)) return this[구분];
            return null;
        }

        private 카메라장치 GetItem(String serial) => this.Values.Where(e => e.코드 == serial).FirstOrDefault();

        #region 오류메세지
        public static Boolean Validate(String message, Int32 errorNum, Boolean show)
        {
            //Debug.WriteLine(message);
            if (errorNum == CErrorDefine.MV_OK) return true;

            String errorMsg = String.Empty;
            switch (errorNum)
            {
                case CErrorDefine.MV_E_HANDLE: errorMsg = "Error or invalid handle"; break;
                case CErrorDefine.MV_E_SUPPORT: errorMsg = "Not supported function"; break;
                case CErrorDefine.MV_E_BUFOVER: errorMsg = "Cache is full"; break;
                case CErrorDefine.MV_E_CALLORDER: errorMsg = "Function calling order error"; break;
                case CErrorDefine.MV_E_PARAMETER: errorMsg = "Incorrect parameter"; break;
                case CErrorDefine.MV_E_RESOURCE: errorMsg = "Applying resource failed"; break;
                case CErrorDefine.MV_E_NODATA: errorMsg = "No data"; break;
                case CErrorDefine.MV_E_PRECONDITION: errorMsg = "Precondition error, or running environment changed"; break;
                case CErrorDefine.MV_E_VERSION: errorMsg = "Version mismatches"; break;
                case CErrorDefine.MV_E_NOENOUGH_BUF: errorMsg = "Insufficient memory"; break;
                case CErrorDefine.MV_E_UNKNOW: errorMsg = "Unknown error"; break;
                case CErrorDefine.MV_E_GC_GENERIC: errorMsg = "General error"; break;
                case CErrorDefine.MV_E_GC_ACCESS: errorMsg = "Node accessing condition error"; break;
                case CErrorDefine.MV_E_ACCESS_DENIED: errorMsg = "No permission"; break;
                case CErrorDefine.MV_E_BUSY: errorMsg = "Device is busy, or network disconnected"; break;
                case CErrorDefine.MV_E_NETER: errorMsg = "Network error"; break;
                default: errorMsg = "Unknown error"; break;
            }

            Global.오류로그("Camera", "Error", $"[{errorNum}] {message} {errorMsg}", show);
            return false;
        }
        #endregion
    }

    public class 카메라장치
    {
        [JsonProperty("Camera")]
        public virtual CameraType 구분 { get; set; } = CameraType.None;
        [JsonIgnore, Translation("Index", "번호")]
        public virtual Int32 번호 { get; set; } = 0;
        [JsonProperty("Serial"), Translation("Serial", "Serial")]
        public virtual String 코드 { get; set; } = String.Empty;
        [JsonIgnore, Translation("Name", "명칭")]
        public virtual String 명칭 { get; set; } = String.Empty;
        [JsonProperty("Description"), Translation("Description", "설명")]
        public virtual String 설명 { get; set; } = String.Empty;
        [JsonProperty("IpAddress"), Translation("IP", "IP")]
        public virtual String 주소 { get; set; } = String.Empty;
        //[JsonProperty("Timeout"), Description("Timeout"), Translation("Timeout", "제한시간", "Čas vypršal")]
        //public virtual Double 시간 { get; set; } = 1000;
        //[JsonProperty("Exposure"), Description("Exposure"), Translation("Exposure", "노출", "Vystavenie")]
        //public virtual Single 노출 { get; set; } = 300;
        [JsonProperty("BlackLevel"), Description("Black Level"), Translation("BlackLevel", "밝기")]
        public virtual UInt32 밝기 { get; set; } = 0;
        [JsonProperty("Contrast"), Description("Contrast"), Translation("Contrast", "대비")]
        public virtual Single 대비 { get; set; } = 10;

        [JsonProperty("Width"), Description("Width"), Translation("Width", "가로")]
        public virtual Int32 가로 { get; set; } = 0;
        [JsonProperty("Height"), Description("Height"), Translation("Height", "세로")]
        public virtual Int32 세로 { get; set; } = 0;
        [JsonProperty("OffsetX"), Description("OffsetX"), Translation("OffsetX", "OffsetX")]
        public virtual Int32 OffsetX { get; set; } = 0;

        [JsonIgnore, Description("카메라 초기화 상태"), Translation("Live", "상태")]
        public virtual Boolean 상태 { get; set; } = false;
        [JsonIgnore]
        public virtual Boolean 검사결과 { get; set; } = false;
        [JsonIgnore]
        public const String 로그영역 = "카메라장치";

        public virtual void Set(카메라장치 장치)
        {
            if (장치 == null) return;
            this.코드 = 장치.코드;
            this.설명 = 장치.설명;
            //this.시간 = 장치.시간;
            //this.노출 = 장치.노출;
            //this.대비 = 장치.대비;
            this.밝기 = 장치.밝기;
            this.가로 = 장치.가로;
            this.세로 = 장치.세로;
            this.OffsetX = 장치.OffsetX;
        }

        public virtual Boolean Init() => false;
        public virtual Boolean Ready() => false;
        public virtual Boolean Start() => false;
        public virtual Boolean Stop() => false;
        public virtual Boolean Close() => false;
        public virtual Boolean ClearImageBuffer() => false;
    }

    public class HikeGigE : 카메라장치
    {
        [JsonIgnore]
        private CCamera Camera = null;
        [JsonIgnore]
        private CCameraInfo Device;
        [JsonIgnore]
        private cbOutputExdelegate ImageCallBackDelegate;

        public uint ImageCount = 6;
        public List<Mat> MatImage = new List<Mat>();
        //public List<IntPtr> MatImageData = new List<IntPtr>();
        //public int grabCount = 0;

        public Boolean Init(CGigECameraInfo info)
        {
            try
            {
                this.Camera = new CCamera();
                this.Device = info;
                this.ImageCallBackDelegate = new cbOutputExdelegate(ImageCallBack);

                this.명칭 = info.chManufacturerName + " " + info.chModelName;
                UInt32 ip1 = (info.nCurrentIp & 0xff000000) >> 24;
                UInt32 ip2 = (info.nCurrentIp & 0x00ff0000) >> 16;
                UInt32 ip3 = (info.nCurrentIp & 0x0000ff00) >> 8;
                UInt32 ip4 = info.nCurrentIp & 0x000000ff;
                this.주소 = $"{ip1}.{ip2}.{ip3}.{ip4}";
                this.상태 = this.Init();
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "초기화", $"초기화 오류: {ex.Message}", true);
                this.상태 = false;
            }

            Debug.WriteLine($"{this.명칭}, {this.코드}, {this.주소}, {this.상태}");
            return this.상태;
        }

        //private Boolean RequireSet(CIntValue val, Int32 current) => val.CurValue != current && val.Min >= current && val.Max <= current;
        public override Boolean Init()
        {
            Int32 nRet = this.Camera.CreateHandle(ref Device);
            if (!그랩제어.Validate($"[{this.구분}] 카메라 초기화에 실패하였습니다.", nRet, true)) return false;

            nRet = this.Camera.OpenDevice();
            if (!그랩제어.Validate($"[{this.구분}] 카메라 연결 실패!", nRet, true)) return false;

            그랩제어.Validate("", this.Camera.SetBoolValue("BlackLevelEnable", true), false);

            this.Camera.SetImageNodeNum(ImageCount);
            //this.옵션적용();

            Global.정보로그(로그영역, "카메라 연결", $"[{this.구분}] 카메라 연결 성공!", false);

            그랩제어.Validate("RegisterImageCallBackEx", this.Camera.RegisterImageCallBackEx(this.ImageCallBackDelegate, IntPtr.Zero), false);
            return true;
        }

        private void 옵션적용()
        {
            //this.노출적용();
            //this.대비적용();
            //this.밝기적용();
        }

        //public void 밝기적용() // Black Level : 0 ~ 4095
        //{
        //    if (this.Camera == null) return;
        //    Int32 nRet = this.Camera.SetIntValue("BlackLevel", this.밝기); //this.Camera.SetBrightness(this.밝기);
        //    그랩제어.Validate($"[{this.구분}] 밝기 설정에 실패하였습니다.", nRet, true);
        //}

        ////public void 노출적용()
        //{
        //    if (this.Camera == null) return;
        //    Int32 nRet = this.Camera.SetFloatValue("ExposureTime", this.노출);
        //    그랩제어.Validate($"[{this.구분}] 노출 설정에 실패하였습니다.", nRet, true);
        //}

        //public void 대비적용() // Gain
        //{
        //    if (this.Camera == null) return;
        //    Int32 nRet = this.Camera.SetFloatValue("Gain", this.대비);
        //    그랩제어.Validate($"[{this.구분}] 대비 설정에 실패하였습니다.", nRet, true);
        //}

        public override Boolean Start()
        {
            return 그랩제어.Validate($"{this.구분} 그래버 시작 오류!", Camera.StartGrabbing(), true);
        }

        public override Boolean Ready()
        {
            this.Camera.ClearImageBuffer();
            return Start();
        }

        public override Boolean Close()
        {
            if (this.Camera == null || !this.상태) return true;
            //this.Stop();
            //this.Camera.ClearImageBuffer();
            return 그랩제어.Validate($"{this.구분} 종료오류!", Camera.CloseDevice(), false);
        }

        public override Boolean Stop()
        {
            Camera.ClearImageBuffer();
            return 그랩제어.Validate($"{this.구분} 정지오류!", Camera.StopGrabbing(), false);
        }

        public override Boolean ClearImageBuffer()
        {
            Camera.ClearImageBuffer();
            return 그랩제어.Validate($"{this.구분} 이미지 버퍼 클리어!", Camera.ClearImageBuffer(), false);
        }

        #region 이미지 그랩
        public Boolean TrigForce() => 그랩제어.Validate($"{this.구분} TriggerSoftware", this.Camera.SetCommandValue("TriggerSoftware"), true);

        private void ImageCallBack(IntPtr data, ref MV_FRAME_OUT_INFO_EX frameInfo, IntPtr user)
        {
            try
            {
                Mat image = new Mat(frameInfo.nHeight, frameInfo.nWidth, MatType.CV_8U, data);

                if (this.구분 == CameraType.Cam02)
                {
                    Global.그랩제어.그랩완료(this.구분, image);
                    Debug.WriteLine($"Check End Grab : {this.구분}");
                    this.Stop();
                }
                else if (this.구분 == CameraType.Cam04)
                {
                    this.MatImage.Add(image);
                    Debug.WriteLine($"***************4번카메라 이미지 {this.MatImage.Count}개획득완료\"***************");
                    if (Global.그랩제어.카메라4.MatImage.Count == 6)
                    {
                        Debug.WriteLine("\"***************4번카메라 이미지 6개획득완료\"***************");
                        this.Stop();
                        Global.그랩제어.그랩완료(this.구분, this.MatImage);
                    }
                }
                else if (this.구분 == CameraType.Cam03)
                {
                    this.MatImage.Add(image);
                    if (Global.그랩제어.카메라3.MatImage.Count == 6)
                    {
                        this.Stop();
                        Global.그랩제어.그랩완료(this.구분, this.MatImage);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "Camera Call Back Error", $"[{this.구분}] {ex.Message}", false);
                return;
            }
        }
        #endregion
    }

    public class EuresysLink : 카메라장치
    {
        [Description("이미지 그랩 이벤트")]
        public delegate void AcquisitionFinished(AcquisitionData Data);

        public event AcquisitionFinished AcquisitionFinishedEvent;

        //private UInt32[] SurfaceTable;
        //private Int32 SurfaceCount = 2;
        private Int32 PageIndex = 1;
        public ProductIndex ProductIndex = ProductIndex.PRODUCT_INDEX1;

        [JsonIgnore, Description("채널번호")]
        public UInt32 Channel;
        [JsonIgnore, Description("카메라 설정 파일")]
        public string CamFile { get; set; } = "LA-CM-16K05A_L16380SC.cam";
        [JsonIgnore, Description("그래버 보드 Index")]
        public UInt32 DriverIndex { get; set; } = 0;
        [JsonIgnore, Description("Connector")]
        public Connector Connector { get; set; } = Connector.M;
        [JsonIgnore, Description("Acquisition Mode")]
        public AcquisitionMode AcquisitionMode { get; set; } = AcquisitionMode.PAGE;
        [JsonIgnore, Description("LineRateMode")]
        public LineRateMode LineRateMode { get; set; } = LineRateMode.PULSE;
        [Description("LineCaptureMode")]
        public virtual LineCaptureMode LineCaptureMode { get; set; } = LineCaptureMode.ALL;
        [JsonIgnore, Description("Trig Mode")]
        public TrigMode TrigMode { get; set; } = TrigMode.HARD;
        [JsonIgnore, Description("SeqLength_pg")]
        public Int32 SeqLength_pg { get; set; } = 2;
        [JsonIgnore, Description("Page Length")]
        public Int32 PageLength_Ln { get; set; } = 45000;
        [JsonIgnore]
        private MC.CALLBACK CamCallBack;

        //private UInt32 currentSurface;

        public Int32 height;
        public Int32 width;

        public EuresysLink(CameraType 구분)
        {
            this.구분 = 구분;
            this.상태 = Init();
            this.AcquisitionFinishedEvent += AcquisitionFinishedEvent;
        }

        public override Boolean Init()
        {
            //일단 대부분 프로퍼티에 넣어놔서 패스
            //카메라세팅값 적용
            MC.Create("CHANNEL", out this.Channel);
            MC.SetParam(this.Channel, "DriverIndex", this.DriverIndex);
            MC.SetParam(this.Channel, "Connector", this.Connector.ToString());
            MC.SetParam(this.Channel, "CamFile", Path.Combine(Global.환경설정.기본경로, this.CamFile));

            MC.SetParam(this.Channel, "AcquisitionMode", this.AcquisitionMode.ToString());
            MC.SetParam(this.Channel, "TrigMode", this.TrigMode.ToString());
            //MC.SetParam(this.Channel, "NextTrigMode", this.NextTrigMode.ToString());
            //MC.SetParam(this.Channel, "EndTrigMode", this.EndTrigMode.ToString());
            //MC.SetParam(this.Channel, "BreakEffect", this.BreakEffect.ToString());
            MC.SetParam(this.Channel, "PageLength_Ln", this.PageLength_Ln);
            MC.SetParam(this.Channel, "SeqLength_pg", this.SeqLength_pg);

            MC.GetParam(this.Channel, "ImageSizeY", out this.height);
            MC.GetParam(this.Channel, "ImageSizeX", out this.width);

            //콜백등록
            this.CamCallBack = new MC.CALLBACK(MultiCamCallback);
            MC.RegisterCallback(this.Channel, this.CamCallBack, this.Channel);
            // Enable the signals corresponding to the callback functions
            MC.SetParam(this.Channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
            MC.SetParam(this.Channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");
            MC.SetParam(this.Channel, "ChannelState", ChannelState.READY);
            Debug.WriteLine($"{this.Channel}, {this.CurrentState()}", "READY currentState");
            //Global.정보로그(로그영역, "카메라 연결", $"[{this.구분}] 카메라 연결 성공!", false);
            return true;
        }

        public override Boolean Close()
        {
            this.Free();
            return true;
        }

        public override Boolean Start()
        {
            this.Ready();
            return true;
        }

        public override Boolean Stop()
        {
            if (this.CurrentState() != ChannelState.READY)
                MC.SetParam(this.Channel, "ChannelState", ChannelState.READY);
            //Debug.WriteLine("Set Ready!");
            return true;
        }

        [Description("채널 활성화 준비")]
        public override Boolean Ready()
        {
            if (this.CurrentState() != ChannelState.ACTIVE)
                MC.SetParam(this.Channel, "ChannelState", ChannelState.ACTIVE);
            Debug.WriteLine("Line Camera Set Active!");
            return true;
        }
        [Description("Software Trig")]
        public void SoftTrig()
        {
            MC.SetParam(this.Channel, "ForceTrig", "TRIG");
        }
        [Description("채널 Release")]
        public void Free()
        {
            MC.SetParam(this.Channel, "ChannelState", ChannelState.FREE);
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
            Debug.WriteLine("멀티캠콜백 들어옴");
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
            Debug.WriteLine("LineCamera ProcessingCallback");
            //this.Ready();
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
                this.AcquisitionFinishedEvent?.Invoke(new AcquisitionData(this.구분, $"MultiCam Exception: {ex.Message}"));
            }
            catch (Exception ex)
            {
                //IvmUtils.Utils.DebugException(ex, 3, "MultiCamSystemException");
                this.AcquisitionFinishedEvent?.Invoke(new AcquisitionData(this.구분, $"System Exception: {ex.Message}"));
            }
        }
        private void ImageGrap(UInt32 Channel, UInt32 SurfaceAddr, Int32 Width, Int32 Height, Int32 BufferPitch)
        {
            //AcquisitionData acq = new AcquisitionData(this.Camera, this.ProductIndex);
            Debug.WriteLine("LineCamera ImageGrab");
            AcquisitionData acq = new AcquisitionData(this.구분, PageIndex);
            Mat image = new Mat();
            PageIndex += 1;
            if (PageIndex == 3) PageIndex = 1;

            try
            {
                IntPtr BufferAddress;
                MC.GetParam(SurfaceAddr, "SurfaceAddr", out BufferAddress);
                image = new Mat(Height, Width, MatType.CV_8U, BufferAddress);
                acq.SetImage(image);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "System Exception");
                acq.Dispose();
                acq.Error = ex.Message;
            }
            this.AcquisitionFinishedEvent?.Invoke(acq);
        }

        [Description("이미지 획득 정보")]
        public class AcquisitionData : IDisposable
        {
            public CameraType Camera { get; set; } = CameraType.None;
            public Bitmap BmpImage { get; set; } = null;
            public Mat MatImage { get; set; }
            public string Error { get; set; } = String.Empty;
            public ProductIndex ProductIndex;
            public Int32 PageIndex;

            public AcquisitionData(CameraType Cam)
            {
                this.Camera = Cam;
            }

            public AcquisitionData(CameraType Cam, Mat Image)
            {
                this.Camera = Cam;
                this.MatImage = Image;
            }

            public AcquisitionData(CameraType Cam, ProductIndex productIndex)
            {
                this.Camera = Cam;
                this.ProductIndex = productIndex;
            }

            public AcquisitionData(CameraType Cam, Int32 pageIndex)
            {
                this.Camera = Cam;
                this.PageIndex = pageIndex;
            }

            public AcquisitionData(CameraType Cam, String Error)
            {
                this.Camera = Cam;
                this.Error = Error;
            }

            public void SetImage(Mat image)
            {
                this.MatImage?.Dispose();
                this.MatImage = image;
                this.BmpImage?.Dispose();
                this.BmpImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(this.MatImage);
            }

            public void Dispose()
            {
                this.MatImage?.Dispose();
                this.MatImage = null;
            }
        }

        [Description("Acquisition Failed")]
        private void AcqFailureCallback(MC.SIGNALINFO signalInfo)
        {
            Debug.WriteLine("유레시스영상획득 실패");
            //Utils.MessageBox("영상획득", "유레시스영상획득 실패", 2000);
        }
    }
}
