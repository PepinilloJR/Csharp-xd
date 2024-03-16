// las clases y objetos a esta altura deberiamos estar familiarizados
// con su creacion e instanciacion, pero hay ciertos detalles
// que deben ser tratados para ahora si, crear correctamente clases para aplicar de mejor forma
// la programacion orientada a objetos
// leer primero la declaracion de clases y luego leer el main para entender mejor todo

namespace ClasesYobjetos
{
    class Program
    {
        static void Main(string[] args)
        {

            // instanciamos un objeto tipo Circulo de la siguiente forma
            // declaramos el objeto tipo Circulo, y luego lo instanciamos con new Circulo();
            Circulo circulito = new Circulo();
            // ahora sabemos entonces que new llama al constructor de la clase, que es el metodo Circulo(); y por ello tiene (), lo estuvimos haciendo todo el tiempo
            // pero usando un metodo constructor por default

            // creando otro ciruculo pero con el otro constructor creado por sobrecarga
            Circulo circulaso = new Circulo([20,20], 15);

            // uso metodos de acceso para modificar circulito

            circulito.setCentro([6, 7]);
            circulito.setRadio(10);


            circulito.DibujarCirculo();
            circulaso.DibujarCirculo();


            Console.ReadKey();
        }

    }

    // creando la clase circulo, partial es una forma de dividir la clase en trozos para facilitar la lectura, en este caso
    // el primer trozo trae todo lo mas importante de la creacion de clases.
    // la segunda parte, trae metodos para utilizar la clase circulo, osea darle algo de chicha al programilla.
    partial class Circulo
    {

        // el siguiente metodo es denominado constructor, es el que da un estado inicial
        // a la clase cuando se instancia, este metodo es especial y se escribe con el mismo
        // nombre que la clase, a su vez, no tiene un tipo de return especificado y debe ser publico
        // puede o no tener parametros, y puede producirse sobrecarga de constructores (tener mas de uno),
        // donde estos deben diferenciarse por el numero o tipo de parametros

        public Circulo()
        {
            centro = [5, 5];
            radio = 5;
        }
        // este metodo tiene declarado que es public, esto cambia el encapsulamiento del metodo
        // es decir, deja de ser privado y es accesible fuera de la clase, para ser accedido por 
        // otros metodos de otras clases, o ser llamado por una instancia de la clase 




        // sobrecarga de circulo, donde se puede especificar campos de clase por si no queremos los default
        public Circulo(int[] centro, int radio)
        {
            this.centro = centro;
            // usando la palabra clave this, podemos usar mismo nombre en el parametro del constructor o cualquier metodo, y en el campo de la clase.
            // this entonces es una forma de hacer referencia a la instancia actual de la clase, es decir 
            // cuando esto se ejecuta en un objeto digamos, circulito, decir this, es como decir circulito
            // entonces this para su caso, hara referencia a circulito y a la propiedad centro de circulito

            this.radio = radio;
        }


        // podemos declarar propiedades, algunas se inicializaran con el constructor 
        const double pi = 3.1416;
        int[] centro; 
        int radio;
        // en general, se recomienda dejar las propiedades encapsuladas o privadas, y crear metodos de acceso
        // de este modo se puede controlar que valores pueden colocarse en las propiedades del objeto

        // por ejemplo
        public int[] getCentro() // metodo de acceso para leer Centro
        {
            return centro;
        }
        public void setCentro(int[] centro)  // metodo de acceso para modificar Centro
        { 
            this.centro = centro;
        }

        public int getRadio() // metodo de acceso para leer Radio
        {
            return radio;
        }

        public void setRadio(int radio) // metodo de acceso para modificar Radio
        {
            this.radio = radio; 
        }


        
    }
    partial class Circulo
    {

        public void DibujarCirculo()
        {
            // no es perfecto pero bueno es un aproximado, dibuja un casi circulo
            int diametro = (radio * 2);
            int inicio = centro[0] - radio;
            int final = centro[0] + radio;
            Console.SetCursorPosition(centro[0] + final, centro[1] + final);
            Console.WriteLine("*");
            Console.WriteLine(diametro);
            Console.WriteLine(inicio);
            Console.WriteLine(final);

            for (int i = inicio; i <= final; i++)
            {
                double cuadratico = Math.Pow(radio, 2) - Math.Pow(i - centro[0], 2);
                double raiz1 = Math.Sqrt(cuadratico) + centro[1];
                double raiz2 = (-Math.Sqrt(cuadratico) + centro[1]);
                int y = (int)Math.Floor(raiz1);
                int y2 = (int)Math.Floor(raiz2);
                Console.SetCursorPosition(i + final, y + final);
                Console.WriteLine("#");
                Console.SetCursorPosition(i + final, y2 + final);
                Console.WriteLine("#");
            }
        }

        // un metodo para calcular un perimetro
        public double CalcularPerimetro(int radio)
        {
            return 2 * pi * radio;
        }
    }
}
    
