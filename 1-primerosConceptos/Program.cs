// sintaxis basica

// para utilizar diferentes namespaces sin mencionarlos usamos using
// de este modo escribimos menos

using System;
using System.Timers;
using PrimerNamespace;

/* toda instruccion que no sea declaracion de clase, metodo, funcion, bucle y otras estructuras de control
 deben terminar con ; 

int bolas = 0; // lleva punto y coma

while(bolas < 4) // no lleva punto y coma
{
    Console.BackgroundColor = ConsoleColor.Red;
    Console.WriteLine("cola rota 24"); // lleva punto y coma
    bolas++; // recordemos algunos recortes de C para sumar 1 a una variable uwu
}

 lo escencial es entender las reglas de C#

 C# necesita que todo este dentro de una clase para su correcto uso

 supongamos que queremos crear dos clases denominadas programa_p

 de ser asi, necesitamos usar un namespace, que nos permitira tener clases repetidas pero de 
 distinto namespace, como si fuera una carpeta que contiene a una clase, y otra carpeta 
 que contiene a la otra

 en definitiva, es una direccion para acceder a todas las clases que creamos dentro de ese namespace

 -------------- IMPORTANTE --------
si antes no se define using con el namespace indicado, sera nescesario colocarlo antes de llamar a una clase
por ejemplo, si no usaramos using system, habria que escribir para el console, lo siguiente
 --> system.Console.metodo  

 */

// Console.WriteLine("sas"); -->  si una instruccion esta fuera y antes de la definicion de las clases, se denomina instruccion de
// orden superior y se sobrepone a la ejecucion de Main, ojo con eso

namespace PrimerNamespace
{
    // con class creamos una clase, contenida en el namespace 'PrimerNamespace'
    class Programa_p
    {
        /* el siguiente metodo estatico denominado Main puede colocarse en cualquier clase
         el metodo se ejecuta al ejecutar el programa, siendo el unico metodo estatico que se llama sin necesidad de que se especifique
         de este modo funciona como el hilo principal de ejecucion
         se denomina como punto de entrada y solo puede existir uno solo dentro del script

        un metodo static es un metodo que no necesita que la clase se instancie para ser llamado, es decir, podemos llamarlo desde la clase y no
        nescesariamente de un objeto de la clase, por ejemplo Destructor.destruirElUniverso() seria un metodo estatico ya que lo estamos
        llamando desde la clase y no desde un objeto destructorito

         el argumento de entrada que tiene el metodo main, representa los parametros de la consola
         que es un array de strings, y pueden obtenerse con Environment.GetCommandLineArgs()

         tambien se puede usar como punto de entrada instrucciones de nivel superior
         que son instrucciones fuera de los metodos de cualquier clase */
        static void Main(string[] args)
        {
            int cola = 0;
            int colaEspejo = 0;
            int modulo = 0; // es el resto
            int division = 0;
            while (cola < 3)
            {

                // algunas operaciones aritmeticas
                cola++; // suma 1 
                colaEspejo--; // en este caso resta 1
                division = cola / 2;
                modulo = cola % 2; // obetenemos el resto con %
                


                if (args.Length > 0)
                {
                    Console.WriteLine(args[0]);
                }
                Console.WriteLine("hola");

                // prestar atencion aqui

                // en esta escritura, usamos un ToString para convertir un valor Entero en String, para sumarlo a la frase
                // que estamos escribiendo
                Console.WriteLine("tamaño de la cola:" + cola.ToString()); 
                // pero no es nescesario en muchos casos, en este caso la inferencia de tipos de C# entiende que
                // si le sumamos un valor No String a otro String, debe obtener un String
                // por lo tanto, podemos hacer la suma sin conversiones explicitas
                Console.WriteLine("division por 2 de la cola:" + division);
                Console.WriteLine("resto de la division:" + modulo);
                Console.WriteLine("tamaño de la cola negativa:"+colaEspejo);
            }
            // luego tenemos las conversiones

            // si tengo un valor double y un entero y quiero pasar el double a entero, puedo hacer esto

            double valor = 3.45;
            int novalor;

            novalor = (int) valor;
            Console.WriteLine("valor double 3.45 convertido a entero:" + novalor);

            // esto se denomina conversion explicita o casting

            // luego la conversion implicita son las que existen entre algunos tipos de datos
            // como el int y el long y el float y el double, haciendo uso de la inferencia de tipos que vimos antes

            int habitantes = 1000000;
            long habitantesLong = habitantes;
            Console.WriteLine("longitud de habitantes:" + habitantesLong);
            float pesoFloat = 56.56F;
            double pesoDouble = pesoFloat;
            Console.WriteLine("peso en float convertido a double:" + pesoDouble);

            // conversiones de tipo, de texto a valores numericos
            // para convertir letras a numeros, utilizamos el metodo de la clase number, parse
            string palabra = Console.ReadLine(); // la palabra no puede contener datos no numericos // lee lo que se ingresa por consola retorna string

            int numero = int.Parse(palabra); // puede hacerse con double, float, etc etc
            Console.WriteLine("numero ingresado como string convertido a int:" + numero);



            // uso de constantes

            const int value = 0; // las constantes deben inicializarse en la propia declaracion
        }
    }


}

