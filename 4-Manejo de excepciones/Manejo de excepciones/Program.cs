// que es una excepcion? 

//se producen cuando la ejecución de un método no termina correctamente, 
//sino que termina de manera innesperada devido a un error, como consecuencia puede llegar 
// cerrarse el programa, un crasheo chaval

// para ello, manejaremos estas para poder actuar en caso de darse una y no interrumpir el programa
// de forma abrupta


namespace manejo_excep
{
    class Programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Resultado final: " + suma_mas_Nueva());
        }

        // leer cada metodo de sumas para aprender a manejar interrupciones

        static int Suma() {
            int numeroFinal = 0;
            int intentos = 3;
            while (true)
            {
                Console.WriteLine("ingrese primer numero:");
                string Numero1 = Console.ReadLine();
                Console.WriteLine("Ingrese segundo numero:");
                string Numero2 = Console.ReadLine();
                // aqui empezamos a utilizar una estructura denominada try catch
                // try catch ejecutara instrucciones dentro de las llaves del try
                // si se genera una excepcion, reenviara el error al catch como parametro
                // donde se ejecutara luego una secuencia en este alternativa al try
                try
                {
                    // esto puede fallar si el usuario ingresa un valor no numerico
                    numeroFinal = int.Parse(Numero1) + int.Parse(Numero2);
                    // cuidado al crear variables en un try, si nunca se logra, todo lo que se crea
                    // deja de existir para el programa si ocurre una excepcion, por eso la declaramos antes 
                    break;
                } catch (Exception ex) // catch debe recibir un objeto de tipo Exception, que lo llamaremos ex por convencion
                {
                    intentos--;
                    Console.WriteLine("Se ha producido un error - Intentos restantes:" + intentos);
                    // aca podemos acceder al mensaje de error, que es una propiedad del objeto ex de tipo Exception
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
                if (intentos < 0) {
                    Console.WriteLine("Se acabaron los intentos, entregando resultado final 0.");
                    break;
                }
            }
            return numeroFinal;
        }



        // ademas, podemos especificar que tipo de excepcion a la cual queremos hacerle catch
        // esto se hace declarando a esta como el tipo de excepcion que queremos, que es una clase especifica, que hereda de la clase Exception
        static int Suma_nueva()
        {
            int numeroFinal = 0;
            int intentos = 3;
            while (true)
            {
                Console.WriteLine("ingrese primer numero:");
                string Numero1 = Console.ReadLine();
                Console.WriteLine("Ingrese segundo numero:");
                string Numero2 = Console.ReadLine();
                
                try
                {
                    numeroFinal = int.Parse(Numero1) + int.Parse(Numero2);
                    break;
                }
                catch (FormatException ex) // tipo de error que ocurre cuando el formato de un argumento es erroneo
                {
                    intentos--;
                    Console.WriteLine("Se ha producido un error de formato - Intentos restantes:" + intentos);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                } catch (OverflowException ex) // si queremos atrapar mas de una excepcion, podemos agregar otro catch luego del primero, esto si no usamos 
                // la clase base Exception
                {
                    intentos--;
                    Console.WriteLine("Se ha producido un error de overflow - Intentos restantes: " + intentos);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }

                if (intentos < 0)
                {
                    Console.WriteLine("Se acabaron los intentos, entregando resultado final 0.");
                    break;
                }
            }
            return numeroFinal;
        }


        static int suma_mas_Nueva()
        {
            int numeroFinal = 0;
            int intentos = 3;
            while (true)
            {
                Console.WriteLine("ingrese primer numero:");
                string Numero1 = Console.ReadLine();
                Console.WriteLine("Ingrese segundo numero:");
                string Numero2 = Console.ReadLine();

                try
                {
                    numeroFinal = int.Parse(Numero1) + int.Parse(Numero2);
                    break;
                }

                // conociendo las versiones anteriores, con la captura general 
                // tendremos que buscar la forma de tratar especificamente cada tipo
                // de excepcion


                // para ello usamos excepciones de filtro, utilizando la palabra clave contextual when,
                // se usa para especificar una condición de filtro en los siguientes contextos: try catch, switch
                

                // podemos obtener el tipo con la funcion heredada de tipo object,
                // .GetType(), que devuelve un objeto System.Type que representa que tipo es
                // luego, el operador --> typeof --> devuelve un objeto System.Type que muestra el tipo de una clase
                catch (Exception ex) when (ex.GetType() != typeof(FormatException))
                {
                    intentos--;
                    Console.WriteLine("Se ha producido un error - Intentos restantes: " + intentos);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Source);
                }
                catch (FormatException ex) // luego planteamos el catch para el que estamos especificando
                {
                    intentos--;
                    Console.WriteLine("Se ha producido un error de formato - Intentos restantes: " + intentos);
                    Console.WriteLine("el tipo de la excepcion: " + ex.GetType().Name); // aca muestro el nombre del tipo de ex
                    Console.WriteLine("el tipo de la clase de FormatException: " + typeof(FormatException).Name); // aca mustreo el nombre de tipo de la clase FormatException
                    Console.WriteLine("Mensaje de la excepcion: " + ex.Message);

                } 

                if (intentos < 0)
                {
                    Console.WriteLine("Se acabaron los intentos, entregando resultado final 0.");
                    break;
                }
            }
            return numeroFinal;
        }
    }
}
