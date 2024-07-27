using System;
using System.Text.Json;
using juegoOnlineBasico;

namespace Principal
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(100, 50);
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
                        Jugador jugador = new Jugador(20,20, iD);

                        Dictionary<int, Jugador> jugadores = new Dictionary<int, Jugador>();

                        Cliente cliente = new Cliente("26.34.159.22", 27015, jugador, jugadores);
                        Task.Run(cliente.Iniciar);  
                        LogicaJuegoC(jugador, jugadores);

                    }
                    
                }

            }



        }


        public static void LogicaJuegoC(Jugador jugador, Dictionary<int, Jugador> jugadores)
        {
            int bPosX = jugador.POSX;
            int bPosY = jugador.POSY;
            Entrada entrada = new Entrada();
            Task.Run(() =>
            {
               LeerEntrada(entrada);
            });

            while (true) {
                bPosX = jugador.POSX;
                bPosY = jugador.POSY;
                //entrada.tecla = Console.ReadKey(intercept: true).Key.ToString();
                if (entrada.tecla == "LeftArrow")
                {
                    jugador.POSX -= 1;
                    entrada.tecla = "";
                }
                else if (entrada.tecla == "RightArrow")
                {
                    jugador.POSX += 1;
                    entrada.tecla = "";
                }
                else if (entrada.tecla == "UpArrow")
                {
                    jugador.POSY -= 1;
                    entrada.tecla = "";
                }
                else if (entrada.tecla == "DownArrow")
                {
                    jugador.POSY += 1;
                    entrada.tecla = "";
                }

                if (bPosX != jugador.POSX || bPosY != jugador.POSY)
                {
                    jugador.Borrar(bPosX, bPosY);
                    jugador.Dibujar();
                }


                foreach (KeyValuePair<int, Jugador> par in jugadores.ToArray()) // el ToArray() es porque la lista de jugadores se pude modificar en pleto forEach, entonces tengo que usar un array temporal
                                                                                 // para evitar este error 
                {
                    if (par.Key != jugador.ID)
                    {
                        par.Value.Dibujar();

                    }

                }
                Console.CursorLeft = 5;
                Console.CursorTop = 20;
                Console.Write(JsonSerializer.Serialize(jugadores) + "||||");
                
                


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
            set {  cursor = value; }
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

        string spriteCabeza = "  []";
        string spriteCuerpo = "-([])-";

        public int POSX { get { return posX; } set { posX = value; } }

        public int POSY { get {  return posY; } set { posY = value; } } 

        public int ID { get { return id; } set { id = value; } }
        

        //public string SPRITEC { get { return spriteCabeza; } set { spriteCabeza = value; } }
        //public string SPRITECU { get { return spriteCuerpo; } set { spriteCuerpo = value; } }


        public Jugador(int posX, int posY, int id) {
            this.posX = posX ;
            this.posY = posY;
            this.id = id;
        }

        public void Dibujar()
        { 
            Console.CursorLeft = posX;
            Console.CursorTop = posY;

            Console.Write(spriteCabeza);                
            Console.CursorLeft = posX;
            Console.CursorTop = posY + 1;

            Console.Write(spriteCuerpo);

        }

        public void Borrar(int AposX, int AposY)
        {
            Console.CursorLeft = AposX;
            Console.CursorTop = AposY;
                          
            Console.Write("    ");

            Console.CursorLeft = AposX;
            Console.CursorTop = AposY + 1;

            Console.Write("       ");
        }

    }


    class Entrada
    {
        public string tecla;

    }
}