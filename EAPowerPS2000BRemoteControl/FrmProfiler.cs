﻿using System;
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
        public int iCurrentStep { get; set; }

        private PS2000 msPSU;

        public FrmProfiler(string sPortName)
        {
            InitializeComponent();
            timer1.Interval = 1000;
            msPSU = new PS2000(sPortName);

            btnStop.Enabled = false;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 2)
            {
                // we need at least one entry
                stsLblIInfo.Text = "Nothing to run...";
                return;
            }
            sStartTime = DateTime.Now;
            sCurrentTime = sStartTime;
            resetDataGridViewCellColor();

            updateAbsoluteTimes(sStartTime);
            stsLblIInfo.Text = "Test started on: " + sStartTime.ToString();

            iCurrentStep = 0;

            timer1.Enabled = true;
            btnRun.Enabled = false;
            btnStop.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // check current time and look if we need to update psu settings
            sCurrentTime = DateTime.Now;
            stsLblCurrTime.Text = "Current time is: " + sCurrentTime.ToString();
            object timeObj = dataGridView1.Rows[iCurrentStep + 1].Cells["AbsoluteTime"].Value;
            DateTime sNextStepTime = DateTime.Parse((string)timeObj);

            dataGridView1.Rows[iCurrentStep].DefaultCellStyle.BackColor = Color.GreenYellow;

            if (DateTime.Compare(sCurrentTime, sNextStepTime) > 0)
            {
                // time to take the next step...
                iCurrentStep += 1;
                double dTargetV = 0.0;
                double dTargetI = 0.0;
                double.TryParse((string)dataGridView1.Rows[iCurrentStep].Cells["Voltage"].Value, out dTargetV);
                double.TryParse((string)dataGridView1.Rows[iCurrentStep].Cells["Current"].Value, out dTargetI);
                try
                {
                    msPSU.Connect();
                    msPSU.SetTarget(dTargetV, dTargetI);
                    msPSU.Disconnect();
                }
                catch (Exception TimerExeption)
                {
                    timer1.Enabled = false;
                    btnRun.Enabled = true;
                    btnStop.Enabled = false;
                    MessageBox.Show(TimerExeption.Message, "Error");
                }
                
            }
            try
            {
                msPSU.Connect();
                msPSU.RequestActualValues();
                msPSU.Disconnect();
            }
            catch (Exception TimerExeption)
            {
                timer1.Enabled = false;
                btnRun.Enabled = true;
                btnStop.Enabled = false;
                MessageBox.Show(TimerExeption.Message, "Error");
            }
            

            // update current voltage and current labels
            txtCurrentCurrent.Text = msPSU.mdLastReceivedCurrentSetting.ToString("0.00");
            txtCurrentVoltage.Text = msPSU.mdLastReceivedVoltageSetting.ToString("0.00");

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
            DateTime sTriggerAt = new DateTime(0);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                biDidParse = int.TryParse((string)row.Cells["RelativeTime"].Value, out iNMinutes);
                if (biDidParse)
                {
                    duration = new System.TimeSpan(0, iNMinutes, 0);
                    if (sTriggerAt.Ticks == 0)
                    {
                        sTriggerAt = sStart.Add(duration);
                    }
                    else
                    {
                        sTriggerAt = sTriggerAt.Add(duration);
                    }
                    row.Cells["AbsoluteTime"].Value = sTriggerAt.ToString();
                }
            }
            
        }

        private void resetDataGridViewCellColor()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }
    }
}
