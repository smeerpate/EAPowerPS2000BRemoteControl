
namespace EAPowerPS2000BRemoteControl
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtActVoltage = new System.Windows.Forms.TextBox();
            this.txtActCurrent = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdateActual = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetTarget = new System.Windows.Forms.Button();
            this.txtTargetCurrent = new System.Windows.Forms.TextBox();
            this.txtTargetVoltage = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtComPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusStr1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.chkOutputEnabled = new System.Windows.Forms.CheckBox();
            this.chkERemoteEnabled = new System.Windows.Forms.CheckBox();
            this.rbRemoteEnabled = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Actual Voltage";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Actual Current";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Target Current";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Target Voltage";
            // 
            // txtActVoltage
            // 
            this.txtActVoltage.Location = new System.Drawing.Point(111, 19);
            this.txtActVoltage.Name = "txtActVoltage";
            this.txtActVoltage.Size = new System.Drawing.Size(100, 20);
            this.txtActVoltage.TabIndex = 2;
            // 
            // txtActCurrent
            // 
            this.txtActCurrent.Location = new System.Drawing.Point(111, 45);
            this.txtActCurrent.Name = "txtActCurrent";
            this.txtActCurrent.Size = new System.Drawing.Size(100, 20);
            this.txtActCurrent.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbRemoteEnabled);
            this.groupBox1.Controls.Add(this.btnUpdateActual);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtActCurrent);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtActVoltage);
            this.groupBox1.Location = new System.Drawing.Point(257, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 146);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actual Values";
            // 
            // btnUpdateActual
            // 
            this.btnUpdateActual.Location = new System.Drawing.Point(142, 117);
            this.btnUpdateActual.Name = "btnUpdateActual";
            this.btnUpdateActual.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateActual.TabIndex = 4;
            this.btnUpdateActual.Text = "Update";
            this.btnUpdateActual.UseVisualStyleBackColor = true;
            this.btnUpdateActual.Click += new System.EventHandler(this.btnUpdateActual_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkERemoteEnabled);
            this.groupBox2.Controls.Add(this.chkOutputEnabled);
            this.groupBox2.Controls.Add(this.btnSetTarget);
            this.groupBox2.Controls.Add(this.txtTargetCurrent);
            this.groupBox2.Controls.Add(this.txtTargetVoltage);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 146);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target Values";
            // 
            // btnSetTarget
            // 
            this.btnSetTarget.Location = new System.Drawing.Point(158, 71);
            this.btnSetTarget.Name = "btnSetTarget";
            this.btnSetTarget.Size = new System.Drawing.Size(75, 23);
            this.btnSetTarget.TabIndex = 3;
            this.btnSetTarget.Text = "Set";
            this.btnSetTarget.UseVisualStyleBackColor = true;
            this.btnSetTarget.Click += new System.EventHandler(this.btnSetTarget_Click);
            // 
            // txtTargetCurrent
            // 
            this.txtTargetCurrent.Location = new System.Drawing.Point(126, 45);
            this.txtTargetCurrent.Name = "txtTargetCurrent";
            this.txtTargetCurrent.Size = new System.Drawing.Size(100, 20);
            this.txtTargetCurrent.TabIndex = 2;
            // 
            // txtTargetVoltage
            // 
            this.txtTargetVoltage.Location = new System.Drawing.Point(126, 19);
            this.txtTargetVoltage.Name = "txtTargetVoltage";
            this.txtTargetVoltage.Size = new System.Drawing.Size(100, 20);
            this.txtTargetVoltage.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDisconnect);
            this.groupBox3.Controls.Add(this.btnConnect);
            this.groupBox3.Controls.Add(this.txtComPort);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(264, 165);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(223, 76);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Communication";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(9, 47);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtComPort
            // 
            this.txtComPort.Location = new System.Drawing.Point(94, 19);
            this.txtComPort.Name = "txtComPort";
            this.txtComPort.Size = new System.Drawing.Size(100, 20);
            this.txtComPort.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "COM Port";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusStr1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 262);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(499, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusStr1
            // 
            this.lblStatusStr1.Name = "lblStatusStr1";
            this.lblStatusStr1.Size = new System.Drawing.Size(118, 17);
            this.lblStatusStr1.Text = "toolStripStatusLabel1";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(109, 47);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 2;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisonnect_Click);
            // 
            // chkOutputEnabled
            // 
            this.chkOutputEnabled.AutoSize = true;
            this.chkOutputEnabled.Location = new System.Drawing.Point(6, 123);
            this.chkOutputEnabled.Name = "chkOutputEnabled";
            this.chkOutputEnabled.Size = new System.Drawing.Size(100, 17);
            this.chkOutputEnabled.TabIndex = 4;
            this.chkOutputEnabled.Text = "Output Enabled";
            this.chkOutputEnabled.UseVisualStyleBackColor = true;
            // 
            // chkERemoteEnabled
            // 
            this.chkERemoteEnabled.AutoSize = true;
            this.chkERemoteEnabled.Location = new System.Drawing.Point(6, 100);
            this.chkERemoteEnabled.Name = "chkERemoteEnabled";
            this.chkERemoteEnabled.Size = new System.Drawing.Size(105, 17);
            this.chkERemoteEnabled.TabIndex = 5;
            this.chkERemoteEnabled.Text = "Remote Enabled";
            this.chkERemoteEnabled.UseVisualStyleBackColor = true;
            this.chkERemoteEnabled.CheckedChanged += new System.EventHandler(this.chkERemoteEnabled_CheckedChanged);
            // 
            // rbRemoteEnabled
            // 
            this.rbRemoteEnabled.AutoSize = true;
            this.rbRemoteEnabled.Location = new System.Drawing.Point(34, 77);
            this.rbRemoteEnabled.Name = "rbRemoteEnabled";
            this.rbRemoteEnabled.Size = new System.Drawing.Size(104, 17);
            this.rbRemoteEnabled.TabIndex = 5;
            this.rbRemoteEnabled.TabStop = true;
            this.rbRemoteEnabled.Text = "Remote Enabled";
            this.rbRemoteEnabled.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 284);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtActVoltage;
        private System.Windows.Forms.TextBox txtActCurrent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUpdateActual;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTargetCurrent;
        private System.Windows.Forms.TextBox txtTargetVoltage;
        private System.Windows.Forms.Button btnSetTarget;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtComPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusStr1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.CheckBox chkOutputEnabled;
        private System.Windows.Forms.CheckBox chkERemoteEnabled;
        private System.Windows.Forms.RadioButton rbRemoteEnabled;
    }
}

