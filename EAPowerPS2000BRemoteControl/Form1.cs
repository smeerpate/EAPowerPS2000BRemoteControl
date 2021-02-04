using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAPowerPS2000BRemoteControl
{
    public partial class Form1 : Form
    {
        double mdFullScaleVoltage = 42.0; // Volts
        double mdFullScaleCurrent = 20.0; // Amperes

        enum PS2000_DIRECTION
        {
            FROMDEVICE = 0,
            TODEVICE = 1,
        }

        enum PS2000_CASTTYPE
        {
            ANSWER = 0,
            QUERY = 1,
        }

        enum PS2000_TRANSMISSIONTYPE
        {
            RESERVED = 0,
            QUERYDATA = 1,
            ANSWERTOQUERY = 2,
            SENDDATA = 3,
        }

        enum PS2000_DEVICENODE
        {
            OUTPUT1 = 0,
            OUTPUT2 = 1,
        }

        struct PS2000_TelegramStartDelimiter
        {
            byte LengthMinOne;
            byte Direction;
            byte CastType;
            byte TransMissionType;
        }

        struct PS2000_Telegram
        {
            PS2000_TelegramStartDelimiter StartDelimiter;
            byte DeviceNode;
            byte CommObject;
            byte[] Data;
            byte DataLength;
            UInt16 CheckSum;

        }

        PS2000_Telegram msOutTelegram;

        byte[] mabTxBuffer;
        byte[] mabRxBuffer;
        byte[] mabDataBuffer;

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

            // setup serial port.
            serialPort1.PortName = txtComPort.Text;
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = Parity.Odd;
            serialPort1.StopBits = StopBits.One;

            // init buffers
            mabRxBuffer = new byte[32];
            mabTxBuffer = new byte[32];
            mabDataBuffer = new byte[16];
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


        private void PS2000_BuildTelegramToDevice(byte[] abDestBuffer, byte[] abData, byte bDataLength)
        {
            byte bTemp = 0;

            // Build start delimiter [0]
            if (bDataLength == 0)
            {
                bTemp = 0;
            }
            else
            {
                bTemp = (byte)(bDataLength - 1);
            }
            abDestBuffer[0] = (byte)(bTemp & 0x0F); // data lenth -1
            abDestBuffer[0] |= (byte)PS2000_DIRECTION.TODEVICE << 4; // Direction
            abDestBuffer[0] |= (byte)PS2000_CASTTYPE.QUERY << 5; // Cast type
            abDestBuffer[0] |= (byte)PS2000_TRANSMISSIONTYPE.QUERYDATA << 6; // Transmission type

            // Build device node parameter [1]
            abDestBuffer[1] = (byte)PS2000_DEVICENODE.OUTPUT1;

            // Build Object parameter [2]


        }

        private bool PS2000_GetOututEnabledState()
        {
            bool biReturnValue = false;



            return biReturnValue;
        }

        private double PS2000_ConvertToRealValue(UInt16 uwRawPercValue, double dFullScaleValue)
        {
            double dReturnValue = 0.0;

            dReturnValue = (dFullScaleValue * (double)uwRawPercValue) / 25600.0;

            return dReturnValue;
        }

        private UInt16 PS2000_ConvertToRawPercValue(double dRealValue, double dFullScaleValue)
        {
            double dTemp = 0.0;

            dTemp = (25600 * dRealValue) / dFullScaleValue;

            return (UInt16)dTemp;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // close comport if open
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }
    }
}
