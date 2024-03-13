
namespace UsoDelChecked { 
    
    class Program
    {
        static void Main(string[] args)
        {
            // para algunos casos de excepciones, el compilador por razones de optimizacion, implementa por si mismo 
            // un manejo de excepcion y deja pasar el error

            // veamos el siguiente ejemplo

            int numeroMaximoEntero = int.MaxValue; // este es el valor maximo que puede tener un entero

            Console.WriteLine("este es el valor de un overflow sin el checked: " + (numeroMaximoEntero + 1));
            // esto deberia generar un overFlow, pero el compilador lo maneja solo, simplemente entregando un valor incorrecto

            // para que no suceda, usamos la estructura Checked, que indica al compilador analizar el codigo de manea exhaustiva y no dejar pasar ninguna excepcion

            checked
            {
                try
                {
                    numeroMaximoEntero = int.MaxValue;

                    Console.WriteLine((numeroMaximoEntero + 1));

                    // esta secuencia ahora deberia enviar una excepcion
                } catch (OverflowException ex) {
                    Console.WriteLine("hubo un error de overflow, el numero supera el maximo permitido para enteros de 32 bits");
                
                }
            }
                      
        }
    }


}