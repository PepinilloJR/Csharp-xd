using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
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

            Thread Receptor = new Thread(cliente.RecibirMensaje);
            Receptor.Start();
            Console.WriteLine("conexion exitosa, puede escribir y tocar enter para enviar un mensaje...");
            while (true)
            {
                
                string texto = Console.ReadLine();
                Mensaje mensaje = new Mensaje(texto, "usuario");
                //Console.WriteLine(mensaje.CONTENIDO);
                cliente.EnviarMensaje(ToJson(mensaje));
                if (mensaje.CONTENIDO == "x")
                {
                    break;
                }

                
            }
            cliente.Stop();

        }


        // metodo para serializar una instancia de mensaje a un Json para ser enviado luego
        public static string ToJson(Mensaje mensaje)
        {
            string Json = JsonSerializer.Serialize(mensaje);
            return Json;
        }

        // una funcion para pasar un Json en un objeto mensaje
        public static Mensaje ToMensaje(string Json)
        {
            string json = Json.TrimEnd('\0'); // esto para eliminar los \0 que estan al final
            // de forma similar, uso el metodo de la clase generica JsonSerializer y le indico que trabaje con tipo Mensaje
            Mensaje mensaje = JsonSerializer.Deserialize<Mensaje>(json);// debe existir un constructor default sin parametros para que funcione la deserializacion
            return mensaje;
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
            // de forma similar, usamos la clase Encoding para transformar el Json en
            // un buffer de bytes que el server recibira y decodificara con la misma clase y codificacion
            socket.Send(Encoding.ASCII.GetBytes(msg));
        }

       public void RecibirMensaje()
       {
            while (true)
            {
                byte[] buffer = new byte[1024];
                socket.Receive(buffer);
               
                string msg = Encoding.ASCII.GetString(buffer);
                
                Mensaje mensaje = Program.ToMensaje(msg);

                if (buffer.Length > 0)
                {
                    Console.WriteLine($"{mensaje.NOMBRE}: {mensaje.CONTENIDO}");

                }

                Thread.Sleep(100);
            }

       }

        // con este metodo cerramos la conexion
        public void Stop() { socket.Close(); }  
    }



    // serializacion --- para poder enviar objetos a travez del socket, o para otras actividades, es util serializarlos

    // en nuestro caso usaremos serializacion JSON lo que convertira un objeto en un string con el formato JSON para que podamos luego
    // deserializarlo y usarlo como objeto desde el server tanto como en el cliente


    // marcamos la clase Mensaje como serializable


    class Mensaje
    {

        string contenido;

        string nombre;

        // el constructor por defecto es obligatorio para que la desserializacion funcione
        // da igual si no hace nada, pero es obligatorio un constructor default sin parametro alguno
        public Mensaje()
        {

        }

        public Mensaje(string msg, string nom) {
            contenido = msg;
            nombre = nom;
        }

        //
        public override string ToString()
        {
            return $"nombre: ";
        }


        // para poder luego pasar a un JSON, deben existir propiedades, estas son las que leera el JSON

        public string CONTENIDO {
            get { return contenido; }
            set { contenido = value; }
        }

        public string NOMBRE { 
            get { return nombre; } 
            set { nombre = value; }
        }
    }

}