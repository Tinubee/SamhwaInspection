using Euresys.MultiCam;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using IvmUtils;
using System.IO;
using SamhwaInspection.Utils;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.CodeParser.Diagnostics;
using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using OpenCvSharp.Extensions;
using System.Windows.Media.Imaging;

namespace SamhwaInspection.Schemas
{
    #region EnumSection

    public enum MvsCameraType
    {
        [Description("None"), ListBindable(false)]
        None = -1,

        [Description("수치측정")]
        Camera1 = 0,

        [Description("유무검사")]
        Camera2 = 1,

        [Description("표면검사1")]
        Camera3 = 2,

        [Description("표면검사2")]
        Camera4 = 3
    }


    public enum MvsCamAcquisitionMode
    {
        SINGLE,
        MUTLI,
        CONTINUOUS
    }

    public enum MvsCamTrigMode
    {
        OFF,
        ON
    }

    public enum MvsCamTirgSource
    {
        LINE0 = 0,
        LINE1 = 1,
        LINE2 = 2,
        LINE3 = 3,
        COUNTER0 = 4,
        SOFTWARE = 7,
        FrequencyConverter = 8
    }



    public enum MvsGainMode
    {
        OFF,
        ONCE,
        CONTINUOUS
    }

    public enum MvsExposureMode
    {
        TIMED,
        TRIGGER_WIDTH
    }

    public enum MvsExposureAutoMode
    {
        OFF,
        ONCE,
        CONTINUOUS
    };


    #endregion EnumSection

    #region ConvertStringToVar

    public static class MvsChannelState
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

    [Description("Mvs그랩제어")]
    public class Mvs그랩제어 : List<MvsCam>
    {
        public delegate void 이미지그랩완료(MvsAcquisitionData Data);

        public event 이미지그랩완료 이미지그랩완료보고;

        private string 저장파일 { get { return Path.Combine(Global.환경설정.기본경로, "카메라설정.json"); } }

        public void Init()
        {
            Debug.WriteLine("OpenCameraDriver");
            MvCam연결된카메라찾기();
        }

        public MvsCam GetItem(MvsCameraType cameraType)
        {
            return this.Where(e => e.Camera == cameraType).FirstOrDefault();
        }


        public void Start()
        {
            this.ActiveAll();
        }

        public void Stop()
        {
            this.IdleAll();
        }

        public void Close()
        {
            this.FreeAll();
            MC.CloseDriver();
            this.Clear();
            this.MvCamClose();
        }

        public void MvCamClose()
        {
            for (int lop = 0; lop < Global.Cam.Count; lop++)
            {
                Global.Cam[lop].CloseDevice();
                Global.Cam[lop].DestroyHandle();
            }
        }

        public void ActiveAll()
        {
            this.ForEach(c => c.Active());
        }

        public void IdleAll()
        {
            this.ForEach(c => c.Idle());
        }

        public void FreeAll()
        {
            this.ForEach(c => c.Free());
        }

        public void Load()
        {
            try
            {
                List<MvsCam> 자료 = JsonConvert.DeserializeObject<List<MvsCam>>(File.ReadAllText(this.저장파일), IvmUtils.Utils.JsonSetting());
                foreach (MvsCam 정보 in 자료)
                {
                    MvsCam 카메라 = this.GetItem(정보.Camera);
                    if (카메라 == null) continue;
                    카메라.Set();
                }
            }
            catch (Exception ex)
            {
                //Global.오류로그(로그영역, "카메라 설정 로드", ex.Message, false);
            }
        }

        public void Save()
        {
            if (!IvmUtils.Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this, IvmUtils.Utils.JsonSetting())))
            {
                //Global.오류로그(로그영역, "카메라 설정 저장", "카메라 설정 저장에 실패하였습니다.", true);
            }
        }

        public new void Add(MvsCam cam)
        {
            cam.Init();
            //cam.Ready();
            cam.AcquisitionFinishedEvent += AcquisitionFinishedEvent;

            base.Add(cam);
        }
        public void MvCam연결된카메라찾기()
        {
            GC.Collect();
            int nRet = 0;
            nRet = CSystem.EnumDevices(CSystem.MV_GIGE_DEVICE | CSystem.MV_USB_DEVICE, ref Global.m_ltDeviceList);
            if (0 != nRet)
            {
                Debug.WriteLine("Enumerate devices fail!");
                return;
            }
            int cnCamCount = Global.m_ltDeviceList.Count;

            for (int lop = 0; lop < cnCamCount; lop++)
            {
                if (Global.m_ltDeviceList[lop].nTLayerType == CSystem.MV_GIGE_DEVICE)
                {
                    CGigECameraInfo gigeInfo = (CGigECameraInfo)Global.m_ltDeviceList[lop];

                    if (gigeInfo.UserDefinedName != "")
                    {
                        Debug.WriteLine($"{gigeInfo.UserDefinedName} ({gigeInfo.chSerialNumber})");
                    }
                    else
                    {
                        Debug.WriteLine($"{gigeInfo.chManufacturerName} ({gigeInfo.chSerialNumber})");
                    }

                    CCameraInfo device = Global.m_ltDeviceList[lop];
                    CCamera m_MyCamera = new CCamera();

                    nRet = m_MyCamera.CreateHandle(ref device);
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        return;
                    }

                    nRet = m_MyCamera.OpenDevice();
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        m_MyCamera.DestroyHandle();
                        Debug.WriteLine($"Device open fail! {nRet}");
                        return;
                    }

                    if (device.nTLayerType == CSystem.MV_GIGE_DEVICE)
                    {
                        int nPacketSize = m_MyCamera.GIGE_GetOptimalPacketSize();
                        if (nPacketSize > 0)
                        {
                            nRet = m_MyCamera.SetIntValue("GevSCPSPacketSize", (uint)nPacketSize);
                            if (nRet != CErrorDefine.MV_OK)
                            {
                                Debug.WriteLine($"Set Packet Size failed! {nRet}");
                            }
                        }
                        else
                        {
                            Debug.WriteLine($"Get Packet Size failed!{nRet}");
                        }
                    }

                    m_MyCamera.SetEnumValue("AcquisitionMode", (uint)MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
                    m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                    m_MyCamera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);

                    Global.Cam.Add(m_MyCamera);
                    Global.Grabbing.Add(true);

                    nRet = NecessaryOperBeforeGrab(Global.Cam[lop], lop);
                    if (CErrorDefine.MV_OK != nRet)
                    {
                        return;
                    }
                }
            }
        }
       
        private Int32 NecessaryOperBeforeGrab(CCamera cam, int lop)
        {
            CIntValue pcWidth = new CIntValue();
            int nRet = cam.GetIntValue("Width", ref pcWidth);
            if (CErrorDefine.MV_OK != nRet)
            {
                Debug.WriteLine($"Get Width Info Fail! {nRet}");
                return nRet;
            }

            CIntValue pcHeight = new CIntValue();
            nRet = cam.GetIntValue("Height", ref pcHeight);
            if (CErrorDefine.MV_OK != nRet)
            {
                Debug.WriteLine($"Get Height Info Fail! {nRet}");
                return nRet;
            }

            CEnumValue pcPixelFormat = new CEnumValue();
            nRet = cam.GetEnumValue("PixelFormat", ref pcPixelFormat);
            if (CErrorDefine.MV_OK != nRet)
            {
                Debug.WriteLine($"Get Pixel Format Fail! {nRet}");
                return nRet;
            }

            if ((Int32)MvGvspPixelType.PixelType_Gvsp_Undefined == (Int32)pcPixelFormat.CurValue)
            {
                Debug.WriteLine($"Unknown Pixel Format! {CErrorDefine.MV_E_UNKNOW}");
                return CErrorDefine.MV_E_UNKNOW;
            }
            else if (IsMono((MvGvspPixelType)pcPixelFormat.CurValue))
            {
                Global.PixelFormat.Add(PixelFormat.Format8bppIndexed);
            }
            else
            {
                Global.PixelFormat.Add(PixelFormat.Format24bppRgb);
            }

            Global.Bitmap.Add(new Bitmap((Int32)pcWidth.CurValue, (Int32)pcHeight.CurValue, Global.PixelFormat[lop]));

            if (PixelFormat.Format8bppIndexed == Global.PixelFormat[lop])
            {
                ColorPalette palette = Global.Bitmap[lop].Palette;
                for (int i = 0; i < palette.Entries.Length; i++)
                {
                    palette.Entries[i] = Color.FromArgb(i, i, i);
                }
                Global.Bitmap[lop].Palette = palette;
            }

            return CErrorDefine.MV_OK;
        }

        private Boolean IsMono(MvGvspPixelType enPixelType)
        {
            switch (enPixelType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono1p:
                case MvGvspPixelType.PixelType_Gvsp_Mono2p:
                case MvGvspPixelType.PixelType_Gvsp_Mono4p:
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_Mono8_Signed:
                case MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono14:
                case MvGvspPixelType.PixelType_Gvsp_Mono16:
                    return true;
                default:
                    return false;
            }
        }

        private void AcquisitionFinishedEvent(MvsAcquisitionData Data)
        {
            Debug.WriteLine($"Camera: {Data.Camera}, Error: {Data.Error}");
            if (Data.BmpImage == null)
            {
                Debug.WriteLine("이미지 없음");
                //Global.오류로그(로그영역, "이미지그랩 오류", $"[{Data.Camera.ToString()}] {Data.Error}", true);
            }
            else
            {
                Task.Run(() =>
                {
                    Debug.WriteLine("이미지 찍힘");
                    this.이미지그랩완료보고?.Invoke(Data);
                });
            }
        }
    }

    [Description("이미지 획득 정보")]
    public class MvsAcquisitionData : IDisposable
    {
        public MvsCameraType Camera { get; set; } = MvsCameraType.None;
        public Bitmap BmpImage { get; set; } = null;
        public Mat MatImage { get; set; }
        public string Error { get; set; } = String.Empty;
        public ProductIndex ProductIndex;
        public Int32 PageIndex;

        public MvsAcquisitionData(MvsCameraType Cam)
        {
            this.Camera = Cam;
        }

        public MvsAcquisitionData(Mat Image)
        {
            this.MatImage = Image;
        }

        public MvsAcquisitionData(MvsCameraType Cam, Mat Image)
        {
            this.Camera = Cam;
            this.MatImage = Image;
        }

        public MvsAcquisitionData(MvsCameraType Cam, ProductIndex productIndex)
        {
            this.Camera = Cam;
            this.ProductIndex = productIndex;
        }

        public MvsAcquisitionData(MvsCameraType Cam, Int32 pageIndex)
        {
            this.Camera = Cam;
            this.PageIndex = pageIndex;
        }

        //public AcquisitionData(CameraType Cam, Bitmap Image)
        //{
        //    this.Camera = Cam;
        //    this.BmpImage = Image;
        //    //this.MatImage = OpenCvSharp.Extensions.BitmapConverter.ToMat(Image);
        //}

        public MvsAcquisitionData(MvsCameraType Cam, String Error)
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

        //public void Inspection()
        //{
        //    this.BmpImage.RotateFlip(RotateFlipType.Rotate90FlipX);
        //}

        public void Dispose()
        {
            this.MatImage?.Dispose();
            //this.BmpImage?.Dispose();
            //this.BmpImage = null;
            this.MatImage = null;
        }

        public void SaveImage()
        {
            if (this.BmpImage == null) return;
            //String file = $@"C:\IVM\1.bmp";
            //if (Schemas.환경설정.SaveImageFormat == ImageFormat.Png) file += ".png";
            //else file += ".bmp";
            //file += ".bmp";

            //Cv2.ImWrite(file, this.MatImage);
        }
    }
}