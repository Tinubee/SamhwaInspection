namespace SamhwaInspection
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabFormControl1 = new DevExpress.XtraBars.TabFormControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.skinPaletteDropDownButtonItem1 = new DevExpress.XtraBars.SkinPaletteDropDownButtonItem();
            this.p비전검사 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer1 = new DevExpress.XtraBars.TabFormContentContainer();
            this.tc검사디스플레이 = new DevExpress.XtraTab.XtraTabControl();
            this.p치수검사 = new DevExpress.XtraTab.XtraTabPage();
            this.panel치수검사 = new DevExpress.XtraEditors.PanelControl();
            this.p유무검사 = new DevExpress.XtraTab.XtraTabPage();
            this.panel유무검사 = new DevExpress.XtraEditors.PanelControl();
            this.p표면검사_앞 = new DevExpress.XtraTab.XtraTabPage();
            this.panel표면검사_앞 = new DevExpress.XtraEditors.PanelControl();
            this.p표면검사_뒤 = new DevExpress.XtraTab.XtraTabPage();
            this.panel표면검사_뒤 = new DevExpress.XtraEditors.PanelControl();
            this.state1 = new SamhwaInspection.UI.Control.State();
            this.p검사현황 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer2 = new DevExpress.XtraBars.TabFormContentContainer();
            this.resultList1 = new SamhwaInspection.UI.Control.ResultList();
            this.p환경설정 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer3 = new DevExpress.XtraBars.TabFormContentContainer();
            this.settings1 = new SamhwaInspection.UI.Settings();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).BeginInit();
            this.tabFormContentContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tc검사디스플레이)).BeginInit();
            this.tc검사디스플레이.SuspendLayout();
            this.p치수검사.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel치수검사)).BeginInit();
            this.p유무검사.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel유무검사)).BeginInit();
            this.p표면검사_앞.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel표면검사_앞)).BeginInit();
            this.p표면검사_뒤.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel표면검사_뒤)).BeginInit();
            this.tabFormContentContainer2.SuspendLayout();
            this.tabFormContentContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFormControl1
            // 
            this.tabFormControl1.AllowMoveTabs = false;
            this.tabFormControl1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.barStaticItem2,
            this.skinPaletteDropDownButtonItem1});
            this.tabFormControl1.Location = new System.Drawing.Point(0, 0);
            this.tabFormControl1.Name = "tabFormControl1";
            this.tabFormControl1.Pages.Add(this.p비전검사);
            this.tabFormControl1.Pages.Add(this.p검사현황);
            this.tabFormControl1.Pages.Add(this.p환경설정);
            this.tabFormControl1.SelectedPage = this.p비전검사;
            this.tabFormControl1.ShowAddPageButton = false;
            this.tabFormControl1.ShowTabCloseButtons = false;
            this.tabFormControl1.ShowTabsInTitleBar = DevExpress.XtraBars.ShowTabsInTitleBar.True;
            this.tabFormControl1.Size = new System.Drawing.Size(1920, 30);
            this.tabFormControl1.TabForm = this;
            this.tabFormControl1.TabIndex = 0;
            this.tabFormControl1.TabLeftItemLinks.Add(this.barStaticItem1);
            this.tabFormControl1.TabRightItemLinks.Add(this.barStaticItem2);
            this.tabFormControl1.TabRightItemLinks.Add(this.skinPaletteDropDownButtonItem1);
            this.tabFormControl1.TabStop = false;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "BusBar Inspection";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barStaticItem1.ImageOptions.SvgImage")));
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "IVM: 23-0331-001";
            this.barStaticItem2.Id = 0;
            this.barStaticItem2.Name = "barStaticItem2";
            // 
            // skinPaletteDropDownButtonItem1
            // 
            this.skinPaletteDropDownButtonItem1.Id = 1;
            this.skinPaletteDropDownButtonItem1.Name = "skinPaletteDropDownButtonItem1";
            // 
            // p비전검사
            // 
            this.p비전검사.ContentContainer = this.tabFormContentContainer1;
            this.p비전검사.Name = "p비전검사";
            this.p비전검사.Text = "비전검사";
            // 
            // tabFormContentContainer1
            // 
            this.tabFormContentContainer1.Controls.Add(this.tc검사디스플레이);
            this.tabFormContentContainer1.Controls.Add(this.state1);
            this.tabFormContentContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer1.Location = new System.Drawing.Point(0, 30);
            this.tabFormContentContainer1.Name = "tabFormContentContainer1";
            this.tabFormContentContainer1.Size = new System.Drawing.Size(1920, 1010);
            this.tabFormContentContainer1.TabIndex = 1;
            // 
            // tc검사디스플레이
            // 
            this.tc검사디스플레이.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc검사디스플레이.Location = new System.Drawing.Point(0, 104);
            this.tc검사디스플레이.Name = "tc검사디스플레이";
            this.tc검사디스플레이.SelectedTabPage = this.p치수검사;
            this.tc검사디스플레이.Size = new System.Drawing.Size(1920, 906);
            this.tc검사디스플레이.TabIndex = 2;
            this.tc검사디스플레이.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.p치수검사,
            this.p유무검사,
            this.p표면검사_앞,
            this.p표면검사_뒤});
            // 
            // p치수검사
            // 
            this.p치수검사.Controls.Add(this.panel치수검사);
            this.p치수검사.Name = "p치수검사";
            this.p치수검사.Size = new System.Drawing.Size(1918, 875);
            this.p치수검사.Text = "치수검사";
            // 
            // panel치수검사
            // 
            this.panel치수검사.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel치수검사.Appearance.Options.UseBackColor = true;
            this.panel치수검사.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel치수검사.Location = new System.Drawing.Point(0, 0);
            this.panel치수검사.Name = "panel치수검사";
            this.panel치수검사.Size = new System.Drawing.Size(1918, 875);
            this.panel치수검사.TabIndex = 1;
            // 
            // p유무검사
            // 
            this.p유무검사.Controls.Add(this.panel유무검사);
            this.p유무검사.Name = "p유무검사";
            this.p유무검사.Size = new System.Drawing.Size(1918, 875);
            this.p유무검사.Text = "유무검사";
            // 
            // panel유무검사
            // 
            this.panel유무검사.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel유무검사.Appearance.Options.UseBackColor = true;
            this.panel유무검사.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel유무검사.Location = new System.Drawing.Point(0, 0);
            this.panel유무검사.Name = "panel유무검사";
            this.panel유무검사.Size = new System.Drawing.Size(1918, 875);
            this.panel유무검사.TabIndex = 2;
            // 
            // p표면검사_앞
            // 
            this.p표면검사_앞.Controls.Add(this.panel표면검사_앞);
            this.p표면검사_앞.Name = "p표면검사_앞";
            this.p표면검사_앞.Size = new System.Drawing.Size(1918, 875);
            this.p표면검사_앞.Text = "표면검사 [앞면]";
            // 
            // panel표면검사_앞
            // 
            this.panel표면검사_앞.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel표면검사_앞.Appearance.Options.UseBackColor = true;
            this.panel표면검사_앞.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel표면검사_앞.Location = new System.Drawing.Point(0, 0);
            this.panel표면검사_앞.Name = "panel표면검사_앞";
            this.panel표면검사_앞.Size = new System.Drawing.Size(1918, 875);
            this.panel표면검사_앞.TabIndex = 2;
            // 
            // p표면검사_뒤
            // 
            this.p표면검사_뒤.Controls.Add(this.panel표면검사_뒤);
            this.p표면검사_뒤.Name = "p표면검사_뒤";
            this.p표면검사_뒤.Size = new System.Drawing.Size(1918, 875);
            this.p표면검사_뒤.Text = "표면검사 [뒷면]";
            // 
            // panel표면검사_뒤
            // 
            this.panel표면검사_뒤.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel표면검사_뒤.Appearance.Options.UseBackColor = true;
            this.panel표면검사_뒤.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel표면검사_뒤.Location = new System.Drawing.Point(0, 0);
            this.panel표면검사_뒤.Name = "panel표면검사_뒤";
            this.panel표면검사_뒤.Size = new System.Drawing.Size(1918, 875);
            this.panel표면검사_뒤.TabIndex = 3;
            // 
            // state1
            // 
            this.state1.Dock = System.Windows.Forms.DockStyle.Top;
            this.state1.Location = new System.Drawing.Point(0, 0);
            this.state1.Name = "state1";
            this.state1.Size = new System.Drawing.Size(1920, 104);
            this.state1.TabIndex = 0;
            // 
            // p검사현황
            // 
            this.p검사현황.ContentContainer = this.tabFormContentContainer2;
            this.p검사현황.Name = "p검사현황";
            this.p검사현황.Text = "검사현황";
            this.p검사현황.Visible = false;
            // 
            // tabFormContentContainer2
            // 
            this.tabFormContentContainer2.Controls.Add(this.resultList1);
            this.tabFormContentContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer2.Location = new System.Drawing.Point(0, 30);
            this.tabFormContentContainer2.Name = "tabFormContentContainer2";
            this.tabFormContentContainer2.Size = new System.Drawing.Size(1920, 1010);
            this.tabFormContentContainer2.TabIndex = 2;
            // 
            // resultList1
            // 
            this.resultList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultList1.Location = new System.Drawing.Point(0, 0);
            this.resultList1.Name = "resultList1";
            this.resultList1.Size = new System.Drawing.Size(1920, 1010);
            this.resultList1.TabIndex = 0;
            // 
            // p환경설정
            // 
            this.p환경설정.ContentContainer = this.tabFormContentContainer3;
            this.p환경설정.Name = "p환경설정";
            this.p환경설정.Text = "환경설정";
            // 
            // tabFormContentContainer3
            // 
            this.tabFormContentContainer3.Controls.Add(this.settings1);
            this.tabFormContentContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer3.Location = new System.Drawing.Point(0, 30);
            this.tabFormContentContainer3.Name = "tabFormContentContainer3";
            this.tabFormContentContainer3.Size = new System.Drawing.Size(1920, 1010);
            this.tabFormContentContainer3.TabIndex = 3;
            // 
            // settings1
            // 
            this.settings1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settings1.Location = new System.Drawing.Point(0, 0);
            this.settings1.Name = "settings1";
            this.settings1.Size = new System.Drawing.Size(1920, 1010);
            this.settings1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1040);
            this.Controls.Add(this.tabFormContentContainer1);
            this.Controls.Add(this.tabFormControl1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("MainForm.IconOptions.Image")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabFormControl = this.tabFormControl1;
            this.Text = "Samhwa";
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).EndInit();
            this.tabFormContentContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tc검사디스플레이)).EndInit();
            this.tc검사디스플레이.ResumeLayout(false);
            this.p치수검사.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel치수검사)).EndInit();
            this.p유무검사.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel유무검사)).EndInit();
            this.p표면검사_앞.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel표면검사_앞)).EndInit();
            this.p표면검사_뒤.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel표면검사_뒤)).EndInit();
            this.tabFormContentContainer2.ResumeLayout(false);
            this.tabFormContentContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.TabFormControl tabFormControl1;
        private DevExpress.XtraBars.TabFormPage p비전검사;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.TabFormPage p검사현황;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer2;
        private DevExpress.XtraBars.TabFormPage p환경설정;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer3;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.SkinPaletteDropDownButtonItem skinPaletteDropDownButtonItem1;
        private UI.Control.State state1;
        private DevExpress.XtraEditors.PanelControl panel치수검사;
        private UI.Settings settings1;
        private UI.Control.ResultList resultList1;
        private DevExpress.XtraTab.XtraTabControl tc검사디스플레이;
        private DevExpress.XtraTab.XtraTabPage p치수검사;
        private DevExpress.XtraTab.XtraTabPage p유무검사;
        private DevExpress.XtraEditors.PanelControl panel유무검사;
        private DevExpress.XtraTab.XtraTabPage p표면검사_앞;
        private DevExpress.XtraEditors.PanelControl panel표면검사_앞;
        private DevExpress.XtraTab.XtraTabPage p표면검사_뒤;
        private DevExpress.XtraEditors.PanelControl panel표면검사_뒤;
    }
}