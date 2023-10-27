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
        표면검사앞,
        표면검사뒤,
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
            for (int i = 0; i < 9; i++)
            {
                if (i > 5) //7,8
                {
                    
                    string plcAddress = string.Empty;
                    if (i == 6) plcAddress = $"W0015";
                    base.Add(new 비전마스터플로우((Flow구분)i, plcAddress));
                }
                else //0,1,2,3,4,5, 6 
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
        public Boolean 결과;
        public Flow구분 구분;
        public Boolean 결과업데이트완료;
        public String PLC결과어드레스;

        public VmProcedure Procedure;
        public ImageSourceModuleTool InputModuleTool;
        public GraphicsSetModuleTool graphicsSetModuleTool;

        public List<GraphicsSetModuleTool> graphicsSetModuleTool_List = new List<GraphicsSetModuleTool>();
        public List<ImageSourceModuleTool> InputModuleTool_List = new List<ImageSourceModuleTool>();
        public List<ShellModuleTool> ShellModuleTool_List = new List<ShellModuleTool>();

        public delegate void InspectionFinished(GraphicsSetModuleTool graphicTool);
        public ShellModuleTool ShellModuleTool;
        public ShellResult shellResult;

        public GlobalVariableModuleTool GlobalVariableModuleTool;
        public DataSetModuleTool DataSetModuleTool;

        public List<IMVSGroup> Slot20PointGroupMouduleTool = new List<IMVSGroup>();
        public List<IMVSGroup> Slot200PointGroupMouduleTool = new List<IMVSGroup>();

        public 비전마스터플로우(Flow구분 구분, String plcAddress)
        {
            this.구분 = 구분;
            this.Init();
            if (this.graphicsSetModuleTool != null) this.graphicsSetModuleTool.EnableResultCallback();
            if (this.ShellModuleTool != null) this.ShellModuleTool.EnableResultCallback();

            for (int lop = 0; lop < graphicsSetModuleTool_List.Count; lop++)
                if (this.graphicsSetModuleTool_List[lop] != null) this.graphicsSetModuleTool_List[lop].EnableResultCallback();

            //슬롯부20Point검사설정();
            //슬롯부200Point검사설정();

            this.결과 = false;
            this.결과업데이트완료 = false;
            if (plcAddress != string.Empty)
                this.PLC결과어드레스 = plcAddress;
        }
        public void Init()
        {
            this.Procedure = VmSolution.Instance[this.구분.ToString()] as VmProcedure;
            if (Procedure != null)
            {
                if (this.구분 == Flow구분.표면검사앞 || this.구분 == Flow구분.표면검사뒤)
                {
                    for (int lop = 0; lop < Global.모델자료[Global.환경설정.선택모델].디스플레이개수; lop++)
                    {
                        this.graphicsSetModuleTool_List.Add(this.Procedure[$"resultImage{lop + 1}"] as GraphicsSetModuleTool);
                        this.InputModuleTool_List.Add(this.Procedure[$"originImage{lop + 1}"] as ImageSourceModuleTool);
                        this.ShellModuleTool_List.Add(this.Procedure[$"AllResult{lop + 1}"] as ShellModuleTool);
                        this.PLC결과어드레스 = this.구분 == Flow구분.표면검사뒤 ? $"W009{lop}" : $"W00A{lop}";
                    }
                }
                else
                {
                    this.InputModuleTool = this.Procedure["originImage"] as ImageSourceModuleTool;
                    this.graphicsSetModuleTool = this.Procedure["resultImage"] as GraphicsSetModuleTool;
                    this.ShellModuleTool = this.Procedure["AllResult"] as ShellModuleTool;
                    for (int lop = 1; lop < 5; lop++)
                    {
                        this.Slot20PointGroupMouduleTool.Add(this.Procedure[$"Slot{lop}_20Point"] as IMVSGroup);
                        this.Slot200PointGroupMouduleTool.Add(this.Procedure[$"Slot{lop}_200Point"] as IMVSGroup);
                    }
                }

                this.GlobalVariableModuleTool = VmSolution.Instance["Global Variable1"] as GlobalVariableModuleTool;
                this.DataSetModuleTool = this.Procedure["inputData"] as DataSetModuleTool;
            }
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

            //Debug.WriteLine($"{구분}_상부 평탄도 데이터 : {topFlatnessData[(int)구분]}");
            //Debug.WriteLine($"{구분}_하부 평탄도 데이터 : {bottomFlatnessData[(int)구분]}");

            if (GlobalVariableModuleTool != null)
            {
                GlobalVariableModuleTool.SetGlobalVar("topFlatness_Value", topFlatnessData[(int)구분]);
                GlobalVariableModuleTool.SetGlobalVar("bottomFlatness_Value", bottomFlatnessData[(int)구분]);
            }
        }

        public Boolean 표면검사(List<Mat> matList)
        {
            this.결과 = false;
            this.결과업데이트완료 = false;
            try
            {
                if (this.InputModuleTool_List != null)
                {
                    for (int lop = 0; lop < matList.Count; lop++)
                        this.InputModuleTool_List[lop].SetImageData(MatToImageBaseData(matList[lop]));
                    //this.InputModuleTool_List[0].SetImageData(MatToImageBaseData(mat));
                    this.Procedure.Run();

                    String resultString = this.ShellModuleTool == null ? "NG" : ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])this.ShellModuleTool.Outputs[6].Value)[0].strValue;

                    if (resultString == "NG")
                    {
                        this.결과 = false;
                        Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 2);
                    }
                    else
                    {
                        this.결과 = true;
                        Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 1);
                    }
                }

                return this.결과;
            }
            catch (Exception ee)
            {
                Debug.WriteLine(ee.Message);
                Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 2);
                return false;
            }

        }

        public Boolean 유무검사(Mat mat)
        {
            this.결과 = false;
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
                        this.결과 = false;
                        Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 2);
                    }
                    else
                    {
                        this.결과 = true;
                        Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 1);
                    }
                }

                return this.결과;
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

        public Boolean Run(Mat mat)
        {
            this.결과 = false;
            this.결과업데이트완료 = false;
            if (this.InputModuleTool != null)
            {
                마스터모드체크();
                SetFlatnessData(); //평탄도 데이터 셋팅.
                this.InputModuleTool.SetImageData(MatToImageBaseData(mat));
                this.Procedure.Run();
                String resultString = this.ShellModuleTool == null ? "NG" : ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])this.ShellModuleTool.Outputs[6].Value)[0].strValue;
                if (resultString == "NG")
                {
                    this.결과 = false;
                    Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 2);

                    if (Global.모델자료[Global.환경설정.선택모델].디스플레이개수 == 4)
                    {
                        if (this.PLC결과어드레스 == "W0003")
                        {
                            //Debug.WriteLine("트리거신호 초기화");
                            Global.신호제어.SendValueToPLC("W0020", 0);
                        }
                    }
                    else
                    {
                        if (this.PLC결과어드레스 == "W0005")
                        {
                            //Debug.WriteLine("트리거신호 초기화");
                            Global.신호제어.SendValueToPLC("W0020", 0);
                        }
                    }
                }
                else
                {
                    this.결과 = true;
                    Global.신호제어.PLC.SetDevice2(this.PLC결과어드레스, 1);
                    if (Global.모델자료[Global.환경설정.선택모델].디스플레이개수 == 4)
                    {
                        if (this.PLC결과어드레스 == "W0003")
                        {
                            //Debug.WriteLine("트리거신호 초기화");
                            Global.신호제어.SendValueToPLC("W0020", 0);
                        }
                    }
                    else
                    {
                        if (this.PLC결과어드레스 == "W0005")
                        {
                            //Debug.WriteLine("트리거신호 초기화");
                            Global.신호제어.SendValueToPLC("W0020", 0);
                        }
                    }
                }
                //Debug.WriteLine($"{this.PLC결과어드레스} 신호 날림");
                //Debug.WriteLine("Process RUN", $"{this.구분}");
                return this.결과;
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
