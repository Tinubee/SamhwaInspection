using SamhwaInspection.Schemas;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using IvmUtils;
using System.Security.RightsManagement;
using SamhwaInspection.Utils;
using MvCamCtrl.NET;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using SamhwaInspection.UI.Form;

namespace SamhwaInspection
{
    public enum 동작구분
    {
        Live = 0,
        LocalTest = 2
    }

    public static class Global
    {
        private const String 로그영역 = "프로그램";
        public static event EventHandler<Boolean> Initialized;
        public static 그랩제어 그랩제어;
        public static 환경설정 환경설정;
        public static 유저자료 유저자료;
        public static 신호제어 신호제어;
        public static LightControl 조명제어;
        //public static 로그자료 로그자료;
        public static 모델자료 모델자료;
        public static 검사도구모음 검사도구모음;
        public static 비전마스터구동 비전마스터구동;
        public static MainForm mainForm;
        public static 검사자료 검사자료;

        public delegate void BaseEvent();

        public static short[] topFlatnessData = new short[18];
        public static short[] bottomFlatnessData = new short[18];
        public static bool bFlatnessData = false;

        public static String GetGuid()
        {
            Assembly assembly = typeof(Program).Assembly;
            GuidAttribute attribute = assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0] as GuidAttribute;
            return attribute.Value;
        }

        public static Boolean Init()
        {
            Debug.WriteLine(GetGuid(), "Process GUID");
            try
            {
                그랩제어 = new 그랩제어();
                Debug.WriteLine("Global 카메라 제어 클래스 생성");
                환경설정 = new 환경설정();
                Debug.WriteLine("Global 환경설정 클래스 생성");
                Global.정보로그(로그영역, "시작", "프로그램을 시작합니다.", false);
                유저자료 = new 유저자료();
                Debug.WriteLine("Global 유저자료 클래스 생성");
                모델자료 = new 모델자료();
                Debug.WriteLine("Global 모델자료 클래스 생성");
                신호제어 = new 신호제어();
                Debug.WriteLine("Global 신호제어 클래스 생성");
                조명제어 = new LightControl();
                Debug.WriteLine("Global 조명제어 클래스 생성");
                검사도구모음 = new 검사도구모음();
                Debug.WriteLine("Global 검사도구모음 클래스 생성");
                비전마스터구동 = new 비전마스터구동();
                Debug.WriteLine("Global 비전마스터구동 클래스 생성");
                검사자료 = new 검사자료();
                Debug.WriteLine("Global 검사자료 클래스 생성");

                //그랩제어.Init();
                //Debug.WriteLine("카메라 제어 클래스 Init완료");
                환경설정.Init();
                Debug.WriteLine("환경설정 클래스 Init완료");
                //로그자료.Init();
                //Debug.WriteLine("로그자료 클래스 Init완료");
                유저자료.Init();
                Debug.WriteLine("유저자료 클래스 Init완료");
                모델자료.Init();
                Debug.WriteLine("모델자료 클래스 Init완료");
                //신호제어.Init();
                //Debug.WriteLine("신호제어 클래스 Init완료");
                //조명제어.Init();
                //Debug.WriteLine("조명제어 클래스 Init완료");
                //검사자료.Init();
                //Debug.WriteLine("검사자료 클래스 Init완료!");
                비전마스터구동.Init();
                Debug.WriteLine("비전마스터구동 클래스 Init완료!");
                Debug.WriteLine("시스템을 초기화 합니다.");
                Initialized?.Invoke(null, true);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                로그정보 로그 = Global.오류로그(로그영역, "초기화 실패", "시스템 초기화에 실패하였습니다.\n" + ex.Message, false);
                Debug.WriteLine("시스템 초기화에 실패하였습니다.");
            }
            Initialized.Invoke(null, false);
            return false;
        }

        public static void Start()
        {
            //if (환경설정.동작구분 != 동작구분.Live) return;
            //신호제어?.Start();
            //장치통신?.Start();
        }

        public static Boolean Close()
        {
            Global.정보로그(로그영역, "종료", "프로그램을 종료합니다.", false);
            try
            {
                그랩제어?.Close();
                Debug.WriteLine("그랩Close");
                조명제어?.Close();
                Debug.WriteLine("조명Close");
                모델자료?.Close();
                Debug.WriteLine("모델자료Close");
                검사자료?.Close();
                Debug.WriteLine("검사자료Close");
                신호제어?.Close();
                Debug.WriteLine("신호Close");
                환경설정?.Close();
                Debug.WriteLine("환경설정Close");
                //로그자료?.Close();
                //Debug.WriteLine("로그Close");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("프로그램 종료 중 오류가 발생하였습니다.\n" + ex.Message);
                return false;
            }
        }
        public static Form MainForm()
        {
            if (Application.OpenForms.Count < 1) return null;
            Form f = Application.OpenForms[0];
            if (f.IsDisposed) return null;
            return f;
        }

        #region 로그 / Alert
        private static AlertControl 알림화면 = new AlertControl() { PopupLocation = AlertControl.PopupLocations.CenterForm };
        private delegate void ShowMessageDelegate(Form Owner, 로그정보 로그);
        private static void ShowMessage(Form Owner, 로그정보 로그)
        {
            if (Owner != null && Owner.InvokeRequired)
            {
                Owner.Invoke(new ShowMessageDelegate(ShowMessage), new object[] { Owner, 로그 });
                return;
            }

            if (로그.구분 == 로그구분.오류)
                알림화면.Show(AlertControl.AlertTypes.Invalid, 로그.제목, 로그.내용, Owner);
            else if (로그.구분 == 로그구분.경고)
                알림화면.Show(AlertControl.AlertTypes.Warning, 로그.제목, 로그.내용, Owner);
            else if (로그.구분 == 로그구분.정보)
                알림화면.Show(AlertControl.AlertTypes.Information, 로그.제목, 로그.내용, Owner);
        }
        public static void ShowMessage(로그정보 로그)
        {
            ShowMessage(MainForm(), 로그);
        }

        public static 로그정보 로그기록(string 영역, 로그구분 구분, string 제목, string 내용)
        {
            try
            {
                //로그정보 로그 = 로그자료.Add(영역, 구분, 제목, 내용);
                Debug.WriteLine($"{IvmUtils.Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss}")}\t{구분.ToString()}\t{영역}\t{제목}\t{내용}");
                //return 로그;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "로그기록 오류");
            }
            return null;
        }

        public static 로그정보 오류로그(string 영역, string 제목, string 내용, bool Show)
        {
            return 오류로그(영역, 제목, 내용, Show ? MainForm() : null);
        }
        public static 로그정보 오류로그(string 영역, string 제목, string 내용, Form Owner)
        {
            로그정보 로그 = 로그기록(영역, 로그구분.오류, 제목, 내용);
            if (로그 != null && Owner != null) ShowMessage(Owner, 로그);
            return 로그;
        }

        public static 로그정보 경고로그(string 영역, string 제목, string 내용, bool Show)
        {
            return 경고로그(영역, 제목, 내용, Show ? MainForm() : null);
        }
        public static 로그정보 경고로그(string 영역, string 제목, string 내용, Form Owner)
        {
            로그정보 로그 = 로그기록(영역, 로그구분.경고, 제목, 내용);
            if (로그 != null && Owner != null) ShowMessage(Owner, 로그);
            return 로그;
        }

        public static 로그정보 정보로그(string 영역, string 제목, string 내용, bool Show)
        {
            return 정보로그(영역, 제목, 내용, Show ? MainForm() : null);
        }
        public static 로그정보 정보로그(string 영역, string 제목, string 내용, Form Owner)
        {
            로그정보 로그 = 로그기록(영역, 로그구분.정보, 제목, 내용);
            if (로그 != null && Owner != null) ShowMessage(Owner, 로그);
            return 로그;
        }
        #endregion
    }
}