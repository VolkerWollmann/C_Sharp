using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C_SharpExamplesLib.Language.TCP
{
    internal class MyTCPListener
    {
        internal static string ReceivedMessage = "";
        internal static void Listener()
        {
            // Define the port to listen on
            int port = 5000;
            // Create a TcpListener to listen for incoming connections
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            try
            {
                // Start the listener
                listener.Start();
                Console.WriteLine($"Server is listening on port {port}...");
                while (true)
                {
                    // Accept an incoming connection
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Client connected!");
                    // Handle the client in a separate method
                    _ = Task.Run(() => HandleClient(client));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Stop the listener
                listener.Stop();
            }
        }
        private static void HandleClient(TcpClient client)
        {
            try
            {
                // Get the network stream to read/write data
                NetworkStream stream = client.GetStream();
                // Read data from the client
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {receivedMessage}");

                ReceivedMessage = receivedMessage;

                // Send a response to the client
                string responseMessage = "Hello from the server!";
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);
                stream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine("Response sent to the client.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while handling the client: {ex.Message}");
            }
            finally
            {
                // Close the client connection
                client.Close();
            }
        }
    }

    internal class MyTCPCaller
    {
        internal static string ReceivedMessage = "";
        internal static async void UseListener()
        {
            using var client = new TcpClient();
            try
            {
                // Connect to the server
                await client.ConnectAsync(IPAddress.Loopback, 5000);
                Console.WriteLine("Connected to the server.");

                // Get the network stream to send/receive data
                using var stream = client.GetStream();

                // Send a message to the server
                var message = "Hello from the client!";
                var messageBytes = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                Console.WriteLine("Message sent to the server.");

                // Read the response from the server
                var buffer = new byte[1024];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                var response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                ReceivedMessage = response;


                Console.WriteLine($"Response from server: {response}");
            }
            catch (Exception)
            {
            }
        }
    }

    public class MYTCPTest
    {
        private static async void PerformTest()
        {
            try
            {
                // Start the TCP listener in a separate task
                var listenerTask = Task.Run(MyTCPListener.Listener);

                // Give the listener some time to start
                await Task.Delay(1000);

                await Task.Run(MyTCPCaller.UseListener);

                await Task.Delay(1000);

            }
            catch (Exception)
            {
                throw; // TODO handle exception
            }
        }
        public static void Test()
        {
            PerformTest();
            Thread.Sleep(3000);
            Assert.AreEqual("Hello from the client!", MyTCPListener.ReceivedMessage);
            Assert.AreEqual("Hello from the server!", MyTCPCaller.ReceivedMessage);

        }
    }
}
