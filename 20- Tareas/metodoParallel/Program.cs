using System;
using System.Threading.Tasks.Sources;

namespace tareasParalelas
{

    // en este inciso planteamos el uso de la clase Parallel, que provee varios metodos estaticos
    // que permiten la creacion y manejo de bucles de tareas paralelas, simplificando la implementacion
    // que en primer lugar seria mas compleja usando solo la clase Task
    class Program
    {


        static void Main(string[] args)
        {

            // entonces, es posible crear un for de tareas paralelas de la siguiente forma

            // toma primero el indice inicial, el indice final y un delegado para su ejecucion
            // al que podemos pasarle un entero como parametro el cual sera la posicion 
            // del indice
            Parallel.For(0, 5, (int i) => { tareaParalela(i); });

            // es una solucion corta y rapida, su funcionamiento se basa en ejecutar un conjunto de hilos
            // segun lo permita el sistema, para que se realice de forma paralela la ejecucion de una funcion
            // esto puede implementarse de manera propia pero este metodo nos ahorra eso

            // considerar que es una funcin asincrona, no realmente paralelo, en el sentido de que cada 
            // "iteracion" se realizara al mismo tiempo, si no que multiples iteraciones se ejecutan 
            // de forma multiprogramada en los nucleos del procesador
        }
    
        
        static void tareaParalela(int i)
        {
            Console.WriteLine($"esta es una tarea paralela {i}");
        }
    
    
    
    }





}