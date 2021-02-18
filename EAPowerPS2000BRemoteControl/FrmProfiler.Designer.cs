namespace EAPowerPS2000BRemoteControl
{
    partial class FrmProfiler
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRun = new System.Windows.Forms.Button();
            this.chkLoop = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsLblIInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsLblCurrTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCurrentCurrent = new System.Windows.Forms.TextBox();
            this.txtCurrentVoltage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.RelativeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Voltage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Current = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AbsoluteTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 389);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profile";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RelativeTime,
            this.Voltage,
            this.Current,
            this.AbsoluteTime});
            this.dataGridView1.Location = new System.Drawing.Point(7, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(763, 363);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 407);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // chkLoop
            // 
            this.chkLoop.AutoSize = true;
            this.chkLoop.Location = new System.Drawing.Point(93, 411);
            this.chkLoop.Name = "chkLoop";
            this.chkLoop.Size = new System.Drawing.Size(50, 17);
            this.chkLoop.TabIndex = 1;
            this.chkLoop.Text = "Loop";
            this.chkLoop.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsLblIInfo,
            this.stsLblCurrTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 493);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsLblIInfo
            // 
            this.stsLblIInfo.Name = "stsLblIInfo";
            this.stsLblIInfo.Size = new System.Drawing.Size(118, 17);
            this.stsLblIInfo.Text = "toolStripStatusLabel1";
            // 
            // stsLblCurrTime
            // 
            this.stsLblCurrTime.Name = "stsLblCurrTime";
            this.stsLblCurrTime.Size = new System.Drawing.Size(118, 17);
            this.stsLblCurrTime.Text = "toolStripStatusLabel1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCurrentCurrent);
            this.groupBox2.Controls.Add(this.txtCurrentVoltage);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(377, 407);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 72);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current PSU output";
            // 
            // txtCurrentCurrent
            // 
            this.txtCurrentCurrent.Location = new System.Drawing.Point(70, 39);
            this.txtCurrentCurrent.Name = "txtCurrentCurrent";
            this.txtCurrentCurrent.ReadOnly = true;
            this.txtCurrentCurrent.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentCurrent.TabIndex = 1;
            // 
            // txtCurrentVoltage
            // 
            this.txtCurrentVoltage.Location = new System.Drawing.Point(70, 17);
            this.txtCurrentVoltage.Name = "txtCurrentVoltage";
            this.txtCurrentVoltage.ReadOnly = true;
            this.txtCurrentVoltage.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentVoltage.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Current:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Voltage:";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 436);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // RelativeTime
            // 
            this.RelativeTime.HeaderText = "Duration [min]";
            this.RelativeTime.Name = "RelativeTime";
            // 
            // Voltage
            // 
            this.Voltage.HeaderText = "Voltage [V]";
            this.Voltage.Name = "Voltage";
            // 
            // Current
            // 
            this.Current.HeaderText = "Current [A]";
            this.Current.Name = "Current";
            // 
            // AbsoluteTime
            // 
            this.AbsoluteTime.HeaderText = "Absolute Time";
            this.AbsoluteTime.Name = "AbsoluteTime";
            this.AbsoluteTime.ReadOnly = true;
            this.AbsoluteTime.Width = 200;
            // 
            // FrmProfiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 515);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkLoop);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmProfiler";
            this.Text = "FrmProfiler";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.CheckBox chkLoop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsLblIInfo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCurrentCurrent;
        private System.Windows.Forms.TextBox txtCurrentVoltage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolStripStatusLabel stsLblCurrTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelativeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Voltage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Current;
        private System.Windows.Forms.DataGridViewTextBoxColumn AbsoluteTime;
    }
}