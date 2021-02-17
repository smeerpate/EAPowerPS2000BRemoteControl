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
            ACK = 0xFF,
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
            txtTargetCurrent.Text = "1";
            txtTargetVoltage.Text = "5";
            chkQOutput.Enabled = false;
            chkQRemote.Enabled = false;

            btnSetTarget.Enabled = false;
            btnUpdateActual.Enabled = false;
            chkERemoteEnabled.Enabled = false;
            chkOutputEnabled.Enabled = false;


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

            btnSetTarget.Enabled = true;
            btnUpdateActual.Enabled = true;
            chkERemoteEnabled.Enabled = true;
            chkOutputEnabled.Enabled = true;
            PS2000_GetActualValues();
        }

        private void btnDisonnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                lblStatusStr1.Text = "Closing port " + serialPort1.PortName;
                serialPort1.Close();
                btnSetTarget.Enabled = false;
                btnUpdateActual.Enabled = false;
                chkERemoteEnabled.Enabled = false;
                chkOutputEnabled.Enabled = false;
                return;
            }
            lblStatusStr1.Text = "Comport is already closed"; 
        }

        private void updateActualValueFieldsFromReply(byte[] abBuffer)
        {
            UInt16 uwTemp = 0;
            double dVoltage = 0.0;
            double dCurrent = 0.0;

            // Actual voltage
            uwTemp = (UInt16)(abBuffer[6] | (abBuffer[5] << 8));
            dVoltage = PS2000_ConvertToRealValue(uwTemp, mdFullScaleVoltage);
            txtActVoltage.Text = dVoltage.ToString("0.00") + " V";
            // Actual current
            uwTemp = (UInt16)(abBuffer[8] | (abBuffer[7] << 8));
            dCurrent = PS2000_ConvertToRealValue(uwTemp, mdFullScaleCurrent);
            txtActCurrent.Text = dCurrent.ToString("0.00") + " A";

            txtActPower.Text = (dCurrent * dVoltage).ToString("0.00") + " W";

            // Actual remote state
            if (abBuffer[3] == 0)
            {
                // Remote control is disabled
                chkQRemote.Checked = false;
            }
            else
            {
                if (abBuffer[3] == 1)
                {
                    // remote is enabled
                    chkQRemote.Checked = true;
                }
            }

            // Actual output state
            if ((abBuffer[4] & 0x01) == 0)
            {
                // Output is disabled
                chkQOutput.Checked = false;
            }
            else
            {
                if ((abBuffer[4] & 0x01) == 1)
                {
                    // routput is enabled
                    chkQOutput.Checked = true;
                }
            }
        }

        private void updateActualStatusStripFromReply(byte[] abBuffer)
        {
            lblStatusStr1.Text = "PS2000 error code = " + abBuffer[3].ToString("0x00");
        }

        // If "byte[] abData" == null no data is inserted and is considered as "PS2000_TRANSMISSIONTYPE.QUERYDATA"
        private int PS2000_BuildQueryTelegramToDevice(byte[] abDestBuffer, byte dObjectCode, byte bExpectedReplyLength, byte[] abData, byte bDataLength)
        {
            byte bTemp = 0;
            UInt16 uwChecksum = 0;
            int iByteIndex = 0;
            int iDataChecksum = 0;

            // Build start delimiter [0]
            if (bExpectedReplyLength == 0)
            {
                bTemp = 0;
            }
            else
            {
                bTemp = (byte)(bExpectedReplyLength - 1);
            }
            abDestBuffer[iByteIndex] = (byte)(bTemp & 0x0F); // expecte reply data lenth -1
            abDestBuffer[iByteIndex] |= (byte)PS2000_DIRECTION.TODEVICE << 4; // Direction
            abDestBuffer[iByteIndex] |= (byte)PS2000_CASTTYPE.QUERY << 5; // Cast type
            if (abData != null)
            {
                abDestBuffer[iByteIndex] |= (byte)PS2000_TRANSMISSIONTYPE.SENDDATA << 6; // Transmission type
            }
            else
            {
                abDestBuffer[iByteIndex] |= (byte)PS2000_TRANSMISSIONTYPE.QUERYDATA << 6; // Transmission type
            }

            // Build device node parameter [1]
            iByteIndex++;
            abDestBuffer[iByteIndex] = (byte)PS2000_DEVICENODE.OUTPUT1;

            // Build Object parameter [2]
            iByteIndex++;
            abDestBuffer[iByteIndex] = dObjectCode;

            if (abData != null)
            {
                for (int i = 0; i < bDataLength; i++)
                {
                    iByteIndex++;
                    abDestBuffer[iByteIndex] = abData[i];
                    iDataChecksum += abData[i];
                }
            }

            // Build 16 bit checksum [3..4]
            uwChecksum = (UInt16)((abDestBuffer[0] + abDestBuffer[1] + abDestBuffer[2] + iDataChecksum) & 0xFFFF);
            iByteIndex++;
            abDestBuffer[iByteIndex] = (byte)((uwChecksum & 0xFF00) >> 8); // MS Byte
            iByteIndex++;
            abDestBuffer[iByteIndex] = (byte)(uwChecksum & 0x00FF); // LS Byte

            return (iByteIndex + 1);
        }

        private void PS2000_GetActualValues()
        {
            int iLengthNBytes;

            iLengthNBytes = PS2000_BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.ACTSTATUS, 6, null, 0);
            serialPort1.Write(mabTxBuffer, 0, iLengthNBytes);
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
                    }));
                    break;

                case (byte)PS2000_OBJECT.ACK:
                    // call the UI function in an other thread
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        updateActualStatusStripFromReply(mabRxBuffer);
                    }));
                    break;

                default:
                    break;
            }
        }

        private void btnUpdateActual_Click(object sender, EventArgs e)
        {
            PS2000_GetActualValues();
        }

        private void chkERemoteEnabled_CheckedChanged(object sender, EventArgs e)
        {
            int iLengthNBytes;

            if (chkERemoteEnabled.Checked)
            {
                // send command to enable remote
                mabDataBuffer[0] = 0x10;
                mabDataBuffer[1] = 0x10;
                iLengthNBytes = PS2000_BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.CONTROL, 2, mabDataBuffer, 2);
                serialPort1.Write(mabTxBuffer, 0, iLengthNBytes);
            }
            else
            {
                // send command to disable remote
                mabDataBuffer[0] = 0x10;
                mabDataBuffer[1] = 0x00;
                iLengthNBytes = PS2000_BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.CONTROL, 2, mabDataBuffer, 2);
                serialPort1.Write(mabTxBuffer, 0, iLengthNBytes);
            }
        }

        private void btnSetTarget_Click(object sender, EventArgs e)
        {
            int iLengthNBytes;
            double dTemp;
            UInt16 uwVoltPercValue = 0;
            UInt16 uwAmpPercValue = 0;

            txtTargetVoltage.Text = txtTargetVoltage.Text.Replace('.', ',');
            txtTargetCurrent.Text = txtTargetCurrent.Text.Replace('.', ',');

            if (Double.TryParse(txtTargetVoltage.Text, out dTemp))
            {
                uwVoltPercValue = PS2000_ConvertToRawPercValue(dTemp, mdFullScaleVoltage);
            }
            else
            {
                lblStatusStr1.Text = "Invalid value in Target voltage field";
                return;
            }

            if (Double.TryParse(txtTargetCurrent.Text, out dTemp))
            {
                uwAmpPercValue = PS2000_ConvertToRawPercValue(dTemp, mdFullScaleCurrent);
                
            }
            else
            {
                lblStatusStr1.Text = "Invalid value in Target voltage field";
                return;
            }

            mabDataBuffer[0] = (byte)((uwVoltPercValue & 0xFF00) >> 8);
            mabDataBuffer[1] = (byte)(uwVoltPercValue & 0x00FF);
            iLengthNBytes = PS2000_BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.SETVOLTAGE, 2, mabDataBuffer, 2);
            serialPort1.Write(mabTxBuffer, 0, iLengthNBytes);

            mabDataBuffer[0] = (byte)((uwAmpPercValue & 0xFF00) >> 8);
            mabDataBuffer[1] = (byte)(uwAmpPercValue & 0x00FF);
            iLengthNBytes = PS2000_BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.SETCURRENT, 2, mabDataBuffer, 2);
            serialPort1.Write(mabTxBuffer, 0, iLengthNBytes);
        }

        private void chkOutputEnabled_CheckedChanged(object sender, EventArgs e)
        {
            int iLengthNBytes;

            if (chkOutputEnabled.Checked)
            {
                // send command to enable output
                mabDataBuffer[0] = 0x01;
                mabDataBuffer[1] = 0x01;
                iLengthNBytes = PS2000_BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.CONTROL, 2, mabDataBuffer, 2);
                serialPort1.Write(mabTxBuffer, 0, iLengthNBytes);
            }
            else
            {
                // send command to disable output
                mabDataBuffer[0] = 0x01;
                mabDataBuffer[1] = 0x00;
                iLengthNBytes = PS2000_BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.CONTROL, 2, mabDataBuffer, 2);
                serialPort1.Write(mabTxBuffer, 0, iLengthNBytes);
            }
        }

        private void btnCreateProfile_Click(object sender, EventArgs e)
        {
            FrmProfiler prf = new FrmProfiler(txtComPort.Text);
            // close comport if open
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            prf.Show();
        }
    }
}
