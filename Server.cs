using PhemoteDesktop.Properties;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PhemoteDesktop
{
    public class Server
    {
        private HttpListener listener;

        public Server()
        {
            int port = int.Parse(Resources.Port);
            listener = new HttpListener();
            listener.Prefixes.Add($"http://+:{port}/");
            listener.Start();
            Thread thread = new(new ThreadStart(ListenForRequests))
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void ListenForRequests()
        {
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                Thread requestThread = new(new ThreadStart(() => HandleRequest(context)))
                {
                    IsBackground = true
                };
                requestThread.Start();
            }
        }

        private static void HandleRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request.HttpMethod == "POST")
            {
                using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string body = reader.ReadToEnd();
                    Command command = JsonSerializer.Deserialize<Command>(body)!;
                    Console.WriteLine($"Received command: {command.action}");
                    CommandHandler.HandleCommand(command.action);
                }

                response.StatusCode = 200;
                byte[] buffer = Encoding.UTF8.GetBytes("{\"status\":\"success\"}");
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                response.StatusCode = 405; // Method Not Allowed
            }

            response.Close();
        }

        struct Command
        {
            public string action { get; set; }
        }
    }
}
