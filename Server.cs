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

            try
            {
                if (request.HttpMethod == "POST")
                {
                    using (StreamReader reader = new(request.InputStream, request.ContentEncoding))
                    {
                        string body = reader.ReadToEnd();

                        if (string.IsNullOrWhiteSpace(body))
                        {
                            response.StatusCode = 400;
                            byte[] errorBuffer = Encoding.UTF8.GetBytes("{\"error\":\"Empty request body\"}");
                            response.ContentLength64 = errorBuffer.Length;
                            response.OutputStream.Write(errorBuffer, 0, errorBuffer.Length);
                            response.Close();
                            return;
                        }

                        Command? command = JsonSerializer.Deserialize<Command>(body);

                        if (command == null || string.IsNullOrWhiteSpace(command.Value.action))
                        {
                            response.StatusCode = 400;
                            byte[] errorBuffer = Encoding.UTF8.GetBytes("{\"error\":\"Invalid command format\"}");
                            response.ContentLength64 = errorBuffer.Length;
                            response.OutputStream.Write(errorBuffer, 0, errorBuffer.Length);
                            response.Close();
                            return;
                        }

                        Console.WriteLine($"Received command: {command.Value.action}");
                        CommandHandler.HandleCommand(command.Value.action);
                    }

                    response.StatusCode = 200;
                    byte[] buffer = Encoding.UTF8.GetBytes("{\"status\":\"success\"}");
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    response.StatusCode = 405; // Method Not Allowed
                    byte[] errorBuffer = Encoding.UTF8.GetBytes("{\"error\":\"Only POST requests are allowed\"}");
                    response.ContentLength64 = errorBuffer.Length;
                    response.OutputStream.Write(errorBuffer, 0, errorBuffer.Length);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                response.StatusCode = 400;
                byte[] errorBuffer = Encoding.UTF8.GetBytes("{\"error\":\"Invalid JSON format\"}");
                response.ContentLength64 = errorBuffer.Length;
                response.OutputStream.Write(errorBuffer, 0, errorBuffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling request: {ex.Message}");
                response.StatusCode = 500;
                byte[] errorBuffer = Encoding.UTF8.GetBytes("{\"error\":\"Internal server error\"}");
                response.ContentLength64 = errorBuffer.Length;
                response.OutputStream.Write(errorBuffer, 0, errorBuffer.Length);
            }
            finally
            {
                response.Close();
            }
        }

        struct Command
        {
            public string action { get; set; }
        }
    }
}
