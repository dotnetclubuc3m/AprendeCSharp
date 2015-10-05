using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespace
namespace HelloWorld
{
    // Class (Contiene la funcionalidad)
    class Program
    {
        // Function (Punto de entrada)
        static void Main(string[] args)
        {
            PrintclubNETImaginewareToScreen100times();

            Console.WriteLine("Escribe un número?");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.KeyChar == 'a')
                {
                    Console.WriteLine("\nEso no es un numero! PARA YA!");
                }
            else
                {
                    Console.WriteLine("\nHas escrito {0}", keyInfo.KeyChar.ToString());
                }
            Console.ReadKey();
        }
        /// <summary>
        /// Este método escribe por pantalla 100 veces club.NET-Imagineware
        /// </summary>
        static void PrintclubNETImaginewareToScreen()
        {
            Console.WriteLine("club.NET-Imagineware");
        }

        static void PrintclubNETImaginewareToScreen100times()
        {
            for(int counter=0; counter <= 100; counter++)
            {
                PrintclubNETImaginewareToScreen();
            }
        }
    }
}
