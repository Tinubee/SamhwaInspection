namespace SamhwaInspection.UI.Control
{
    partial class User
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
            this.Bind사용자 = new System.Windows.Forms.BindingSource(this.components);
            this.myGridControl1 = new IvmUtils.MyGridControl();
            this.myGridView1 = new IvmUtils.MyGridView();
            this.col성명 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col암호 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col비고 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col권한 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col허용 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Bind사용자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Bind사용자
            // 
            this.Bind사용자.DataSource = typeof(SamhwaInspection.Schemas.유저자료);
            // 
            // myGridControl1
            // 
            this.myGridControl1.DataSource = this.Bind사용자;
            this.myGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myGridControl1.Location = new System.Drawing.Point(0, 0);
            this.myGridControl1.MainView = this.myGridView1;
            this.myGridControl1.Name = "myGridControl1";
            this.myGridControl1.Size = new System.Drawing.Size(775, 664);
            this.myGridControl1.TabIndex = 0;
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
            this.col성명,
            this.col암호,
            this.col비고,
            this.col권한,
            this.col허용});
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
            this.myGridView1.RowHeight = 20;
            // 
            // col성명
            // 
            this.col성명.AppearanceHeader.Options.UseTextOptions = true;
            this.col성명.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col성명.FieldName = "성명";
            this.col성명.Name = "col성명";
            this.col성명.Visible = true;
            this.col성명.VisibleIndex = 0;
            // 
            // col암호
            // 
            this.col암호.AppearanceHeader.Options.UseTextOptions = true;
            this.col암호.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col암호.FieldName = "암호";
            this.col암호.Name = "col암호";
            this.col암호.Visible = true;
            this.col암호.VisibleIndex = 1;
            // 
            // col비고
            // 
            this.col비고.AppearanceHeader.Options.UseTextOptions = true;
            this.col비고.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col비고.FieldName = "비고";
            this.col비고.Name = "col비고";
            this.col비고.Visible = true;
            this.col비고.VisibleIndex = 2;
            // 
            // col권한
            // 
            this.col권한.AppearanceHeader.Options.UseTextOptions = true;
            this.col권한.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col권한.FieldName = "권한";
            this.col권한.Name = "col권한";
            this.col권한.Visible = true;
            this.col권한.VisibleIndex = 3;
            // 
            // col허용
            // 
            this.col허용.AppearanceHeader.Options.UseTextOptions = true;
            this.col허용.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col허용.FieldName = "허용";
            this.col허용.Name = "col허용";
            this.col허용.Visible = true;
            this.col허용.VisibleIndex = 4;
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.myGridControl1);
            this.Name = "User";
            this.Size = new System.Drawing.Size(775, 664);
            ((System.ComponentModel.ISupportInitialize)(this.Bind사용자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource Bind사용자;
        private IvmUtils.MyGridControl myGridControl1;
        private IvmUtils.MyGridView myGridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col성명;
        private DevExpress.XtraGrid.Columns.GridColumn col암호;
        private DevExpress.XtraGrid.Columns.GridColumn col비고;
        private DevExpress.XtraGrid.Columns.GridColumn col권한;
        private DevExpress.XtraGrid.Columns.GridColumn col허용;
    }
}
