
string texto = "";
while (true)
{
    Console.WriteLine("Ingresar muestras separadas por espacios!");
    string entry = Console.ReadLine();
    texto = texto + entry + " ";
    Console.WriteLine("Continuar con otra linea: AnyKey = si | 2 = no");

    ConsoleKeyInfo tecla = Console.ReadKey();
    Console.Clear();
    
    if (tecla.KeyChar == '2')
    {
        Console.WriteLine("     ");
        Console.WriteLine(texto);    
        break;
    }
}
string palabra = "";
int count = 0;
for (int i = 0; i < texto.Length; i++)
{
    
    if (texto[i] == ' ' || texto[i] == '-')
    {   
        
        if (palabra != " " && palabra != "-" && palabra != "")
        {
            Console.WriteLine(palabra);
            count++;
        }
        palabra = "";
    } else
    {
        palabra = palabra + texto[i];
    }
}
Console.WriteLine("numero de muestra: " + count.ToString());
