using System;
using System.Collections.Generic;

// ahora veremos las colecciones, las colecciones son clases genericas implementeadas en c#
// que permiten almacenar diferentes tipos de datos, en particular, veremos las listas
// que son mas flexibles que los arrays y es tipico su uso para aplicaciones mas complejas
// y los diccionarios, luego, otros tipos de colecciones pueden encontrarse en la documentacion

// son mas pesados, pero nos permiten aplicar metodos tales como ordenar, añadir, eliminar, buscar, etc
// de una forma mas flexible y rapida 

namespace colecciones
{
    class Program
    {
        static void Main(string[] args)
        {

            // --------------------------------listas-----------------------------------------------------
            // la idea es hacer algunas operaciones con listas como para ver su sintaxis y metodos incluidos
            // como sabemos, la lista es una clase generica, entonces su instanciacion es algo que ya sabemos
            
            // aclarando el valor del generico
            List<int> list = new List<int>();   

            // podemos usar tambien para inicializar la expresion de coleccion

            List<string> list2 = ["abc", "dfg", "hij"];


            // podemos añadir al final de la lista un valor tal que asi
            list.Add(1);
            list.Add(6);
            list.Add(2);
            list.Add(5);
            list2.Add("caca");

            // podemos acceder a un valor

            list[1] = 2;

            // podemos ordenarla

            list.Sort();

            // es posible recorrerlas como un array
            foreach (int i in list)
            {
                Console.WriteLine(i);

            }
            foreach (string y in list2)
            {
                Console.WriteLine(y);

            }


            //----------------- linkedList -----------------------------------------

            // ahora, veamos las linkedLists, estas a diferencia de las listas estan implementada por nodos enlazados en lugar de espacios de memoria adyacente como si sucede con las listas

            // por eso es mas eficiente usar estas cuando se realizan operaciones de eliminar y agragar constantemente, ya que lleva menos pasos en su implementacion interna en memoria
            // en si, cada nodo de la linked list apunta a su anterior y posterior nodo, de modo que solo hay que cambiar 2 direcciones de 2 nodos cuando se elimina uno, y no mover todos los valores
            // en memoria para que esten todos adyacentes como sucede con las Listas normales, esta ventaja se paga con mas memoria ya que consumen mas memoria las linkedList

            // creamos una LikedList de forma similar
            LinkedList<int> list3 = new LinkedList<int>();


            // en este caso se permite agregar al final y al inicio un valor
            list3.AddLast(1);
            list3.AddFirst(6);
            list3.AddLast(2);
            list3.AddFirst(5);
        
            // tiene otros metodos pero tambien perdemos otros como el sort, el ordenamiento debera ser manual, ademas, el acceso a un nodo en particular es mas complejo que con un indice
            // para esto ultimo se usa el metodo
            Console.WriteLine(list3.ElementAt(1));

            // podemos ver la cantidad de nodos 

            Console.WriteLine($"cantidad de nodos {list3.Count}");
           
            // podemos recorrerlo de multiples formas

            Console.WriteLine("primer bulce");
            foreach (int i in list3)
            {
                Console.WriteLine(i);
            }

            // asi haria yo un for con la linked list

            Console.WriteLine("segundo bulce con remove");

           // hice una funcion manual para retirar todos los nodos, pero puede hacerse rapidamente
           // con list3.Clear()
            int longitud = list3.Count;
            for (int i = 0; list3.Count > 0; i++) {
                int j = i - (longitud - list3.Count);
                Console.WriteLine($"posicion {j}");
                Console.WriteLine($"valor eliminado: {list3.ElementAt(j)}");
                list3.RemoveFirst(); // esta tarea ocupa menos tiempo de procesador
            }


            // ----------------------------Diccionario ---------------------------------

            // los diccionarios son listados a los cuales cada valor se les asigna una llave, esta llave permite el rapido acceso al valor que le pertenece
            // lo que nos puede llevar a implementaciones rapidas O(n) para busquedas en bases de datos por ejemplo, esto porque se implementan con tablas hash que aproximan a este rendimiento
            // sin embargo son las mas pesadas y abusar puede llevar a usar mucha memoria

            // en este caso, usa dos genericos que son las key y los value, cada uno debe ser de un tipo
            Dictionary<string, int> edades = new Dictionary<string, int>();

            // podemos añadir 


            edades.Add("pedro", 20);
            edades.Add("julia", 12);
            edades.Add("rodrigo", 10);
            edades.Add("tomas", 27);

            Console.WriteLine("diccionarios!!");

            // podemos acceder rapidamente a un elemento mediante su llave

            Console.WriteLine(edades["pedro"]);
            // tambien puede accederse mediante su posicion a los objetos KeyValuePair
            // que estan dentro del listado diccionario
            Console.WriteLine(edades.ElementAt(0)); // <--- muestra el par con la llave y el valor


            // para el foreach, es algo mas complejo
            // los valores dentro del diccionario son del tipo KeyValuePair
            foreach (KeyValuePair<string,int> i in edades)
            {
                Console.WriteLine($"nombre: {i.Key} ; edad: {i.Value}");
            }
        }


    }

}