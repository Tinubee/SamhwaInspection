using DevExpress.XtraEditors;
using SamhwaInspection.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using IvLibs.Graphics;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Media.Media3D;
using System.ComponentModel.Design;
using DevExpress.XtraBars.ViewInfo;
using System.IO;
using VM.Core;
using ImageSourceModuleCs;

using VM.PlatformSDKCS;
using GraphicsSetModuleCs;

using OpenCvSharp.ML;
using System.Runtime.InteropServices;
using DevExpress.Drawing.Internal.Fonts.Interop;
using static VMControls.WPF.ModuleResultView;
using DevExpress.XtraRichEdit.Model;
using MvCamCtrl.NET;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using DevExpress.CodeParser.Diagnostics;
using OpenCvSharp.Dnn;
using System.Threading;
using SamhwaInspection.Utils;

namespace SamhwaInspection.UI.Control
{
    public partial class ResultViewer_유무검사 : DevExpress.XtraEditors.XtraUserControl
    {
        public ResultViewer_유무검사()
        {
            InitializeComponent();
        }

        private MvsCameraType 카메라1 = MvsCameraType.Camera2;
        //private delegate void 이미지그랩완료보고대리자(MvsAcquisitionData Data);

        //private MvsCam cam;
        private Boolean isCompleted_Camera1 = false;
        Thread m_hReceiveThread = null;
        public Bitmap tempBitmap;
        public Mat Page1Image;
        public Mat Page2Image;
        public Mat mergedImage;

        public Mat Image;

        public void Init()
        {
            //Viewer와 Tool 연결
            vmControl_Render1.Init(Global.비전마스터구동.GetItem(Flow구분.유무검사));

            int nRet = 0;
            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();

            //Task.Run(() => { ReceiveTaskProcess(Global.Cam[0]); });

            nRet = Global.Cam[0].StartGrabbing();

            if (CErrorDefine.MV_OK != nRet)
            {
                //m_bGrabbing = false;
                //m_hReceiveThread.Join();
                Debug.WriteLine($"Start Grabbing Fail! {nRet}");
                //return;
            }
            //}
            #region 검사결과 설정
            this.myGridView1.OptionsBehavior.Editable = false;
            this.myGridView1.OptionsView.ShowAutoFilterRow = false;
            this.myGridView1.OptionsView.ShowFooter = false;
            this.myGridView1.CustomDrawCell += MyGridView1_CustomDrawCell;
            this.검사목록BindingSource.DataSource = Global.모델자료.선택모델.검사목록;
            #endregion
        }

        private void MyGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.FieldName != this.col판정.FieldName && e.Column.FieldName != this.col측정.FieldName) return;
            GridView view = sender as GridView;
            if (view == null) return;
            검사정보 정보 = view.GetRow(e.RowHandle) as 검사정보;
            if (정보 == null) return;
            e.Appearance.ForeColor = 환경설정.ResultColor(정보.판정);
        }

        private Mat 자동검사(Mat image, Flow구분 구분)
        {
            //List<검사정보> 수동검사목록 = Global.모델자료.선택모델.선택카메라검사목록(index);
            Mat mat = image;
            bool result = false;

            result = Global.비전마스터구동.GetItem(구분).Run(mat);

            //중간 시간체크

            결과정보생성(mat, result);
            Global.tactTimeChecker.Check($"{구분.ToString()} Run 완료");
            return mat;
        }

        private void DataSourceBind()
        {
            if (Global.모델자료.선택모델 == null)
            {
                //this.viewer1.Canvas.ClearGraphics();
                //this.viewer2.Canvas.ClearGraphics();
                this.myGridControl1.DataSource = null;
                return;
            }
            this.myGridControl1.DataSource = Global.모델자료.선택모델.검사목록;
            this.myGridControl1.RefreshDataSource();
            Debug.WriteLine("데이터리프레시완료");
        }

        public void ReceiveThreadProcess()
        {
            while (Global.Grabbing[0])
            {
                CFrameout pcFrameInfo = new CFrameout();
                CDisplayFrameInfo pcDisplayInfo = new CDisplayFrameInfo();
                CPixelConvertParam pcConvertParam = new CPixelConvertParam();
                Object BufForDriverLock = new Object();
                CImage m_pcImgForDriver;        // 이미지 정보
                CFrameSpecInfo m_pcImgSpecInfo; // 이미지 워터마크 정보
                int nRet = CErrorDefine.MV_OK;

                nRet = Global.Cam[0].GetImageBuffer(ref pcFrameInfo, 1000);

                if (nRet == CErrorDefine.MV_OK)
                {
                    lock (BufForDriverLock)
                    {
                        m_pcImgForDriver = pcFrameInfo.Image.Clone() as CImage;
                        m_pcImgSpecInfo = pcFrameInfo.FrameSpec;

                        pcConvertParam.InImage = pcFrameInfo.Image;
                        if (PixelFormat.Format8bppIndexed == Global.Bitmap[0].PixelFormat)
                        {
                            pcConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;
                            Global.Cam[0].ConvertPixelType(ref pcConvertParam);
                        }
                        else
                        {
                            pcConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_BGR8_Packed;
                            Global.Cam[0].ConvertPixelType(ref pcConvertParam);
                        }
                        BitmapData m_pcBitmapData = Global.Bitmap[0].LockBits(new Rectangle(0, 0, pcConvertParam.InImage.Width, pcConvertParam.InImage.Height), ImageLockMode.ReadWrite, Global.Bitmap[0].PixelFormat);
                        Marshal.Copy(pcConvertParam.OutImage.ImageData, 0, m_pcBitmapData.Scan0, (Int32)pcConvertParam.OutImage.ImageData.Length);
                        Global.Bitmap[0].UnlockBits(m_pcBitmapData);
                    }

                    Global.Cam[0].DisplayOneFrame(ref pcDisplayInfo);
                    Global.Cam[0].FreeImageBuffer(ref pcFrameInfo);

                    Mat mat = BitmapConverter.ToMat(Global.Bitmap[0]);
                    bool result = false;
                    result = Global.비전마스터구동.GetItem(Flow구분.유무검사).유무검사(mat);
                    //Global.Grabbing[0] = false;
                }
            }
        }

        private void Paint_camImage(MvsAcquisitionData Data)
        {
            //try
            //{
            //    if (Data.BmpImage == null) return;
            //    if (this.InvokeRequired)
            //    {
            //        this.Invoke(new 이미지그랩완료보고대리자(Paint_camImage), new object[] { Data });
            //        return;
            //    }

            //    if (Data.Camera == 카메라1)
            //    {
            //        //자동검사(Image, Flow구분.유무검사);

            //        Debug.WriteLine("유무검사 검사완료");

            //        Global.tactTimeChecker.Stop();
            //    }

            //    //모든 검사가 끝나면 실행. 여기서는 아직 카메라 1개만 구현되어 있으므로 아래와 같음.
            //    if (isCompleted_Camera1)
            //    {
            //        //this.DataSourceBind();

            //        //State에 결과 표시
            //        //this.myGridControl1.DataSource = Global.모델자료.선택모델.검사목록;
            //        //결과정보생성(Data.MatImage);
            //        //              결과정보생성(Global.환경설정.resultMatImage_cam1);
            //        isCompleted_Camera1 = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}
        }

        private void 결과정보생성(Mat img1, bool result)
        {
            //검사결과 결과 = new 검사결과(Global.모델자료.선택모델.검사목록);
            //Debug.WriteLine($"{결과.최종결과}", 결과.최종결과);
            //Debug.WriteLine($"{결과.검사일시}", 결과.검사일시);
            //Debug.WriteLine($"{결과.모델번호}", 결과.모델번호);


            if (result)
            {
                Global.환경설정.현재결과상태 = 결과구분.OK;
                Global.환경설정.양품갯수 += 1;
                //Global.신호제어.SendResultSignal(true);
                if (Global.환경설정.사진저장OK)
                {
                    img1.SaveImage(Path.Combine(Global.환경설정.OK이미지Cam1폴더경로, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")) + ".png");
                }
            }
            else
            {
                Global.환경설정.현재결과상태 = 결과구분.NG;
                Global.환경설정.불량갯수 += 1;
                //Global.신호제어.SendResultSignal(false);
                if (Global.환경설정.사진저장NG)
                {
                    img1.SaveImage(Path.Combine(Global.환경설정.NG이미지Cam1폴더경로, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")) + ".png");
                }
            }

            // 검사자료 GridView DataSource에 추가(DB에서 읽어와서 표시하기에는 검사 -> 저장 -> select 하는 시간이 길어서 이전 검사결과까지만 읽어지므로 해당 코드 추가)


            //여기 DB업로드
            //결과.AddToDb();

            //Global.검사자료.AddResult(결과);
            Global.환경설정.결과갱신요청();
            //Global.검사자료.AddResult(결과);
            //결과 = null;
        }
    }
}