using System;


// los thread pools son grupos de threads que se reparten tareas
// de modo que no exista un thread por tarea y ahorrar recursos al equipo
// siendo reutilizados para otra tarea una vez terminan una, de este modo 
// se ahorra la sobrecarga de eliminarlos y crearlos constantemente

// entonces optimizamos la cantidad de threads para multiples tareas repetitivas 
// que requieren muchos threads activos, eso si

// puede ser mas lento si se requiere de muchas tareas, pero ahorra mas recursos
namespace ThreadPools
{
    // la cantidad de hilos que se encuentran en el pool esta determinado por el sistema
    class Program
    {
        static void Main(string[] args)
        {
            
            for (int i = 0; i < 100; i++) {

                // cuando creamos una tarea mas desde el bucle, QueueUserWorkItem
                // pone en fila la tarea hasta que se libere uno de los threads del pool
                // cuando esto suceda se ejecutara la tarea

                // el QueueUserWorkItem admite que la tarea tenga un parametro 
                // de tipo Object que podemos pasarle como lo hacemos con I

                // de este modo varias tareas pueden trabajar sobre un dato distinto
                ThreadPool.QueueUserWorkItem(TareaRepetitiva, i);
            
            
            }
            Console.ReadLine();

        }


        // el objeto objetoAlmacen servira para pasar a las tareas datos distintos de cualquier tipo
        static void TareaRepetitiva(Object objetoAlmacen)
        {

            int NTarea = (int)objetoAlmacen;

            Random rand = new Random();
            int numeroAleatorio = rand.Next(1, 1000);

            Console.WriteLine($"hilo {Thread.CurrentThread.ManagedThreadId} con tarea {NTarea} obtuvo: " + (numeroAleatorio * 4));

        }

    }



}