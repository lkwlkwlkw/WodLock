using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Text;



namespace WodLock
{
    public class HttpServer
    {
        public event EventHandler? WaterPurchased;

        public int Port = 5000;
        private HttpListener? Listener;

        public void Start()
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add("http://127.0.0.1:" + Port.ToString() + "/");
            Listener.Start();
            Receive();
        }

        public void Stop()
        {
            Listener?.Stop();
        }

        private void Receive()
        {
            Listener?.BeginGetContext(new AsyncCallback(ListenerCallback), Listener);
        }

        private void ListenerCallback(IAsyncResult result)
        {
            if (Listener is not null && Listener.IsListening)
            {
                var context = Listener.EndGetContext(result);
                var request = context.Request;
                string documentContents;
                if (request.HttpMethod == "POST")
                {
                    using (Stream receiveStream = request.InputStream)
                    {
                        using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            documentContents = readStream.ReadToEnd();
                        }
                    }
                    Debug.WriteLine(documentContents);

                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;
                    // Construct a response.
                    string responseString = "Accepted";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();

                    if (CheckPurchasedProduct(documentContents))
                    {
                        // Debug.WriteLine("Kupiono Wodę");
                        WaterPurchased?.Invoke(this, EventArgs.Empty);
                    }

                }
                Receive();
            }
        }

        private static bool CheckPurchasedProduct(string ResponseString)
        {
            return (ResponseString.Contains("product_purchase") & (ResponseString.Contains("Woda") || ResponseString.Contains("Fake Product")));

        }




    }
}
