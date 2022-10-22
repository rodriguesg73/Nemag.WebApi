using System;
using System.Threading;
using System.IO.Ports;

namespace Nemag.Auxiliar
{
    class Sms
    {
        static SerialPort serialPort = new SerialPort
        {
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One,
            BaudRate = 9600,
            DtrEnable = true,
            RtsEnable = true,
            Encoding = System.Text.Encoding.GetEncoding("iso-8859-1")
        };

        public static void Main()
        {
            serialPort.PortName = "COM12";

            serialPort.Open();

            serialPort.DiscardInBuffer();

            serialPort.DiscardOutBuffer();

            SendCommand("AT", "OK"); // "Ping"
            SendCommand("AT+CFUN=1");
            SendCommand("AT+CMGF=1"); // Message format

            //SendCommand("AT+CMGL=\"ALL\"");

            for (int i = 0; i < 30; i++)
            {
                SendCommand("AT+CMGS=\"+5513988075227\"\r\n", ">");
                SendCommand("Oi Sumida! Amanha tem inauguracao da Colcci Santos no Shopping Praiamar com o ator Cleber Toledo! Quer saber mais? Chama no Whats:  http://bit.ly/2vUOWyc" + "\x1A", "+CMGS");
            }
        }

        static void SendCommand(string command, string expectedResponse = "OK")
        {
            serialPort.Write(command + "\r");

            ReadResponse(expectedResponse);
        }

        static void ReadResponse(string expectedResponse)
        {
            var timeout = 3000;

            var response = string.Empty;

            var dataInicioProcesso = DateTime.Now;

            while (serialPort.BytesToRead > 0 || DateTime.Now.Subtract(dataInicioProcesso).Seconds <= timeout / 1000)
            {
                response += serialPort.ReadExisting();

                while (response.Contains("\r\n\r\n"))
                    response = response.Replace("\r\n\r\n", "\r\n");

                if (response.Replace("\r\n", string.Empty).Equals("ERROR"))
                    break;

                if (response.Replace("\r\n", string.Empty).Equals(expectedResponse))
                    break;

                if (response.Replace("\r\n", string.Empty).StartsWith(expectedResponse))
                    break;

                Thread.Sleep(100);
            }

            if (!string.IsNullOrEmpty(response))
                Console.WriteLine(response[2..^1]);
        }
    }
}
