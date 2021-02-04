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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtComPort.Text = "COM3";
            txtActVoltage.ReadOnly = true;
            txtActCurrent.ReadOnly = true;
            txtActCurrent.Text = "-";
            txtActVoltage.Text = "-";
            txtTargetCurrent.Text = "0";
            txtTargetVoltage.Text = "0";

            serialPort1.PortName = txtComPort.Text;

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                lblStatusStr1.Text = "Comport is already open";
                return;
            }
            serialPort1.Open();
            lblStatusStr1.Text = "Opened port " + serialPort1.PortName;
        }

        private void btnDisonnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                lblStatusStr1.Text = "Closing port " + serialPort1.PortName;
                serialPort1.Close();
                return;
            }
            lblStatusStr1.Text = "Comport is already closed";
        }
    }
}
