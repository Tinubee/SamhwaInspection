using DevExpress.XtraBars;
using IvmUtils;
using IvLibs.Graphics;
using SamhwaInspection.Schemas;
using OpenCvSharp;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using static SamhwaInspection.Schemas.환경설정;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Utils.Svg;
using VM.Core;
using DevExpress.XtraEditors.Camera;
using System.Threading;
using SamhwaInspection.Utils;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using DevExpress.CodeParser.Diagnostics;
using static SamhwaInspection.Utils.MyCamera;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MvCamCtrl.NET;
using System.Drawing;

namespace SamhwaInspection.UI.Control
{
    public partial class Models : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void CompleteAreaClick();
        public event CompleteAreaClick CompleteArea;

        private CameraType 카메라 { get { return (CameraType)this.e카메라선택.EditValue; } }
        private Int32 모델번호 { get { return IvmUtils.Utils.IntValue(this.e모델선택.EditValue); } }
        private Boolean isEditing { get; set; } = false;

        public Models()
        {
            InitializeComponent();
        }

        public void Init()
        {
            #region VisionMaster RenderControl설정
            //this.vmControl_Render1.Init(Global.비전마스터구동.GetItem(Flow구분.Flow1));
            #endregion

            #region 카메라목록 LookUpEdit설정
            EnumToList 카메라목록 = new EnumToList(typeof(CameraType));
            카메라목록.SetLookUpEdit(this.e카메라선택);
            this.e카메라선택.EditValue = CameraType.Camera1;
            this.e카메라선택.EditValueChanged += E카메라선택_EditValueChanged;
            #endregion

            #region 모델자료 GridView2설정
            모델자료BindingSource.DataSource = Global.모델자료;
            this.GridView2.Init(this.barManager1);
            //this.GridView2.AddPopupMemuItem("Test", IvmUtils.Resources.인쇄, ButtonPrintClick);
            this.GridView2.AddDeleteMenuItem(ButtonDeleteClick_GridView2);
            this.GridView2.OptionsBehavior.Editable = true;
            this.GridView2.OptionsView.ShowAutoFilterRow = false;
            this.GridView2.OptionsView.ShowFooter = false;
            //this.GridView2.RowCountChanged += GridView2_RowCountChanged;
            this.GridView2.RowUpdated += GridView2_RowUpdated;

            //가끔가다가 데이터를 변경해도 열이 예전형식에서 안바뀌는 경우 대비
            //MyGridView gridView = GridControl2.MainView as MyGridView;
            //gridView.Columns.Clear();
            //gridView.OptionsBehavior.AutoPopulateColumns = true;
            #endregion

            #region 검사목록 GridView1설정
            this.GridView1.Init(this.barManager1);
            this.GridView1.AddDeleteMenuItem(ButtonDeleteClick_GridView1);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsView.ShowAutoFilterRow = false;
            this.GridView1.OptionsView.ShowFooter = false;
            this.GridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            #endregion

            #region 모델선택 LookUpEdit설정
            Global.환경설정.모델변경알림 += 모델변경알림;
            this.e모델선택.Properties.DataSource = Global.모델자료;
            this.e모델선택.EditValue = Global.환경설정.선택모델;
            this.e모델선택.EditValueChanging += E모델선택_EditValueChanging; ;
            this.e모델선택.EditValueChanged += E모델선택_EditValueChanged;
            #endregion

            #region 버튼이벤트설정
            this.b마스터로드.Click += B마스터로드_Click;
            this.b마스터저장.Click += B마스터저장_Click;
            this.b편집모드.Click += B편집모드_Click;
            this.b모델저장.Click += B모델저장_Click;
            this.b수동검사.Click += B수동검사_Click;
            this.b카메라촬영.Click += B카메라촬영_Click;
            #endregion

            #region VMMainViewControl 설정.

            #endregion

            //Init시 초기 화면 및 데이터 바인딩 설정
            //this.DataSourceBind();
            //마스터이미지출력();
        }
       
        private void GridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            Global.모델자료.모델변경적용();
        }
       

        private void B카메라촬영_Click(object sender, EventArgs e)
        {
            int camNumber = (int)this.카메라;

            if (camNumber == 0) return;

            int nRet = Global.Cam[camNumber - 1].SetCommandValue("TriggerSoftware");
            if (CErrorDefine.MV_OK != nRet)
            {
                Debug.WriteLine($"Trigger Software Fail! {nRet}");
            }
        }

        private void B수동검사_Click(object sender, EventArgs e)
        {
            if (isEditing) isEditing = false;
            //List<검사정보> 검사목록 = Global.모델자료.선택모델.검사목록.ToList(); 
            List<검사정보> 수동검사목록 = Global.모델자료.선택모델.선택카메라검사목록(this.카메라); 
            Mat mat = Cv2.ImRead(Global.모델자료.선택모델.마스터이미지경로(this.카메라));

            bool result = false;

            //result = Global.비전마스터구동.GetItem(Flow구분.Flow1).Run(mat);



                //중간 시간체크

                //결과정보생성(mat);
                //Global.tactTimeChecker.Check($"{구분.ToString()} Run 완료");



                //foreach(검사정보 정보 in 수동검사목록)
                //{
                //    int x = (int)정보.rectangle.Left;
                //    int y = (int)정보.rectangle.Top;
                //    int width = (int)정보.rectangle.Width;
                //    int height = (int)정보.rectangle.Height;

                //    Rect rect = new Rect(x, y, width, height);
                //    List<Rect> blobs = Global.검사도구모음.FindBlobs(mat, rect, 128, ThresholdTypes.Binary, SearchMode.BigOne);
                //    Rect largestBlob = Global.검사도구모음.FindLargestBlob(blobs, rect);
                //    Scalar 선색상;

                //    정보.측정 = largestBlob.Width * 97.364 / 1000;

                //    if (정보.측정 < 정보.최소 || 정보.측정 > 정보.최대)
                //    {

                //        정보.판정 = 결과구분.NG;
                //        선색상 = Global.검사도구모음.RED;
                //    }
                //    else 
                //    { 선색상 = Global.검사도구모음.GREEN;
                //        정보.판정 = 결과구분.OK;

                //    }

                //    Global.검사도구모음.DrawLargestBlob(mat, largestBlob, 선색상, 2);
                //    //DrawBlobs(mat, blobs, new Scalar(0, 0, 255), 3);

                //}
                //DataSourceBind();

            //this.e뷰어.LoadImage(mat);
            Debug.WriteLine($"{카메라} 수동검사 완료");
            //Cv2.ImShow("Test", mat);
        }

        private void DataSourceBind()
        {
            if (Global.모델자료.선택모델 == null || this.카메라 == CameraType.None)
            {
                //this.e뷰어.Canvas.ClearGraphics();
                this.GridControl1.DataSource = null;
                return;
            }

            Object region = null;
            GraphicCollections graphics = Global.모델자료.선택모델.GetRegions(this.카메라);
            //this.e뷰어.Canvas.AddGraphics(graphics, true);
            //this.e뷰어.RefreshImage();
            if (graphics.Count > 0)
                region = graphics[0];
            GridView1.ActiveFilterString = $"[카메라구분] = '{this.카메라}'";
            this.GridControl1.DataSource = Global.모델자료.선택모델.검사목록;
            this.propertyGridControl1.SelectedObject = region;
        }

        private 검사정보 SelectedRegion = null;
        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // 선택한 행의 객체를 가져옵니다.
            GridView view = sender as GridView;
            if (!(view.GetFocusedRow() is 검사정보 selectedItem)) return;

            if (this.SelectedRegion != null)
                this.SelectedRegion.rectangle.PropertyChanged -= RectanglePropertyChanged;

            this.SelectedRegion = selectedItem;
            Global.모델자료.선택모델.SelectAll(this.카메라, false);
            this.SelectedRegion.rectangle.Selected = true;
            this.SelectedRegion.rectangle.PropertyChanged += RectanglePropertyChanged;

            

            this.propertyGridControl1.SelectedObject = selectedItem.rectangle;

            //if (this.e뷰어.Canvas.Image != null) 
            //{
            //this.e뷰어.RefreshImage();
            //}
        }


        private void 검사목록_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.DataSourceBind();
            if (!isEditing)
            {
                //마스터이미지출력();
                return;
            }
        }

        private void RectanglePropertyChanged(object sender, EventArgs e)
        {
            this.propertyGridControl1.RefreshAllProperties();
        }

        private void E카메라선택_EditValueChanged(object sender, EventArgs e)
        {
            //마스터이미지출력();
            this.DataSourceBind();
            if (!isEditing)
            {
                //마스터이미지출력();
                return;
            }
        }

        private void E모델선택_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

            if (Global.환경설정.선택모델 == IvmUtils.Utils.IntValue(e.NewValue)) return;
            if (!IvmUtils.Utils.Confirm("모델을 변경하시겠습니까?"))
            {
                e.Cancel = true;
                return;
            }
            //VmSolution.Load(Global.모델자료[e.NewValue], null);
        }

        private void E모델선택_EditValueChanged(object sender, EventArgs e)
        {
            if (Global.모델자료.선택모델 != null)
                Global.모델자료.선택모델.검사목록.ListChanged -= 검사목록_ListChanged;
            Debug.WriteLine($"{this.모델번호}, {this.카메라}", "모델변경");
            Global.환경설정.모델변경요청(모델번호);
            //솔루션파일 변경 및 MainViewControl 변경.
            //VmSolution.Load(Global.모델자료[모델번호].솔루션파일저장경로, null);
            Global.비전마스터구동.Init();
      
            //마스터이미지출력();
            this.DataSourceBind();
            Global.정보로그("모델영역", "모델 변경", "모델변경이 완료되었습니다.", true);
            if (!isEditing)
            {
                //마스터이미지출력(false);
                return;
            }
        }

        private void 마스터이미지출력(Boolean isShow = true)
        {
            if(Global.모델자료.선택모델 is null)
            {
                Global.오류로그("데이터로드", "마스터이미지 로드", "선택된 모델이 없습니다.", isShow);
                return;
            }
            String 마스터이미지경로 = Path.Combine(Global.모델자료.선택모델.마스터이미지경로(this.카메라));
            if (!File.Exists(마스터이미지경로))
            {
                Global.오류로그("데이터로드", "마스터이미지 로드", "마스터이미지가 없습니다.", isShow);
                return;
            }
            Mat 마스터이미지 = Cv2.ImRead(Global.모델자료.선택모델.마스터이미지경로(this.카메라));
            //this.e뷰어.LoadImage(마스터이미지);
            //Global.비전마스터구동.GetItem(Flow구분.Flow1).Run(마스터이미지);
        }

        private void B마스터로드_Click(object sender, EventArgs e)
        {
            마스터이미지출력();
        }
        private void B마스터저장_Click(object sender, EventArgs e)
        {
            if (!IvmUtils.Utils.Confirm("현재 이미지를 마스터이미지로 등록 하시겠습니까?")) return;
            //vmControl_Render1.SaveOriginalImage(Global.모델자료.선택모델.마스터이미지경로(this.카메라));
            Global.정보로그("모델영역", "마스터이미지 저장", "모델저장이 완료되었습니다.", true);
        }

        private void B편집모드_Click(object sender, EventArgs e)
        {
            //마스터이미지출력();
            //if (isEditing)
            //{
            //    isEditing = false;
            //    return;
            //}
            //this.DataSourceBind();
            //isEditing = true;
        }

        private void B모델저장_Click(object sender, EventArgs e)
        {
            Global.모델자료.Save();
            if (!IvmUtils.Utils.Confirm("변경된 내용을 저장 하시겠습니까?")) return;
            VmSolution.SaveAs(Global.모델자료.선택모델.솔루션파일저장경로,null);
            Global.정보로그("모델영역", "모델 저장", "모델저장이 완료되었습니다.", true);
        }

        private void ButtonDeleteClick_GridView1(object sender, ItemClickEventArgs e)
        {
            if (!IvmUtils.Utils.Confirm("선택한 항목을 제거 하시겠습니까?")) return;
            this.GridView1.DeleteSelectedRows();
        }

        private void ButtonDeleteClick_GridView2(object sender, ItemClickEventArgs e)
        {
            if (!IvmUtils.Utils.Confirm("선택한 모델을 제거 하시겠습니까?")) return;
            모델정보 selectedItem = this.GridView2.GetFocusedRow() as 모델정보;
            Utils.Common.DeleteDirectoryAndFiles(selectedItem.모델저장폴더);

            this.GridView2.DeleteSelectedRows();
            Global.모델자료.Save();
        }

        private void 모델변경알림(Int32 모델번호)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => this.모델변경알림(모델번호)));
                return;
            }
            if (IvmUtils.Utils.IntValue(this.e모델선택.EditValue) == 모델번호) return;
            this.e모델선택.EditValue = 모델번호;
            Global.모델자료.선택모델.검사목록.ListChanged += 검사목록_ListChanged;
        }
    }
}
