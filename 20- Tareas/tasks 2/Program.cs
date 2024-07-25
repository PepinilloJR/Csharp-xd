using System;

// continuamos con las Tasks

namespace tasks2
{
    class Program
    {
        static void Main(string[] args)
        {
            NumeroMagico numeroMagico = new NumeroMagico();
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


            // ahora para la cancelacion de tareas, necesitamos un CancellationTokenSource

            // el CancellationTokenSource genera un objeto que entre otras cosas tiene un token de cancelacion
            // que es una struct utilizada por un TaskFactory
            CancellationTokenSource token = new CancellationTokenSource();

            CancellationToken tokenCancelacion = token.Token; // tenemos entonces la struct Token

            Task tarea6 = Task.Run(() => { TareaCancelable(tokenCancelacion, numeroMagico); });

            while (true)
            {
                if (numeroMagico.NUM == 1)
                {
                    // luego, el ToketSource que creamos para cancelacion, una vez que la tarea recibien el token
                    // es capaz de enviar una señal de cancelacion al token
                    token.Cancel();
                    // ESTA SEÑAL NO MATA LA TASK EN SI, envia el estado de cancelacion, y el metodo debe 
                    // especificar como actua ante esto

                    // salimos del bucle de comprobacion
                    break;
                }

            }

            Console.ReadLine();

            // como se puede ver hay muchisimas herramientas y metodos utiles, a travez de la experiencia es posible
            // encontrar todos los dias herramientas utiles en este lenguaje maravilloso!!!

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

        // es posible cancelar tareas 
        // para ello requiere de una struct CancellationToken, que utiliza una de las clases 
        // implicadas en este proceso para cancelar la tarea que recibe el token

        static void TareaCancelable(CancellationToken tokenCancelacion, NumeroMagico numeroMagico)
        {
            while (true)
            {
                Random rand = new Random();

                numeroMagico.NUM = rand.Next(0, 10);
                Console.WriteLine($"el hilo {Thread.CurrentThread.ManagedThreadId} genero {numeroMagico.NUM}");

                // esta estructura contiene el estado de la cancelacion de la tarea
                if (tokenCancelacion.IsCancellationRequested)
                {
                    Console.WriteLine("SE CANCELO LA TAREA PORQUE SALIO UNO!!!");

                    // SALIMOS DEL BUCLE CUANDO NOS CANCELARON, IMPORTANTE
                    // ES DECIR, en realidad es una herramienta para poder comunicar cancelaciones entre Tasks 
                    // y no una especie de eliminacion por parte del sistema del a task
                    break;
                }
            }
        }

    }

    class NumeroMagico
    {
        int num;

        public int NUM { get { return num; }
            set { num = value; }
        }

    }
}