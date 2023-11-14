namespace SamhwaInspection.UI
{
    partial class Settings
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.e기본설정 = new SamhwaInspection.UI.Control.BaseConfig();
            this.eIO컨트롤 = new SamhwaInspection.UI.Control.IOControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.e모델설정 = new SamhwaInspection.UI.Control.Models();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.e변수설정 = new SamhwaInspection.UI.Control.SetVariables();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.e마스터설정 = new SamhwaInspection.UI.Control.MasterSetting();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.e마스터데이터 = new SamhwaInspection.UI.Control.MasterData();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.xtraTabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            this.xtraTabControl1.Size = new System.Drawing.Size(1816, 996);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4,
            this.xtraTabPage5});
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.splitContainerControl1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1814, 965);
            this.xtraTabPage2.Text = "환경설정";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.e기본설정);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.eIO컨트롤);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1814, 965);
            this.splitContainerControl1.SplitterPosition = 829;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // e기본설정
            // 
            this.e기본설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e기본설정.Location = new System.Drawing.Point(0, 0);
            this.e기본설정.Name = "e기본설정";
            this.e기본설정.Size = new System.Drawing.Size(829, 965);
            this.e기본설정.TabIndex = 0;
            // 
            // eIO컨트롤
            // 
            this.eIO컨트롤.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eIO컨트롤.Location = new System.Drawing.Point(0, 0);
            this.eIO컨트롤.Name = "eIO컨트롤";
            this.eIO컨트롤.Size = new System.Drawing.Size(975, 965);
            this.eIO컨트롤.TabIndex = 0;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.e모델설정);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.PageVisible = false;
            this.xtraTabPage1.Size = new System.Drawing.Size(1814, 965);
            this.xtraTabPage1.Text = "모델설정";
            // 
            // e모델설정
            // 
            this.e모델설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e모델설정.Location = new System.Drawing.Point(0, 0);
            this.e모델설정.Name = "e모델설정";
            this.e모델설정.Size = new System.Drawing.Size(1814, 965);
            this.e모델설정.TabIndex = 0;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.e변수설정);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1814, 965);
            this.xtraTabPage3.Text = "VM설정";
            // 
            // e변수설정
            // 
            this.e변수설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e변수설정.Location = new System.Drawing.Point(0, 0);
            this.e변수설정.Name = "e변수설정";
            this.e변수설정.Size = new System.Drawing.Size(1814, 965);
            this.e변수설정.TabIndex = 0;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.e마스터설정);
            this.xtraTabPage4.Controls.Add(this.barDockControlLeft);
            this.xtraTabPage4.Controls.Add(this.barDockControlRight);
            this.xtraTabPage4.Controls.Add(this.barDockControlBottom);
            this.xtraTabPage4.Controls.Add(this.barDockControlTop);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1814, 965);
            this.xtraTabPage4.Text = "보정값설정";
            // 
            // e마스터설정
            // 
            this.e마스터설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e마스터설정.Location = new System.Drawing.Point(0, 0);
            this.e마스터설정.Name = "e마스터설정";
            this.e마스터설정.Size = new System.Drawing.Size(1814, 965);
            this.e마스터설정.TabIndex = 4;
            this.e마스터설정.위치 = SamhwaInspection.UI.Control.MasterSetting.지그위치.Front;
            this.e마스터설정.플로우 = SamhwaInspection.Schemas.Flow구분.Flow1;
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = null;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 965);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1814, 0);
            this.barDockControlRight.Manager = null;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 965);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 965);
            this.barDockControlBottom.Manager = null;
            this.barDockControlBottom.Size = new System.Drawing.Size(1814, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = null;
            this.barDockControlTop.Size = new System.Drawing.Size(1814, 0);
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.e마스터데이터);
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(1814, 965);
            this.xtraTabPage5.Text = "마스터데이터";
            // 
            // e마스터데이터
            // 
            this.e마스터데이터.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e마스터데이터.Location = new System.Drawing.Point(0, 0);
            this.e마스터데이터.Name = "e마스터데이터";
            this.e마스터데이터.Size = new System.Drawing.Size(1814, 965);
            this.e마스터데이터.TabIndex = 0;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(1816, 996);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage4.PerformLayout();
            this.xtraTabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Control.IOControl eIO컨트롤;
        private Control.Models e모델설정;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private Control.SetVariables e변수설정;
        private Control.BaseConfig e기본설정;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private Control.MasterSetting e마스터설정;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private Control.MasterData e마스터데이터;
    }
}
