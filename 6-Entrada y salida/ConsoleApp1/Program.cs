using System.Drawing;

namespace EntradaSalida
{
    class Program
    {   
        static void Main(string[] args)
        {
            Logica.CrearHilos();
        }

    }
    
    class Logica
    {
        public static async Task CrearHilos()
        {
            Thread thread1 = new Thread(new ThreadStart(void () =>
            {
                //Jugador jugador = new Jugador();
                //jugador.Borrar();
            }));

            Thread thread2 = new Thread(new ThreadStart(void () =>
            {
                Enemigo enemigo = new Enemigo();
                enemigo.Escribir();
            }));

            thread1.Start();
            thread2.Start();


            
        }
    }



    class Enemigo
    {
        public void Escribir()
        {
            while (true)
            {
                Thread.Sleep(400);
                Console.WriteLine("lol");
            }
        }
    }

    class Jugador
    {
        int Color { get; set; }
        int Peso { get; set; }

        public void Borrar()
        {
            while (true)
            {
                Console.Se
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}