// LINQ es un lenguaje de consulta de datos integrado en C#, muy parecido a SQL

// nos sera util para realizar operaciones en la que intervienen
// estructuras de datos complejas como arrays, listas o tambien con bases de datos

using System;

using System.Linq;

namespace probandoLINQ
{
    class Program
    {
        static void Main(string[] args) {
            int[] valores = {1,2,6,7,8,9, 3, 4, 5, 10,14, 11, 12, 13, 15};

            // probemos por ejemplo filtrar los numeros pares y guardarlos en una lista
            // la solucion sin el lenguaje de consulta puede ser sencilla pero requiere 
            // bucles y mas codigo

            // uso Linq, de la siguiente forma
            // LINQ incluye varias palabras clave para la consulta de datos
            // veamos que resulta en una coleccion generica IEnumerable que podemos convertir en lista

            // en nuestro caso nos interesa los tipos de filtros que podemos implementar en las consultas


            // primero, usamos la palabra where para aplicar el filtro, aqui el filtro es numero % 2 == 0 para saber que es par

            // luego, la siguiente palabra clave sera la accion que se llevara a cabo para formar la lista, en este caso
            // select guarda en el nuevo IEnumerable el elemento filtrado, donde podemos aplicarle una operacion, como pueden serlo matematicas
            // o una comparacion booleana, y nos devolvera entonces un IEnumerable con los elementos resultado de este select, por lo que hay que estar
            // seguros que la salida sera la misma que definimos en la declaracion del IEnumerable numerosPares, ya que nuestro select podria guardar 
            // el elemento pero modificado, y podria ser un booleano por ejemplo
            
            // en este caso, filtra los numeros pares y los multiplica por 4

            IEnumerable<int> numerosPares = (from numero in valores where numero % 2 == 0 select numero * 4);

            Console.WriteLine("Primer ejemplo--------------------------------");
            foreach (int numero in numerosPares)
            {
                Console.WriteLine(numero);
            }

            // esto sirve para objetos y sus propiedades, en el select podemos aplicar metodos
            // y demas

            // otras palabras clave utiles pueden ser orderby, que sirve para ordenar segun un booleano los elementos del array
            // o coleccion de turno

            // GENERO LA LISTA A USAR

            List<Numero> listaNumeros = new List<Numero>();
            for (int i = 0; i < 10; i++)
            {
                listaNumeros.Add(new Numero());
            }

            // ahora ordenaremos la lista segun su propiedad VALOR

            // con orderby, le indicamos segun que propiedad debe ordenar los objetos dentro de la lista, y luego select 
            // simplemente guarda los valores

            // adicionalmente, podemos agregarle el filtro where para agregar solo aquellos tipo Numero que tengan VALOR mayor a 100

            // ademas, si agregamos la palabra clave descending luego del orderby y la propiedada a ordenar, invierte el ordenamiento de menor a mayor, dando mayor a menor
            IEnumerable<Numero> numerosOrdenados = from numero in listaNumeros orderby numero.VALOR descending where numero.VALOR > 10 select numero;

            Console.WriteLine("Segundo ejemplo--------------------------------");
            foreach (Numero numer in numerosOrdenados)
            {
                Console.WriteLine(numer.VALOR);   
            }


            List<LetraNumero> listaNumerosLetras = new List<LetraNumero>();
            for (int i = 0; i < 10; i++)
            {
                listaNumerosLetras.Add(new LetraNumero());
            }


            // ahora, podemos filtrar basandonos en diferentes listas
            // por ejemplo, filtramos los elementos que coinciden entre las dos listas listaNumeros y listaNumerosLetras
            // usando la palabra clave join, join hacer coincidir dos elementos de dos listas comparandolos basado en algo
            // indicado por la palabra clave on y un condicional clave que puede ser equals, is, as, with, switch (donde los elementos deben ser del mismo tipo)

            // de forma similar tambien podemos aplicar un filtro a los valores que se ingresan con un where al final y un booleano, luego, el select ingresa el numero a la coleccion

            IEnumerable<Numero> Coincidentes = from numero in listaNumeros join letra in listaNumerosLetras on numero.VALOR.ToString() equals letra.VALOR where numero.VALOR > 10 select numero;

            Console.WriteLine("tercer ejemplo--------------------------------");

            foreach (Numero coincidencia in Coincidentes)
            {
                Console.WriteLine(coincidencia.VALOR);
            }


            // al ser un lenguaje de consultas existen muchas posibilidades y otras palabras clave, ademas existe una profunca conexion con SQL que puede aprovecharse pero que se vera en otro script
        }
    }


    class Numero {
        int valor;
        public int id = 0;
        public Numero() {
            Random rand = new Random();
            valor = rand.Next(0, 50);
        }

        public int VALOR {
            get { return valor; }
            set { valor = value; }
        }


    }

    class LetraNumero
    {
        string valor;
        public int id = 1;
        public LetraNumero()
        {
            Random rand = new Random();
            valor = rand.Next(0, 50).ToString();
        }

        public string VALOR
        {
            get { return valor; }
            set { valor = value; }
        }


    }

}