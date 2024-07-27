using Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace juegoOnlineBasico
{
    class Server
    {
        IPAddress iP;

        IPEndPoint iPEndPoint;

        Socket socket ;

        List<Socket> clientes = new List<Socket>();

        Dictionary<int,Jugador> datosJugadores = new Dictionary<int, Jugador>();   

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
                byte[] buffer = new byte[2048]; 

                await cliente.ReceiveAsync(buffer);

                string mensaje = Encoding.ASCII.GetString(buffer);
                string json = mensaje.TrimEnd('\0');


                Jugador jugador = JsonSerializer.Deserialize<Jugador>(json);

                if (datosJugadores.ContainsKey(jugador.ID))
                {
                    datosJugadores[jugador.ID] = jugador;
                    Console.WriteLine(jugador.ID);
                }
                else
                {
                    datosJugadores.Add(jugador.ID, jugador);
                    Console.WriteLine(jugador.ID);
                }
                  
                //Console.WriteLine("jugador actualizado");
                //Console.WriteLine(json);

            }
        }

        async Task Sender()
        {
            while (true)
            {
                await Task.Delay(50);
                Parallel.ForEach(clientes.ToArray(), (Socket cliente) =>
                {
                    string json = JsonSerializer.Serialize(datosJugadores);
                    //await socket.SendAsync(Encoding.ASCII.GetBytes(json));
                    cliente.Send(Encoding.ASCII.GetBytes(json));
                });
            }
        }
    }
}
