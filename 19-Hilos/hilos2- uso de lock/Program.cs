// continuando con hilos, ahora un ejemplo para el lockeo de hilos

using System;

namespace hilos2
{


    // haremos un descontador para crear una condicion de carrera con varios threads

    // se supone que cuando se llegue a cero, no se pueda descontar mas

    // como sabemos, existe la posibilidad de que hilos simultaneos descuenten la variable al mismo tiempo
    // lo que podria producir que se descuente incluso a numeros negativos

    // para ello se introduce la instruccion lock()


    class Program
    {
       

        static void Main(string[] args)
        {

            // objeto que usaremos para el lock, podemos usar varios para diferentes locks
            Object objetoLock = new Object();

            // objeto que contendra nuestro contador que queremos descontar
            Contenedor Contenedor = new Contenedor();

            // objeto que se encargara de crear e iniciar los hilos con los objetos nescesarios para nuestra
            // aplicacion
            ManejadorHilos manejadorHilos = new ManejadorHilos();


            // creamos el Array de hilos 
            Thread[] HILOS = manejadorHilos.crearHilos(objetoLock, Contenedor);

            // iniciamos los hilos uno a uno, generando bastante disparidad en los tiempos de ejecucion, con lo que sin un lock
            // hace cosas locas, veamos mas abajo
            manejadorHilos.IniciarHilos(HILOS);

        }
    }

    class ManejadorHilos {

        public Thread[] crearHilos(Object objetoLock, Contenedor contenedor) {

            Thread[] hilos = new Thread[4];
            for (int i = 0; i < 4; i++)
            {
                hilos[i] = new Thread(() => descontar(objetoLock, contenedor));
                hilos[i].Name = $"hilo {i}";
            }
            return hilos;
        }

        public void IniciarHilos(Thread[] hilos)
        {
            
            foreach (Thread t in hilos)
            {
                t.Start();

            }
        }

        public void descontar(Object objetoLock, Contenedor contenedor)
        {

            // PUEDE SUCEDER que dos hilos entren al if cuando Contenedor > 0 y resten cuando no se debia
            // entonces, una solucion es usar lock, de modo que cuando un hilo ejecute la funcion, bloquee al resto
            // hasta terminar, de cierto modo es bloquear una seccion critica del codigo a los otros hilos 
            // mientras otro lo esta ejecutando, evitando asi una condicion de carrera o peor aun un interbloqueo

            // para ello utiliza monitores, tema teorico de hilos y de sistemas operativos
            // muy por encima puede explicarse su funcionamiento del siguiente modo:

            // el modo de implementar el monitor es con un objeto que se usa de referencia para detectar cuando un hilo entro 
            // a seccion critica, la instruccion lock llama a la funcion Enter de la clase Monitor, que se encarga de mantener 
            // la constancia de que el objeto esta siendo usado por uno de los hilos, si otro hilo entra a la seccion critica
            // ejecutara la instruccion lock y por lo tanto el Enter, pero este detectara que el objeto esta en uso 
            // y hara esperar al hilo hasta que el objeto se libere cuando lock llame a la funcion Exit de la clase Monitor al finalizar 
            // las acciones dentro de sus llaves, liberando asi el objeto y permitiendo a otros hilos ejecutarse

            // ahora, cada vez que un hilo intente descontar, llamara la funcion Enter en el objeto y hara que si otro lo intenta
            // deba esperar que este termine el descuento, de este modo aseguramos que ningun hilo entre simultaneamente dentro
            // del if (Contenedor > 0) y arruine nuestro descontador

            for (int i = 0; i < 3; i++)
            {
                // veremos que con el lock no sucedera que mas de un hilo reste 50 a la cantidad del contenedor
                lock (objetoLock)
                {
                    if (contenedor.CANTIDAD > 0)
                    {
                        contenedor.CANTIDAD -= 50;
                        Console.WriteLine(Thread.CurrentThread.Name + " desconto 50" + $" quedando {contenedor.CANTIDAD}");

                    }
                    else {
                        Console.WriteLine(Thread.CurrentThread.Name + " no pudo descontar ya que no hay mas contenido");
                    }
                } 
            }
        }

    }

    class Contenedor
    {

        int cantidad;

        public Contenedor()
        {
            cantidad = 500;
        }

        public int CANTIDAD { 
            get { return cantidad; }
            set { cantidad = value; }
        }
    }
}

