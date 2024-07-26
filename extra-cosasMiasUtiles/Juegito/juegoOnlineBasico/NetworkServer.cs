using Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace juegoOnlineBasico
{
    class Server
    {
        IPAddress iP;

        IPEndPoint iPEndPoint;

        Socket socket ;

        List<Socket> clientes = new List<Socket>();

        List<string> datosJugadores = new List<string>();   

        public Server(string ip, int port)
        {
            this.iP = IPAddress.Parse(ip);

            iPEndPoint = new IPEndPoint(this.iP, port);


        }

        public void Iniciar()
        {
            socket = new Socket(this.iP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(iPEndPoint);

            socket.Listen(30);

            LogicaServer();
        }

        public async void LogicaServer()
        {

            Task.Run(Sender);
            
            while (true)
            {
                Socket cliente = socket.Accept();
                clientes.Add(cliente);
                Console.WriteLine("Conexion aceptada");

                Task.Run(() => { Receiver(cliente); });

            }

        }


        async Task Receiver(Socket cliente)
        {
            while (true)
            {
                byte[] buffer = new byte[1024]; 

                await cliente.ReceiveAsync(buffer);

                Console.WriteLine("recibi un dato");

            }
        }

        async Task Sender()
        {
            while (true)
            {
                await Task.Delay(100);
                Parallel.ForEach(clientes.ToArray(), (Socket cliente) =>
                {
                    cliente.Send(Encoding.ASCII.GetBytes("hola soy raro wow"));
                });
            }
        }
    }
}
