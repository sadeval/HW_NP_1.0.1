using System;
using System.Net.Sockets;
using System.Text;

namespace TimeClient
{
    class Program
    {
        static void Main()
        {
            const string serverAddress = "127.0.0.1";
            const int port = 13000;

            try
            {
                using (TcpClient client = new TcpClient(serverAddress, port))
                {
                    Console.WriteLine("Введите запрос (date или time):");
                    string? userRequest = Console.ReadLine()?.ToLower();

                    NetworkStream stream = client.GetStream();
                    byte[] requestBytes = Encoding.UTF8.GetBytes(userRequest);
                    stream.Write(requestBytes, 0, requestBytes.Length);

                    byte[] buffer = new byte[256];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string serverResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine("Ответ от сервера: " + serverResponse);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
        }
    }
}
