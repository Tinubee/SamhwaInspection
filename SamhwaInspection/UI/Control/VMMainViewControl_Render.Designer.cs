namespace SamhwaInspection.UI
{
    partial class VMMainViewControl_Render
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
            this.vmMainViewConfigControl1 = new VMControls.Winform.Release.VmMainViewConfigControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.vmGlobalToolControl1 = new VMControls.Winform.Release.VmGlobalToolControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // vmMainViewConfigControl2
            // 
            this.vmMainViewConfigControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmMainViewConfigControl1.Location = new System.Drawing.Point(2, 42);
            this.vmMainViewConfigControl1.Margin = new System.Windows.Forms.Padding(2);
            this.vmMainViewConfigControl1.Name = "vmMainViewConfigControl1";
            this.vmMainViewConfigControl1.Size = new System.Drawing.Size(1005, 762);
            this.vmMainViewConfigControl1.TabIndex = 0;
// TODO: '기본 형식이 잘못되었습니다. System.IntPtr. CodeObjectCreateExpression을 사용하십시오.' 예외가 발생하여 ''의 코드를 생성하지 못했습니다.
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.vmMainViewConfigControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.vmGlobalToolControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1009, 806);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // vmGlobalToolControl2
            // 
            this.vmGlobalToolControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vmGlobalToolControl1.Location = new System.Drawing.Point(4, 3);
            this.vmGlobalToolControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.vmGlobalToolControl1.Name = "vmGlobalToolControl1";
            this.vmGlobalToolControl1.Size = new System.Drawing.Size(1001, 34);
            this.vmGlobalToolControl1.TabIndex = 1;
            // 
            // VMMainViewControl_Render
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VMMainViewControl_Render";
            this.Size = new System.Drawing.Size(1009, 806);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VMControls.Winform.Release.VmMainViewConfigControl vmMainViewConfigControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private VMControls.Winform.Release.VmGlobalToolControl vmGlobalToolControl1;
    }
}
