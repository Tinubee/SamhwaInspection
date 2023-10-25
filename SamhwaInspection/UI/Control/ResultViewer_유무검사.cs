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

        //private MvsCameraType 카메라1 = MvsCameraType.Camera2;
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