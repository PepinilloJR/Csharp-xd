using System;

// en este construiremos una estructura tipica de herencia, partiendo desde una clase Object hasta las clases que necesitaremos implementar etc

// especificamente, la clase Object es la clase padre de todas las clases y provee metodos generales para todos los objetos y clases 
// esta ya existe en C# como sabemos, y es util notarlo, todas las clases tienen padre, excepto Object

namespace Herencia
{
    class Program
    {
        static void Main(string[] args) { 
        
            Caja caja1 = new Caja(5);
            Caja caja2 = new Caja(3);
            Paquete paquete1 = new Paquete(21);
            caja2.mostrarPeso();
            paquete1.mostrarPeso();
            Console.WriteLine(caja1.Id); // Id es static y por lo tanto por defecto public
            Console.WriteLine(caja2.getNumero());
            Console.WriteLine(caja2.getTamaño());
            caja1.mostrarIDsCantidad();

            // PRINCIPIO DE SUSTITUCION --> todo lo que sigue dentro de este main debe leerse al final

            // el principio de sustitucion permite que, si una clase es superclase de otra, es posible usar la subclase como constructor de la superclase
            // por ejemplo, un perro siempre es un animal, por lo tanto, se puede definir un animal a partir del perro, por asi decirlo
            // en terminos practicos, el constructor de la subclase considera el constructor de la superclase y por lo tanto esta ultima puede ser
            // instanciada usando una de sus subclases

            // por ejemplo

            Material material1 = new Paquete(5); // esto es bizarro pero tiene sentido desde el punto de vista del POO, ya que un Paquete es un tipo Material, 
                                                 // y el constructor del paquete considerara al constructor de la clase material, no hay conflicto o problema.
            Console.WriteLine($" resultado de usar el principio de sustitucion: {material1.getNumero()}"); // cabe aclarar que solo seran accesibles los metodos 
                                                                                                           // de la clase padre y no los de la subclase que usamos
                                                                                                           // para construir a su padre

            // esto es re loco porque entonces cierra con todo el sentido del mundo lo siguiente

            Object obj1 = new Paquete(7); // el objeto es padre de todo, y por lo tanto un objeto puede ser un paquete, BOOM!!! este principio es escencial para POO

            // esto nos permite, o mejor dicho explica, el porque es posible crear un array que contenga a varios objetos por ejemplo del tipo material,
            // pero que cada uno de estos tengan las caracteristicas de subclases mas complejas y diferentes entre si

            // por ejemplo
            Material[] Materiales = {material1, caja2, paquete1};
            Materiales[0].mostrarPeso();
            Materiales[1].mostrarPeso(); 
            Materiales[2].mostrarPeso();
            // aunque estos no contienen los datos y metodos mas complejos que definimos en las subclases
            // porque el array es del tipo Material, similar a lo que paso en la linea 33
            // --> A NO SER, que los metodos hayan sido afectador por el Override, ya que hicimos Override, el metodo mostrarIntencion() si se modificara y se mostrara 
            // diferente, mientras que el mostrar peso, se mantendra el del material, ya que en caja solo lo ocultamos, no lo invalidamos

            Materiales[0].mostrarIntencion(); // este material1 tambien mostrara el de la caja y es porque mas arriba lo instanciamos con un constructor de un Paquete
            Materiales[1].mostrarIntencion();
            Materiales[2].mostrarIntencion();

        }
    }

    // defino una clase base Material, que sera la cabeza o base para el resto de clases que definire
    class Material
    {

        static int IDs = 0;
        int numero;

        
        public Material() {
            Id = IDs;

            Random rnd = new Random();
            numero = rnd.Next(1, 10);
            IDs++;
        }
        
        public int getNumero() {
            return numero;
        }

        public int Id { get; set; }

        public void mostrarIDsCantidad()
        {
            Console.WriteLine($"la cantidad de objetos es {IDs}");
        }


        // este otro metodo lo usaremos para definir algunos conceptos de polimorfismo que estan presentes en C#

        // en este caso, el modificador virtual, indica que el metodo deberia invalidarse y reemplazarse su implementacion en las 
        // siguientes subclases, ESTO ES DIFERENTE A OCULTAR QUE SE LOGRA CON EL MODIFICADOR NEW, lo vemos en la siguiente clase
        public virtual void mostrarPeso ()
        {
            Console.WriteLine("peso indefinido");
        }

        public virtual void mostrarIntencion() // los materiales tienen intenciones no te trages nunca un U-235
        {
            Console.WriteLine("SOY UN MATERIAL BUENITO");
        }

    }

    //luego, para denotar una herencia, se usa la siguiente sintaxis

    // entonces la nueva clase caja hereda las propiedades y metodos de la clase object
    class Caja : Material
    {
        // LA CAJA ya hereda el Id estatico que le corresponde y el constructor del object se ejecuta tambien al ejecutar el constructor de la caja
        
        // adicionalmente, incluimos un nuevo modificador de acceso que es protected, el acceso protected indica que es solo accesible para su clase y sus clases derivadas
        protected int tamaño;
        

        // cuando llamemos a este constructor, tambien se llamara al constructor de la superclase, por lo que caja tendra un ID con un valor
        public Caja(int t)
        {
            // adicionalmente la caja tendra un tamaño
            tamaño = t;
            
        }

        public int getTamaño()
        {
            return tamaño;
        }



        // veamos ahora un ejemplo de polimorfismo, cambiemos el efecto que tiene el metodo mostrarPeso() en esta clase
        new public void mostrarPeso() // redefinimos? NO, OCULTAMOS el metodo mostrarPeso() de la clase padre para objetos del tipo Caja
                                      // cabe aclarar que el new es totalmente opcional para la ocultacion y
                                      // es mas bien una convencion de sintaxis para hacer notar que fue intencional
        {
           Console.WriteLine("peso algo pesado");
        }

        // ahora, si queremos realmente invalidar y reemplazar el metodo para cuando se instancia un objeto con esta clase, usamos override, QUE SOLO SE PERMITE si el metodo
        // que queremos invalidar es virtual y a diferencia de con el NEW, aqui el tipo del return y los parametros deben ser los mismos para poder invalidarla realmente

        // el metodo ToString siempre es virtual en cualquier clase
        public override string ToString() // este ToString sera asi ahora siempre que se incialice un objeto con la clase caja, incluso si el objeto es de la clase padre Material
                                          // --> esto ultimo tiene sentido si revisamos el principio de sustitucion mas arriba
        {
            // al final, el override es como modificar el metodo heredaro.
            // mientras el NEW es ocultar un metodo y usar otro con el mismo nombre y parametros, pero que es muy distinto al oculto
            return "soy yo, la caja";
        }

        public override void mostrarIntencion()
        {
            Console.WriteLine("SOY UNA CAJA CORRED");
        }
    }

    class Paquete : Caja
    {
        // con esta otra clase, como la clase Caja tiene un constructor que requiere de un parametro, debemos nosotros 
        // especificar en el constructor del Paquete los parametros nescesarios para el tipo Caja, es decir
        // cuando el constructor de alguna de las clases no es el default y requiere parametros, al heredarse esto en otra subclase
        // el constructor de esta debera especificar estos al constructor de su padre para poder construirse 

        // para esto se usa el metodo base de la siguiente forma, base llama al constructor de la clase padre y se le pueden pasar los parametros
        // con los que se quiere llamar este constructor, si el constructor de la clase padre no tiene parametros, esto no es nescesario

        int cantidadContenido;
        public Paquete(int cantidadContenido) : base(5) // llamamos al constructor de Caja con base y especificamos un tamaño de 5
        {
            this.cantidadContenido = cantidadContenido;
        }

      

    }

}