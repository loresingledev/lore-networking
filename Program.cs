using Lore.Networking;
using Lore.Networking.Communication;
using Lore.Networking.TCP;
using System.Net;
using System.Reflection;

namespace Lore.Example
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            Console.Write($" >> Server or client: ");
            string input = Console.ReadLine()!;

            if (input == "server")
                Server();
            else if (input == "client")
                Client();

            Console.Write("Press any key for continue... ");
            Console.ReadKey();
        }

        internal static void Server()
        {
            TCPServer server = new TCPServer();

            server.OnStarted += (s, e) =>
            {
                Console.WriteLine($" + Server started at {DateTime.Now.ToLongTimeString()}");
            };

            server.OnShutdowned += (s, e) =>
            {
                Console.WriteLine($" - Server shutdowned at {DateTime.Now.ToLongTimeString()}");
            };

            server.OnClientAccepted += (s, e) =>
            {
                Console.WriteLine($" + Server accept new client with IP:Port => {e.Client.RemotePoint?.ToString()}");

                server.Announce(new Message($"new client connected -> {e.Client.RemotePoint?.ToString()}"),
                    new List<IPEndPoint>
                    {
                        e.Client.RemotePoint!
                    });
            };

            server.OnClientDisconnected += (s, e) =>
            {
                Console.WriteLine($" - Client {e.Client.RemotePoint?.ToString()} disconnected from server");

                server.Announce(new Message($"{e.Client.RemotePoint?.ToString()} left at {DateTime.Now.ToLongTimeString()}"));
            };

            server.OnClientMessageReceived += (s, e) =>
            {
                Connection sender = (s as Connection)!;

                Console.WriteLine($" * Server receive message from client {sender.RemotePoint}: {e.Message.ToString()}");
                
                server.Announce(new Message($"(message by {sender.RemotePoint?.ToString()}) {e.Message.ToString()}"),
                    new List<IPEndPoint>
                    {
                        sender.RemotePoint!
                    });
            };

            Console.Write($" * Enter server IP (skip = Any IP): ");
            string addressInput = Console.ReadLine()!;
            IPAddress address = addressInput != string.Empty ? IPAddress.Parse(addressInput) : IPAddress.Any;
            
            Console.Write($" * Enter server port: ");
            uint port = (uint)(int.Parse(Console.ReadLine()!));

            server.Start(address, port);

            int handlesPerSecond = 128;
            bool isRunning = true;

            Task handling = Task.Run(() =>
            {
                while (isRunning)
                {
                    server.Handle();
                    Thread.Sleep(1000 / handlesPerSecond);
                }
            });

            while (isRunning)
            {
                string text = Console.ReadLine()!;

                if (text.Length == 0)
                    isRunning = false;

                server.Announce(new Message(text));
            }

            server.Shutdown(new Message("Server is shutdowned now."));
        }

        internal static void Client()
        {
            TCPClient client = new TCPClient();

            client.OnConnected += (s, e) =>
            {
                Console.WriteLine($" + Client connected to server {e.RemotePoint.ToString()} at {DateTime.Now.ToLongTimeString()}");
            };

            client.OnDisconnected += (s, e) =>
            {
                Console.WriteLine($" - Client disconnected from server at {DateTime.Now.ToLongTimeString()}");
            };

            client.Connection.OnSentMessage += (s, e) =>
            {
                Console.WriteLine($" > Client sent message to server ({e.Message.Length} bytes): {e.Message.ToString()}");
            };

            client.Connection.OnReceivedMessage += (s, e) =>
            {
                Console.WriteLine($" * Client received message from server: {e.Message.ToString()}");
            };

            client.Connection.OnClosed += (s, e) =>
            {
                Console.WriteLine(" - Client close socket");
            };

            Console.Write($" * Enter server IP: ");
            string addressInput = Console.ReadLine()!;
            IPAddress address = addressInput != string.Empty ? IPAddress.Parse(addressInput) : IPAddress.Any;

            Console.Write($" * Enter server port: ");
            uint port = (uint)(int.Parse(Console.ReadLine()!));

            client.Connect(address, port);

            int handlesPerSecond = 128;
            bool isRunning = true;

            Task handling = Task.Run(() =>
            {
                while (isRunning)
                {
                    client.Handle();
                    Thread.Sleep(1000 / handlesPerSecond);
                }
            });

            while (isRunning)
            {
                string text = Console.ReadLine()!;

                if (text.Length == 0)
                    break;

                client.Connection.Send(new Message(text));
            }

            client.Disconnect();
        }
    }
}