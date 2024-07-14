using System;


namespace Genericos
{
    class Program
    {
        static void Main(string[] args) { 
            
            // aqui entonces podemos definirle el tipo que usara para esta instancia
            ClaseGenerica<String> lista = new ClaseGenerica<String>(10);
        
            // luego, podemos hacer otra pero de otros tipos, usando la misma clase

            ClaseGenerica<int> edades = new ClaseGenerica<int>(20);

            lista.agregarElemento("pedro", 0);

            edades.agregarElemento(15, 25);

            Console.WriteLine("todo bien loquita");

            // ahora probamos con la clase generica limitada por el metodo Soypelotudo();

            
            AlmacenPelotudos<Pelotudo> listaP = new AlmacenPelotudos<Pelotudo>(20);

            listaP.agregarElemento(new Pelotudo(), 2);

            listaP.GetLista()[2].SoyPelotudo();





            // luego, para mezclar entre clases que se heredan entre si como 
            // haciamos como con Materiales, caja, paquetes
            // tendremos que empezar a usar el casting, aun asi, esto nos ahorra 
            // mucha implementacion y nos permite hacer multiples listas de muchas clases
            // aunque tambien puede usarse para cualquier otra cosa,
            // el punto es que con genericos podemos hacer clases que traten con cualquier 
            // tipo, desde primitivos a mas complejos
        }

    }
    // a veces es nescesario crear una clase 
    // que sea capaz de contener y manejar multiples tipos
    // esto puede desarrollarse usando herencia y el casting
    // PERO, hay una mejor forma desde C#


    // se utiliza la siguiente sintaxix
    class ClaseGenerica<T> // la T es el denominado generico, es como decir "un tipo cualquiera"
    {
        // en si, T es como si fuera una variable que almacena un tipo y que puede usarse
        // en vez de las palabras clave que lo definen al tipo que queremos usar
        
        // la clase sera un almacen de objetos cualquiera sean

        // el array almacena tipos genericos, es decir, un array de "un tipo cualquiera"
        
        T[] valores;

        public ClaseGenerica(int cantidad) {
            // sera de cantidad espacios
            valores = new T[cantidad]; // un array de cantidad genericos
        }

        public void agregarElemento(T elemento, int Pos)
        {
            try
            {
                valores[Pos] = elemento;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
    // es posible limitar los tipos que acepta la clase mediante interfaces, es decir, solo aceptamos tipos
    // que contienen los metodos nescesarios que determinamos en una interfaz

    // se hace con la siguiente sintaxis
    class AlmacenPelotudos<T> where T : Ipelotudos
    {

        // ahora solo se aceptara el tipo si este contiene el metodo Soypelotudo();

        T[] valores;
        public AlmacenPelotudos(int cantidad)
        {
            valores = new T[cantidad]; 
        }

        public void agregarElemento(T elemento, int Pos)
        {
            try
            {
                valores[Pos] = elemento;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public T[] GetLista()
        {
            return valores;
        }

    }


    interface Ipelotudos
    {
        void SoyPelotudo();
    }


    class Pelotudo : Ipelotudos
    {
        public void SoyPelotudo()
        {
            Console.WriteLine($"soy pelotudo");
        }
    }

}