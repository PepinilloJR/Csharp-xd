using System;
using System.IO;


// como sabemos, hay situacion en el que el recolector de basura se encarga de liberar memoria para objetos no referenciados o perdidos

// pero pueden darse situaciones muy especificas en que esto no sucede, y se generen las conocidas memory leaks, para evitar eso podemos manualmente 
// mediante el uso de los destructores, limpiar memoria al destruir objetos que conocemos que se perderan 

// para este ejemplo lo haremos con un stream de datos en la lectura de un archivo, pero ejemplos hay multiples

namespace Destructores
{
    class Program()
    {
        public static void Main(string[] args)
        {

            // inicializamos el objeto lector que hicimos
            LectorArchivo lector = new LectorArchivo(@"C:\Users\pepin\OneDrive\Escritorio\proyectos en .NET\13- Destructores\ConsoleApp1\bin\Debug\net8.0\textoaleer.txt");

            lector.MostrarTexto();
            lector.MostrarLinea(3);


        }


    }

    // la clase de ejemplo
    class LectorArchivo
    {
        //StreamReader reader; // definimos para la clase un stream
        string path;

        // este de aqui no hace nada, es para el ejemplo del destructor
        StreamReader reader_importante = new StreamReader(@"C:\Users\pepin\OneDrive\Escritorio\proyectos en .NET\13- Destructores\ConsoleApp1\bin\Debug\net8.0\textoaleer.txt");

        public LectorArchivo(string path)
        {
            this.path = path;

        }

        public void MostrarTexto()
        {

            // el StreamReader no tiene seek asi que hay crear uno para empezar desde el principio (que verga)
            StreamReader reader = new StreamReader(path);
            string texto = reader.ReadToEnd();

            Console.WriteLine(texto);

            // luego, debemos destruir el stream con close, esto en definitiva cierra el stream y libera los recursos, es nescesario hacer esto
            reader.Close();
        }

        public void MostrarLinea(int linea)
        {

            StreamReader reader = new StreamReader(path);
            StreamReader reader2 = new StreamReader(path);
            int cont = 0;
            string _linea = "";
            int longitud = reader.ReadToEnd().Length;
            reader.Close();

            while (cont < linea && cont < longitud)
            {
                cont++;
                _linea = reader2.ReadLine();
            }

            Console.WriteLine(_linea);
            reader2.Close();
        }

        // ahora, debemos ser capaces de modificar ahora si, el destructor de nuestra clase
        // la siguiente sintaxis, afecta el comportamiento del garbage collector cuando esta eliminando nuestro objeto
        ~LectorArchivo() // asi definimos el destructor
        {
            // en nuestro caso los streams ya los cerramos en los metodos
            // pero podriamos cerrar aqui ese stream inutil que creamos para el ejemplo
            // aqui se asegura de liberar el stream antes de eliminar el objeto
            reader_importante.Close();
        }
        // no siempre hay que usar los destructores y su uso puede ser riesgoso y generar aun mas memory leaks si se usa indebidamente, usarlo cuando se sabe bien que se esta destruyendo
    }
}