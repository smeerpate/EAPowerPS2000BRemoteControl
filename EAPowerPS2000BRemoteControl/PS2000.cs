using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using System.Windows.Forms;

namespace EAPowerPS2000BRemoteControl
{
    class PS2000
    {
        private double mdFullScaleVoltage = 42.0; // Volts
        private double mdFullScaleCurrent = 20.0; // Amperes

        SerialPort PS2000Port;

        public bool mbiIsConnected { get; set; }
        public double mdLastReceivedVoltageSetting { get; set; }
        public double mdLastReceivedCurrentSetting { get; set; }
        public byte mbLastReceivedCommStatus { get; set; }
        public bool mbiRemoteActive { get; set; }
        public bool mbiOutputActive { get; set; }
        public bool mbiReceivedNewActualValues { get; set; }
        public bool mbiReceivedNewCommStatus { get; set; }


        private enum PS2000_DIRECTION
        {
            FROMDEVICE = 0,
            TODEVICE = 1,
        }

        private enum PS2000_OBJECT
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

        private enum PS2000_CASTTYPE
        {
            ANSWER = 0,
            QUERY = 1,
        }

        private enum PS2000_TRANSMISSIONTYPE
        {
            RESERVED = 0,
            QUERYDATA = 1,
            ANSWERTOQUERY = 2,
            SENDDATA = 3,
        }

        private enum PS2000_DEVICENODE
        {
            OUTPUT1 = 0,
            OUTPUT2 = 1,
        }

        private struct PS2000_TelegramStartDelimiter
        {
            byte LengthMinOne;
            byte Direction;
            byte CastType;
            byte TransMissionType;
        }

        private struct PS2000_Telegram
        {
            PS2000_TelegramStartDelimiter StartDelimiter;
            byte DeviceNode;
            byte CommObject;
            byte[] Data;
            byte DataLength;
            UInt16 CheckSum;

        }

        private PS2000_Telegram msOutTelegram;

        private byte[] mabTxBuffer;
        private byte[] mabRxBuffer;
        private byte[] mabDataBuffer;


        // Callback when data received
        private void PSDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            PS2000Port.Read(mabRxBuffer, 0, 11);
            switch (mabRxBuffer[2])
            {
                case (byte)PS2000_OBJECT.ACTSTATUS:
                    updateActualValuesFromReply();
                    mbiReceivedNewActualValues = true;
                    break;

                case (byte)PS2000_OBJECT.ACK:
                    updateCommStatusFromReply();
                    mbiReceivedNewCommStatus = true;
                    break;

                default:
                    break;
            }
        }

        private void updateActualValuesFromReply()
        {
            UInt16 uwTemp = 0;

            // Actual voltage
            uwTemp = (UInt16)(mabRxBuffer[6] | (mabRxBuffer[5] << 8));
            mdLastReceivedVoltageSetting = ConvertToRealValue(uwTemp, mdFullScaleVoltage);
            // Actual current
            uwTemp = (UInt16)(mabRxBuffer[8] | (mabRxBuffer[7] << 8));
            mdLastReceivedCurrentSetting = ConvertToRealValue(uwTemp, mdFullScaleCurrent);

            // Actual remote state
            if (mabRxBuffer[3] == 0)
            {
                // Remote control is disabled
                mbiRemoteActive = false;
            }
            else
            {
                if (mabRxBuffer[3] == 1)
                {
                    // remote is enabled
                    mbiRemoteActive = true;
                }
            }

            // Actual output state
            if ((mabRxBuffer[4] & 0x01) == 0)
            {
                // Output is disabled
                mbiOutputActive = false;
            }
            else
            {
                if ((mabRxBuffer[4] & 0x01) == 1)
                {
                    // routput is enabled
                    mbiOutputActive = true;
                }
            }
        }

        private void updateCommStatusFromReply()
        {
            mbLastReceivedCommStatus = mabRxBuffer[3];
        }
        

        public PS2000(string sPortName)
        {
            PS2000Port = new SerialPort();
            this.PS2000Port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.PSDataReceived);

            // setup serial port.
            PS2000Port.PortName = sPortName;
            PS2000Port.BaudRate = 115200;
            PS2000Port.Parity = Parity.Odd;
            PS2000Port.StopBits = StopBits.One;

            // init buffers
            mabRxBuffer = new byte[32];
            mabTxBuffer = new byte[32];
            mabDataBuffer = new byte[16];
        }

        public void Connect()
        {
            if (!PS2000Port.IsOpen)
            {
                try
                {
                    PS2000Port.Open();
                    mbiIsConnected = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Disconnect()
        {
            if (PS2000Port.IsOpen)
            {
                try
                {
                    PS2000Port.Close();
                    mbiIsConnected = false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private UInt16 ConvertToRawPercValue(double dRealValue, double dFullScaleValue)
        {
            double dTemp = 0.0;

            dTemp = (25600 * dRealValue) / dFullScaleValue;

            return (UInt16)dTemp;
        }

        private double ConvertToRealValue(UInt16 uwRawPercValue, double dFullScaleValue)
        {
            double dReturnValue = 0.0;

            dReturnValue = (dFullScaleValue * (double)uwRawPercValue) / 25600.0;

            return dReturnValue;
        }

        // If "byte[] abData" == null no data is inserted and is considered as "PS2000_TRANSMISSIONTYPE.QUERYDATA"
        private int BuildQueryTelegramToDevice(byte[] abDestBuffer, byte dObjectCode, byte bExpectedReplyLength, byte[] abData, byte bDataLength)
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

        public void RequestActualValues()
        {
            int iLengthNBytes;

            iLengthNBytes = BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.ACTSTATUS, 6, null, 0);
            try
            {
                PS2000Port.Write(mabTxBuffer, 0, iLengthNBytes);
                System.Threading.Thread.Sleep(200);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void SetTarget(double dTargetVoltage, double dTargetCurrent)
        {
            int iLengthNBytes;
            UInt16 uwVoltPercValue = 0;
            UInt16 uwAmpPercValue = 0;

            if (dTargetVoltage < mdFullScaleVoltage)
            {
                uwVoltPercValue = ConvertToRawPercValue(dTargetVoltage, mdFullScaleVoltage);
            }
            else
            {
                MessageBox.Show("Invalid value in Target voltage field");
                return;
            }

            if (dTargetCurrent < mdFullScaleCurrent)
            {
                uwAmpPercValue = ConvertToRawPercValue(dTargetCurrent, mdFullScaleCurrent);

            }
            else
            {
                MessageBox.Show("Invalid value in Target voltage field");
                return;
            }

            mabDataBuffer[0] = (byte)((uwVoltPercValue & 0xFF00) >> 8);
            mabDataBuffer[1] = (byte)(uwVoltPercValue & 0x00FF);
            iLengthNBytes = BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.SETVOLTAGE, 2, mabDataBuffer, 2);
            try
            {
                PS2000Port.Write(mabTxBuffer, 0, iLengthNBytes);
                System.Threading.Thread.Sleep(200);
            }
            catch (Exception)
            {

                throw;
            }
            

            mabDataBuffer[0] = (byte)((uwAmpPercValue & 0xFF00) >> 8);
            mabDataBuffer[1] = (byte)(uwAmpPercValue & 0x00FF);
            iLengthNBytes = BuildQueryTelegramToDevice(mabTxBuffer, (byte)PS2000_OBJECT.SETCURRENT, 2, mabDataBuffer, 2);
            try
            {
                PS2000Port.Write(mabTxBuffer, 0, iLengthNBytes);
                System.Threading.Thread.Sleep(200);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

    }
}
