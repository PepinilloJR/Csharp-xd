﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;


namespace Cliente
{

    class Program
    {

        public static void Main(string[] args)
        {

            Cliente cliente = new Cliente("192.168.100.1", 11000); ;
            ManejadorInput manejadorInput = new ManejadorInput();
            while (true)
            {
                
                Console.WriteLine("ingrese ip a conectarse: ");
                string ip = Console.ReadLine();


                try
                {

                    cliente = new Cliente(ip, 11000);
                    cliente.Start();
                    break;
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("La conexion a la IP indicada no es posible, compruebe la conexion de su servidor");
                } catch (FormatException ex) {
                    Console.WriteLine("el formato de la direccion no es valida, debe ingresarse una IPV4");
                }
            }
            
            

            Thread Receptor = new Thread(cliente.RecibirMensaje);
            Receptor.Start(manejadorInput);
            Console.WriteLine("conexion exitosa, puede escribir y tocar enter para enviar un mensaje...");
            while (true)
            {

                //string texto = Console.ReadLine();

                manejadorInput.ManejarInput();
                Mensaje mensaje = new Mensaje(manejadorInput.INPUT, "usuario");
                //Console.WriteLine(mensaje.CONTENIDO);
                cliente.EnviarMensaje(ToJson(mensaje));
                if (mensaje.CONTENIDO == "/quit")
                {
                    break;
                }



            }
            cliente.Stop();

        }
        // metodo para serializar una instancia de mensaje a un Json para ser enviado luego
        public static string ToJson(Mensaje mensaje)
        {
            if (mensaje.CONTENIDO == "")
            {
                mensaje.CONTENIDO = "ENVIO";
            }
            string Json = JsonSerializer.Serialize(mensaje);
            return Json;
        }

        // una funcion para pasar un Json en un objeto mensaje
        public static Mensaje ToMensaje(string Json)
        {
            string json = Json.TrimEnd('\0'); // esto para eliminar los \0 que estan al final
            // de forma similar, uso el metodo de la clase generica JsonSerializer y le indico que trabaje con tipo Mensaje
            Console.WriteLine(json);
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


        List<Mensaje> mensajeList = new List<Mensaje>();

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

        public void RecibirMensaje(object manejador)
        {

            ManejadorInput manejadorInput = (ManejadorInput)manejador;

            while (true)
            {
                byte[] buffer = new byte[1024];
                socket.Receive(buffer);

                string msg = Encoding.ASCII.GetString(buffer);

                Mensaje mensaje = Program.ToMensaje(msg);

                if (buffer.Length > 0)
                {
                    Console.WriteLine($"{mensaje.NOMBRE}: {mensaje.CONTENIDO}");
                    //Console.WriteLine(manejadorInput.INPUT);
                    Console.SetCursorPosition(manejadorInput.INPUT.Length + 2, Console.CursorTop);
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

    class ManejadorInput
    {

        string input;
        bool enviado;

        public ManejadorInput()
        {
            input = "";
            enviado = false;
        }


        public string INPUT
        {
            get { return input; }
            set { input = value; }
        }

        public bool ENVIADO
        {
            get { return enviado; }
            set { enviado = value; }

        }

        // enter = 13
        // backspace 8

        static int Desplazamiento = 2;

        public void ManejarInput()
        {
            Console.CursorLeft = Desplazamiento;
            INPUT = "";
            while (true)
            {


                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(intercept: true); // intercept no permite que la consola 
                                                                                  // escriba en pantalla las entradas
                string l = consoleKeyInfo.KeyChar.ToString();
                string info = consoleKeyInfo.Key.ToString();



                if (info == "LeftArrow" & Console.CursorLeft > 2)
                {
   
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    //Console.Write("");
                    //Console.SetCursorPosition(posIzqNew, Console.CursorTop);
                }

                if (info == "RightArrow" & INPUT.Length + Desplazamiento > Console.CursorLeft)
                {
                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                }

                if (info == "Backspace" & Console.CursorLeft > 2)
                {
                    INPUT = INPUT.Remove(Console.CursorLeft - Desplazamiento - 1, 1);
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }

                if (info == "Enter" & ENVIADO == false)
                {
                    ENVIADO = true;
                    break;
                }

                else if (info != "Enter")
                {
                    if (info != "Backspace" & info != "RightArrow" & info != "LeftArrow")
                    {
                        if (INPUT.Length > 0)
                        {
                            INPUT = INPUT.Remove(Console.CursorLeft - Desplazamiento, 0).Insert(Console.CursorLeft - Desplazamiento, l); // esta solucion la saque de stackOverflow, esta buena, considerarla
                        }
                        else
                        {
                            INPUT += l;
                        }
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    }
                    int L = Console.CursorLeft;
                    int T = Console.CursorTop;
                    Console.SetCursorPosition(Desplazamiento, Console.CursorTop);

                    Console.WriteLine(INPUT + String.Concat(Enumerable.Repeat(" ", Console.BufferWidth)));
                    Console.SetCursorPosition(L, T);

                    ENVIADO = false;
                }
            }

        }

    }
}
