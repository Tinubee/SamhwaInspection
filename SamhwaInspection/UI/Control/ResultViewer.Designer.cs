using DevExpress.XtraLayout.Utils;

namespace SamhwaInspection.UI.Control
{
    partial class ResultViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.col모델번호 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col모델이름 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col모델저장폴더 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col마스터이미지1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col마스터이미지2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.viewer6 = new IvLibs.Controls.Viewer();
            this.viewer5 = new IvLibs.Controls.Viewer();
            this.viewer4 = new IvLibs.Controls.Viewer();
            this.viewer3 = new IvLibs.Controls.Viewer();
            this.viewer2 = new IvLibs.Controls.Viewer();
            this.viewer1 = new IvLibs.Controls.Viewer();
            this.myGridControl1 = new IvmUtils.MyGridControl();
            this.검사목록BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.myGridView1 = new IvmUtils.MyGridView();
            this.col카메라구분 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사번호 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사명칭 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최소 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col기준 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최대 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col교정 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col사용 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col실측 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col판정 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colrectangle = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.검사목록BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // col모델번호
            // 
            this.col모델번호.Name = "col모델번호";
            // 
            // col모델이름
            // 
            this.col모델이름.Name = "col모델이름";
            // 
            // col모델저장폴더
            // 
            this.col모델저장폴더.Name = "col모델저장폴더";
            // 
            // col마스터이미지1
            // 
            this.col마스터이미지1.Name = "col마스터이미지1";
            // 
            // col마스터이미지2
            // 
            this.col마스터이미지2.Name = "col마스터이미지2";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 42.74F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 12.42F)});
            this.tablePanel1.Controls.Add(this.tablePanel2);
            this.tablePanel1.Controls.Add(this.myGridControl1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F)});
            this.tablePanel1.Size = new System.Drawing.Size(1581, 869);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // tablePanel2
            // 
            this.tablePanel1.SetColumn(this.tablePanel2, 0);
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F)});
            this.tablePanel2.Controls.Add(this.viewer6);
            this.tablePanel2.Controls.Add(this.viewer5);
            this.tablePanel2.Controls.Add(this.viewer4);
            this.tablePanel2.Controls.Add(this.viewer3);
            this.tablePanel2.Controls.Add(this.viewer2);
            this.tablePanel2.Controls.Add(this.viewer1);
            this.tablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel2.Location = new System.Drawing.Point(3, 3);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.SetRow(this.tablePanel2, 0);
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F)});
            this.tablePanel2.Size = new System.Drawing.Size(1219, 863);
            this.tablePanel2.TabIndex = 4;
            this.tablePanel2.UseSkinIndents = true;
            // 
            // viewer6
            // 
            this.tablePanel2.SetColumn(this.viewer6, 2);
            this.viewer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer6.Location = new System.Drawing.Point(814, 434);
            this.viewer6.Name = "viewer6";
            this.tablePanel2.SetRow(this.viewer6, 1);
            this.viewer6.Size = new System.Drawing.Size(402, 426);
            this.viewer6.TabIndex = 5;
            // 
            // viewer5
            // 
            this.tablePanel2.SetColumn(this.viewer5, 1);
            this.viewer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer5.Location = new System.Drawing.Point(409, 434);
            this.viewer5.Name = "viewer5";
            this.tablePanel2.SetRow(this.viewer5, 1);
            this.viewer5.Size = new System.Drawing.Size(402, 426);
            this.viewer5.TabIndex = 4;
            // 
            // viewer4
            // 
            this.tablePanel2.SetColumn(this.viewer4, 0);
            this.viewer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer4.Location = new System.Drawing.Point(3, 434);
            this.viewer4.Name = "viewer4";
            this.tablePanel2.SetRow(this.viewer4, 1);
            this.viewer4.Size = new System.Drawing.Size(402, 426);
            this.viewer4.TabIndex = 3;
            // 
            // viewer3
            // 
            this.tablePanel2.SetColumn(this.viewer3, 2);
            this.viewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer3.Location = new System.Drawing.Point(814, 3);
            this.viewer3.Name = "viewer3";
            this.tablePanel2.SetRow(this.viewer3, 0);
            this.viewer3.Size = new System.Drawing.Size(402, 427);
            this.viewer3.TabIndex = 2;
            // 
            // viewer2
            // 
            this.tablePanel2.SetColumn(this.viewer2, 1);
            this.viewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer2.Location = new System.Drawing.Point(409, 3);
            this.viewer2.Name = "viewer2";
            this.tablePanel2.SetRow(this.viewer2, 0);
            this.viewer2.Size = new System.Drawing.Size(402, 427);
            this.viewer2.TabIndex = 1;
            // 
            // viewer1
            // 
            this.tablePanel2.SetColumn(this.viewer1, 0);
            this.viewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer1.Location = new System.Drawing.Point(3, 3);
            this.viewer1.Name = "viewer1";
            this.tablePanel2.SetRow(this.viewer1, 0);
            this.viewer1.Size = new System.Drawing.Size(402, 427);
            this.viewer1.TabIndex = 0;
            // 
            // myGridControl1
            // 
            this.tablePanel1.SetColumn(this.myGridControl1, 1);
            this.myGridControl1.DataSource = this.검사목록BindingSource;
            this.myGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myGridControl1.Location = new System.Drawing.Point(1226, 3);
            this.myGridControl1.MainView = this.myGridView1;
            this.myGridControl1.Name = "myGridControl1";
            this.tablePanel1.SetRow(this.myGridControl1, 0);
            this.myGridControl1.Size = new System.Drawing.Size(352, 863);
            this.myGridControl1.TabIndex = 3;
            this.myGridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.myGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.myGridView1});
            // 
            // myGridView1
            // 
            this.myGridView1.AllowColumnMenu = true;
            this.myGridView1.AllowCustomMenu = true;
            this.myGridView1.AllowExport = true;
            this.myGridView1.AllowPrint = true;
            this.myGridView1.AllowSettingsMenu = false;
            this.myGridView1.AllowSummaryMenu = true;
            this.myGridView1.ApplyFocusedRow = true;
            this.myGridView1.Caption = "";
            this.myGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col카메라구분,
            this.col검사번호,
            this.col검사명칭,
            this.col최소,
            this.col기준,
            this.col최대,
            this.col교정,
            this.col사용,
            this.col측정,
            this.col실측,
            this.col판정,
            this.colrectangle});
            this.myGridView1.FooterPanelHeight = 21;
            this.myGridView1.GridControl = this.myGridControl1;
            this.myGridView1.GroupRowHeight = 21;
            this.myGridView1.IndicatorWidth = 44;
            this.myGridView1.MinColumnRowHeight = 24;
            this.myGridView1.MinRowHeight = 16;
            this.myGridView1.Name = "myGridView1";
            this.myGridView1.OptionsBehavior.Editable = false;
            this.myGridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.myGridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.myGridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.myGridView1.OptionsPrint.AutoWidth = false;
            this.myGridView1.OptionsPrint.UsePrintStyles = false;
            this.myGridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.myGridView1.OptionsView.ShowGroupPanel = false;
            this.myGridView1.OptionsView.ShowIndicator = false;
            this.myGridView1.RowHeight = 20;
            this.myGridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.col카메라구분, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // col카메라구분
            // 
            this.col카메라구분.AppearanceHeader.Options.UseTextOptions = true;
            this.col카메라구분.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col카메라구분.Caption = "카메라";
            this.col카메라구분.FieldName = "카메라구분";
            this.col카메라구분.Name = "col카메라구분";
            this.col카메라구분.Visible = true;
            this.col카메라구분.VisibleIndex = 0;
            this.col카메라구분.Width = 73;
            // 
            // col검사번호
            // 
            this.col검사번호.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사번호.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사번호.FieldName = "검사번호";
            this.col검사번호.Name = "col검사번호";
            this.col검사번호.Visible = true;
            this.col검사번호.VisibleIndex = 1;
            this.col검사번호.Width = 60;
            // 
            // col검사명칭
            // 
            this.col검사명칭.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사명칭.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사명칭.FieldName = "검사명칭";
            this.col검사명칭.Name = "col검사명칭";
            // 
            // col최소
            // 
            this.col최소.AppearanceHeader.Options.UseTextOptions = true;
            this.col최소.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최소.FieldName = "최소";
            this.col최소.Name = "col최소";
            this.col최소.Visible = true;
            this.col최소.VisibleIndex = 2;
            this.col최소.Width = 55;
            // 
            // col기준
            // 
            this.col기준.AppearanceHeader.Options.UseTextOptions = true;
            this.col기준.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col기준.FieldName = "기준";
            this.col기준.Name = "col기준";
            // 
            // col최대
            // 
            this.col최대.AppearanceHeader.Options.UseTextOptions = true;
            this.col최대.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최대.FieldName = "최대";
            this.col최대.Name = "col최대";
            this.col최대.Visible = true;
            this.col최대.VisibleIndex = 3;
            this.col최대.Width = 55;
            // 
            // col교정
            // 
            this.col교정.AppearanceHeader.Options.UseTextOptions = true;
            this.col교정.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col교정.FieldName = "교정";
            this.col교정.Name = "col교정";
            // 
            // col사용
            // 
            this.col사용.AppearanceHeader.Options.UseTextOptions = true;
            this.col사용.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col사용.FieldName = "사용";
            this.col사용.Name = "col사용";
            // 
            // col측정
            // 
            this.col측정.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정.FieldName = "측정";
            this.col측정.Name = "col측정";
            this.col측정.Visible = true;
            this.col측정.VisibleIndex = 4;
            this.col측정.Width = 55;
            // 
            // col실측
            // 
            this.col실측.AppearanceHeader.Options.UseTextOptions = true;
            this.col실측.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col실측.FieldName = "실측";
            this.col실측.Name = "col실측";
            // 
            // col판정
            // 
            this.col판정.AppearanceHeader.Options.UseTextOptions = true;
            this.col판정.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col판정.FieldName = "판정";
            this.col판정.Name = "col판정";
            this.col판정.Visible = true;
            this.col판정.VisibleIndex = 5;
            // 
            // colrectangle
            // 
            this.colrectangle.AppearanceHeader.Options.UseTextOptions = true;
            this.colrectangle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colrectangle.FieldName = "rectangle";
            this.colrectangle.Name = "colrectangle";
            // 
            // ResultViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tablePanel1);
            this.Name = "ResultViewer";
            this.Size = new System.Drawing.Size(1581, 869);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.검사목록BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn col모델번호;
        private DevExpress.XtraGrid.Columns.GridColumn col모델이름;
        private DevExpress.XtraGrid.Columns.GridColumn col모델저장폴더;
        private DevExpress.XtraGrid.Columns.GridColumn col마스터이미지1;
        private DevExpress.XtraGrid.Columns.GridColumn col마스터이미지2;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private IvmUtils.MyGridControl myGridControl1;
        private IvmUtils.MyGridView myGridView1;
        private System.Windows.Forms.BindingSource 검사목록BindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn col카메라구분;
        private DevExpress.XtraGrid.Columns.GridColumn col검사번호;
        private DevExpress.XtraGrid.Columns.GridColumn col검사명칭;
        private DevExpress.XtraGrid.Columns.GridColumn col최소;
        private DevExpress.XtraGrid.Columns.GridColumn col기준;
        private DevExpress.XtraGrid.Columns.GridColumn col최대;
        private DevExpress.XtraGrid.Columns.GridColumn col교정;
        private DevExpress.XtraGrid.Columns.GridColumn col사용;
        private DevExpress.XtraGrid.Columns.GridColumn col측정;
        private DevExpress.XtraGrid.Columns.GridColumn col실측;
        private DevExpress.XtraGrid.Columns.GridColumn col판정;
        private DevExpress.XtraGrid.Columns.GridColumn colrectangle;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private IvLibs.Controls.Viewer viewer6;
        private IvLibs.Controls.Viewer viewer5;
        private IvLibs.Controls.Viewer viewer4;
        private IvLibs.Controls.Viewer viewer3;
        private IvLibs.Controls.Viewer viewer2;
        private IvLibs.Controls.Viewer viewer1;
    }
}
