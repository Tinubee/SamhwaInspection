using GraphicsSetModuleCs;
using ImageSourceModuleCs;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using OpenCvSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VM.Core;
using VM.PlatformSDKCS;
using ShellModuleCs;
using DevExpress.Utils.Extensions;
using GlobalVariableModuleCs;
using DataSetModuleCs;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.CodeParser.Diagnostics;
using SaveTextModuleCs;
using System.Windows.Shapes;
using Microsoft.VisualBasic.Logging;

namespace SamhwaInspection.Schemas
{
    public enum Flow구분
    {
        Flow1,
        Flow2,
        Flow3,
        Flow4,
        Flow5,
        Flow6,
        유무검사,
        표면검사앞1,
        표면검사앞2,
        표면검사앞3,
        표면검사앞4,
        표면검사앞5,
        표면검사앞6,
    }

    public class 비전마스터구동 : List<비전마스터플로우>
    {
        public 비전마스터구동() { }
        public VmGlobals 글로벌변수제어 = new VmGlobals();
        public void Save() => VmSolution.Save();
        public void Init()
        {
            base.Clear(); //모델변경시 기존데이터 clear
            string lastModelSolutionPath = Global.모델자료[Global.환경설정.선택모델].솔루션파일저장경로;
            VmSolution.Load(lastModelSolutionPath, null); //VM Solution 불러오기
            글로벌변수제어.Init();
            VmSolution.Instance.DisableModulesCallback();
            for (int i = 0; i < 13; i++)
            {
                if (i > 5) //6,7,8~~
                {
                    string plcAddress = string.Empty;
                    if (i == 6) plcAddress = $"W0015";
                    base.Add(new 비전마스터플로우((Flow구분)i, plcAddress));
                }
                else //0,1,2,3,4,5
                {
                    base.Add(new 비전마스터플로우((Flow구분)i, $"W000{i}"));
                }

            }

            if (Global.mainForm != null)
                Global.mainForm.DisplaySetting(Global.모델자료.선택모델.모델번호);
        }

        public new void Add(비전마스터플로우 툴)
        {
            base.Add(툴);
        }

        public 비전마스터플로우 GetItem(Flow구분 구분)
        {
            foreach (비전마스터플로우 플로우 in this)
            {
                if (플로우.구분 == 구분) return 플로우;
            }
            return null;
        }
    }

    public class 비전마스터플로우
    {
        public Boolean 치수검사결과;
        public Boolean 표면검사결과;
        public Boolean 유무검사결과;
        public Flow구분 구분;
        public Boolean 결과업데이트완료;
        public String PLC결과어드레스;

        public VmProcedure Procedure;
        public ImageSourceModuleTool InputModuleTool;
        public GraphicsSetModuleTool graphicsSetModuleTool;

        //public List<GraphicsSetModuleTool> graphicsSetModuleTool_List = new List<GraphicsSetModuleTool>();
        //public List<ImageSourceModuleTool> InputModuleTool_List = new List<ImageSourceModuleTool>();
        //public List<ShellModuleTool> ShellModuleTool_List = new List<ShellModuleTool>();

        public delegate void InspectionFinished(GraphicsSetModuleTool graphicTool);
        public ShellModuleTool ShellModuleTool;
        public ShellResult shellResult;

        public GlobalVariableModuleTool GlobalVariableModuleTool;
        public DataSetModuleTool DataSetModuleTool;

        public List<IMVSGroup> Slot20PointGroupMouduleTool = new List<IMVSGroup>();
        public List<IMVSGroup> Slot200PointGroupMouduleTool = new List<IMVSGroup>();

        public SaveTextModuleTool SaveTextModuleTool;
        public SaveTextModuleTool SaveTextMasterModuleTool;

        public 비전마스터플로우(Flow구분 구분, String plcAddress)
        {
            this.구분 = 구분;
            this.Init();
            if (this.graphicsSetModuleTool != null)
                this.graphicsSetModuleTool.EnableResultCallback();

            if (this.ShellModuleTool != null)
                this.ShellModuleTool.EnableResultCallback();

            if (this.InputModuleTool != null)
                this.InputModuleTool.ModuParams.ImageSourceType = ImageSourceParam.ImageSourceTypeEnum.SDK;

            this.치수검사결과 = false;
            this.표면검사결과 = false;
            this.유무검사결과 = false;

            this.결과업데이트완료 = false;
            if (plcAddress != string.Empty)
                this.PLC결과어드레스 = plcAddress;
        }
        public void Init()
        {
            this.Procedure = VmSolution.Instance[this.구분.ToString()] as VmProcedure;
            if (Procedure != null)
            {
                this.InputModuleTool = this.Procedure["originImage"] as ImageSourceModuleTool;
                this.graphicsSetModuleTool = this.Procedure["resultImage"] as GraphicsSetModuleTool;
                this.ShellModuleTool = this.Procedure["AllResult"] as ShellModuleTool;
                for (int lop = 1; lop < 5; lop++)
                {
                    this.Slot20PointGroupMouduleTool.Add(this.Procedure[$"Slot{lop}_20Point"] as IMVSGroup);
                    this.Slot200PointGroupMouduleTool.Add(this.Procedure[$"Slot{lop}_200Point"] as IMVSGroup);
                }

                this.GlobalVariableModuleTool = VmSolution.Instance["Global Variable1"] as GlobalVariableModuleTool;
                this.DataSetModuleTool = this.Procedure["inputData"] as DataSetModuleTool;
                this.SaveTextModuleTool = this.Procedure["Save Text1"] as SaveTextModuleTool;
                this.SaveTextMasterModuleTool = this.Procedure["마스터데이터"] as SaveTextModuleTool;

                데이터저장경로설정(Global.환경설정.자료저장경로);
            }
        }

        public void 데이터저장경로설정(string path)
        {
            string dataPath = System.IO.Path.Combine(path, "InspectData");

            if (this.SaveTextModuleTool != null)
                this.SaveTextModuleTool.ModuParams.Path = dataPath;

            if (this.SaveTextMasterModuleTool != null)
                this.SaveTextMasterModuleTool.ModuParams.Path = System.IO.Path.Combine(dataPath, "Master");
        }


        public void SetFlatnessData()
        {
            int pieces = 6;
            int chunkSize = Global.topFlatnessData.Length / pieces;

            string[] topFlatnessData = new string[pieces];
            string[] bottomFlatnessData = new string[pieces];

            for (int i = 0; i < pieces; i++)
            {
                short[] chunkTop = Global.topFlatnessData.Skip(i * chunkSize).Take(chunkSize).ToArray();
                short[] chunkBottom = Global.bottomFlatnessData.Skip(i * chunkSize).Take(chunkSize).ToArray();
                topFlatnessData[i] = string.Join(",", chunkTop);
                bottomFlatnessData[i] = string.Join(",", chunkBottom);
            }

            if (GlobalVariableModuleTool != null)
            {
                GlobalVariableModuleTool.SetGlobalVar("topFlatness_Value", topFlatnessData[(int)구분]);
                GlobalVariableModuleTool.SetGlobalVar("bottomFlatness_Value", bottomFlatnessData[(int)구분]);
            }
        }
        public void 결과정보생성(bool result)
        {
            if (result)
            {
                Global.환경설정.현재결과상태 = 결과구분.OK;
                Global.환경설정.양품갯수 += 1;
                //if (Global.환경설정.사진저장OK)
                //{
                //    img1.SaveImage(System.IO.Path.Combine(Global.환경설정.OK이미지Cam1폴더경로, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")) + ".png");
                //}
            }
            else
            {
                Global.환경설정.현재결과상태 = 결과구분.NG;
                Global.환경설정.불량갯수 += 1;
                //if (Global.환경설정.사진저장NG)
                //{
                //    img1.SaveImage(System.IO.Path.Combine(Global.환경설정.NG이미지Cam1폴더경로, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")) + ".png");
                //}
            }
            Global.환경설정.결과갱신요청();
        }
        public Boolean 표면검사(Mat mat)
        {
            this.표면검사결과 = false;
            this.결과업데이트완료 = false;
            if (this.InputModuleTool != null)
            {
                this.InputModuleTool.SetImageData(MatToImageBaseData(mat));
                this.Procedure.Run();
                if ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])this.ShellModuleTool.Outputs[6].Value != null)
                {
                    String resultString = this.ShellModuleTool == null ? "NG" : ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])this.ShellModuleTool.Outputs[6].Value)[0].strValue;
                    this.표면검사결과 = resultString == "OK" ? true : false;
                }

                int checkFlow = (int)this.구분 - 7;

                string plcAdress = Global.비전마스터구동.GetItem((Flow구분)checkFlow).PLC결과어드레스;

                if (Global.비전마스터구동.GetItem((Flow구분)checkFlow).치수검사결과 && this.표면검사결과) // 둘다 OK
                {
                    Global.신호제어.PLC.SetDevice2(plcAdress, 1);
                    if (plcAdress == "W0005" && Global.모델자료[Global.환경설정.선택모델].디스플레이개수 == 6)
                        Global.신호제어.SendValueToPLC("W0020", 0);

                    결과정보생성(true);
                }
                else
                {
                    Global.신호제어.PLC.SetDevice2(plcAdress, 2);
                    if (plcAdress == "W0005" && Global.모델자료[Global.환경설정.선택모델].디스플레이개수 == 6)
                        Global.신호제어.SendValueToPLC("W0020", 0);

                    결과정보생성(false);
                }
            }
            return false;
        }

        public Boolean 유무검사(Mat mat)
        {
            this.유무검사결과 = false;
            this.결과업데이트완료 = false;
            try
            {
                if (this.InputModuleTool != null)
                {
                    this.InputModuleTool.SetImageData(MatToImageBaseData(mat));
                    this.Procedure.Run();
                    //Debug.WriteLine("공트레이 제품유무검사완료");
                    String resultString = this.ShellModuleTool == null ? "NG" : (ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])this.ShellModuleTool.Outputs[6].Value == null ? "NG" : ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])this.ShellModuleTool.Outputs[6].Value)[0].strValue;

                    if (resultString == "NG")
                    {
                        this.유무검사결과 = false;
                        //Global.그랩제어.GetItem(CameraType.Cam02).검사결과 = false;
                        Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 2);
                    }
                    else
                    {
                        this.유무검사결과 = true;
                        //Global.그랩제어.GetItem(CameraType.Cam02).검사결과 = true;
                        Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 1);
                    }
                }

                return this.유무검사결과;
            }
            catch (Exception ee)
            {
                Debug.WriteLine(ee.Message);
                Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 2);
                return false;
            }

        }

        public void 슬롯부20Point검사설정()
        {
            for (int lop = 0; lop < this.Slot20PointGroupMouduleTool.Count; lop++)
            {
                if (this.Slot20PointGroupMouduleTool[lop] != null)
                    this.Slot20PointGroupMouduleTool[lop].IsForbidden = !Global.환경설정.슬롯부20Point검사;
            }

        }

        public void 슬롯부200Point검사설정()
        {
            for (int lop = 0; lop < this.Slot200PointGroupMouduleTool.Count; lop++)
            {
                if (this.Slot200PointGroupMouduleTool[lop] != null)
                    this.Slot200PointGroupMouduleTool[lop].IsForbidden = !Global.환경설정.슬롯부200Point검사;
            }

        }

        private void 지그위치체크()
        {
            if (this.구분 == Flow구분.Flow1)
            {
                if (Global.신호제어.Front지그 == 1)
                {
                    this.GlobalVariableModuleTool.SetGlobalVar("Front지그", "1");
                    this.GlobalVariableModuleTool.SetGlobalVar("Rear지그", "0");
                }
                else if (Global.신호제어.Rear지그 == 1)
                {
                    this.GlobalVariableModuleTool.SetGlobalVar("Front지그", "0");
                    this.GlobalVariableModuleTool.SetGlobalVar("Rear지그", "1");
                }
            }

        }

        private void 마스터모드체크()
        {
            if (this.구분 == Flow구분.Flow1)
            {
                if (Global.신호제어.마스터모드여부 == 1)
                    this.GlobalVariableModuleTool.SetGlobalVar("마스터모드", "1");
                else
                    this.GlobalVariableModuleTool.SetGlobalVar("마스터모드", "0");
            }
        }

        public Boolean 치수검사(Mat mat)
        {
            this.치수검사결과 = false;
            this.결과업데이트완료 = false;
            if (this.InputModuleTool != null)
            {
                지그위치체크();
                마스터모드체크();
                SetFlatnessData(); //평탄도 데이터 셋팅.
                this.InputModuleTool.SetImageData(MatToImageBaseData(mat));
                this.Procedure.Run();
                String resultString = this.ShellModuleTool == null ? "NG" : ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])this.ShellModuleTool.Outputs[6].Value)[0].strValue;
                this.치수검사결과 = resultString == "OK" ? true : false;

                if (Global.신호제어.마스터모드여부 == 1)
                {
                    if (this.치수검사결과) Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 1);
                    else Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 2);

                    Global.신호제어.SendValueToPLC("W0020", 0);
                    return this.치수검사결과;
                }
                return this.치수검사결과;
            }
            return false;
        }

        private ImageBaseData MatToImageBaseData(Mat mat)
        {
            if (mat.Channels() != 1) return null;

            ImageBaseData imageBaseData;

            uint dataLen = (uint)(mat.Width * mat.Height * mat.Channels());
            byte[] buffer = new byte[dataLen];
            Marshal.Copy(mat.Ptr(0), buffer, 0, buffer.Length);
            imageBaseData = new ImageBaseData(buffer, dataLen, mat.Width, mat.Height, (int)VMPixelFormat.VM_PIXEL_MONO_08);

            return imageBaseData;
        }
    }
}
