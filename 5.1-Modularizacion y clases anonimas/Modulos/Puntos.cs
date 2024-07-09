using System;

// este modulo creamos clases que usaremos en el programa principal, vemos que no tiene un Main, porque actua mas bien de 
// libreria, donde guardamos nuestras clases que usaremos en un modulo principal que si tiene un Main

// ya que estamos con modulos, de forma similar a que podemos "importar" el namespace System de modo que podamos acceder a sus clases 
// llamando a estas, como puede ser Math.pow() o demas sin llamar constantemente a su namespace
// podemos tambien de forma similar importar las clases y sus metodos estaticos usando static despues del using.
// lo que era originalmente para importar namespaces ahora tambien sirve para ahorrarnos llamar a la clase Math cada vez que 
// queramos usar uno de sus metodos estaticos, ya que la "importamos" 
// esto seria tal que asi:
using static System.Math;

// de forma similar podemos hacerlo con console
using static System.Console;

namespace Modulos
{
    internal class Puntos
    {
        int[] posicion;
        // creamos sus constructores, nada realmente util, solo para hacer de ejemplo

        public Puntos() { 
            // por defecto tendra dos coordenadas
            posicion = new int[2];
            posicion = [0, 0];

        } 
        // podemos ingresarle un array con multiples dimensiones, aunque solo vamos a dibujar 2 en el otro modulo
        public Puntos(int[] pos)
        {
            posicion = pos;
            if (posicion.Length < 2 ) {
                posicion.Append(0);    
            }
        }

        // no hay razon para evitar usar como parametro de uno de nuestros metodos la misma clase, solo para aclarar 
        // ya que podria ser una mala concepcion y no entender correctamente POO
        public int Distancia(Puntos other) 
        {
            // teorema de emmmm como se llamaba??? creo que este no lo enseñan en este pais
            int Dx = other.posicion[0] - posicion[0]; 
            int Dy = other.posicion[1] - posicion[1];
            double suma = Pow(Dx, 2) + Pow(Dy, 2);
            double D = Sqrt(suma);

            return (int)D;
        }

    }
}
