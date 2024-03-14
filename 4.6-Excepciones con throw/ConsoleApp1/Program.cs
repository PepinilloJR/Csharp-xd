namespace usoDeThrow
{
    class Program
    {
        static void Main(string[] args)
        {
            // ejemplo de los checked cortos
            int Numero = int.MaxValue;
            try
            {
                int resultado = checked(Numero + 20);// podemos usar el checked solo para una linea de este modo
                int resultadoUnchecked = unchecked(Numero + 20); // tambien podemos pedir que cierta linea no maneje su excepcion
                Console.WriteLine(resultado);

            } catch (OverflowException ex)
            {   
                Console.WriteLine(ex.Message);
            }

            // ejemplo del uso de throw
            try
            {
                Console.WriteLine("Introduce numero de mes: ");
                int MesSoli = int.Parse(Console.ReadLine());
                Console.WriteLine(obtenerMes(MesSoli));
            } catch (ArgumentOutOfRangeException ex) // para tomar el exception que generamos con throw
            {
                Console.WriteLine("NO INGRESO UN NUMERO VALIDO");
                Console.WriteLine("activaste el error pibe -> " + ex.Message);
            }
            catch (FormatException ex) // otro posible exception que se puede dar
            {
                Console.WriteLine("NO INGRESO UN NUMERO");
                Console.WriteLine("activaste el error pibe -> " + ex.Message);
            }

        }



        static string obtenerMes(int numMes)
        {
            // uso tipico de un switch
            switch (numMes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    throw new ArgumentOutOfRangeException(); // throw obliga a lanzar una excepcion especificada
            }
            
            }       
        }
    
    
    }


