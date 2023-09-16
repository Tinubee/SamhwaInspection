namespace SamhwaInspection.UI.Control
{
    partial class ResultList
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultList));
            this.GridView2 = new IvmUtils.MyGridView();
            this.col일시 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사번호 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col카메라구분 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최소 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col기준 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최대 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col판정 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GridControl1 = new IvmUtils.MyGridControl();
            this.Bind검사자료 = new System.Windows.Forms.BindingSource(this.components);
            this.GridView1 = new IvmUtils.MyGridView();
            this.col검사일시 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col모델번호 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최종결과 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.e결과표시 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.b자료조회 = new DevExpress.XtraEditors.SimpleButton();
            this.b엑셀파일 = new DevExpress.XtraEditors.SimpleButton();
            this.e시작일자 = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.GridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사자료)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e결과표시)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e시작일자.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e시작일자.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // GridView2
            // 
            this.GridView2.AllowColumnMenu = true;
            this.GridView2.AllowCustomMenu = true;
            this.GridView2.AllowExport = true;
            this.GridView2.AllowPrint = true;
            this.GridView2.AllowSummaryMenu = true;
            this.GridView2.Caption = "";
            this.GridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col일시,
            this.col검사번호,
            this.col카메라구분,
            this.col최소,
            this.col기준,
            this.col최대,
            this.col측정,
            this.col판정});
            this.GridView2.FooterPanelHeight = 21;
            this.GridView2.GridControl = this.GridControl1;
            this.GridView2.GroupRowHeight = 21;
            this.GridView2.IndicatorWidth = 44;
            this.GridView2.MinColumnRowHeight = 24;
            this.GridView2.MinRowHeight = 16;
            this.GridView2.Name = "GridView2";
            this.GridView2.OptionsCustomization.AllowColumnMoving = false;
            this.GridView2.OptionsCustomization.AllowFilter = false;
            this.GridView2.OptionsCustomization.AllowGroup = false;
            this.GridView2.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.GridView2.OptionsCustomization.AllowQuickHideColumns = false;
            this.GridView2.OptionsCustomization.AllowRowSizing = true;
            this.GridView2.OptionsView.ShowGroupPanel = false;
            this.GridView2.RowHeight = 20;
            // 
            // col일시
            // 
            this.col일시.AppearanceHeader.Options.UseTextOptions = true;
            this.col일시.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col일시.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.col일시.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.col일시.FieldName = "일시";
            this.col일시.Name = "col일시";
            this.col일시.Visible = true;
            this.col일시.VisibleIndex = 0;
            // 
            // col검사번호
            // 
            this.col검사번호.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사번호.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사번호.FieldName = "검사번호";
            this.col검사번호.Name = "col검사번호";
            this.col검사번호.Visible = true;
            this.col검사번호.VisibleIndex = 1;
            // 
            // col카메라구분
            // 
            this.col카메라구분.AppearanceHeader.Options.UseTextOptions = true;
            this.col카메라구분.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col카메라구분.FieldName = "카메라구분";
            this.col카메라구분.Name = "col카메라구분";
            this.col카메라구분.Visible = true;
            this.col카메라구분.VisibleIndex = 2;
            // 
            // col최소
            // 
            this.col최소.AppearanceHeader.Options.UseTextOptions = true;
            this.col최소.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최소.FieldName = "최소";
            this.col최소.Name = "col최소";
            this.col최소.Visible = true;
            this.col최소.VisibleIndex = 3;
            // 
            // col기준
            // 
            this.col기준.AppearanceHeader.Options.UseTextOptions = true;
            this.col기준.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col기준.FieldName = "기준";
            this.col기준.Name = "col기준";
            this.col기준.Visible = true;
            this.col기준.VisibleIndex = 4;
            // 
            // col최대
            // 
            this.col최대.AppearanceHeader.Options.UseTextOptions = true;
            this.col최대.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최대.FieldName = "최대";
            this.col최대.Name = "col최대";
            this.col최대.Visible = true;
            this.col최대.VisibleIndex = 5;
            // 
            // col측정
            // 
            this.col측정.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정.FieldName = "측정";
            this.col측정.Name = "col측정";
            this.col측정.Visible = true;
            this.col측정.VisibleIndex = 6;
            // 
            // col판정
            // 
            this.col판정.AppearanceHeader.Options.UseTextOptions = true;
            this.col판정.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col판정.FieldName = "판정";
            this.col판정.Name = "col판정";
            this.col판정.Visible = true;
            this.col판정.VisibleIndex = 7;
            // 
            // GridControl1
            // 
            this.GridControl1.DataSource = this.Bind검사자료;
            gridLevelNode1.LevelTemplate = this.GridView2;
            gridLevelNode1.RelationName = "검사내역";
            this.GridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GridControl1.Location = new System.Drawing.Point(2, 37);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.MenuManager = this.barManager1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.e결과표시});
            this.GridControl1.Size = new System.Drawing.Size(1288, 764);
            this.GridControl1.TabIndex = 5;
            this.GridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView1,
            this.GridView2});
            // 
            // Bind검사자료
            // 
            this.Bind검사자료.DataSource = typeof(SamhwaInspection.Schemas.검사결과);
            // 
            // GridView1
            // 
            this.GridView1.AllowColumnMenu = true;
            this.GridView1.AllowCustomMenu = true;
            this.GridView1.AllowExport = true;
            this.GridView1.AllowPrint = true;
            this.GridView1.AllowSummaryMenu = true;
            this.GridView1.Caption = "";
            this.GridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col검사일시,
            this.col모델번호,
            this.col최종결과});
            this.GridView1.FooterPanelHeight = 21;
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.GroupRowHeight = 21;
            this.GridView1.IndicatorWidth = 44;
            this.GridView1.MinColumnRowHeight = 24;
            this.GridView1.MinRowHeight = 16;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.Editable = false;
            this.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView1.OptionsCustomization.AllowColumnMoving = false;
            this.GridView1.OptionsCustomization.AllowFilter = false;
            this.GridView1.OptionsCustomization.AllowGroup = false;
            this.GridView1.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.GridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView1.OptionsPrint.AutoWidth = false;
            this.GridView1.OptionsPrint.UsePrintStyles = false;
            this.GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.RowHeight = 20;
            // 
            // col검사일시
            // 
            this.col검사일시.AppearanceCell.Options.UseTextOptions = true;
            this.col검사일시.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사일시.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사일시.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사일시.Caption = "Date";
            this.col검사일시.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.col검사일시.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.col검사일시.FieldName = "검사일시";
            this.col검사일시.Name = "col검사일시";
            this.col검사일시.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.col검사일시.Visible = true;
            this.col검사일시.VisibleIndex = 0;
            // 
            // col모델번호
            // 
            this.col모델번호.AppearanceCell.Options.UseTextOptions = true;
            this.col모델번호.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col모델번호.AppearanceHeader.Options.UseTextOptions = true;
            this.col모델번호.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col모델번호.Caption = "Model Number";
            this.col모델번호.FieldName = "모델번호";
            this.col모델번호.Name = "col모델번호";
            this.col모델번호.Visible = true;
            this.col모델번호.VisibleIndex = 1;
            // 
            // col최종결과
            // 
            this.col최종결과.AppearanceCell.Options.UseTextOptions = true;
            this.col최종결과.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최종결과.AppearanceHeader.Options.UseTextOptions = true;
            this.col최종결과.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최종결과.Caption = "Result";
            this.col최종결과.ColumnEdit = this.e결과표시;
            this.col최종결과.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.col최종결과.FieldName = "최종결과";
            this.col최종결과.Name = "col최종결과";
            this.col최종결과.Visible = true;
            this.col최종결과.VisibleIndex = 2;
            // 
            // e결과표시
            // 
            this.e결과표시.AutoHeight = false;
            this.e결과표시.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.DisplayText;
            this.e결과표시.Name = "e결과표시";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1292, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 803);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1292, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 803);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1292, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 803);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.GridControl1);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1292, 803);
            this.layoutControl1.TabIndex = 9;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl2);
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1288, 31);
            this.panelControl1.TabIndex = 4;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.b자료조회);
            this.layoutControl2.Controls.Add(this.b엑셀파일);
            this.layoutControl2.Controls.Add(this.e시작일자);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 2);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1284, 27);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // b자료조회
            // 
            this.b자료조회.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.b자료조회.Appearance.Options.UseFont = true;
            this.b자료조회.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b자료조회.ImageOptions.SvgImage")));
            this.b자료조회.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.b자료조회.Location = new System.Drawing.Point(248, 2);
            this.b자료조회.Name = "b자료조회";
            this.b자료조회.Size = new System.Drawing.Size(105, 22);
            this.b자료조회.StyleController = this.layoutControl2;
            this.b자료조회.TabIndex = 5;
            this.b자료조회.Text = "Search";
            // 
            // b엑셀파일
            // 
            this.b엑셀파일.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.b엑셀파일.Appearance.Options.UseFont = true;
            this.b엑셀파일.Location = new System.Drawing.Point(1148, 2);
            this.b엑셀파일.Name = "b엑셀파일";
            this.b엑셀파일.Size = new System.Drawing.Size(134, 22);
            this.b엑셀파일.StyleController = this.layoutControl2;
            this.b엑셀파일.TabIndex = 6;
            this.b엑셀파일.Text = "Export To Excel";
            // 
            // e시작일자
            // 
            this.e시작일자.EditValue = null;
            this.e시작일자.Location = new System.Drawing.Point(39, 2);
            this.e시작일자.MenuManager = this.barManager1;
            this.e시작일자.Name = "e시작일자";
            this.e시작일자.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.e시작일자.Properties.Appearance.Options.UseFont = true;
            this.e시작일자.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e시작일자.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e시작일자.Properties.CalendarTimeProperties.DisplayFormat.FormatString = "d";
            this.e시작일자.Properties.CalendarTimeProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.e시작일자.Properties.MaskSettings.Set("mask", "");
            this.e시작일자.Size = new System.Drawing.Size(205, 22);
            this.e시작일자.StyleController = this.layoutControl2;
            this.e시작일자.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem5});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1284, 27);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.e시작일자;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(246, 27);
            this.layoutControlItem3.Text = "Date";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(25, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.b자료조회;
            this.layoutControlItem4.Location = new System.Drawing.Point(246, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(109, 27);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(355, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(791, 27);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.b엑셀파일;
            this.layoutControlItem5.Location = new System.Drawing.Point(1146, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(138, 27);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1292, 803);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1292, 35);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.GridControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1292, 768);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ResultList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ResultList";
            this.Size = new System.Drawing.Size(1292, 803);
            ((System.ComponentModel.ISupportInitialize)(this.GridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사자료)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e결과표시)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e시작일자.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e시작일자.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private IvmUtils.MyGridControl GridControl1;
        private IvmUtils.MyGridView GridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.SimpleButton b자료조회;
        private DevExpress.XtraEditors.SimpleButton b엑셀파일;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit e시작일자;
        private IvmUtils.MyGridView GridView2;
        private System.Windows.Forms.BindingSource Bind검사자료;
        private DevExpress.XtraGrid.Columns.GridColumn col검사일시;
        private DevExpress.XtraGrid.Columns.GridColumn col모델번호;
        private DevExpress.XtraGrid.Columns.GridColumn col최종결과;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit e결과표시;
        private DevExpress.XtraGrid.Columns.GridColumn col일시;
        private DevExpress.XtraGrid.Columns.GridColumn col검사번호;
        private DevExpress.XtraGrid.Columns.GridColumn col카메라구분;
        private DevExpress.XtraGrid.Columns.GridColumn col최소;
        private DevExpress.XtraGrid.Columns.GridColumn col기준;
        private DevExpress.XtraGrid.Columns.GridColumn col최대;
        private DevExpress.XtraGrid.Columns.GridColumn col측정;
        private DevExpress.XtraGrid.Columns.GridColumn col판정;
    }
}
