using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamhwaInspection.Schemas
{
    public class LightController
    {
        public 조명포트 포트 { get; set; }
        public Int32 통신속도 { get; set; }
        public SerialPort SerialPort { get; set; }
        public String STX { get; set; }
        public String ETX { get; set; }
        public LightController(조명포트 포트, Int32 통신속도, String STX, String ETX)
        {
            this.포트 = 포트;
            this.통신속도 = 통신속도;
            this.STX = STX;
            this.ETX = ETX;
        }

        public Int32 밝기변환(Int32 밝기)
        {
            //Debug.WriteLine($"최대밝기={this.최대밝기}, {(Double)this.최대밝기 * 밝기 / 100}");
            return Convert.ToInt32(Math.Round((Double)밝기));
        }

        public void Init()
        {
            SerialPort = new SerialPort();
            SerialPort.PortName = this.포트.ToString();
            SerialPort.BaudRate = this.통신속도;
            SerialPort.DataBits = (Int32)8;
            SerialPort.StopBits = StopBits.One;
            SerialPort.Parity = Parity.None;
        }

        public Boolean IsOpen()
        {
            return SerialPort != null && SerialPort.IsOpen;
        }

        public Boolean Open()
        {
            if (SerialPort == null) return false;
            try
            {
                SerialPort.Open();
                return SerialPort.IsOpen;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                SerialPort.Dispose();
                SerialPort = null;
                Debug.WriteLine($"조명 제어 포트에 연결할 수 없습니다. {ex.Message}");
                return false;
            }
        }
        public void Close()
        {
            this.SendCommand("f", "0000");
            if (SerialPort == null || !SerialPort.IsOpen) return;
            SerialPort.Close();
            SerialPort.Dispose();
            SerialPort = null;
        }

        public Boolean SendCommand(String 채널, String Command)
        {
            if (!IsOpen())
            {
                Debug.WriteLine("조명컨트롤러 포트에 연결할 수 없습니다.");
                return false;
            }
            try
            {
                if (Command.StartsWith("w"))
                {
                    int channelNumber = Convert.ToInt32(채널);
                    for (int lop = channelNumber; lop < channelNumber + 4; lop++)
                        SerialPort.Write($"{STX}{lop}{Command}{ETX}");
                }
                else
                    SerialPort.Write($"{STX}{채널}{Command}{ETX}");

                //Debug.WriteLine($"{STX}{Command}{ETX}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                //Global.오류로그(로그영역, 구분, ex.Message, true);
                return false;
            }
        }
        public Boolean Get(조명정보 정보)
        {
            return SendCommand($"?{(Int32)정보.채널}", "");
        }
        public Boolean Set(조명정보 정보)
        {
            return SendCommand($"{(Int32)정보.채널}", $"d{this.밝기변환(정보.밝기).ToString("d4")}");
        }
        public Boolean TurnOn(조명정보 정보)
        {
            //return SendCommand($"{(Int32)정보.채널}", $"{정보.밝기}");
            //후면검사 조명 : 1,2,3,4  상부검사 조명 : 6,7,8,9
            Debug.WriteLine($"{조명포트.COM3} - {정보.채널} 조명 ON");
            if (정보.포트 == 조명포트.COM3)
                return SendCommand($"{(Int32)정보.채널}", "w1023");
            else
                return SendCommand($"{(Int32)정보.채널}", "o0000");
        }
        public Boolean TurnOff(조명정보 정보)
        {
            Debug.WriteLine($"{조명포트.COM3} - {정보.채널} 조명 OFF");
            if (정보.포트 == 조명포트.COM3)
                return SendCommand($"{(Int32)정보.채널}", "w0000");
            else
                return SendCommand($"{(Int32)정보.채널}", "f0000");
        }

    }
}
