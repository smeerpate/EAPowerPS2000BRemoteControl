using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAPowerPS2000BRemoteControl
{
    public partial class FrmProfiler : Form
    {
        public DateTime sStartTime { get; set; }
        public DateTime sCurrentTime { get; set; }

        public FrmProfiler()
        {
            InitializeComponent();
            timer1.Interval = 1000;

            btnStop.Enabled = false;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            sStartTime = DateTime.Now;
            sCurrentTime = sStartTime;

            updateAbsoluteTimes(sStartTime);
            stsLblIInfo.Text = "Test started on: " + sStartTime.ToString();
            
            
            timer1.Enabled = true;
            btnRun.Enabled = false;
            btnStop.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // check current time and look if we need to update psu settings
            sCurrentTime = DateTime.Now;
            stsLblCurrTime.Text = "Current time is: " + sCurrentTime.ToString();
            // update current voltage and current labels
            txtCurrentCurrent.Text = "0";
            txtCurrentVoltage.Text = "0";

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btnRun.Enabled = true;
            btnStop.Enabled = false;
        }

        private void updateAbsoluteTimes(DateTime sStart)
        {
            System.TimeSpan duration;
            int iNMinutes;
            bool biDidParse;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                biDidParse = int.TryParse((string)row.Cells["RelativeTime"].Value, out iNMinutes);
                if (biDidParse)
                {
                    duration = new System.TimeSpan(0, iNMinutes, 0);
                    DateTime sTriggerAt = sStart.Add(duration);
                    row.Cells["AbsoluteTime"].Value = sTriggerAt.ToString();
                }
            }
            
        }
    }
}
