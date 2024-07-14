using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cliente
{

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ingrese ip a conectarse: ");
            string ip = Console.ReadLine();

            Cliente cliente = new Cliente(ip, 11000);
            cliente.Start();
            while (true)
            {
                Console.WriteLine("ingrese un mensaje a enviar: ");
                String Mensaje = "";
                Mensaje += Console.ReadLine();
                cliente.EnviarMensaje(Mensaje);

                if (Mensaje == "x")
                {
                    break;
                }
            }
            cliente.Stop();

        }
    }

    class Cliente
    {
        // de forma similar al server, necesitamos una Ip y un puerto, un endpoint 
        // y finalmente una instancia de un socket funcionando

        //IPHostEntry host;
        IPAddress ip;

        IPEndPoint endpoint;

        Socket socket;

        // el constructor es similar al del servidor
        public Cliente(string ip, int port)
        {
            //host = Dns.GetHostEntry(ip); // la clase Dns nos provee de la informacion de la IP
            //this.ip = host.AddressList[0]; // obtenenmos la direccion IP de la entry del host que 
                                           // indicamos
            this.ip = IPAddress.Parse(ip);  
            endpoint = new IPEndPoint(this.ip, port); // creamos el endpoint en la ip y puerto final

            socket = new Socket(this.ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // iniciamos el socket 
            
        }

        public void Start()
        {
            socket.Connect(endpoint); // en este caso, el socket se conecta al endpoint remoto que mencionamos
                                      // en vez de asociar al socket a un endpoint local como haciamos con bind
        }

        public void EnviarMensaje(string msg)
        {
            // de forma similar, usamos la clase Encoding para transformar el string en
            // un buffer de bytes que el server recibira y decodificara con la misma clase y codificacion
            socket.Send(Encoding.ASCII.GetBytes(msg));
        }

        // con este metodo cerramos la conexion
        public void Stop() { socket.Close(); }  
    }

}