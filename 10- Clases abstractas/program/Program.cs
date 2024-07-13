using System;

// DESARROLLO BASICO DE LA SINTAXIS PARA CLASES ABSTRACTAS Y SELLADAS
namespace ClasesAbstractas
{
    class Program()
    {
        public static void Main(string[] args) { 
            
        
        }

        // para crear una clase abstracta, se usa el modificador abstract antes de class
        abstract class Humanos
        {
            int edad;
            string nombre;


            public Humanos()
            {
                edad = 0;
                nombre = string.Empty;
            }


            public Humanos(int edad, string nombre)
            {
                this.edad = edad;
                this.nombre = nombre;
            }


            // ahora desarrollamos un metodo abstracto, este metodo abstracto debe ser desarrollado en todas las clases de forma singular que hereden de la clase abstracta humanos
            public abstract void MostrarNombre();


            // un par de getters
            public int getEdad() { return edad; }
            public string getNombre() {  return nombre; }

        }


        // luego, creamos una clase sealed, sealed hara que esta clase NO permita ser heredara por otra, es como decir, aqui termina
        sealed class Proletario : Humanos
        {
            public Proletario()
            {

            }
            // de forma similar a los metodos virtuales, hacemos override para cambiar este metodo, solo que al ser abstracto estamos obligados a desarrollarlo, diferente al virtual 
            // que simplemente nos permitia invalidarlo, aqui es obligatorio invalidar el metodo abstracto y desarrollarlo

            // ademas, podemos hacer que el metodo sea sealed, si la clase no fuera sealed, aun asi podemos limitar algunos metodos para que no puedan ser sobreescritos, que es lo que hace
            // en los metodos, estos aun asi si seran heredados
            sealed public override void MostrarNombre()
            {
                Console.WriteLine(getNombre()); 
            }
        }

    }

}