using System;

using System.Threading.Tasks;

namespace Task
{

    // esta parte no esta del todo bien asi que puede tener errores en la explicacion

    // basicamente si queremos que un hilo se ejecute al finalizar otro, podemos usar
    // TaskCompletionSource para tener una forma de indicar esto a otros hilos

    // tan como se realiza en el main, tenemos un objeto del tipo TaskCompletionSource
    // el cual solicita como generico el tipo que la tarea devolvera cuando finalice
    class Program
    {
       
        static void Main(string[] args) {

            var tareaTerminada = new TaskCompletionSource<bool>();

            Thread hilo1 = new Thread(() =>
            {
                Thread.CurrentThread.Name = "hilo1";
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + $" {i}");
                    Thread.Sleep(50);
                }

                // indicamos que la tarea termino al darle un resultado
                tareaTerminada.SetResult(true);

            });

            Thread hilo2 = new Thread(() =>
            {
                Thread.CurrentThread.Name = "hilo2";
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + $" {i}");
                    Thread.Sleep(50);
                }


            });

            Thread hilo3 = new Thread(() =>
            {
                Thread.CurrentThread.Name = "hilo3";
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(Thread.CurrentThread.Name + $" {i}");
                    Thread.Sleep(50);
                }

            });

            // se ve que solo se ejecutara este hasta que task de un resultado

            // es una forma de sincronizar 
            hilo1.Start();

            // cuando termine el hilo1, se podra recibir entonces el resultado 
            // en la variable resultadoDeLaTarea accediendo a su propiedade Task.Result
            var resultadoDeLaTarea = tareaTerminada.Task.Result;


            // entonces podran empezar los siguientes hilos, que ahora se ejecutaran de forma
            // asyncrona 
            hilo2.Start();

            hilo3.Start();  

        }
    }



}