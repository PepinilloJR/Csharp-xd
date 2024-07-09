using System;

// este script sera mas que nada una recopilacion de cosas basicas sobre arrays
// adicionalmente, voy a decir que son mas o menos el equivalente a las tuplas (aunque no la misma cosa) que vimos en AED
// es decir mas adelante se veran Listas que no tienen las limitaciones de los array
namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            // declaracion de un array ----------------------------------


            int[] array = {1,2}; // declarar array inicializado --> tmb es posible int[] array = [1, 2];

            Console.WriteLine(array[1]);

            int[] array2; // declarar array sin inicialiar 
            array2 = new int[2]; // instanciar array vacio indicando cantidad de elementos
            // luego podemos asignarle estos valores
            array2[0] = 1; array2[1] = 2;


            int[] array3; // declarar array sin inicializar
            array3 = new int[] { 4, 5, 6 }; // instanciamos el array declarando los elementos que lo componen, podemos indicar tambien el tamaño pero es irrelevante


            // una forma similar a la anterior pero mas simple, es usar la expresion de coleccion
            int[] array4;
            array4 = new int[] { 1, 2, 3}; // inicializamos el array usando la expresion de coleccion

            // cada forma de crear estos arrays tiene una ventaja dependiendo la implementacion que necesitemos 


            // esta ultima puede ser util para clases anonimas
            // declaracion implicita
            var arrayImplicito = new[] { 1, 2, 3 }; // en este caso, el tipo del array se infiere de sus elementos y debe inicializarse con la declaracion
                                                    // tal como toda otra variable implicita, es decir de tipo var
                                                    // --> es posible ingresar elementos de distinto tipo, pero C# los considerara del tipo mas dominante
                                                    // --> ejemplo si mezclo int y floats, definira que son floats
                                                    // --> si mezclo string e int, ya es mucho y revienta
                                                    // --> algo util de esto es que podriamos declararles dentro clases anonimas y la inferencia se encargaria sola
            
            // por ejemplo, aqui un array de objetos de tipo clase anonima 
            var arrayImplicito2 = new[]
            {
                new { Nombre = "xd", casas = new [] { "caca", "pueblo", "castillo" }},
                new { Nombre = "pito", casas = new [] { "caca", "pueblo", "castillo", "puta"}}
            };
            Console.WriteLine(arrayImplicito2[0].Nombre);
            Console.WriteLine(arrayImplicito2[1].casas[3]);



            // donde apuntan los array!?!?!? ------------------------------------------------

            // recordemos que un array apunta a una direccion de memoria, por lo tanto, veamos esto
            array4 = array3;
            array4[2] = 564;
            Console.WriteLine(array3[2]); // veremos que mostrara 564 y no 4, esto porque modificamos el array4, que apunta al mismo lugar que el array3 al haber declarado
                                          // array4 = array3; , modificando tambien al array3, en si, son el mismo array porque apuntan a la misma direccion de memoria 
                                          
            // una correcta forma de copiar los datos del array3 al array4 seria usando el metodo copy
            // Copy o CopyTo son metodos de los array que toman como argumento primero el array donde copiare los datos de mi array y luego el indice desde donde 
            // comenzamos a copiar
            array3.CopyTo(array4, 0);
            Console.WriteLine($"{array4[0]}, {array4[1]}, {array4[2]}"); // no notaremos diferencia porque ya hechamos el moco de  array4 = array3; pero se entiende

            // esto afectara tambien arrays pasados como argumento, el array que pasemos como argumento, cualquier cambio dentro del metodo que se le aplique
            // al parametro, afectara al array original, al compartir misma direccion


            // metodos tipicos de arrays ---> voy a ir agregando

            Console.WriteLine($"ver longitud del array: Longitud {array2.Length}");

            Console.WriteLine($"usar foreach con el array");
            foreach (int elemento in array3) // se recorre por elemento el array, la sintaxis es intuitiva, que paja explicar lo obvio
            {
                Console.WriteLine(elemento);
            }
        }
    }


}
