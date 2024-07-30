using Principal;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace juegoOnlineBasico
{
    class Cliente 
    {
        IPAddress iP;

        IPEndPoint iPendpoint;

        Socket socket;

        Jugador jugador;

        Dictionary<int, Jugador> jugadores;
        Dictionary<int, Jugador> jugadoresT;

        List<Bala> balas;
        List<Bala> balasT;

        public Cliente(string ip, int port, Jugador jugador, Dictionary<int, Jugador> jugadores, Dictionary<int, Jugador> jugadoresT, List<Bala> balas) { 
        
            this.jugador = jugador; 
            iP = IPAddress.Parse(ip);
            iPendpoint = new IPEndPoint(iP, port);
            this.jugadores = jugadores;
            this.jugadoresT = jugadoresT;
            this.balas = balas;
            socket = new Socket(iP.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
        }


        public void Iniciar()
        {
            socket.Connect(iPendpoint);

            LogicaCliente();
        }


        async void LogicaCliente()
        {
            var Receptor = Task.Run(Receiver);
            var Enviador = Task.Run(Sender);
            await Task.WhenAll(Receptor, Enviador);
        }


        async Task Sender()
        {
            while (true)
            {
                await Task.Delay(50);
                try
                {
                    string json = JsonSerializer.Serialize(jugador);
                    await socket.SendAsync(Encoding.ASCII.GetBytes(json));
                    jugador.BALAS.Clear();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    continue;
                }


                }
        }


        async Task Receiver()
        {
            while (true)
            {
                byte[] buffer = new byte[2048];
                await socket.ReceiveAsync(buffer);

                string mensaje = Encoding.ASCII.GetString(buffer);
                string json = mensaje.TrimEnd('\0');
                try {

                    Dictionary<int, Jugador> JugadoresTemp = JsonSerializer.Deserialize<Dictionary<int, Jugador>>(json);

                    // aca tambien lockea, que solo una pueda modifcar el bloque de datos de JugadoresTemp

                    foreach (KeyValuePair<int, Jugador> par in JugadoresTemp.ToArray())
                    {
                        if (jugadores.ContainsKey(par.Key))
                        {
                            jugadoresT[par.Key] = jugadores[par.Key];
                            jugadores[par.Key] = par.Value;
                        } else
                        {
                            jugadores.Add(par.Key, par.Value);
                            jugadoresT.Add(par.Key, par.Value);
                        }
                    }



                } catch (Exception e) {

                    //Console.CursorLeft = 5;
                   // Console.CursorTop = 30;
                   // Console.Write(json);
                    continue;
                
                }
            }
        }
        
    }
}
