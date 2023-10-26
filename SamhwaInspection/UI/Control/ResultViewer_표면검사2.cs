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
using static SamhwaInspection.Schemas.EuresysLink;

namespace SamhwaInspection.UI.Control
{
    public partial class ResultViewer_표면검사2 : DevExpress.XtraEditors.XtraUserControl
    {
        public ResultViewer_표면검사2()
        {
            InitializeComponent();
        }

        private CameraType 카메라1 = CameraType.Cam03;
        //private CameraType 카메라2 = CameraType.Camera2;
        private delegate void 이미지그랩완료보고대리자(AcquisitionData Data);
        //private Cam cam2;
        private Boolean isCompleted_Camera1 = false;

        private Boolean isGrabCompleted_Page1;
        private Boolean isGrabCompleted_Page2;

        public Bitmap tempBitmap;
        public Mat Page1Image;
        public Mat Page2Image;
        public Mat mergedImage;

        //public Rect roi1, roi2, roi3, roi4, roiAlign;
        public Rect[] roi = new Rect[4];
        public Rect roiAlign;

        //public Mat splitImage1, splitImage2, splitImage3, splitImage4;
        public Mat[] splitImage = new Mat[4];
        public Mat masterModeImage = new Mat();

        public Int32 height_cam, width_cam;

        public void Init()
        {
            //vmControl_Render1.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 0);
            //vmControl_Render2.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 1);
            //vmControl_Render3.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 2);
            //vmControl_Render4.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 3);
            //vmControl_Render5.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 4);
            //vmControl_Render6.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 5);

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
            return mat;

            //foreach (검사정보 정보 in 수동검사목록)
            //{
            //    int x = (int)정보.rectangle.Left;
            //    int y = (int)정보.rectangle.Top;
            //    int width = (int)정보.rectangle.Width;
            //    int height = (int)정보.rectangle.Height;

            //    Rect rect = new Rect(x, y, width, height);
            //    List<Rect> blobs = Global.검사도구모음.FindBlobs(mat, rect, 128, ThresholdTypes.Binary, SearchMode.BigOne);
            //    Rect largestBlob = Global.검사도구모음.FindLargestBlob(blobs, rect);
            //    Scalar 선색상;
            //    Int32 선굵기;

            //    // 가운데 커다란 홀 기준으로 Calibration함.
            //    정보.측정 = largestBlob.Width * 97.364/1000;

            //    if (정보.측정 < 정보.최소 || 정보.측정 > 정보.최대)
            //    {
            //        정보.판정 = 결과구분.NG;
            //        선색상 = Global.검사도구모음.RED;
            //        선굵기 = 10;
            //    }
            //    else
            //    {
            //        정보.판정 = 결과구분.OK;
            //        선색상 = Global.검사도구모음.GREEN;
            //        선굵기 = 5;
            //    }

            //    Global.검사도구모음.DrawLargestBlob(mat, largestBlob, 선색상, 선굵기);
            //}

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

        private void Paint_camImage(AcquisitionData Data)
        {
            try
            {
                if (Global.모델자료.선택모델.디스플레이개수 != 4) return;
                //Debug.WriteLine("Paint까지 들어옴");
                if (Data.BmpImage == null) return;
                if (this.InvokeRequired)
                {
                    this.Invoke(new 이미지그랩완료보고대리자(Paint_camImage), new object[] { Data });
                    return;
                }
                //Debug.WriteLine("이프문 전");
                if (Data.Camera == 카메라1)
                {
                    // 여기에 6개 분할 로직 추가
                    if (Data.PageIndex == 1)
                    {
                        Page1Image = Data.MatImage;
                        isGrabCompleted_Page1 = true;
                    }
                    if (Data.PageIndex == 2)
                    {
                        Page2Image = Data.MatImage;
                        isGrabCompleted_Page2 = true;
                    }


                    //Debug.WriteLine("자동검사 전");
                    if (isGrabCompleted_Page1 & isGrabCompleted_Page2)
                    {
                        //Global.신호제어.SendValueToPLC("W0020", 0);
                    }
                }

                //모든 검사가 끝나면 실행. 여기서는 아직 카메라 1개만 구현되어 있으므로 아래와 같음.
                if (isCompleted_Camera1)
                {
                    //this.DataSourceBind();

                    //State에 결과 표시
                    //this.myGridControl1.DataSource = Global.모델자료.선택모델.검사목록;
                    //결과정보생성(Data.MatImage);
                    //              결과정보생성(Global.환경설정.resultMatImage_cam1);
                    isCompleted_Camera1 = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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