namespace SamhwaInspection.UI.Control
{
    partial class ResultViewer_6
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
            this.myGridControl1 = new IvmUtils.MyGridControl();
            this.myGridView1 = new IvmUtils.MyGridView();
            this.col검사일시 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col모델번호 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최종결과 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.vmControl_Render6 = new SamhwaInspection.UI.Control.VMControl_Render();
            this.vmControl_Render3 = new SamhwaInspection.UI.Control.VMControl_Render();
            this.vmControl_Render1 = new SamhwaInspection.UI.Control.VMControl_Render();
            this.vmControl_Render4 = new SamhwaInspection.UI.Control.VMControl_Render();
            this.vmControl_Render2 = new SamhwaInspection.UI.Control.VMControl_Render();
            this.vmControl_Render5 = new SamhwaInspection.UI.Control.VMControl_Render();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.검사목록BindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.myGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.검사목록BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // myGridControl1
            // 
            this.myGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myGridControl1.Location = new System.Drawing.Point(0, 0);
            this.myGridControl1.MainView = this.myGridView1;
            this.myGridControl1.Name = "myGridControl1";
            this.myGridControl1.Size = new System.Drawing.Size(0, 0);
            this.myGridControl1.TabIndex = 0;
            this.myGridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.myGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.myGridView1});
            this.myGridControl1.Visible = false;
            // 
            // myGridView1
            // 
            this.myGridView1.AllowColumnMenu = true;
            this.myGridView1.AllowCustomMenu = true;
            this.myGridView1.AllowExport = true;
            this.myGridView1.AllowPrint = true;
            //this.myGridView1.AllowSettingsMenu = false;
            this.myGridView1.AllowSummaryMenu = true;
            //this.myGridView1.ApplyFocusedRow = true;
            this.myGridView1.Caption = "";
            this.myGridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col검사일시,
            this.col모델번호,
            this.col최종결과});
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
            this.myGridView1.RowHeight = 20;
            // 
            // col검사일시
            // 
            this.col검사일시.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사일시.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사일시.FieldName = "검사일시";
            this.col검사일시.Name = "col검사일시";
            this.col검사일시.Visible = true;
            this.col검사일시.VisibleIndex = 0;
            // 
            // col모델번호
            // 
            this.col모델번호.AppearanceHeader.Options.UseTextOptions = true;
            this.col모델번호.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col모델번호.FieldName = "모델번호";
            this.col모델번호.Name = "col모델번호";
            this.col모델번호.Visible = true;
            this.col모델번호.VisibleIndex = 1;
            // 
            // col최종결과
            // 
            this.col최종결과.AppearanceHeader.Options.UseTextOptions = true;
            this.col최종결과.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최종결과.FieldName = "최종결과";
            this.col최종결과.Name = "col최종결과";
            this.col최종결과.Visible = true;
            this.col최종결과.VisibleIndex = 2;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Controls.Add(this.vmControl_Render6);
            this.tablePanel1.Controls.Add(this.vmControl_Render3);
            this.tablePanel1.Controls.Add(this.vmControl_Render1);
            this.tablePanel1.Controls.Add(this.vmControl_Render4);
            this.tablePanel1.Controls.Add(this.vmControl_Render2);
            this.tablePanel1.Controls.Add(this.vmControl_Render5);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Size = new System.Drawing.Size(1581, 869);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // vmControl_Render6
            // 
            this.tablePanel1.SetColumn(this.vmControl_Render6, 2);
            this.vmControl_Render6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmControl_Render6.Location = new System.Drawing.Point(1056, 437);
            this.vmControl_Render6.Name = "vmControl_Render6";
            this.tablePanel1.SetRow(this.vmControl_Render6, 1);
            this.vmControl_Render6.Size = new System.Drawing.Size(522, 429);
            this.vmControl_Render6.TabIndex = 9;
            // 
            // vmControl_Render3
            // 
            this.tablePanel1.SetColumn(this.vmControl_Render3, 2);
            this.vmControl_Render3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmControl_Render3.Location = new System.Drawing.Point(1056, 3);
            this.vmControl_Render3.Name = "vmControl_Render3";
            this.tablePanel1.SetRow(this.vmControl_Render3, 0);
            this.vmControl_Render3.Size = new System.Drawing.Size(522, 430);
            this.vmControl_Render3.TabIndex = 8;
            // 
            // vmControl_Render1
            // 
            this.tablePanel1.SetColumn(this.vmControl_Render1, 0);
            this.vmControl_Render1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmControl_Render1.Location = new System.Drawing.Point(3, 3);
            this.vmControl_Render1.Name = "vmControl_Render1";
            this.tablePanel1.SetRow(this.vmControl_Render1, 0);
            this.vmControl_Render1.Size = new System.Drawing.Size(522, 430);
            this.vmControl_Render1.TabIndex = 7;
            // 
            // vmControl_Render4
            // 
            this.tablePanel1.SetColumn(this.vmControl_Render4, 0);
            this.vmControl_Render4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmControl_Render4.Location = new System.Drawing.Point(3, 437);
            this.vmControl_Render4.Name = "vmControl_Render4";
            this.tablePanel1.SetRow(this.vmControl_Render4, 1);
            this.vmControl_Render4.Size = new System.Drawing.Size(522, 429);
            this.vmControl_Render4.TabIndex = 6;
            // 
            // vmControl_Render2
            // 
            this.tablePanel1.SetColumn(this.vmControl_Render2, 1);
            this.vmControl_Render2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmControl_Render2.Location = new System.Drawing.Point(529, 3);
            this.vmControl_Render2.Name = "vmControl_Render2";
            this.tablePanel1.SetRow(this.vmControl_Render2, 0);
            this.vmControl_Render2.Size = new System.Drawing.Size(522, 430);
            this.vmControl_Render2.TabIndex = 5;
            // 
            // vmControl_Render5
            // 
            this.tablePanel1.SetColumn(this.vmControl_Render5, 1);
            this.vmControl_Render5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmControl_Render5.Location = new System.Drawing.Point(529, 437);
            this.vmControl_Render5.Name = "vmControl_Render5";
            this.tablePanel1.SetRow(this.vmControl_Render5, 1);
            this.vmControl_Render5.Size = new System.Drawing.Size(522, 429);
            this.vmControl_Render5.TabIndex = 3;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tablePanel1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.myGridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl1.Size = new System.Drawing.Size(1581, 869);
            this.splitContainerControl1.SplitterPosition = 357;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // ResultViewer_6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ResultViewer_6";
            this.Size = new System.Drawing.Size(1581, 869);
            ((System.ComponentModel.ISupportInitialize)(this.myGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.검사목록BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private IvmUtils.MyGridControl myGridControl1;
        private IvmUtils.MyGridView myGridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col검사일시;
        private DevExpress.XtraGrid.Columns.GridColumn col모델번호;
        private DevExpress.XtraGrid.Columns.GridColumn col최종결과;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.BindingSource 검사목록BindingSource;
        public VMControl_Render vmControl_Render5;
        public VMControl_Render vmControl_Render4;
        public VMControl_Render vmControl_Render2;
        public VMControl_Render vmControl_Render1;
        public VMControl_Render vmControl_Render6;
        public VMControl_Render vmControl_Render3;
    }
}
