using Newtonsoft.Json;
using Npgsql;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;
using VM.Core;

namespace SamhwaInspection.Schemas
{
    public class 환경설정
    {
        public delegate void 모델변경(Int32 모델번호);
        public event 모델변경 모델변경알림;

        public delegate void 현재결과상태갱신();
        public event 현재결과상태갱신 결과상태갱신알림;

        [Description("설정저장 기본경로"), JsonProperty("ConfigSavePath")]
        public string 기본경로 { get; set; } = @"C:\IVM\SamhwaENP\Config";
        [Description("이미지 저장위치"), JsonProperty("ImageSavePath")]
        public string 이미지저장경로 { get; set; } = @"C:\IVM\SamhwaENP\SaveImage"; //Path.Combine(Application.StartupPath, @"SaveImage");
        [Description("결과 저장위치"), JsonProperty("DocumentSavePath")]
        public string 자료저장경로 { get; set; } = @"C:\IVM\SamhwaENP\SaveData"; //Path.Combine(Application.StartupPath, @"SaveData");
        [Description("결과 저장위치"), JsonProperty("ModelSavePath")]
        public string 모델저장경로 { get; set; } = @"C:\IVM\SamhwaENP\SaveData\Models";
        [Description("OK 이미지 저장"), JsonProperty("SaveOK")]
        public Boolean 사진저장OK { get; set; } = false;
        [Description("NG 이미지 저장"), JsonProperty("SaveNG")]
        public Boolean 사진저장NG { get; set; } = false;
        [Description("현재 검사모델"), JsonProperty("CurrentModel")]
        public Int32 선택모델 { get; set; } = 1;
        [Description("검사결과 보관일수"), JsonProperty("DaysToKeepResults")]
        public int 결과보관 { get; set; } = 180;
        [Description("로그 보관일수"), JsonProperty("DaysToKeepLogs")]
        public int 로그보관 { get; set; } = 120;

        [Description("큰원 치수 측정"), JsonProperty("BigCircleIns")]
        public Boolean 큰원치수측정검사 { get; set; } = false;
        [Description("작은원 치수 측정"), JsonProperty("SmallCircleIns")]
        public Boolean 작은원치수측정검사 { get; set; } = false;
        [Description("높이 측정"), JsonProperty("Height")]
        public Boolean 높이측정검사 { get; set; } = false;
        [Description("너비 측정"), JsonProperty("Width")]
        public Boolean 너비측정검사 { get; set; } = false;
        [Description("슬롯부 20Point 측정"), JsonProperty("Slot20PointIns")]
        public Boolean 슬롯부20Point검사 { get; set; } = false;
        [Description("슬롯부 200Point 측정"), JsonProperty("Slot200PointIns")]
        public Boolean 슬롯부200Point검사 { get; set; } = false;
        [Description("50.5부 측정"), JsonProperty("50.5PointIns")]
        public Boolean D50_5부측정 { get; set; } = false;
        [Description("33.94부 측정"), JsonProperty("33.94PointIns")]
        public Boolean D33_94부측정 { get; set; } = false;
        [Description("15부 측정"), JsonProperty("15PointIns")]
        public Boolean D15부측정 { get; set; } = false;


        #region 수량관리
        [Description("양품갯수"), JsonProperty("OK")]
        public Int32 양품갯수 { get; set; } = 0;
        [Description("불량갯수"), JsonProperty("NG")]
        public Int32 불량갯수 { get; set; } = 0;
        [JsonIgnore]
        public Int32 전체갯수 { get { return 양품갯수 + 불량갯수; } }
        [JsonIgnore]
        public Double 양품수율 { get { return 전체갯수 > 0 ? (Double)양품갯수 / (Double)전체갯수 * (Double)100 : (Double)0; } }
        [JsonIgnore]
        public String 양품갯수표현 { get { return IvmUtils.Utils.FormatNumeric(양품갯수, "{0:#,0}"); } }
        [JsonIgnore]
        public String 불량갯수표현 { get { return IvmUtils.Utils.FormatNumeric(불량갯수, "{0:#,0}"); } }
        [JsonIgnore]
        public String 전체갯수표현 { get { return IvmUtils.Utils.FormatNumeric(전체갯수, "{0:#,0}"); } }
        [JsonIgnore]
        public String 양품수율표현 { get { return IvmUtils.Utils.FormatNumeric(양품수율, "{0:#,0}"); } }
        [JsonIgnore]
        public 결과구분 현재결과상태 { get; set; } = 결과구분.NO;
        #endregion

        #region 용량관리
        [JsonIgnore, Description("이미지 저장 디스크 사용율")]
        public int 저장비율 { get { return 100 - this.SaveImageDriveFreeSpace(); } }
        #endregion

        //#region 결과뷰관리
        //[JsonIgnore, Description("결과이미지1_MatType")]
        //public Mat resultMatImage_cam1 { get; set; } = new Mat();
        //[JsonIgnore, Description("결과이미지2_MatType")]
        //public Mat resultMatImage_cam2 { get; set; } = new Mat();
        //[JsonIgnore, Description("결과이미지2의 잘라낼 합성지점")]
        //public int cuttingPixel { get; set; } = 1030;
        //#endregion

        #region 사용자관리
        [JsonIgnore, Description("로그인상태")]
        public Boolean 로그인상태 = false;
        [JsonIgnore, Description("사용자명")]
        public string 사용자명 { get; set; } = String.Empty;

        [JsonIgnore, Description("권한구분")]
        public 유저권한구분 사용권한 { get; set; } = 유저권한구분.없음;

        [JsonIgnore, Description("슈퍼유저")]
        public const string 시스템관리자 = "ivmadmin";

        public 유저권한구분 시스템관리자인증(string 사용자명, string 비밀번호)
        {
            if (사용자명 != 시스템관리자) return 유저권한구분.없음;
            string pass = $"{시스템관리자}";// {Utils.FormatDate(DateTime.Now, "{0:ddHH}")}";
            if (비밀번호 == pass) return 유저권한구분.시스템;
            return 유저권한구분.없음;
        }
        #endregion

        [JsonIgnore, Description("이미지 저장 Format")]
        public static ImageFormat SaveImageFormat = ImageFormat.Bmp;
        [JsonIgnore]
        public 동작구분 동작구분 { get; set; } = 동작구분.Live;

        [JsonIgnore]
        private string 저장파일 { get { return Path.Combine(this.기본경로, "환경설정.json"); } }

        [JsonIgnore]
        public String 로그경로 { get { return Path.Combine(Application.StartupPath, "logs"); } }
        [JsonIgnore]
        public String OK이미지저장경로 { get { return Path.Combine(this.이미지저장경로, "OK"); } }
        [JsonIgnore]
        public String NG이미지저장경로 { get { return Path.Combine(this.이미지저장경로, "NG"); } }
        [JsonIgnore]
        public String OK이미지Cam1폴더경로 { get { return Path.Combine(this.OK이미지저장경로, "Camera1"); } }
        [JsonIgnore]
        public String OK이미지Cam2폴더경로 { get { return Path.Combine(this.OK이미지저장경로, "Camera2"); } }
        [JsonIgnore]
        public String NG이미지Cam1폴더경로 { get { return Path.Combine(this.NG이미지저장경로, "Camera1"); } }
        [JsonIgnore]
        public String NG이미지Cam2폴더경로 { get { return Path.Combine(this.NG이미지저장경로, "Camera2"); } }

        //[JsonIgnore]
        //public String 검사데이터저장경로 { get { return Path.Combine(this.자료저장경로, "InspectData"); } }

        [JsonIgnore]
        public Boolean 메뉴얼검사확인 = false;

        public String 로그파일() { return 로그파일(DateTime.Today); }

        public String 로그파일(DateTime 일자) { return Path.Combine(로그경로, IvmUtils.Utils.FormatDate(일자, "yyyyMMdd") + ".db"); }

        [JsonIgnore]
        private const string 로그영역 = "환경설정";

        public bool Init()
        {
            return this.Load();
        }

        public void Close()
        {
            this.Save();
        }

        public bool Load()
        {
            if (!CanDbConnect())
            {
                Global.오류로그(로그영역, "데이터베이스 연결실패", "데이터베이스에 연결할 수 없습니다.", true);
                return false;
            }

            if (!Utils.Common.DirectoryExists(기본경로, true))
            {
                Global.오류로그(로그영역, "기본경로", "기본경로 디렉토리를 생성할 수 없습니다.", true);
                return false;
            }

            if (!Utils.Common.DirectoryExists(로그경로, true))
            {
                Global.오류로그(로그영역, "로그경로", "로그 디렉토리를 생성할 수 없습니다.", true);
                return false;
            }

            if (File.Exists(저장파일))
            {
                try
                {
                    환경설정 설정 = JsonConvert.DeserializeObject<환경설정>(File.ReadAllText(저장파일, Encoding.UTF8));
                    foreach (PropertyInfo p in 설정.GetType().GetProperties())
                    {
                        if (!p.CanWrite) continue;
                        Object v = p.GetValue(설정);
                        if (v == null) continue;
                        p.SetValue(this, v);
                    }
                }
                catch (Exception ex)
                {
                    Global.오류로그(로그영역, "환경설정 로드", ex.Message, true);
                }
            }

            else
            {
                this.Save();
                Global.정보로그(로그영역, "환경설정 로드", "저장된 설정파일이 없습니다.", false);
            }

            if (!Utils.Common.DirectoryExists(이미지저장경로, true))
            {
                Global.오류로그(로그영역, "환경설정 로드", "이미지 저장경로를 생성할 수 없습니다.", true);
                return false;
            }
            if (!Utils.Common.DirectoryExists(자료저장경로, true))
            {
                Global.오류로그(로그영역, "환경설정 로드", "자료 저장경로를 생성할 수 없습니다.", true);
                return false;
            }
            if (!Utils.Common.DirectoryExists(모델저장경로, true))
            {
                Global.오류로그(로그영역, "환경설정 로드", "모델 저장경로를 생성할 수 없습니다.", true);
                return false;
            }
            this.ImageSaveDrive = this.GetSaveImageDrive();

            return true;
        }

        public void Save()
        {
            if (!IvmUtils.Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this, IvmUtils.Utils.JsonSetting())))
            {
                Global.오류로그(로그영역, "환경설정 저장", "환경설정 저장에 실패하였습니다.", true);
            }
        }

        public void 모델변경요청(Int32 모델번호)
        {
            모델정보 모델 = Global.모델자료.GetItem(모델번호);
            if (모델 == null)
            {
                Global.정보로그(로그영역, "모델변경", $"모델번호 [{모델번호}]은(는) 등록되지 않았습니다.", false);
                return;
            }
            if (this.선택모델 == 모델번호) return;
            this.선택모델 = 모델번호;
            Global.정보로그(로그영역, "모델변경", $"검사모델을 [{this.선택모델}]로 변경하였습니다.", false);
            this.모델변경알림?.Invoke(this.선택모델);
        }

        public void 결과갱신요청()
        {
            this.결과상태갱신알림?.Invoke();
        }

        public void 수량리셋()
        {
            this.양품갯수 = 0;
            this.불량갯수 = 0;
            Debug.WriteLine($"{양품갯수}", nameof(양품갯수));
            Debug.WriteLine($"{불량갯수}", nameof(불량갯수));
            Debug.WriteLine($"{전체갯수}", nameof(전체갯수));
            Debug.WriteLine($"{양품갯수표현}", nameof(양품갯수표현));
            Debug.WriteLine($"{불량갯수표현}", nameof(불량갯수표현));
            Debug.WriteLine($"{전체갯수표현}", nameof(전체갯수표현));
        }


        [Description("결과별 표현색상")]
        public static System.Drawing.Color ResultColor(결과구분 구분)
        {
            if (구분 == 결과구분.NO) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;

            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }

        public static NpgsqlConnection CreateDbConnection()
        {
            NpgsqlConnectionStringBuilder b = new NpgsqlConnectionStringBuilder() { Host = "localhost", Port = 5432, Username = "postgres", Password = "ivmadmin", Database = "Samhwa" };
            return new NpgsqlConnection(b.ConnectionString);
        }

        public Boolean CanDbConnect()
        {
            Boolean can = false;
            try
            {
                NpgsqlConnection conn = CreateDbConnection();
                conn.Open();
                can = conn.ProcessID > 0;
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "데이터베이스 연결실패", ex.Message, true);
            }
            return can;
        }



        #region 드라이브 용량계산
        private DriveInfo ImageSaveDrive = null;
        private DriveInfo GetSaveImageDrive()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (this.이미지저장경로.StartsWith(drive.Name))
                {
                    //Debug.WriteLine(drive.Name, drive.VolumeLabel);
                    this.ImageSaveDrive = drive;
                    return this.ImageSaveDrive;
                }
            }
            if (drives.Length > 0)
                this.ImageSaveDrive = drives[0];

            return this.ImageSaveDrive;
        }

        public Int32 SaveImageDriveFreeSpace()
        {
            DriveInfo drive = this.GetSaveImageDrive();
            double FreeSpace = drive.AvailableFreeSpace / (double)drive.TotalSize * 100;
            return Convert.ToInt32(FreeSpace);
        }
        #endregion
    }
}