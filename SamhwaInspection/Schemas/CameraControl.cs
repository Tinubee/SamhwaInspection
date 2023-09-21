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

    public enum CameraType
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
        PAGE,
        LONGPAGE,
        WEB
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

    public enum MvCamAcquisitionMode
    {
        SINGLE,
        MUTLI,
        CONTINUOUS
    }

    public enum MvCamTrigMode
    {
        OFF,
        ON
    }

    public enum MvCamTirgSource
    {
        LINE0 = 0,
        LINE1 = 1,
        LINE2 = 2,
        LINE3 = 3,
        COUNTER0 = 4,
        SOFTWARE = 7,
        FrequencyConverter = 8
    }

    public enum NextTrigMode
    {
        SAME,
        REPEAT,
        HARD,
        SOFT,
        COMBINED,
    }

    public enum EndTrigMode
    {
        AUTO,
        HARD,
    }

    public enum BreakEffect
    {
        FINISH,
        ABORT,
    }

    public enum GainMode
    {
        OFF,
        ONCE,
        CONTINUOUS
    }

    public enum ExposureMode
    {
        TIMED,
        TRIGGER_WIDTH
    }

    public enum ExposureAutoMode
    {
        OFF,
        ONCE,
        CONTINUOUS
    };


    #endregion EnumSection

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

    [Description("그랩제어")]
    public class 그랩제어 : List<Cam>
    {
        public delegate void 이미지그랩완료(AcquisitionData Data);

        public event 이미지그랩완료 이미지그랩완료보고;

        private string 저장파일 { get { return Path.Combine(Global.환경설정.기본경로, "카메라설정.json"); } }

        public void Init()
        {
            MC.OpenDriver();
            Debug.WriteLine("OpenCameraDriver");
            MC.SetParam(MC.CONFIGURATION, "ErrorLog", "error.log");

            //MvCam연결된카메라찾기();
            //Camera1,2 추가
            this.Add(new Cam(CameraType.Camera1, 0, Connector.M, AcquisitionMode.PAGE, LineRateMode.CAMERA));
            //this.Load();
        }

        public Cam GetItem(CameraType cameraType)
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
                Global.Grabbing[lop] = false;   
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
                List<Cam> 자료 = JsonConvert.DeserializeObject<List<Cam>>(File.ReadAllText(this.저장파일), IvmUtils.Utils.JsonSetting());
                foreach (Cam 정보 in 자료)
                {
                    Cam 카메라 = this.GetItem(정보.Camera);
                    if (카메라 == null) continue;
                    카메라.Set();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
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

        public new void Add(Cam cam)
        {
            cam.Init();
            cam.Ready();
            cam.AcquisitionFinishedEvent += AcquisitionFinishedEvent;
            base.Add(cam);
        }

        private void AcquisitionFinishedEvent(AcquisitionData Data)
        {
            Debug.WriteLine($"Camera: {Data.Camera}, Error: {Data.Error}");
            if (Data.BmpImage == null)
            {
                Debug.WriteLine("이미지 획득 실패");
                //Global.오류로그(로그영역, "이미지그랩 오류", $"[{Data.Camera.ToString()}] {Data.Error}", true);
            }
            else
            {
                Task.Run(() =>
                {
                    Debug.WriteLine("이미지 획득 완료.");
                    this.이미지그랩완료보고?.Invoke(Data);
                });
            }
        }
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
}