namespace SamhwaInspection.UI.Control
{
    partial class State
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.e저장용량 = new DevExpress.XtraEditors.ProgressBarControl();
            this.b수량리셋 = new DevExpress.XtraEditors.SimpleButton();
            this.lbl판정결과 = new DevExpress.XtraEditors.LabelControl();
            this.b운전상태 = new DevExpress.XtraEditors.SimpleButton();
            this.b운전모드 = new DevExpress.XtraEditors.SimpleButton();
            this.e모델선택 = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ciView1 = new SamhwaInspection.UI.Control.CiView();
            this.환경설정BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.e양품수율 = new SamhwaInspection.UI.Control.CountViewer();
            this.e전체수량 = new SamhwaInspection.UI.Control.CountViewer();
            this.e불량수량 = new SamhwaInspection.UI.Control.CountViewer();
            this.e양품수량 = new SamhwaInspection.UI.Control.CountViewer();
            this.모델자료BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.titleView1 = new SamhwaInspection.UI.Control.TitleView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e저장용량.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e모델선택.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.환경설정BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.모델자료BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ciView1);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Controls.Add(this.b수량리셋);
            this.layoutControl1.Controls.Add(this.e양품수율);
            this.layoutControl1.Controls.Add(this.e전체수량);
            this.layoutControl1.Controls.Add(this.e불량수량);
            this.layoutControl1.Controls.Add(this.e양품수량);
            this.layoutControl1.Controls.Add(this.lbl판정결과);
            this.layoutControl1.Controls.Add(this.b운전상태);
            this.layoutControl1.Controls.Add(this.b운전모드);
            this.layoutControl1.Controls.Add(this.e모델선택);
            this.layoutControl1.Controls.Add(this.titleView1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1920, 104);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 9.5F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.e저장용량);
            this.groupControl1.Location = new System.Drawing.Point(1555, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(120, 94);
            this.groupControl1.TabIndex = 17;
            this.groupControl1.Text = "Disk Usage";
            // 
            // e저장용량
            // 
            this.e저장용량.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.환경설정BindingSource, "저장비율", true));
            this.e저장용량.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e저장용량.EditValue = 50;
            this.e저장용량.Location = new System.Drawing.Point(2, 27);
            this.e저장용량.Name = "e저장용량";
            this.e저장용량.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.e저장용량.Properties.ShowTitle = true;
            this.e저장용량.Size = new System.Drawing.Size(116, 65);
            this.e저장용량.TabIndex = 0;
            // 
            // b수량리셋
            // 
            this.b수량리셋.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.b수량리셋.Appearance.Options.UseFont = true;
            this.b수량리셋.Appearance.Options.UseTextOptions = true;
            this.b수량리셋.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.b수량리셋.Location = new System.Drawing.Point(1466, 5);
            this.b수량리셋.Name = "b수량리셋";
            this.b수량리셋.Size = new System.Drawing.Size(85, 94);
            this.b수량리셋.StyleController = this.layoutControl1;
            this.b수량리셋.TabIndex = 16;
            this.b수량리셋.Text = "Count Reset";
            // 
            // lbl판정결과
            // 
            this.lbl판정결과.Appearance.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl판정결과.Appearance.Options.UseFont = true;
            this.lbl판정결과.Appearance.Options.UseTextOptions = true;
            this.lbl판정결과.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl판정결과.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lbl판정결과.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lbl판정결과.Location = new System.Drawing.Point(698, 5);
            this.lbl판정결과.Name = "lbl판정결과";
            this.lbl판정결과.Size = new System.Drawing.Size(297, 94);
            this.lbl판정결과.StyleController = this.layoutControl1;
            this.lbl판정결과.TabIndex = 11;
            this.lbl판정결과.Text = "OK";
            // 
            // b운전상태
            // 
            this.b운전상태.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.b운전상태.Appearance.Options.UseFont = true;
            this.b운전상태.Location = new System.Drawing.Point(219, 5);
            this.b운전상태.Name = "b운전상태";
            this.b운전상태.Size = new System.Drawing.Size(132, 94);
            this.b운전상태.StyleController = this.layoutControl1;
            this.b운전상태.TabIndex = 10;
            this.b운전상태.Text = "Stop";
            // 
            // b운전모드
            // 
            this.b운전모드.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.b운전모드.Appearance.Options.UseFont = true;
            this.b운전모드.Location = new System.Drawing.Point(355, 5);
            this.b운전모드.Name = "b운전모드";
            this.b운전모드.Size = new System.Drawing.Size(135, 94);
            this.b운전모드.StyleController = this.layoutControl1;
            this.b운전모드.TabIndex = 9;
            this.b운전모드.Text = "Manual";
            // 
            // e모델선택
            // 
            this.e모델선택.Location = new System.Drawing.Point(494, 5);
            this.e모델선택.Name = "e모델선택";
            this.e모델선택.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18.25F, System.Drawing.FontStyle.Bold);
            this.e모델선택.Properties.Appearance.Options.UseFont = true;
            this.e모델선택.Properties.Appearance.Options.UseTextOptions = true;
            this.e모델선택.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e모델선택.Properties.AutoHeight = false;
            this.e모델선택.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e모델선택.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("모델이름", "모델이름", 61, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.e모델선택.Properties.DataSource = this.모델자료BindingSource;
            this.e모델선택.Properties.DisplayMember = "모델이름";
            this.e모델선택.Properties.NullText = "[모델 선택]";
            this.e모델선택.Properties.ValueMember = "모델번호";
            this.e모델선택.Size = new System.Drawing.Size(200, 94);
            this.e모델선택.StyleController = this.layoutControl1;
            this.e모델선택.TabIndex = 8;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem13});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.Root.Size = new System.Drawing.Size(1920, 104);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.e모델선택;
            this.layoutControlItem2.Location = new System.Drawing.Point(489, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(54, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(204, 98);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.b운전모드;
            this.layoutControlItem3.Location = new System.Drawing.Point(350, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(139, 98);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.b운전상태;
            this.layoutControlItem4.Location = new System.Drawing.Point(214, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(136, 98);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lbl판정결과;
            this.layoutControlItem5.Location = new System.Drawing.Point(693, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(76, 19);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(301, 98);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.b수량리셋;
            this.layoutControlItem10.Location = new System.Drawing.Point(1461, 0);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(89, 98);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.groupControl1;
            this.layoutControlItem11.Location = new System.Drawing.Point(1550, 0);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(124, 98);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // ciView1
            // 
            this.ciView1.Location = new System.Drawing.Point(1679, 5);
            this.ciView1.Name = "ciView1";
            this.ciView1.Size = new System.Drawing.Size(236, 94);
            this.ciView1.TabIndex = 19;
            // 
            // 환경설정BindingSource
            // 
            this.환경설정BindingSource.DataSource = typeof(SamhwaInspection.Schemas.환경설정);
            // 
            // e양품수율
            // 
            this.e양품수율.BaseColor = System.Drawing.Color.Empty;
            this.e양품수율.Caption = "양품률(%)";
            this.e양품수율.DataBindings.Add(new System.Windows.Forms.Binding("ValueText", this.환경설정BindingSource, "양품수율표현", true));
            this.e양품수율.Location = new System.Drawing.Point(1347, 5);
            this.e양품수율.Name = "e양품수율";
            this.e양품수율.Size = new System.Drawing.Size(115, 94);
            this.e양품수율.TabIndex = 15;
            this.e양품수율.ValueText = "100.0";
            // 
            // e전체수량
            // 
            this.e전체수량.BaseColor = System.Drawing.Color.Empty;
            this.e전체수량.Caption = "전체수량";
            this.e전체수량.DataBindings.Add(new System.Windows.Forms.Binding("ValueText", this.환경설정BindingSource, "전체갯수", true));
            this.e전체수량.Location = new System.Drawing.Point(1232, 5);
            this.e전체수량.Name = "e전체수량";
            this.e전체수량.Size = new System.Drawing.Size(111, 94);
            this.e전체수량.TabIndex = 14;
            this.e전체수량.ValueText = "100.0";
            // 
            // e불량수량
            // 
            this.e불량수량.BaseColor = System.Drawing.Color.Empty;
            this.e불량수량.Caption = "불량";
            this.e불량수량.DataBindings.Add(new System.Windows.Forms.Binding("ValueText", this.환경설정BindingSource, "불량갯수표현", true));
            this.e불량수량.Location = new System.Drawing.Point(1116, 5);
            this.e불량수량.Name = "e불량수량";
            this.e불량수량.Size = new System.Drawing.Size(112, 94);
            this.e불량수량.TabIndex = 13;
            this.e불량수량.ValueText = "100.0";
            // 
            // e양품수량
            // 
            this.e양품수량.BaseColor = System.Drawing.Color.Empty;
            this.e양품수량.Caption = "양품";
            this.e양품수량.DataBindings.Add(new System.Windows.Forms.Binding("ValueText", this.환경설정BindingSource, "양품갯수표현", true));
            this.e양품수량.Location = new System.Drawing.Point(999, 5);
            this.e양품수량.Name = "e양품수량";
            this.e양품수량.Size = new System.Drawing.Size(113, 94);
            this.e양품수량.TabIndex = 12;
            this.e양품수량.ValueText = "100.0";
            // 
            // 모델자료BindingSource
            // 
            this.모델자료BindingSource.DataSource = typeof(SamhwaInspection.Schemas.모델자료);
            // 
            // titleView1
            // 
            this.titleView1.Location = new System.Drawing.Point(5, 5);
            this.titleView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.titleView1.Name = "titleView1";
            this.titleView1.Size = new System.Drawing.Size(210, 94);
            this.titleView1.TabIndex = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.titleView1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(48, 48);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(214, 98);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.e양품수량;
            this.layoutControlItem6.Location = new System.Drawing.Point(994, 0);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(35, 70);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(117, 98);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.e불량수량;
            this.layoutControlItem7.Location = new System.Drawing.Point(1111, 0);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(35, 70);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(116, 98);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.e전체수량;
            this.layoutControlItem8.Location = new System.Drawing.Point(1227, 0);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(35, 70);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(115, 98);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.e양품수율;
            this.layoutControlItem9.Location = new System.Drawing.Point(1342, 0);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(35, 70);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(119, 98);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.ciView1;
            this.layoutControlItem13.Location = new System.Drawing.Point(1674, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(240, 98);
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextVisible = false;
            // 
            // State
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "State";
            this.Size = new System.Drawing.Size(1920, 104);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e저장용량.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e모델선택.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.환경설정BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.모델자료BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private TitleView titleView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton b운전상태;
        private DevExpress.XtraEditors.SimpleButton b운전모드;
        private DevExpress.XtraEditors.LookUpEdit e모델선택;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LabelControl lbl판정결과;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private CountViewer e양품수율;
        private CountViewer e전체수량;
        private CountViewer e불량수량;
        private CountViewer e양품수량;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton b수량리셋;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.ProgressBarControl e저장용량;
        private System.Windows.Forms.BindingSource 모델자료BindingSource;
        private System.Windows.Forms.BindingSource 환경설정BindingSource;
        private CiView ciView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
    }
}
