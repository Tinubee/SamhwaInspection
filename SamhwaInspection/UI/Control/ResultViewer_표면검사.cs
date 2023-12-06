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
using static SamhwaInspection.MainForm;
using MvCamCtrl.NET;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using DevExpress.CodeParser.Diagnostics;

namespace SamhwaInspection.UI.Control
{
    public partial class ResultViewer_표면검사 : DevExpress.XtraEditors.XtraUserControl
    {
        public ResultViewer_표면검사()
        {
            InitializeComponent();
        }

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

        public 검사면 검사면;

        public void Init(검사면 검사면)
        {
            this.검사면 = 검사면;
            //Viewer와 Tool 연결
            if (검사면 == 검사면.앞면)
            {
                vmControl_Render1.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사앞), 0);
                vmControl_Render2.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사앞), 1);
                vmControl_Render3.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사앞), 2);
                vmControl_Render4.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사앞), 3);
                vmControl_Render5.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사앞), 4);
                vmControl_Render6.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사앞), 5);
            }
            else
            {
                vmControl_Render1.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 0);
                vmControl_Render2.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 1);
                vmControl_Render3.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 2);
                vmControl_Render4.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 3);
                vmControl_Render5.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 4);
                vmControl_Render6.Init2(Global.비전마스터구동.GetItem(Flow구분.표면검사뒤), 5);
            }

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

        public void 최종결과정보생성(Mat img1, bool result)
        {

            if (result)
            {
                Global.환경설정.현재결과상태 = 결과구분.OK;
                Global.환경설정.양품갯수 += 1;
                if (Global.환경설정.사진저장OK)
                {
                    img1.SaveImage(Path.Combine(Global.환경설정.OK이미지Cam1폴더경로, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")) + ".png");
                }
            }
            else
            {
                Global.환경설정.현재결과상태 = 결과구분.NG;
                Global.환경설정.불량갯수 += 1;
                if (Global.환경설정.사진저장NG)
                {
                    img1.SaveImage(Path.Combine(Global.환경설정.NG이미지Cam1폴더경로, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")) + ".png");
                }
            }
            Global.환경설정.결과갱신요청();
        }
    }
}