// para este ejemplo tambien aprenderemos sobre IO (in/out), una subclase de System que permite la lectura de archivos, sean secuencias de bits, bytes y lo que venga


namespace usoDelFinally
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LecturaDatosTexto("a"));
        }
        
        // vamos a crear una funcion que lea numeros de un texto y los sume
        static int LecturaDatosTexto(string datos)
        {
            System.IO.StreamReader lector = null; // declaramos un lector de tipo StreamReader, StreamReader es una subclase de IO que permite leer una secuencia de bytes
                                                  // lo que le indicamos aca, es que es un tipo StreamReader pero que no contiene ninguna instancia todavia, esto debido a que no podemos simplemente declarar una var como null
                                                  // esto es asi porque las variables de tipo var funcionan con tipo implicito, es decir, debe haber algo que le diga al compilador que tipo son, por lo tanto, deben inicializarse
                                                  // al mismo tiempo que se declaran, pero nosotros no queremos hacer eso, por ello, hicimos lo anterior descrito 

            // y si, podemos declarar variables con clases de este modo, es decir, tipos StreamReader, ya que las clases declaran en si nuevos tipos de datos mas complejos, diferentes de los tipos primitivos como int, float, etc.


            int sumatoria = 0;
            try
            {
                // inicializamos el lector de bytes creando una nueva instancia de la clase StreamReader, el cual necesita de un path, es decir, creamos un objeto StreamReader
                lector = new System.IO.StreamReader("texto.txt"); // originalmente lo declaramos como un objeto vacio, llamamos a su constructor con los datos que queremos
                // en este metodo le decimos que lea y devuelva toda la secuencia desde la posicion actual del puntero de lectura, en este caso 0 (estamos leyendo a partir del byte 0)
                string texto = lector.ReadToEnd();
                for (int i = 0; i <= texto.Length; i++) {
                    int val = int.Parse(texto[i].ToString());  // voy leyendo y convirtiendo a enteros los valores del archivo
                    sumatoria += val;   
                }


            } catch (FormatException ex) {
                Console.WriteLine("hay valores no numericos en el archivo de texto, por lo tanto, se entrega cero para no alterar resultados");
                Console.WriteLine(ex.Message);
                sumatoria = 0;
            } catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("El archivo buscado no existe");
                Console.WriteLine(ex.Message);

            } finally // ahora si, la estructura finally, acompaña al try catch funcionando como un codigo que se ejecutara dando igual si hay o no una excepcion, en este caso, lo usamos para cerrar siempre el stream
            {
                // cerramos el stream, un stream es un flujo de datos atraves de un canal que utiliza un buffer para almacenar datos temporalmente mientras se transfieren
                // de un punto a otro, este debe ser liberado una vez que terminemos de usarlo, para liberar recursos por el resto del programa

                if (lector != null)
                {
                    Console.WriteLine("CERRANDO LECTURA DE DATOS");
                    lector.Close();
                }
            }
                
            

            return sumatoria;
            
        }
    }

}


// para aprender mas sobre la lectura de archivos, es solo investigar sobre System.IO y especificamente para la lectura de archivos binarios, investigar sobre seek, seekOrigin y algoritmos para la lectura de estos archivos
