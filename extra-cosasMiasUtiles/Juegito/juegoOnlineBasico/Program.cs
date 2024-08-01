using System;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml;
using juegoOnlineBasico;

namespace Principal
{

    //TODO: asegurarse de crear locks, para el dibujado general, y para la modificacion de las listas que se comparten entre modulos, para
    // ello crear funciones lockeadas para la modificacion de las bibliotecas, y crear funciones lockeadas para el posicionamiento del cursor

    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            // Console.SetWindowSize(100, 50);
            //Console.SetBufferSize(100, 50);

            Menu menu = new Menu(">>>");



            while (true)
            {
                menu.DibujarMenu();
                menu.DibujarCursor();

                string key = Console.ReadKey(intercept: true).Key.ToString();


                if (key == "UpArrow" & menu.posCURSOR > (Console.WindowTop / 2) + 5)
                {
                    menu.posCURSOR -= 5;
                }
                else if (key == "DownArrow" & menu.posCURSOR < (Console.WindowTop / 2) + 10)
                {
                    menu.posCURSOR += 5;
                }

                else if (key == "Enter")
                {
                    if (menu.posCURSOR == (Console.WindowTop / 2) + 5)
                    {
                        Console.Clear();
                        //Console.WriteLine("Crear partida!!!");

                        Server server = new Server("26.34.159.22", 27015);
                        server.Iniciar();
                    }
                    else if (menu.posCURSOR == (Console.WindowTop / 2) + 10)
                    {
                        Console.Clear();
                        //Console.WriteLine("Unirse a partida!!!");
                        Random rnd = new Random();
                        int iD = rnd.Next(0, 1000);
                        object locker = new object();
                        Jugador jugador = new Jugador(20, 20, iD, locker);

                        Dictionary<int, Jugador> jugadores = new Dictionary<int, Jugador>();
                        Dictionary<int, Jugador> jugadoresT = new Dictionary<int, Jugador>(); // una lista para recordar el estado anterior de recibir un nuevo mensaje con los datos de los jugadores

                        List<Bala> balas = new List<Bala>();

                       

                        Cliente cliente = new Cliente("26.34.159.22", 27015, jugador, jugadores, jugadoresT, balas);
                        Task.Run(cliente.Iniciar);
                        LogicaJuegoC(jugador, jugadores, jugadoresT, balas, locker);

                    }

                }

            }



        }

        public static void LogicaJuegoC(Jugador jugador, Dictionary<int, Jugador> jugadores, Dictionary<int, Jugador> jugadoresT, List<Bala> balas, object locker)
        {


            Timer timerBalas = new Timer();
            Timer timerDisparo = new Timer();
            int bPosX = jugador.POSX;
            int bPosY = jugador.POSY;
            Entrada entrada = new Entrada();
            Task.Run(() =>
            {
                LeerEntrada(entrada);
            });
            
            Task.Run(() => { 
                TimerBalas(timerBalas);
            });

            Task.Run(() =>
            {
                TimerDisparo(timerDisparo);
            });
            
            while (true) {
                bPosX = jugador.POSX;
                bPosY = jugador.POSY;
                //entrada.tecla = Console.ReadKey(intercept: true).Key.ToString();
                if (entrada.tecla == "LeftArrow")
                {
                    jugador.POSX -= 1;
                    entrada.tecla = "";
                    jugador.FACING = 0;

                }
                else if (entrada.tecla == "RightArrow")
                {
                    jugador.POSX += 1;
                    entrada.tecla = "";
                    jugador.FACING = 1;

                }
                else if (entrada.tecla == "UpArrow")
                {
                    jugador.POSY -= 1;
                    entrada.tecla = "";
                    jugador.FACING = 2;

                }
                else if (entrada.tecla == "DownArrow")
                {
                    jugador.POSY += 1;
                    entrada.tecla = "";
                    jugador.FACING = 3;

                }
                else if (entrada.tecla == "Spacebar" & timerDisparo.FINALIZADO)
                {

                    Bala bala;
                    if (jugador.FACING == 0)
                    {
                        bala = new Bala(0, jugador.POSX, jugador.POSY);
                        jugador.BALAS.Add(bala);
                       // balas.Add(bala);
                    } else if (jugador.FACING == 1) {
                        bala = new Bala(1, jugador.POSX, jugador.POSY);
                        jugador.BALAS.Add(bala);
                      //  balas.Add(bala);
                    } else if (jugador.FACING == 2)
                    {
                        bala = new Bala(2, jugador.POSX, jugador.POSY);
                        jugador.BALAS.Add(bala);
                       // balas.Add(bala);
                    } else if (jugador.FACING == 3)
                    {
                        bala = new Bala(3, jugador.POSX, jugador.POSY);
                        jugador.BALAS.Add(bala);
                      //  balas.Add(bala);
                    }
                    timerDisparo.FINALIZADO = false;
                    entrada.tecla = "";
                }

                if (bPosX != jugador.POSX || bPosY != jugador.POSY)
                {

                    jugador.Borrar(bPosX, bPosY);
                }
                jugador.Dibujar();

                if (jugador.VIDA <= 0)
                {
                    jugador.MUERTO = true;
                }


                // aca no deberia hacer falta porque no se modifica el coso, pero hay que ir viendo

                foreach (KeyValuePair<int, Jugador> par in jugadores.ToArray()) // el ToArray() es porque la lista de jugadores se pude modificar en pleto forEach, entonces tengo que usar un array temporal
                                                                                // para evitar este error 
                {
                    if (par.Key != jugador.ID)
                    {
                        par.Value.LOCKER = locker;
                        if (jugadoresT[par.Key].POSX != par.Value.POSX || jugadoresT[par.Key].POSY != par.Value.POSY)
                        {
                            
                            par.Value.Borrar(jugadoresT[par.Key].POSX, jugadoresT[par.Key].POSY);
                            
                        }
                        par.Value.Dibujar();

                    }

                }

                ManejarBalas(balas, locker, timerBalas, jugador);   


                
                // Console.CursorLeft = 5;
                 //Console.CursorTop = 20;
                 //Console.Write(JsonSerializer.Serialize(jugadores.ToDictionary()) + "||||");




            }
        }

        public static void PosicionarCursor(int x, int y, object locker, string dato)
        {
            lock(locker) {
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(dato);
            }
        }

        public static void PosicionarCursor(int x, int y, object locker, string dato, ConsoleColor color)
        {
            lock (locker)
            {
                Console.ForegroundColor = color;
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.Write(dato);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void LeerEntrada(Entrada ent)
        {
            while (true)
            {
                ent.tecla = Console.ReadKey(intercept: true).Key.ToString();
                // Task.Delay(50);
            }
        }

        static void ManejarBalas(List<Bala> balas, object locker, Timer timer, Jugador jugador)
        {
            if (timer.FINALIZADO)
            {
                try
                {

                    foreach (Bala bala in balas.ToArray())
                    {
                        //Task.Delay(100);
                        Program.PosicionarCursor(bala.SPOSX, bala.SPOSY, locker, " ");
                        //Console.CursorLeft = bala.SPOSX;
                        //Console.CursorTop = bala.SPOSY;
                        // Console.Write(" ");


                        if (bala.DIRECCION == 0)
                        {
                            bala.SPOSX -= 1;
                        }
                        if (bala.DIRECCION == 1)
                        {
                            bala.SPOSX += 1;
                        }
                        if (bala.DIRECCION == 2)
                        {
                            bala.SPOSY -= 1;
                        }
                        if (bala.DIRECCION == 3)
                        {
                            bala.SPOSY += 1;
                        }
                        bala.DISTANCIA += 1; 


                        if (bala.SPOSX == jugador.POSX & bala.SPOSY == jugador.POSY)
                        {
                            jugador.VIDA = 0;
                        }

                        if (bala.comprobarDistancia())
                        {
                            balas.Remove(bala);
                        } else
                        {
                           Program.PosicionarCursor(bala.SPOSX, bala.SPOSY, locker, "*", ConsoleColor.Yellow);
                        }
                       
                        
                        //Console.CursorLeft = bala.SPOSX;
                        // Console.CursorTop = bala.SPOSY;
                        //Console.Write("*");

                    }
                    timer.FINALIZADO = false;   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        static async void TimerBalas (Timer timer )
        {
            while(true)
            {
                await Task.Delay(10);
                timer.FINALIZADO = true;
            }
        }

        static async void TimerDisparo (Timer timer )
        {
            while (true)
            {
                if (timer.FINALIZADO == false)
                {
                    await Task.Delay(1000);
                    timer.FINALIZADO = true;
                } 
            }
        }
    }


    class Timer
    {
        bool finalizado;

        public bool FINALIZADO { get { return finalizado; } set { finalizado = value; } }


        public Timer()
        {
            finalizado = true;
        }
    }


    class Menu
    {

        int posCursor;
        string cursor;
        int cursorBeforePos;

        public int posCURSOR
        {
            get { return posCursor; }
            set { posCursor = value; }
        }

        public string CURSOR
        {
            get { return cursor; }
            set { cursor = value; }
        }


        public Menu(string cursor) {

            this.cursor = cursor;
            posCursor = (Console.WindowTop / 2) + 5;
        }


        public void DibujarMenu()
        {
            Console.CursorLeft = (Console.BufferWidth / 2) - 5;
            Console.CursorTop = (Console.WindowTop / 2) + 5;

            Console.Write("Crear Partida.");

            Console.CursorLeft = (Console.BufferWidth / 2) - 5;
            Console.CursorTop = (Console.WindowTop / 2) + 10;

            Console.Write("Unirse a Partida.");

        }

        public void DibujarCursor()
        {

            Console.CursorLeft = (Console.BufferWidth / 2) - 9;
            Console.CursorTop = cursorBeforePos;

            

            Console.Write(String.Concat(Enumerable.Repeat(" ", CURSOR.Length)));

            Console.CursorTop = posCursor;
            Console.CursorLeft = (Console.BufferWidth / 2) - 9;
            Console.Write(cursor);
            cursorBeforePos = posCursor;

        }




    }


    class Jugador
    {
        int posX;
        int posY;
        int id;

        object locker;

        string spriteSombrero = "__▄▄__";
        string spriteCabeza =   "  ▀▀  ";
        string spriteCuerpo =   "┌█▒▒█┐";

        int facing;

        int vida;
        bool muerto;

        List<Bala> balas;

        public int POSX { get { return posX; } set { posX = value; } }

        public int POSY { get { return posY; } set { posY = value; } }

        public int ID { get { return id; } set { id = value; } }

        public int FACING { get { return facing; } set { facing = value; } }

        public int VIDA { get { return vida; } set { vida = value; } }

        public bool MUERTO { get { return muerto; } set { muerto = value; } }   

        public List<Bala> BALAS { get { return balas; } set { balas = value; } }

        public object LOCKER { get { return locker; } set { locker = value; } } 
        //public string SPRITEC { get { return spriteCabeza; } set { spriteCabeza = value; } }
        //public string SPRITECU { get { return spriteCuerpo; } set { spriteCuerpo = value; } }


        public Jugador() {
            balas = new List<Bala>();
            this.vida = 100;
        }

        public Jugador(int posX, int posY, int id, object locker) {
            this.posX = posX ;
            this.posY = posY;
            this.id = id;
            balas = new List<Bala>(); 
            this.locker = locker;
            this.vida = 100;
        }

        public void Dibujar()
        {
            if (muerto == false)
            {
                Program.PosicionarCursor(POSX, POSY, LOCKER, spriteSombrero);
                Program.PosicionarCursor(POSX, POSY + 1, LOCKER, spriteCabeza);
                Program.PosicionarCursor(POSX, POSY + 2, LOCKER, spriteCuerpo);
            }
            else
            {
                Program.PosicionarCursor(POSX, POSY, LOCKER, spriteSombrero, ConsoleColor.Red);
                Program.PosicionarCursor(POSX, POSY + 1, LOCKER, spriteCabeza, ConsoleColor.Red);
                Program.PosicionarCursor(POSX, POSY + 2, LOCKER, spriteCuerpo, ConsoleColor.Red);
            }
        }

        public void Borrar(int bPosX, int bPosY)
        {
            Console.CursorLeft = bPosX;
            Console.CursorTop = bPosY;                     
            Program.PosicionarCursor(bPosX, bPosY, LOCKER, "      ");
            //Console.Write("    ");

            Program.PosicionarCursor(bPosX, bPosY + 1, LOCKER, "      ");

            Program.PosicionarCursor(bPosX, bPosY + 2, LOCKER, "      ");
            
            //Console.CursorLeft = bPosX;
            //Console.CursorTop = bPosY + 1;

            //Console.Write("       ");
        }

    }


    class Entrada
    {
        public string tecla;

    }


    class Bala
    {

        int direccion;
        int sposx;
        int sposy;

        int distancia;
        //int bposx;
        //int bposy;

        public Bala() { }

        public Bala(int direccion, int x, int y)
        {
            this.direccion = direccion;
            sposx = x;
            sposy = y;
            distancia = 0;
          //  bposx = x;
          //  bposy = y;
        }


        public bool comprobarDistancia()
        {
            if (direccion == 3 || direccion == 2) {
                if (distancia > 10) { return true; }
            } else if (direccion == 1 || direccion == 0) {
                if (distancia > 30) { return true;}
            }
            return false;

        }


        public int DIRECCION { get { return direccion; } set { direccion = value; } }
        public int SPOSX { get { return sposx; } set { sposx = value; } }
        public int SPOSY { get { return sposy; } set { sposy = value; } }

        public int DISTANCIA { get { return distancia; } set { distancia = value; } }

       // public int BPOSX { get { return bposx; } set {  bposx = value; } }
        //public int BPOSY { get { return bposy; } set { bposy = value; } }


    }
}