using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Json;

namespace BankServer
{
    class bankServer
    {
        HttpListener listener;
        string url;

        public bankServer(string url)
        {
            this.url = url;
            listener = new HttpListener();          
            listener.Prefixes.Add(url);
        }

        public void listen()
        {
            listener.Start();

            while (listener.IsListening)
            {
                Console.WriteLine("Waiting for connection...\n");
                HttpListenerContext context = listener.GetContext();
                HttpProcessor processor = new HttpProcessor(context);
                Thread thread = new Thread(new ThreadStart(processor.process));
                thread.Start();
                Thread.Sleep(1);
            }
        }
        //

        static void Main(string[] args)
        {
            //string url = "http://localhost:8080/";
            //string url = "http://127.0.0.1:8080/";
            //string url = "http://192.168.100.110:80/";
            string url = "https://192.168.100.110:443/";
            bankServer myBankServer = new bankServer(url);
            myBankServer.listen();
        }
    }
}
