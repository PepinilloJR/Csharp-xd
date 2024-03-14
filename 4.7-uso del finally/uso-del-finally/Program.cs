namespace usoDelFinally
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LecturaEnterosYsuma("1234a"));
        }
        static int LecturaEnterosYsuma(string datos)
        {
            int sumatoria = 0;
            
            for (int i=0; i < datos.Length; i++ )
            {
                int dato = 1;
                try
                {
                    dato = int.Parse(datos[i].ToString());
                } catch (FormatException e) {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("el dato no era un numero, se transmitira como 1");
                } finally // finally funcionara como una casilla que se ejecutara siempre independientemente del catch o el try
                {
                    sumatoria += dato;
                }
            }
            return sumatoria;
        }
    }

}
