using System;

// veremos el concepto de propiedades, las propiedades son una forma distinda de acceder a los campos de clase, que es mas comoda y
// se acerca mas a las costumbres de la programacion estructurada

namespace Propiedades
{
    class Program ()
    {
        public static void Main(string[] args )
        {
           // ahora, con las propiedades, podemos tratarlas como si fueran variables public, realizando operaciones directas, y la propiedad tratata estas operaciones
           // con los metodos que les definimos

           Perro perro = new Perro ();

            perro.PATAS += 4; // como se puede ver, podemos realizar operaciones directas y todo el proceso funcionara con los metodos definidos en la propiedad
                                // de esta forma podemos continuar cumpliendo con las reglas del POO y al mismo tiempo hacerlo mas comodamente
                                // de por si las propiedades actuan como una especie de interfaz entre el programador y los metodos de los campos de clase  
            perro.EDAD += 10;

            //perro.VICTIMAS += 10;
            Console.WriteLine ($"patas: {perro.PATAS},  edad: {perro.EDAD}, victimas: {perro.VICTIMAS}");


        }

        // creamos una clase para el ejemplo

       

    }
    class Perro
    {
        // definimos el campo de clase que usaremos para la propiedad

        int patas;
        int edad;
        int victimas;
        // el constructor
        public Perro()
        {
            patas = 0;
            edad = 0;
        }


        // como sabemos, los campos estan encapsulados, entonces, para accederlos, debemos usar metodos, pero llega a ser incomodo
        // a la larga, generando muchas lineas de codigo de forma incomoda

        // entonces, aca entran las propiedades, POR CONVENCION SE DEFINEN LOS NOMBRES DE LAS PROPIEDADES CON MAYUSCULAS
        public int PATAS {
            // dentro de la propiedad, podemos definir los metodos tipicos para el campo de clase encapsulado
           
            // dentro de los metodos posibles en la propiedad definimos sus comportamientos
            get { 
                return patas; 
            }
            set { 
                patas = value; // la palabra clave value hace referencia al valor que se le intenta ingresar a la propiedad mediante '='
            } 

        }
        // otra forma es desarrollar los metodos set y get primero y luego asignarlos en una propiedad

        public int GetEdad ()
        {
            return edad;    
        }
        public void SetEdad(int edad)
        {
            this.edad = edad;
        }

        public int EDAD
        {
            get { 
                return GetEdad(); 
            }

            set { 
                SetEdad(value); 
            }

        }
        // es util para ahorrar codigo usar expresiones lambda para las propiedades
        public int VICTIMAS
        {
            // una ventaja tambien es que visual entiende bastante bien de esto y probablemente autocomplete todo de forma correcta y nosotros solo tendremos que tabear 
            get => victimas;
            //set => SetVictimas(value); 
            // podemos evitar la escritura o la lectura simplemente no definiendo el metodo

        }

        // una funcion Set para ejemplificar como se colocan estas en una expresion lambda, o una de las formas
        // la funcion en si es bastante mala pero sirve para el ejemplo xd
        public void SetVictimas(int victimas)
        {
            if (victimas > 0)
            {
                this.victimas = victimas;
            } else
            {
                throw new ArgumentException();
            }
        }


    }
}