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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.Bind사용자 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col성명 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col암호 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col비고 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col권한 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col허용 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.e암호 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind사용자)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e암호)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 599);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(775, 65);
            this.panelControl1.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.Bind사용자;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.e암호});
            this.gridControl1.Size = new System.Drawing.Size(775, 599);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // Bind사용자
            // 
            this.Bind사용자.DataSource = typeof(SamhwaInspection.Schemas.유저자료);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col성명,
            this.col암호,
            this.col비고,
            this.col권한,
            this.col허용});
            this.gridView1.FooterPanelHeight = 21;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupRowHeight = 21;
            this.gridView1.IndicatorWidth = 44;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 20;
            // 
            // col성명
            // 
            this.col성명.FieldName = "성명";
            this.col성명.Name = "col성명";
            this.col성명.Visible = true;
            this.col성명.VisibleIndex = 0;
            // 
            // col암호
            // 
            this.col암호.ColumnEdit = this.e암호;
            this.col암호.FieldName = "암호";
            this.col암호.Name = "col암호";
            this.col암호.Visible = true;
            this.col암호.VisibleIndex = 1;
            // 
            // col비고
            // 
            this.col비고.FieldName = "비고";
            this.col비고.Name = "col비고";
            this.col비고.Visible = true;
            this.col비고.VisibleIndex = 2;
            // 
            // col권한
            // 
            this.col권한.FieldName = "권한";
            this.col권한.Name = "col권한";
            this.col권한.Visible = true;
            this.col권한.VisibleIndex = 3;
            // 
            // col허용
            // 
            this.col허용.FieldName = "허용";
            this.col허용.Name = "col허용";
            this.col허용.Visible = true;
            this.col허용.VisibleIndex = 4;
            // 
            // e암호
            // 
            this.e암호.AutoHeight = false;
            this.e암호.Name = "e암호";
            this.e암호.UseSystemPasswordChar = true;
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "User";
            this.Size = new System.Drawing.Size(775, 664);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind사용자)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e암호)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource Bind사용자;
        private DevExpress.XtraGrid.Columns.GridColumn col성명;
        private DevExpress.XtraGrid.Columns.GridColumn col암호;
        private DevExpress.XtraGrid.Columns.GridColumn col비고;
        private DevExpress.XtraGrid.Columns.GridColumn col권한;
        private DevExpress.XtraGrid.Columns.GridColumn col허용;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit e암호;
    }
}
