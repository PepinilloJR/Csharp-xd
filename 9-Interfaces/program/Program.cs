using System;


namespace Intefaces
{
    class Program
    {
        static void Main(string[] args) { 
            Empleado empleado = new Empleado();
            Cajero cajero = new Cajero();
            VendedorRopa vendedorRopa = new VendedorRopa(); 

            Console.WriteLine($" el salario generico es {empleado.GetSalario()}");
            Console.WriteLine($" el salario base para un cajero es {cajero.GetSalario()}");
            Console.WriteLine($" el salario de un cajero vendedor de ropa es {vendedorRopa.GetSalario()}");

            Console.WriteLine("las frases de atencion para cada cajero son: ");
            cajero.Atender();
            vendedorRopa.Atender();

        }

    }

    // creamos una estructura de clases para el ejemplo

    class Empleado {

        protected int salario;

        public Empleado() {
            salario = 500; // salario base
        }

        public int GetSalario()
        {
            return salario;
        }
    }


// las interfaces definen una especie de contrato, en la que todas las clases que lo firman deben tener implementados metodos definidos en esta interfaz
// --> con misma cantidad de parametros y tipo
// por ejemplo, creemos una interfaz para la clase Cajero y sus derivados
// por convencion todas las interfaces comienzan con I
interface ICajeros
    {
        // listamos los metodos que todos los cajeros deben de tener, con la siguiente sintaxis, no requieren modificadores de acceso o desarrollo en llaves
        void Atender();
    }

interface IVendedores
    {
        void Atender(); // prestar atencion a este metodo repetido
        void MostrarVentars();
    }

    // luego, a las clases que requieren esta interfaz, deben heredarla, similar a con las clases padre, simplemente ponemos coma para indicar otra herencia
    // y le metemos la interfaz
    // es escencial colocar primero la clase padre, luego las interfaces (pueden ser mas de una)
    class Cajero : Empleado, ICajeros { 
        
        public Cajero()
        {
            salario = 1000; 
        }

        // si no desarrollaramos esto, marcaria un error
        public virtual void Atender()
        {
            Console.WriteLine("te estoy atendiendo uwu");
        }
    }

    class VendedorRopa : Cajero, ICajeros { 

        public VendedorRopa()
        {
            salario += 400;
        }

        public override void Atender()
        {
            Console.WriteLine("te estoy vendiendo ropa");
        }
    }
}
