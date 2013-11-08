using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankServer
{
    class HttpProcessor
    {
        HttpListenerContext context;
        HttpListenerRequest request;
        HttpListenerResponse response;

        public HttpProcessor(HttpListenerContext context)
        {
            this.context = context;
        }

        public void process()
        {
            this.request = context.Request;
            this.response = context.Response;
            Console.WriteLine("client connected\n");
            String requestMethod = request.HttpMethod;
            if (requestMethod == "GET")
            {
                handleGetRequest();
            }
            else if (requestMethod == "POST")
            {
                handlePostRequest();
            }
        }

        public void handleGetRequest()
        {
            string url = request.Url.OriginalString;

            System.IO.Stream responseBody = response.OutputStream;
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            responseBody.Write(buffer, 0, buffer.Length);

            response.Close();
        }

        public void handlePostRequest()
        {
            System.IO.Stream requestBody = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(requestBody, encoding);
            string requestBodyInString = reader.ReadToEnd();

        }
    }
}
