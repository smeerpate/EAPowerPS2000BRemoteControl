
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtActVoltage = new System.Windows.Forms.TextBox();
            this.txtActCurrent = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdateActual = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTargetVoltage = new System.Windows.Forms.TextBox();
            this.txtTargetCurrent = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.btnUpdateActual);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtActCurrent);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtActVoltage);
            this.groupBox1.Location = new System.Drawing.Point(257, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 199);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actual Values";
            // 
            // btnUpdateActual
            // 
            this.btnUpdateActual.Location = new System.Drawing.Point(61, 133);
            this.btnUpdateActual.Name = "btnUpdateActual";
            this.btnUpdateActual.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateActual.TabIndex = 4;
            this.btnUpdateActual.Text = "Update";
            this.btnUpdateActual.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTargetCurrent);
            this.groupBox2.Controls.Add(this.txtTargetVoltage);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 192);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target Values";
            // 
            // txtTargetVoltage
            // 
            this.txtTargetVoltage.Location = new System.Drawing.Point(126, 19);
            this.txtTargetVoltage.Name = "txtTargetVoltage";
            this.txtTargetVoltage.Size = new System.Drawing.Size(100, 20);
            this.txtTargetVoltage.TabIndex = 2;
            // 
            // txtTargetCurrent
            // 
            this.txtTargetCurrent.Location = new System.Drawing.Point(126, 45);
            this.txtTargetCurrent.Name = "txtTargetCurrent";
            this.txtTargetCurrent.Size = new System.Drawing.Size(100, 20);
            this.txtTargetCurrent.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 405);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

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
    }
}

