using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Collections.Generic;


using CommonFunction;

namespace CommonFunction
{
    public class TCPIP_Server
    {
        #region parameter define
        private bool isTerminal = false;

        Queue<string> q_Command = new Queue<string>();

        TcpListener listener;
        TcpClient client;
        NetworkStream stream;
        Tool tool = new Tool();
        #endregion

        #region private function
        private string GetCommand = "";
        #endregion

        #region public function
        public bool Open(string ip, int port)
        {
            bool res = false;

            try
            {
                listener = new TcpListener(IPAddress.Parse(ip), port);   //本地地址ex:127.0.0.1 區網地址ex:192.168.x.x Port:87
                listener.Start();
                tool.SaveHistoryToFile($"[Server]Open server at {ip}:{port}");
                //Task task = Task.Run(() => MainTask());

                res = true;
            }
            catch(Exception ex)
            {
                tool.SaveHistoryToFile($"{ex}");
            }

            return res;
        }

        public bool Close()
        {
            bool res = false;

            try
            {
                // 關閉連線
                client.Close();
                isTerminal = true;
                res = true;
            }
            catch(Exception ex)
            {
                tool.SaveHistoryToFile($"{ex}");
            }

            return res;
        }

        public bool SendMessage(string msg)
        {
            bool res = false;

            try
            {
                // 回應客戶端
                string response = msg;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
                res = true;
            }
            catch(Exception ex)
            {
                tool.SaveHistoryToFile($"{ex}");
            }
            
            return res;
        }

        public string GetMessage()
        {
            string cmd = "non_command";

            if (q_Command.Count != 0)
                cmd = q_Command.Dequeue();

            return cmd;
        }

        public void WaitClientCommand()
        {
            // 等待客戶端連線
            client = listener.AcceptTcpClient();

            // 處理客戶端的請求
            stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            GetCommand = receivedMessage;
            q_Command.Enqueue(GetCommand);
            tool.SaveHistoryToFile($"[Server]Read:{receivedMessage}", "Server");
        }
        #endregion

        public TCPIP_Server()
        {
            
        }

        private void MainTask()
        {
            while (!isTerminal)
            {
                // 等待客戶端連線
                client = listener.AcceptTcpClient();

                // 處理客戶端的請求
                stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                GetCommand = receivedMessage;
                q_Command.Enqueue(GetCommand);
                tool.SaveHistoryToFile($"[Server]Read:{receivedMessage}", "Server");

                Thread.Sleep(1);
            }
        }
    }

    public class TCPIP_Client
    {
        #region parameter define
        TcpClient client;
        NetworkStream stream;
        Tool tool = new Tool();
        #endregion

        #region private function
        
        #endregion

        public bool Open(string serverIP, int port)
        {
            bool res = false;

            try
            {
                // 連接到伺服器
                client = new TcpClient(serverIP, port);
                tool.SaveHistoryToFile($"[Client]Connected to server at {serverIP}:{port}");
                res = true;
            }
            catch(Exception ex)
            {
                tool.SaveHistoryToFile($"{ex}");
            }

            return res;
        }

        public bool Close()
        {
            bool res = false;

            try
            {
                // 關閉連線
                client.Close();
                res = true;
            }
            catch (Exception ex)
            {
                tool.SaveHistoryToFile($"{ex}");
            }

            return res;
        }

        public bool SendMessage(string msg)
        {
            bool res = false;

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(msg);
                stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                tool.SaveHistoryToFile($"[Client]Sent:{msg}");
                res = true;
            }
            catch(Exception ex)
            {
                tool.SaveHistoryToFile($"{ex}");
                res = false;
            }

            return res;
        }

        public string ReceiveMessage()
        {
            string res = "FAIL";

            try
            {
                // 接收伺服器回應
                byte[] buffer = new byte[1024];
                stream.ReadTimeout = 1500; // 設定 1.5 秒超時
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                res = response;
                tool.SaveHistoryToFile($"[Client]Receive:{response}", "Client");
            }
            catch (Exception ex)
            {
                tool.SaveHistoryToFile($"{ex}");
                return res = "FAIL";
            }

            return res;
        }


    }
}
