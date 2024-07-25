using System;


// considerando Task, hemos visto bastantes cosas sobre como usarlas, diferentes librerias dinamicas 
// las utilizan y los metodos relacionadas a estas para su sincronizacion, bloqueo, etc.

// pero entonces surge la necesidad de una implementacion mas moderna y general 
// haciendo uso de los tipos Task y Task<TResult>, este ultimo utilizando un generico 
// para indicar la devolucion de un valor del tipo del generico que dara la tarea que estamos
// definiendo

// esta implementacion es la denominada TAP (Task-based asynchronous pattern)
// que agrega dos palabras clave escenciales, Async y Await

namespace AsyncYawait
{

    // entonces, un proceso asincrono es aquel donde se realizan operaciones paralelas 
    // y no se conoce cual terminara primero o ultimo, este tema ya lo vimos con los threads
    // y la implementacion de alto nivel Task
    // (antes de continuar, hay que aclarar que seguimos usando Tasks, asi que todo lo aprendido
    // sigue siendo valido, tal como los tokens de cancelacion y etc).


    class Program
    {
        public static async Task Main(string[] args)
        {
            TazaCafe tazaCafe = new TazaCafe();

            await tazaCafe.Mezclar();

            Console.ReadLine(); 
        }
    }

    // para el ejemplo, creamos una clase denominada taza de cafe
    // taza de cafe requiere de cierto tiempo para completarse y puede prepararse de forma separada
    // y paralela, tal como limpiar la taza, colocar azucar, cafe, hervir el agua, etc

    class TazaCafe
    {
        int cafe; // cantidad de cucharadas de cafe
        int azucar; // cantidad de cucharadas de azucar
         
        bool limpio;

        public int CAFE { get { return cafe; } set { cafe = value; } }
        public int AZUCAR { get { return azucar; } set { azucar = value; } }

        public bool LIMPIO { get { return limpio; } set { limpio = value; } }


        // para una funcion que sera asincrona, tenemos dos opciones
        // debera ser de tipo Task<Generico> (donde devolvera lo indicado en el generico, por lo que es
        // una funcion de tipo getter)
        // o si es un tipo de funcion sin return, de tipo Task (una funcion del tipo setter)

        // a veces se puede ser tipo Void pero es inconveniente la mayoria de veces

        public async Task limpiarArriba()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("limpiando taza arriba");
                await Task.Delay(100); // para las tareas, debemos usar Task.Delay ya que thread.Sleep funciona mal con async
                                       // adicionalmente, se le agrega el await ya que Task actua de forma asincrona y mientras se solicita el Delay
                                       // es probable que el hilo actual logre continuar el bucle antes del delay
            }
        }

        public async Task limpiarAbajo()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("limpiando taza abajo");
                await Task.Delay(100);
            }
        }

        public async Task<bool> Limpiar () {


            // aqui creamos dos Task, una encargada de limpiar arriba de la taza 
            // y otra de abajo
            bool limpioA = false;
            bool limpioB = false;

            Task lArriba = limpiarArriba();

            Task lAbajo = limpiarAbajo();


            // ahora es util introducir await y un metodo muy util tambien de la libreria Task

            // await frena la evaluacion dentro de un metodo marcado como asincrono
            // a espera de que una tarea se complete, por lo que await toma como argumento un tipo Task
            // o Task<generico>

            // nosotros queremos esperar a que se completen limpiarArriba y limpiarAbajo antes 
            // de indicar que esta limpia la taza, para ellos
            // podemos usar el metodo estatico Task.WhenAll(tareas) que devuelve una tarea que 
            // se considerara completada para el await cuando las tareas pasadas por parametro
            // se hallan completado
            await Task.WhenAll(lArriba, lAbajo);
            limpioA = true;
            limpioB=true;
            if (limpioA & limpioB) { return true; } else {
                Console.WriteLine("la taza se quedo sucia");
                return false; }
        }

        public async Task ColocarContenido(int C, int A) { 
            // antes de colocar contenido, es correcto que la taza este limpia
            // debemos usar await para esperar que esta tarea se complete antes de colocar contenido

            LIMPIO = await Limpiar();

            // una vez se completa, comprobamos si esta limpia o si hubo un error

            if (LIMPIO)
            {
                CAFE = C;
                AZUCAR = A;
                Console.WriteLine("Cafe y azucar colocados");
            } else
            {
                Console.WriteLine("No se pudo colocar el cafe y el azucar");
            }

        }


        public async Task HervirAgua()
        {
            for (int i = 0;i < 3;i++)
            {
                Console.WriteLine("hirviendo agua...");
                await Task.Delay(100);
            }

            Console.WriteLine("agua hervida!");
        }


        public async Task Mezclar()
        {
            Task preparar = ColocarContenido(2, 2);
            Task prepararAgua = HervirAgua();

            //preparar.Start();
            //prepararAgua.Start();

            await Task.WhenAll(preparar, prepararAgua);
            Console.WriteLine("Cafe mezclado y preparado!");
            Console.WriteLine(this.CAFE + " " + this.AZUCAR + " " + this.LIMPIO);
        }

    }
}