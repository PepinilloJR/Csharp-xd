using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace ServerTCP
{

    class Program 
    { 
        public static void Main(string[] args)
        {
            Server server = new Server("26.34.159.22", 11000);
            server.Start();
        }
    }

    class Server
    {
        // para la creacion del servidor requerimos de los objetos tipo

        //IPHostEntry hostEntry; // objeto para la informacion del host
        IPAddress ip; // clase para el protocolo IP
        IPEndPoint endPoint; // el EndPoint es el punto de conexion que utilizaremos,
                             // necesita de la ip (que obtendremos con los primeros campos) y un puerto
        Socket socket; // el socket del servidor
        Socket cliente; // socket de intercambio que se inicializara mas adelante, lo llamo cliente porque tecnicamente es toda la informacion
                        // de los mensajes entrantes por otros clientes

        // el constructor del server solicita una direccion IP y un Puerto
        public Server(string ip, int puerto)
        {
            // este codigo comentado es una implementacion que no parece funcionar usando radmin, en cambio decidi usar un parse para la obtencion de la ip mas adelante
            
            //hostEntry = Dns.GetHostEntry(ip); // obtenemos informacion del host de la 
                                              // ip que mencionames, para ello usamos un mentodo
                                              // estatico GetHostEntry de la clase DNS
                                              // que provee metodos para obtener informacion
                                              // de los host de un sistema DNS
            
           // this.ip = hostEntry.AddressList[0]; // guardamos en nuestro IP la direccion obtenida
                                                // por la clase Dns, esta sera la IP de nuestro host
            /*foreach (IPAddress i in hostEntry.AddressList)
            {
                Console.WriteLine(i.ToString());
            }*/

            this.ip = IPAddress.Parse(ip);
            endPoint = new IPEndPoint(this.ip, puerto); // construimos el endpoint con la ip obtenida y el puerto

            // al socket le pasaremos entonces tres datos, la address family que utiliza nuestro IP
            // que puede obtenerse del IP que definimos

            // luego el tipo de socket, que se obtiene de la enumeracion SocketType, en nuestro caso usaremos Stream que es el mas entendible por ahora y lo que necesitamos 
            // luego, el tipo de protocolo a usar, que en nuestro caso sera TCP, tambien obtenido de una enumeracion ProtocolType
            socket = new Socket(this.ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(endPoint);  // luego, el metodo Bind asocia el socket a un endPoint para que escuche en este, basicamente asociamos la IP y el Puerto
                                    // al socket para que sepa donde recibir y enviar datos

            socket.Listen(10); // el socket se pone en modo escucha, el socket escuchara tantas conexiones indicadas
        }


        public void Start()
        {
            List<Thread> hilosCliente = new List<Thread>();

            while (true) {

                // PARA INICIAR A TRABAJAR, el socket debe aceptar una solicitud 
                // cuando aceptamos una solicitud, se crea otro socket, lo guardamos en el objeto Socket cliente
                // el objeto cliente sera otro socket que se inicia al aceptar la solicitud, de aqui en adelante este socket
                // sera el que se encarga del intercambio de datos entre este y el servidor
                cliente = socket.Accept();
                Console.Write("se conecto alguien...");
                hilosCliente.Add(new Thread(ConexionCliente));
                hilosCliente.Last().Start(cliente);
            }

        }


        public void ConexionCliente(Object c)
        {

            
            Socket client = (Socket)c;

            while (true)
            {
                // podemos recibir datos entrantes con el metodo receive, los cuales guardaremos en nuestro buffer de bytes
                // el cual luego podremos transformar en informacion legible
                byte[] buffer = new byte[1024]; // 1024 sera la longitud del buffer, sera de 1024 bytes


                int bytes = client.Receive(buffer);
                // para leer el mensaje, podemos usar la clase Encoding, la clase encondig tiene propiedades que representan el tipo de codificacion
                // y luego metodos definidos en esta propiedad, como GetString, para obtener el texto guardado en el buffer de bytes 
                string Mensaje = Encoding.ASCII.GetString(buffer);
                //if (buffer.Length > 0)
                //{
                Console.WriteLine(Mensaje);
                //}

                // si recibe cero bytes implica que se desconecto el cliente
                if (bytes == 0)
                {
                    break;
                }
            }
           // Thread.CurrentThread.Abort();

            // cuando el cliente se desconecta, debemos primero deshabilitar el intercambio de datos con shutdown, y escogiendo que tipo desde el enum SocketShutdowm, esto evitara
            // problemas extraños resultado de un cierre abrupto del socket, ya que shutdown se asegura que primero todos los datos sean intercambiados antes de continuar
            client.Shutdown(SocketShutdown.Both);

            // finalmente cerramos este socket que creamos en este hilo, liberando los recursos y cerrando conexiones
            client.Close();
            Console.Write("se desconecto alguien...");
        }


        // con este metodo cerramos la conexion
        public void Stop() { cliente.Close(); }
    }

}