using System;

using juegoOnlineBasico;

namespace Principal
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(100, 50);
            Console.SetBufferSize(100, 50);
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
                        Jugador jugador = new Jugador(20,20);
                        Cliente cliente = new Cliente("26.34.159.22", 27015, jugador);
                        Task.Run(cliente.Iniciar);  
                        LogicaJuegoC(jugador);

                    }
                    
                }

            }



        }


        public static void LogicaJuegoC(Jugador jugador)
        {
            int bPosX = jugador.POSX;
            int bPosY = jugador.POSY;

            while (true) {
                bPosX = jugador.POSX;
                bPosY = jugador.POSY;
                string tecla = Console.ReadKey(intercept: true).Key.ToString();
                if ( tecla == "LeftArrow")
                {
                    jugador.POSX -= 1;
                }
                else if (tecla == "RightArrow")
                {
                    jugador.POSX += 1;
                }
                else if (tecla == "UpArrow")
                {
                    jugador.POSY -= 1;
                }
                else if (tecla == "DownArrow")
                {
                    jugador.POSY += 1;
                }
                jugador.Borrar(bPosX, bPosY);
                jugador.Dibujar();


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

        string spriteCabeza = "  []";
        string spriteCuerpo = "-([])-";

        public int POSX { get { return posX; } set { posX = value; } }

        public int POSY { get {  return posY; } set { posY = value; } } 

        public string SPRITEC { get { return spriteCabeza; } set { spriteCabeza = value; } }
        public string SPRITECU { get { return spriteCuerpo; } set { spriteCuerpo = value; } }


        public Jugador(int posX, int posY) {
            this.posX = posX ;
            this.posY = posY;
            
        }

        public void Dibujar()
        {
            Console.CursorLeft = posX;
            Console.CursorTop = posY;

            Console.Write(SPRITEC);

            Console.CursorLeft = posX;
            Console.CursorTop = posY + 1;

            Console.Write(SPRITECU);
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

}