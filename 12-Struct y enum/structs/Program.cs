using System;


// la estructuras tiene ciertas caracterisitas referidas a la memoria pero no es importante por lo menos para mi

// lo que si, vamos a diferenciarlas rapidamente de las clases, porque resulta dificil al recien aprenderlas

// diferencias de una estructura a una clase

// -las intancias de las clases son punteros a los objetos construidos, por lo tanto la asignacion de tipo objeto1 = objeto2 se refiere a las direcciones
//  de memoria que señalan estas instancias

// -las estructuras se almacenan de misma forma que las variables o instancias, es decir, que la asignacion del tipo struc1 = struc2 se refiera directamente a las estructuras,
//  y no a un puntero o direccion de memoria, como NO lo seria con un objeto o los arrays

// - no puede existir sobrecarga de constructor y tampoco constructor por defecto, no existe el concepto de herencia
//   por lo que son selladas y no pueden heredar

// - los campos deben ser inicializados en el constructor

// --------------- cuando usamos struct? ------------------------------------

// -cuando se trabaja con muchos datos en memoria, debido al tipo de almacenamiento, struct es mas rapido
//  en ese sentido, por ejemplo, cuando se manejan arrays grandes, trigonometria, numeros complejos, etc.

// -cuando no se requiera manejar objetos

// -en general cuando la implementacion de la estructura que queremos crear es simple



namespace Estructuras
{
    // depaso, damos un rapido ejemplo del uso de la palabra clave enum

    // un enum colecciona un conjunto de variables constantes de tipo entero que definimos dentro de un contexto
    // aqui por ejemplo dentro del contexto del namespace
    // por lo que seran usadas en todas las clases

    // la sintaxis es la siguiente
    // si no definimos sus valores, C# le asigna un valor entero segun su posicion
    // dentro de la coleccion, por lo tanto seria dIz = 0 , dDer = 1 y Darr = 2
    enum NumerosIrracionales { dIz = -5, dDer = 5, Darr = 2 }; // le podemos asignar otro valor int como se muestra en la sintaxis, pero se limita a tipos enteros



    class Program
    {
        static void Main(string[] args) {

            // puedo obtener cada constante del enum de la siguiente forma
            NumerosIrracionales dIz = NumerosIrracionales.dIz;
            Console.WriteLine($"el valor estatico que obtuvimos es {dIz}"); // si lo mostramos, vemos que mostrara el nombre de la constante, esto no es un string,
                                                                          // es asi como se muestra un enum

            // para mostrar su valor numerico o usarlo, debemos convertirlo a algun tipo numero, del siguiente modo
            Console.WriteLine($"el valor del desplazamiento izquierdo es {(int)dIz}");


            //---------ahora, veamos que hacemos con las estructuras de abajo------------------

            // notar que como el constructor es solo uno, y no existe herencia, el new llama siempre al constructor que pertenece al tipo Punto
            // porque estamos definiendo un tipo Punto, muy distinta a si fuera una clase
            Punto punto = new(20,20);
            Cuadrado cuadrado = new(5,5);

            Console.WriteLine($"{punto.ToString()} y {cuadrado.ToString()}");

            // desplazamos a la izquierda el punto
            punto.x += (int)dIz;
            Console.WriteLine($"desplazamiento realizado: {punto.ToString()}");
        }
    }

    public struct Punto
    {
         public int x;
         public int y;

        public Punto(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"punto ubicado en las coordenadas {x} y {y}";
        }

    }


    // una forma mas simplificada y a razon de la no existencia de sobrecarga de constructores, podemos definir al struct como el constructor en si mismo, llamada constructor principal
    public struct Cuadrado(int ladoX, int ladoY) 
    {
        public int ladoX = ladoX;
        public int ladoY = ladoY;

        public override string ToString()
        {
            return $"el area del cuadrado es {this.ladoX * this.ladoY}";
        }
    }
}
