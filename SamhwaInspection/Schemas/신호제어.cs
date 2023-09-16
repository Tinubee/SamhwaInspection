using SamhwaInspection.Utils;
using OpenCvSharp.Aruco;
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using static SamhwaInspection.Schemas.신호제어;
using DevExpress.ClipboardSource.SpreadsheetML;
using ActUtlType64Lib;
using System.Security.Policy;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using DevExpress.Utils.About;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using MvCamCtrl.NET;

namespace SamhwaInspection.Schemas
{
    public delegate void MyEventHandler();
    // PLC 통신
    [Description("MELSEC Q13UDV")]
    public class 신호제어
    {
        public event Global.BaseEvent 동작상태알림;
        public ActUtlType64 PLC = null;
        public Boolean 작업여부 = false;
        public static String 로그영역 = "장치통신";

        private enum 주소구분 : Int32
        {
            [Address("W0010")]
            모델변경트리거,

            [Address("W0020")]
            결과값요청트리거,

            [Address("W0021")]
            제품확인카메라트리거1,

            //FrontJig 하부표면검사카메라 트리거
            [Address("W0022")]
            하부표면검사카메라트리거1,
            //[Address("W0023")]
            //하부표면검사카메라트리거2,
            //[Address("W0024")]
            //하부표면검사카메라트리거3,
            //[Address("W0025")]
            //하부표면검사카메라트리거4,
            //[Address("W0026")]
            //하부표면검사카메라트리거5,
            //[Address("W0027")]
            //하부표면검사카메라트리거6,

            //FrontJig 상부치수검사카메라 트리거
            [Address("W0028")]
            F상부치수검사카메라트리거1,
            //[Address("W0029")]
            //F상부치수검사카메라트리거2,
            //[Address("W002A")]
            //F상부치수검사카메라트리거3,
            //[Address("W002B")]
            //F상부치수검사카메라트리거4,
            //[Address("W002C")]
            //F상부치수검사카메라트리거5,
            //[Address("W002D")]
            //F상부치수검사카메라트리거6,

            //RearJig 상부치수검사카메라 트리거
            [Address("W002E")]
            R상부치수검사카메라트리거1,
            //[Address("W002F")]
            //R상부치수검사카메라트리거2,
            //[Address("W0030")]
            //R상부치수검사카메라트리거3,
            //[Address("W0031")]
            //R상부치수검사카메라트리거4,
            //[Address("W0032")]
            //R상부치수검사카메라트리거5,
            //[Address("W0033")]
            //R상부치수검사카메라트리거6,

            //FrontJig 상부표면검사카메라 트리거
            [Address("W0034")]
            F상부표면검사카메라트리거1,
            //[Address("W0035")]
            //F상부표면검사카메라트리거2,
            //[Address("W0036")]
            //F상부표면검사카메라트리거3,
            //[Address("W0037")]
            //F상부표면검사카메라트리거4,
            //[Address("W0038")]
            //F상부표면검사카메라트리거5,
            //[Address("W0039")]
            //F상부표면검사카메라트리거6,

            //RearJig 상부표면검사카메라 트리거
            [Address("W003A")]
            R상부표면검사카메라트리거1,
            //[Address("W003B")]
            //R상부표면검사카메라트리거2,
            //[Address("W003C")]
            //R상부표면검사카메라트리거3,
            //[Address("W003D")]
            //R상부표면검사카메라트리거4,
            //[Address("W003E")]
            //R상부표면검사카메라트리거5,
            //[Address("W003F")]
            //R상부표면검사카메라트리거6,

            //변위센서 트리거
            [Address("W0040")]
            상부변위센서확인트리거,
            [Address("W0041")]
            하부변위센서확인트리거,

            //입력
            //[Address("B1000")]
            //Heartbit_PC,
            [Address("B1010")]
            Heartbit_PLC,
            //[Address("B1020")]
            //수동모드,
            [Address("B1030")]
            자동모드,
            [Address("B1040")]
            운전시작,
            [Address("B1017")]
            마스터모드,
            //[Address("B1050")]
            //운전정지,
            //[Address("B1060")]
            //리셋,
            //[Address("B1070")]
            //알람,
        }

        public enum 입력신호
        {
            수동모드 = 0,
            자동모드 = 1,
            현재모델번호 = 2,
            자동운전시작 = 3,
            카메라상태 = 4,
        }

        public enum 출력신호
        {
            모델변경요청 = 0,
            OK신호 = 1,
            NG신호 = 2,
            프로그램구동펄스 = 3,
        }

        public enum 입력주소
        {
            수동모드 = 0x010,
            자동모드 = 0x011,
            현재모델번호 = 0x020,
            자동운전시작 = 0x021,
            카메라상태 = 0x022,
        }

        public enum 출력주소
        {
            모델변경요청 = 0x100,
            OK신호 = 0x000,
            NG신호 = 0x001,
            프로그램구동펄스 = 0x200,
            버퍼 = 0x201,
        }


        //private DotUtlType PLC2 = null;
        private BackgroundWorker cclink_thred;
        private BackgroundWorker cclink_echo;
        private BackgroundWorker send_thred;




        int thred_roop_index = 0;
        private 주소자료 입출자료 = new 주소자료();

        public static bool bit = false;

        public event MyEventHandler CompleteReceive;

        public 신호제어()
        {
        }

        #region Propertys

        //Input Part
        //public int 수동모드여부 { get { return 신호읽기(주소구분.수동모드); } }
        public int 자동모드여부 { get { return 신호읽기(주소구분.자동모드); } }
        public int 운전시작여부 { get { return 신호읽기(주소구분.운전시작); } }

        public int 마스터모드여부 { get { return 신호읽기(주소구분.마스터모드); } }
        //public int 운전정지여부 { get { return 신호읽기(주소구분.운전정지); } }
        //public int 리셋여부 { get { return 신호읽기(주소구분.리셋); } }
        //public int 알람여부 { get { return 신호읽기(주소구분.알람); } }
        //public int Heartbit_PC { get { return 신호읽기(주소구분.Heartbit_PC); } }
        public int Heartbit_PLC { get { return 신호읽기(주소구분.Heartbit_PLC); } }

        //Output Part
        public short 제품확인카메라트리거1 { get { return 신호읽기(주소구분.제품확인카메라트리거1); } set { 신호쓰기(주소구분.제품확인카메라트리거1, value); } }

        public short 하부표면검사카메라트리거1 { get { return 신호읽기(주소구분.하부표면검사카메라트리거1); } set { 신호쓰기(주소구분.하부표면검사카메라트리거1, value); } }

        public short F상부치수검사카메라트리거1 { get { return 신호읽기(주소구분.F상부치수검사카메라트리거1); } set { 신호쓰기(주소구분.F상부치수검사카메라트리거1, value); } }

        public short R상부치수검사카메라트리거1 { get { return 신호읽기(주소구분.R상부치수검사카메라트리거1); } set { 신호쓰기(주소구분.R상부치수검사카메라트리거1, value); } }

        public short 상부변위센서확인트리거 { get { return 신호읽기(주소구분.상부변위센서확인트리거); } set { 신호쓰기(주소구분.상부변위센서확인트리거, value); } }

        public short 하부변위센서확인트리거 { get { return 신호읽기(주소구분.하부변위센서확인트리거); } set { 신호쓰기(주소구분.하부변위센서확인트리거, value); } }

        public short F상부표면검사카메라트리거1 { get { return 신호읽기(주소구분.F상부표면검사카메라트리거1); } set { 신호쓰기(주소구분.F상부표면검사카메라트리거1, value); } }

        public short R상부표면검사카메라트리거1 { get { return 신호읽기(주소구분.R상부표면검사카메라트리거1); } set { 신호쓰기(주소구분.R상부표면검사카메라트리거1, value); } }

        public short 결과값요청트리거 { get { return 신호읽기(주소구분.결과값요청트리거); } set { 신호쓰기(주소구분.결과값요청트리거, value); } }



        #endregion

        private short 신호읽기(주소구분 구분) { return this.입출자료.GetValue(구분); }
        private void 신호쓰기(주소구분 구분, short value) { this.입출자료.SetValue(구분, value); }


        public void Init()
        {
            this.PLC = new ActUtlType64();
            this.PLC.ActLogicalStationNumber = 2;
            int error = 101;
            error = this.PLC.Open();

            if (error != 0)
            {
                Debug.WriteLine("PLC 연결안됨");
                return;
            }

            Debug.WriteLine("PLC 연결됨");
            cclink_thred = new BackgroundWorker();
            cclink_thred.DoWork += cclink_thred_DoWork;
            cclink_thred.RunWorkerAsync(0);
            cclink_echo = new BackgroundWorker();
            cclink_echo.DoWork += cclink_echo_DoWork;
            cclink_echo.RunWorkerAsync(0);
            //Start();
        }

        // 작업을 생성하고 통신 작업 실행
        public void SendValueToPLC(String 주소, Int32 값)
        {
            Task task = CommunicationTask(주소, 값);
            // 필요한 경우 작업을 대기하거나 계속 다른 작업을 수행할 수 있습니다.
        }

        private Task CommunicationTask(String 주소, Int32 값)
        {
            // PLC와 통신하는 코드 작성
            PLC.SetDevice(주소, 값);
            return Task.CompletedTask;
        }

        private void cclink_thred_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //PLC 결과요청 F치수검사, R치수검사 시그널 off
                Global.신호제어.SendValueToPLC("W0028", 0);
                //Global.신호제어.SendValueToPLC("W0034", 0);

                //ThreadRoop flag On
                thred_roop_index = 0;

                while (thred_roop_index == 0)
                {
                    foreach (주소정보 정보 in 입출자료.Values)
                    {
                        PLC.GetDevice2(정보.주소, out 정보.값);

                        if (정보.주소 == "W0010" & 정보.값 > 0)
                        {
                            if (Global.환경설정.선택모델 != 정보.값 - 1)
                            {
                                Debug.WriteLine($"현재선택모델 번호 : {정보.값 - 1}");
                                Global.환경설정.모델변경요청(정보.값 - 1);
                                Global.비전마스터구동.Init();
                            }
                            //Global.신호제어.SendValueToPLC("W0010", 0);
                        }

                        if (정보.주소 == "W0021" & 정보.값 == 1) // 유무검사 트리거신호
                        {
                            int nRet = Global.Cam[0].SetCommandValue("TriggerSoftware");
                            if (CErrorDefine.MV_OK != nRet)
                            {
                                Debug.WriteLine($"Trigger Software Fail! {nRet}");
                            }
                            SendValueToPLC(정보.주소, 0);
                        }

                        // 치수검사 트리거 On일 경우( F지그, R지그 둘 중 하나라도 On이면 실행)
                        if (정보.주소 == "W0028" & 정보.값 == 1)
                        {

                            //조명키고
                            Global.조명제어.TurnOn(조명구분.BACK);

                            SendValueToPLC(정보.주소, 0);

                            //시간체크 함수 Start -> 검사시간
                            Global.tactTimeChecker.Start();

                            Global.그랩제어[0].ProductIndex = ProductIndex.PRODUCT_INDEX1;

                            //카메라 Software Trig날림(영상찍기 시작)
                            Global.그랩제어[0].SoftTrig();
                        }
                        if ((정보.주소 == "W0040") && 정보.값 == 1) //상부 평탄도 검사 데이터 트리거
                        {
                            Global.bFlatnessData = true;
                            //\0050~W0061 상부 평탄도 데이터
                            Debug.Write("상부 평탄도 데이터 획득 시작");
                            GetFlatnessData(0x50);
                            SendValueToPLC(정보.주소, 0);
                            Debug.Write("상부 평탄도 데이터 획득 종료");
                        }
                        if ((정보.주소 == "W0041") && 정보.값 == 1) //하부 평탄도 검사 데이터 트리거
                        {
                            //\0062~W0072 하부 평탄도 데이터
                            Debug.Write("하부 평탄도 데이터 획득 시작");
                            GetFlatnessData(0x62);
                            SendValueToPLC(정보.주소, 0);
                            Debug.Write("하부 평탄도 데이터 획득 종료");
                            Global.bFlatnessData = false;
                        }
                    }
                    OnCompleteReceive(EventArgs.Empty);
                    Thread.Sleep(2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[" + MethodBase.GetCurrentMethod().Name + "]" + ex.ToString());
            }
        }
        private void GetFlatnessData(int startAddress) //평탄도 데이터 가져오기.
        {
            int Count = 18;
            for (int i = 0; i < Count; i++)
            {
                string address = "W" + (startAddress + i).ToString("X4");
                if (startAddress == 0x50) PLC.GetDevice2(address, out Global.topFlatnessData[i]);
                if (startAddress == 0x62) PLC.GetDevice2(address, out Global.bottomFlatnessData[i]);
            }

            for (int i = 0; i < Count; i++)
            {
                string address = "W" + (startAddress + i).ToString("X4");
                SendValueToPLC(address, 0);
            }
        }

        protected virtual void OnCompleteReceive(EventArgs e)
        {
            this.CompleteReceive?.Invoke();
        }

        public static Int32 ConvertBooleanToInt32(Boolean value)
        {
            return value ? 1 : 0;
        }

        private void cclink_echo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    bit = !bit;
                    Global.신호제어.SendValueToPLC("B1000", ConvertBooleanToInt32(bit));
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[" + MethodBase.GetCurrentMethod().Name + "]" + ex.ToString());
            }
        }

        public void Close()
        {
            thred_roop_index = 1;
            if (PLC == null) return;
            PLC.Close();
        }

        private class 주소정보
        {
            public String 주소 = String.Empty;
            public Boolean 변경 = false;
            public short 값 = 0;

            public 주소정보(주소구분 구분)
            {
                AddressAttribute a = IvmUtils.Utils.GetAttribute<AddressAttribute>(구분);
                this.주소 = a.Address;
                this.변경 = false;
                this.값 = 0;
            }
        }

        private class 주소자료 : Dictionary<주소구분, 주소정보>
        {
            public String[] 주소목록;

            public 주소자료()
            {
                List<String> 주소 = new List<String>();
                foreach (주소구분 구분 in typeof(주소구분).GetEnumValues())
                {
                    주소정보 정보 = new 주소정보(구분);

                    this.Add(구분, 정보);
                    주소.Add(정보.주소);
                }
                this.주소목록 = 주소.ToArray();
            }

            public String Address(주소구분 구분)
            {
                if (!this.ContainsKey(구분)) return String.Empty;
                return this[구분].주소;
            }

            public short GetValue(주소구분 구분)
            {
                if (!this.ContainsKey(구분)) return 501;
                return this[구분].값;
            }

            public bool SetValue(주소구분 구분, short value)
            {
                if (!this.ContainsKey(구분)) return false;
                this[구분].값 = value;
                return true;
            }
        }


        #region AddressAttribute 영역
        public class AddressAttribute : Attribute
        {
            public String Address = String.Empty;
            public Int32 Delay = 0;

            public AddressAttribute(String address)
            {
                this.Address = address;
            }
            public AddressAttribute(String address, Int32 delay)
            {
                this.Address = address;
                this.Delay = delay;
            }
        }
        #endregion
    }
}