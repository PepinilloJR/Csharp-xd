using System;


namespace hilos
{
    class Program {

        // main es el hilo principal y el primero en ejecutarse
        static void Main(string[] args)
        {
            // vamos a interiorizar en los metodos de la clase Thread

            // creamos varios hilos para las pruebas, del siguiente modo 

            // solicita una funcion para ejecutar
            Thread hilo1 = new Thread(Metodo1);
            
            hilo1.Start();

            // el metodo Join suspende el thread donde lo llamamos hasta que el Thread desde el cual es accedido el metodo
            // termina su ejecucion, es otra forma que tenemos para sincronizar los hilos, en este caso
            // lo usamos para que primero muestre hilo1, luego hilo2 y luego hilo0
            hilo1.Join();

            Thread hilo2 = new Thread(Metodo2);
            
            hilo2.Start();
            hilo2.Join();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("hilo 0: " + i);
            }

        }


        static void Metodo1()
        {  
            for (int i = 0; i < 10; i++)
            {
                    // podemos suspender un hilo temporalmente (dentro de su contexto)
                    // con el metodo estatico Sleep de la clase Thread
                    Thread.Sleep(10);
                    Console.WriteLine("hilo 1: " + i);
            }
        }


        static void Metodo2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("hilo 2: " + i);
            }

        }



    }
}