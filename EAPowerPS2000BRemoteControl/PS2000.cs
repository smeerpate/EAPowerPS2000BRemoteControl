using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Xml.Schema;

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
            return;
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

    }
}
