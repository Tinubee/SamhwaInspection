﻿using DevExpress.XtraEditors;
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

namespace SamhwaInspection.UI.Control
{
    public partial class ResultViewer_4 : DevExpress.XtraEditors.XtraUserControl
    {
        public ResultViewer_4()
        {
            InitializeComponent();
        }

        private CameraType 카메라1 = CameraType.Camera1;
        //private CameraType 카메라2 = CameraType.Camera2;
        private delegate void 이미지그랩완료보고대리자(AcquisitionData Data);



        private Cam cam1;
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
            //if (cam1 == null)
            //{
            //    IvmUtils.Utils.MessageBox("카메라영역", "카메라 Init 실패", 5);
            //}
            if (cam1 == null)
            {
                cam1 = Global.그랩제어.GetItem(카메라1);
                cam1.AcquisitionFinishedEvent += Paint_camImage;
                this.cam1.Active();
            }


            //Viewer와 Tool 연결
            vmControl_Render1.Init(Global.비전마스터구동.GetItem(Flow구분.Flow1));
            vmControl_Render2.Init(Global.비전마스터구동.GetItem(Flow구분.Flow2));
            vmControl_Render3.Init(Global.비전마스터구동.GetItem(Flow구분.Flow3));
            vmControl_Render4.Init(Global.비전마스터구동.GetItem(Flow구분.Flow4));

            #region 검사결과 설정
            this.myGridView1.OptionsBehavior.Editable = false;
            this.myGridView1.OptionsView.ShowAutoFilterRow = false;
            this.myGridView1.OptionsView.ShowFooter = false;
            this.myGridView1.CustomDrawCell += MyGridView1_CustomDrawCell;
            this.검사목록BindingSource.DataSource = Global.모델자료.선택모델.검사목록;
            #endregion


            #region Mat Global변수 설정(추후 이동)
            height_cam = cam1.height;
            width_cam = cam1.width;

            Page1Image = new Mat(height_cam, width_cam, MatType.CV_8UC1);
            Page2Image = new Mat(height_cam, width_cam, MatType.CV_8UC1);
            mergedImage = new Mat(height_cam * 2, width_cam, MatType.CV_8UC1);

            for (int i = 0; i < roi.Length; i++)
            {
                roi[i] = new Rect(0, i * 20000, width_cam, 25000);
                splitImage[i] = new Mat(25000, width_cam, MatType.CV_8UC1);
            }

            //roi1 = new Rect(0, 0, width_cam, 25000);
            //roi2 = new Rect(0, 20000, width_cam, 25000);
            //roi3 = new Rect(0, 40000, width_cam, 25000);
            //roi4 = new Rect(0, 60000, width_cam, 25000);

            //splitImage1 = new Mat(25000, width_cam, MatType.CV_8UC1);
            //splitImage2 = new Mat(25000, width_cam, MatType.CV_8UC1);
            //splitImage3 = new Mat(25000, width_cam, MatType.CV_8UC1);
            //splitImage4 = new Mat(25000, width_cam, MatType.CV_8UC1);
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
                if (Data.BmpImage == null) return;
                if (this.InvokeRequired)
                {
                    this.Invoke(new 이미지그랩완료보고대리자(Paint_camImage), new object[] { Data });
                    return;
                }
                if (Data.Camera == 카메라1)
                {
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

                    Debug.WriteLine("자동검사 전");
                    if (isGrabCompleted_Page1 & isGrabCompleted_Page2)
                    {
                        isGrabCompleted_Page1 = false;
                        isGrabCompleted_Page2 = false;

                        //조명 끔
                        Global.조명제어.TurnOff(조명구분.BACK);

                        // 이미지 연결
                        Cv2.VConcat(Page1Image, Page2Image, mergedImage);

                        roiAlign = new Rect(9300, 0, 2000, 2 * height_cam);

                        List<Rect> blobs = Global.검사도구모음.FindBlobs2(mergedImage, roiAlign, 100, ThresholdTypes.Binary, SearchMode.WhiteBlob, 470000, 600000);

                        Debug.WriteLine($"Blob 개수 : {blobs.Count}");

                        int blobCount = blobs.Count();

                        for (int lop = 0; lop < blobCount; lop++)
                        {
                            Debug.WriteLine($"Blob Y 크기 : {blobs[lop].Y}");
                            if (blobs[lop].Y < 2000)
                            {
                                roi[lop] = new Rect(0, 0, width_cam, 20000);
                            }
                            else
                            {
                                if (blobs[lop].Y < 15000) roi[0] = new Rect(0, blobs[lop].Y - 2000, width_cam, 20000);
                                else if (blobs[lop].Y < 35000) roi[1] = new Rect(0, blobs[lop].Y - 2000, width_cam, 20000);
                                else if (blobs[lop].Y < 55000) roi[2] = new Rect(0, blobs[lop].Y - 2000, width_cam, 20000);
                                else if (blobs[lop].Y < 75000) roi[3] = new Rect(0, blobs[lop].Y - 2000, width_cam, 20000);
                            }
                        }

                        for (int lop = 0; lop < roi.Length; lop++)
                        {
                            if (roi[lop].Y == 0)
                            {
                                if (lop == 0) roi[lop] = new Rect(0, 2000, width_cam, 20000);
                                if (lop == 1) roi[lop] = new Rect(0, roi[lop - 1].Y + 20000, width_cam, 20000);
                                if (lop == 2) roi[lop] = new Rect(0, roi[lop - 1].Y + 20000, width_cam, 20000);
                                if (lop == 3) roi[lop] = new Rect(0, roi[lop - 1].Y + 20000, width_cam, 20000);
                            }

                            splitImage[lop] = new Mat(mergedImage, roi[lop]);
                        }

                        

                        for (int lop = 0; lop < roi.Length; lop++)
                        {
                            roi[lop].Y = 0;
                        }

                        Debug.WriteLine("자동검사 시작");
                        if (Global.신호제어.마스터모드여부 == 1)
                        {
                            자동검사(splitImage[0], (Flow구분)0);
                        }
                        else
                        {
                            for (int i = 0; i < splitImage.Length; i++)
                            {
                                자동검사(splitImage[i], (Flow구분)i);
                            }
                        }

                        //자동검사(splitImage1, Flow구분.Flow1);
                        //자동검사(splitImage2, Flow구분.Flow2);
                        //자동검사(splitImage3, Flow구분.Flow3);
                        //자동검사(splitImage4, Flow구분.Flow4);

                        isCompleted_Camera1 = true;
                        Debug.WriteLine("카메라1 검사완료");

                        //검사 완료 시간 확인
                        Global.tactTimeChecker.Stop();
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