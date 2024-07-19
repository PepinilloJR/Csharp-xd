using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq.Expressions;


namespace ServerTCP
{

    class Program 
    { 
        public static void Main(string[] args)
        {
            Server server = new Server("26.34.159.22", 11000);
            server.Start();
        }


        // una funcion para pasar un objeto a un formato Json
        public static string ToJson(Mensaje mensaje)
        {
            if (mensaje.CONTENIDO == "")
            {
                mensaje.CONTENIDO = "x";
            }
            string Json = JsonSerializer.Serialize(mensaje);
            return Json;
        }


        // una funcion para pasar un Json en un objeto mensaje
        public static Mensaje ToMensaje(string Json)
        {
            string json = Json.TrimEnd('\0');  // esto para eliminar los \0 que estan al final

            // de forma similar, uso el metodo de la clase generica JsonSerializer y le indico que trabaje con tipo Mensaje
            Mensaje mensaje = JsonSerializer.Deserialize<Mensaje>(json); // debe existir un constructor default sin parametros para que funcione la deserializacion
            return mensaje;
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


        public List<Mensaje> MensajesGlobal = [];  // TODO: cambiar el tipo de acceso y ver otra forma para que se comparta
        List<Socket> sockets;

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
            sockets = new List<Socket>();
        }


        public void Start()
        {
            List<Thread> hilosCliente = new List<Thread>();

            Thread Enviador = new Thread(ActualizarClientes);

            Enviador.Start();

            while (true) {

                // PARA INICIAR A TRABAJAR, el socket debe aceptar una solicitud 
                // cuando aceptamos una solicitud, se crea otro socket, lo guardamos en el objeto Socket cliente
                // el objeto cliente sera otro socket que se inicia al aceptar la solicitud, de aqui en adelante este socket
                // sera el que se encarga del intercambio de datos entre este y el servidor
                cliente = socket.Accept();
                sockets.Add(cliente);
                Console.Write("se conecto alguien...");
                hilosCliente.Add(new Thread(ConexionCliente));
                hilosCliente.Last().Start(cliente);
                
            }

        }


        public void ConexionCliente(Object c)
        {

            //List<string> mensajes = MensajesGlobal.ToList();

            Socket client = (Socket)c;

            while (true)
            {
                try
                {
                    // podemos recibir datos entrantes con el metodo receive, los cuales guardaremos en nuestro buffer de bytes
                    // el cual luego podremos transformar en informacion legible
                    byte[] buffer = new byte[1024]; // 1024 sera la longitud del buffer, sera de 1024 bytes


                    int bytes = client.Receive(buffer);
                    // para leer el mensaje, podemos usar la clase Encoding, la clase encondig tiene propiedades que representan el tipo de codificacion
                    // y luego metodos definidos en esta propiedad, como GetString, para obtener el texto guardado en el buffer de bytes 
                    string msg = Encoding.ASCII.GetString(buffer);
                    Mensaje mensaje = Program.ToMensaje(msg);

                    MensajesGlobal.Add(mensaje);

                    Console.WriteLine($"{mensaje.NOMBRE}: {mensaje.CONTENIDO}");


                    // si recibe cero bytes implica que se desconecto el cliente
                    if (bytes == 0)
                    {
                        break;
                    }
                    
                } catch (SocketException ex)
                {
                    Console.WriteLine ("no se pudo recibir un mensaje de un cliente debido a su desconexion");
                    break;
                }

                Thread.Sleep(100);
            }

            // cuando el cliente se desconecta, debemos primero deshabilitar el intercambio de datos con shutdown, y escogiendo que tipo desde el enum SocketShutdowm, esto evitara
            // problemas extraños resultado de un cierre abrupto del socket, ya que shutdown se asegura que primero todos los datos sean intercambiados antes de continuar
            client.Shutdown(SocketShutdown.Both);

            // finalmente cerramos este socket que creamos en este hilo, liberando los recursos y cerrando conexiones
            client.Close();

            sockets.Remove(client); // eliminamos de la lista de sockets al que acabamos de cerrar

            Console.Write("se desconecto alguien...");
        }

        public void ActualizarClientes()
        {
            int cantidadMensajes = MensajesGlobal.Count;
            while (true)
            {
                if (sockets.Count > 0)
                {
                    if (cantidadMensajes < MensajesGlobal.Count)
                    {
                        foreach (Socket Cli in sockets)
                        {

                            // esto es temporal

                            Mensaje mensaje = MensajesGlobal.Last();

                            try
                            {
                                Cli.Send(Encoding.ASCII.GetBytes(Program.ToJson(mensaje)));
                            }
                            catch(SocketException ex) {
                                Console.WriteLine("un mensaje al cliente no pudo ser enviado debido a una desconexion");
                            }

                        }
                        cantidadMensajes = MensajesGlobal.Count;
                    }
                }
                // nescesario frenar un rato el thread, unos milisegundos porque si no se muere el thread por que satura al cpu o algo asi no se
                Thread.Sleep(100);
            }

        }

        // con este metodo cerramos la conexion
        public void Stop() { cliente.Close(); }
    }


    class Mensaje
    {

        string contenido;

        string nombre;

        // el constructor por defecto es obligatorio para que la serializacion funcione
        // da igual si no hace nada, pero es obligatorio un constructor default sin parametro alguno
        public Mensaje()
        {

        }

        public Mensaje(string msg, string nom)
        {
            contenido = msg;
            nombre = nom;
        }

        //
        public override string ToString()
        {
            return $"nombre: ";
        }


        // para poder luego pasar a un JSON, deben existir propiedades, estas son las que leera el JSON

        public string CONTENIDO
        {
            get { return contenido; }
            set { contenido = value; }
        }

        public string NOMBRE
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }

}

