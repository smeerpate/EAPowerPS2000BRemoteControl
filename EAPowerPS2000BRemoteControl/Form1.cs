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

        enum PS2000_OBJECT
        {
            DEVICETYPE = 0, // string
            SERIALNUMBER = 1, // string
            NOMVOLTAGE = 2, // float
            NOMCURRENT = 3, // float
            NOMPOWER = 4, // float
            ARTICLENO = 6, // string
            MANUFACTURER = 8, // string
            SWVERSION = 9, // string
            DEVCLASS = 19, // word
            OVPTHRESH = 38, // word
            OCPTHRESH = 39, // word
            SETVOLTAGE = 50, // word
            SETCURRENT = 51, // word
            CONTROL = 54, // byte, byte
            ACTSTATUS = 71, // byte,byte,word, word
            MOMSTATUS = 72, // byte,byte,word, word 
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

            PS2000_GetActualValues();
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

        private void updateActualValueFieldsFromReply(byte[] abBuffer)
        {
            UInt16 uwTemp = 0;

            uwTemp = (UInt16)(abBuffer[4] | (abBuffer[5] << 8));
            txtActVoltage.Text = PS2000_ConvertToRealValue(uwTemp, mdFullScaleVoltage).ToString();
        }


        private int PS2000_BuildQueryTelegramToDevice(byte[] abDestBuffer, byte dObjectCode, byte bExpectedReplyLength)
        {
            byte bTemp = 0;
            UInt16 uwChecksum = 0;
            int iLengthNBytes = 5;

            // Build start delimiter [0]
            if (bExpectedReplyLength == 0)
            {
                bTemp = 0;
            }
            else
            {
                bTemp = (byte)(bExpectedReplyLength - 1);
            }
            abDestBuffer[0] = (byte)(bTemp & 0x0F); // data lenth -1
            abDestBuffer[0] |= (byte)PS2000_DIRECTION.TODEVICE << 4; // Direction
            abDestBuffer[0] |= (byte)PS2000_CASTTYPE.QUERY << 5; // Cast type
            abDestBuffer[0] |= (byte)PS2000_TRANSMISSIONTYPE.QUERYDATA << 6; // Transmission type

            // Build device node parameter [1]
            abDestBuffer[1] = (byte)PS2000_DEVICENODE.OUTPUT1;

            // Build Object parameter [2]
            abDestBuffer[2] = dObjectCode;

            // Build 16 bit checksum [3..4]
            uwChecksum = (UInt16)((abDestBuffer[0] + abDestBuffer[1] + abDestBuffer[2]) & 0xFFFF);
            abDestBuffer[3] = (byte)((uwChecksum & 0xFF00) >> 8); // MS Byte
            abDestBuffer[4] = (byte)(uwChecksum & 0x00FF); // LS Byte

            return iLengthNBytes;
        }

        private void PS2000_GetActualValues()
        {
            int iLengthNBytes;

            iLengthNBytes = PS2000_BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.ACTSTATUS, 6);
            serialPort1.Write(mabTxBuffer, 0, iLengthNBytes);
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

        // Callback when data received
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            serialPort1.Read(mabRxBuffer, 0, 11);
            switch (mabRxBuffer[2])
            {
                case (byte)PS2000_OBJECT.ACTSTATUS:
                    // call the UI function in an other thread
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        updateActualValueFieldsFromReply(mabRxBuffer);
                    //}));
                    break;
                default:
                    break;
            }
        }
    }
}
