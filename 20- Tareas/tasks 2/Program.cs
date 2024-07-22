using System;

// continuamos con las Tasks

namespace tasks2
{
    class Program
    {
        static void Main(string[] args)
        {
            // existen otros modos para sincronizar las tareas
            // como lo son los metodos Wait, WaitAny y WaitAll

            Task tarea1 = Task.Run(() => { TareaBasica(); });
            Task tarea2 = Task.Run(() => { TareaBasica2(); });


            // WaitAll nos dira que la siguiente Task debera esperar a que finalicen 
            // las task pasadas por parametro
            Task.WaitAll(tarea1, tarea2);

            Task tarea3 = Task.Run(() => { TareaBasicaAll(); });

            // WaitAny nos dira que la siguiente Task debera esperar a que finalicen 
            // una de las task pasadas por parametro, cualquier sea

            Task.WaitAny(tarea1, tarea3);

            Task tarea4 = Task.Run(() => { TareaBasicaAny(); });


            // para una secuencialidad podemos usar Wait que obliga a la siguiente Task
            // a esperar a que finalice la Task, en este caso desde un metodo de la propia task

            // esto indica que se espere a finalizar la tarea4 antes de continuar
            tarea4.Wait();

            Task tarea5 = Task.Run(() => { TareaBasica(); });

            Console.ReadLine();


        }


        static void TareaBasica () { 
        
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"soy el hilo {Thread.CurrentThread.ManagedThreadId}");
            }
        
        
        }

        static void TareaBasica2()
        {

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"soy el hilo {Thread.CurrentThread.ManagedThreadId}");
            }


        }

        static void TareaBasicaAll()
        {

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"soy ALL y el hilo {Thread.CurrentThread.ManagedThreadId}");
            }


        }
        static void TareaBasicaAny()
        {

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"soy ANY y el hilo {Thread.CurrentThread.ManagedThreadId}");
            }


        }

    }


}