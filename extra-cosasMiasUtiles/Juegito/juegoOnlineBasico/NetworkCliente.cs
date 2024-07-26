using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace juegoOnlineBasico
{
    class Cliente 
    {
        IPAddress iP;

        IPEndPoint iPendpoint;

        Socket socket;


        public Cliente(string ip, int port) { 
            iP = IPAddress.Parse(ip);
            iPendpoint = new IPEndPoint(iP, port);
        
            socket = new Socket(iP.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
        }


        public void Iniciar()
        {
            socket.Connect(iPendpoint);

            LogicaCliente();
        }


        async void LogicaCliente()
        {
            await Task.Run(Receiver);
        }


        async Task Receiver()
        {
            while (true)
            {

                byte[] buffer = new byte[1024]; 
                await socket.ReceiveAsync(buffer);

                Console.WriteLine(Encoding.ASCII.GetString(buffer));
            
            
            }
        }
        
    }
}
