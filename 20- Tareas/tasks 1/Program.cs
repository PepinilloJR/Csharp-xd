using System;


// habiendo visto hilos, es correcto usar ahora un concepto de un nivel de abstraccion 
// superior a los hilos, denominado como task

// en este sentido, una task sera una promesa en forma de funcion, que se realiza de forma
// asyncrona, y se deja a disposicion del sistema la creacion de los threads y su manejo 
// para cumplir esta tarea, similar a los thread pools, por lo que nosotros tenemos control desde un nivel mas 
// alto en la programacion que usando threads 

// entonces, al crear una tarea, esta se pone en fila y sera ejecutada en un hilo cuando este disponible
// es decir cuando el procesador sea capaz, por ello lo de "promesa", todo esto manejado por el sistema


namespace task
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("hola");
            // creamos una tarea y le asignamos su funcion
            Task tarea = new Task(TareaBasica);

             // iniciamos la tarea

            Task tarea2 = new Task(TareaBasica);
            

            tarea.Start();
            tarea2.Start();

            // hasta ahora muy similar a los hilos


            // una forma reducida de crear una tarea es con el meteodo run
            // hace la creacion y el start en la misma linea
            Task tarea3 = Task.Run(TareaBasica);

            // si queremos tareas consecutivas
            // debemos llamar al metodo ContinueWith de la tarea anterior que nos devolvera
            // una task que continua solo cuando la primera termina
            // ademas, la funcion que queremos para la tarea debe de recibir como parametro un objeto 
            // tipo task
            Task tarea4 = tarea3.ContinueWith((Task tarea) => { TareaNoTanBasica(); });
            // cabe resaltar que sera el mismo thread que se le asigno a la tarea anterior
            Console.ReadLine();
        }


        static void TareaBasica()
        {
            for (int i = 0; i < 15; i++)
            {
                // es posible obtener el thread que se asigno al task
                Thread hiloTarea = Thread.CurrentThread;

                // vemos que el hilo siempre es uno distinto una vez que compilamos,
                // y es el que elige el sistema 
                Console.WriteLine($"Hola soy el hilo {hiloTarea.ManagedThreadId}");

            }
        }

        static void TareaNoTanBasica()
        {
            for (int i = 0; i < 5; i++)
            {
                // es posible obtener el thread que se asigno al task
                Thread hiloTarea = Thread.CurrentThread;

                // vemos que el hilo siempre es uno distinto una vez que compilamos,
                // y es el que elige el sistema 
                Console.WriteLine($"Hola soy el hilo {hiloTarea.ManagedThreadId} y soy muy complejo");
            }

        }
    }


}