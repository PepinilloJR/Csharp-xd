using System;
// en este script, aprendemos a usar modulos 

// los modulos se colocan dentro de la carpeta raiz del proyecto, y estan integrados en todos los modulos por defecto, es decir
// podemos acceder a las clases de otros archivos mientras esten dentro del proyecto, sin necesidad de incluir estos archivos o modulos
namespace Modulos
{
    class Program
    {
        static void Main(string[] args)
        {
            int x;
            int y;
            try
            {
                 x = int.Parse(Console.ReadLine());
                 y = int.Parse(Console.ReadLine());
            }
            catch (FormatException ex)  // por si se ingresa cualquier cosa
            {
                
                Console.WriteLine(ex.Message);
                x = 0;
                y = 0;
            } 
            
            // habia olvidado pero esto es un tipo de string util para construir mensajes, interpolated string, donde podemos incluir
            // variables, metodos y demas, dentro del string, esto es mejor que usar strings concatenados como solemos hacerlo
            // (usando + entre cada string), es similar a Template Literals de javascript u otros similares en python etc

            Console.WriteLine($" se dibujo el punto en X={x} ; Y={y}"); // ejemplo de string interpolado
            Proceso.DibujarPunto([x, y]);

            Puntos p1 = new Puntos([10, 10]);
            Puntos p2 = new Puntos();

            int DistanciaBuscada = p1.Distancia(p2);
            Console.WriteLine($"la distancia de p1 a p2 es {DistanciaBuscada}");


            //adicionalmente comentamos la existencia de clases anonimas
            // las clases anonimas es basciamente la implementacion de un objeto sin haber definido una clase para este, si no que
            // consideramos una clase en el mismo instante que creamos el objeto, en otras palabras, es crear un objeto sin basarse en una clase anteriormente definida
            // similar a como definirias un objeto en javascript

            var p3 = new { Posicion = new int[] {5,5} }; // declaramos el objeto punto 3 de una clase anonima que tiene una posicion en un array de 2 elementos

            var p4 = new { Posicion = new int[] {1,2} }; // ahora otro punto 4, se debe notar que, si bien no declaramos el nombre de la clase o la clase en si
                                                         // C# detectara que p3 y p4 son de la misma clase, debido a la cantidad de campos, su orden y tipos, tomar en 
                                                         // consideracion todo esto
            // estas clases tienen las siguientes limitaciones
            /*
                - todos los campos son publicos
                - los campos deben ser inicializados
                - los campos NO pueden ser static
                - no existen metodos

             */

            Console.WriteLine($"la posicion de p3 es {p3.Posicion[0]};{p3.Posicion[1]}"); // pequeña demostracion de que funciona la clase anonima
        }
    }

    class Proceso
    {
        static public void DibujarPunto(int[] Posicion)
        {
            // aqui podemos ver que podemos usar la clase Puntos que definimos en el otro modulo, nada muy complejo
            Puntos puntos = new Puntos(Posicion);
            Console.SetCursorPosition(Posicion[0], Posicion[1]);
            Console.WriteLine("*");
        }

    }

}