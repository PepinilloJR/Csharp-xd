using System;

using juegoOnlineBasico;

namespace Principal
{

    class Program
    {

        static void Main(string[] args)
        {

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
                if (key == "DownArrow" & menu.posCURSOR < (Console.WindowTop / 2) + 10)
                {
                    menu.posCURSOR += 5;
                }

                if (key == "Enter")
                {
                    if (menu.posCURSOR == (Console.WindowTop / 2) + 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Crear partida!!!");

                        Server server = new Server("26.34.159.22", 27015);
                        server.Iniciar();
                    }
                    else if (menu.posCURSOR == (Console.WindowTop / 2) + 10)
                    {
                        Console.Clear();
                        Console.WriteLine("Unirse a partida!!!");

                        Cliente cliente = new Cliente("26.34.159.22", 27015);
                        cliente.Iniciar();  
                    }
                    
                }

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




}