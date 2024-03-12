using System;

using programa;

// que es un metodo

// un metodo es un comportamiento que designamos a una clase, sencillamente, a la hora de instanciarla o al llamarla (caso metodos estaticos)
// podemos acceder a estos metodos

namespace programa
{

    class ProgramaPrincipal()
    {
        int numeroMistico = 0; // una propiedad o campo de la clase, lo vemos mas adelante

        // metodo main, de tipo void, que implica que no devolvera una valor
        // es static lo que implica que puede llamarse el metodo directamente desde la clase
        // sin tener que instanciarla 
        // ya sabemos la funcion que cumple el metodo main
        static void Main(String[] args) // ya sabemos que son los argumentos
        {

            // en main instanciamos un objeto para llamar 
            // al metodo que creamos, lo cual es extraño
            // PARA UN CASO ASI, DEBERIAMOS DECLARAR EL METODO COMO ESTATICO
            var objeto = new ProgramaPrincipal();
            Console.WriteLine(objeto.SumatoriaEnteros(-1, 2));
            
            // por ejemplo aca, llamamos otro pero lo llamamos desde la propia clase ya que es estatico
            Console.WriteLine("sumatoria de numeros: " + ProgramaPrincipal.SumatoriaNumeros(-1, 2));
            // ejemplo de sobrecarga
            Console.WriteLine("sumatoria de numeros: " + ProgramaPrincipal.SumatoriaNumeros(-1, 2, 3));
            // ejemplo de parametro opcional
            Console.WriteLine("sumatoria de numeros: " + ProgramaPrincipal.Volumen(1,2));

            Console.ReadKey(false); // el false en el metodo ReadKey desactiva la espera a otra tecla del usuario

        }

        // -----------------------------creacion de metodos-----------------------------

        // para crear otro metodo, la sintaxis es la misma pues


        // en este caso decimos que devuelva un entero, los parametros tambien deben
        // indicarsele el tipo
        int SumatoriaEnteros(int limInf, int limSup)
        {
            int sumatoria = numeroMistico; // las propiedades estan en el contexto superior y por lo tanto son accesibles desde los metodos
            for (int i = limInf; i <= limSup; i++)
            {
                sumatoria += i;
            }

            return sumatoria;
        }

        // los metodos tambien aceptan la nomenclatura de arrow function o Lambda expression (buscar en documentacion para saber mas, medio al pedo)
        // las Lambda expression pueden ser utiles si otro metodo solicita de parametro una funcion y no el return de una funcion, y queremos declararla
        // directamente en los parametros de ese otro metodo

        // un ejemplo de Lamba expression como la siguiente funcion estatica

        static int SumatoriaNumeros(int num1, int num2) => num1 + num2; // directamente se le indica que retorna, es para funciones sencillas

        // ---- sobrecarga de metodos ---
        
        // la sobrecarga de metodos se da cuando existen mas de un metodo con el mismo nombre, pero se diferencian por el numero de parametros
        // o por el tipo de los parametros

        // en este caso, cuando ingresemos 3 parametros a la hora de llamar al metodo, se ejecutara el que coincida con los parametros
        // esto puede ser util para dar flexibilidad a alguna biblioteca que armemos 
        static int SumatoriaNumeros(int num1, int num2, int num3) => num1 + num2 + num3;

        // otra forma de flexibilizar es con parametros opcionales, que no es mas que inicializar un parametro con un valor por defecto
        static decimal Volumen(decimal lado1, decimal lado2, decimal lado3 = 0) => lado1 + lado2 + lado3; // vemos que lado3 inicia con 0 por defecto

    }

}