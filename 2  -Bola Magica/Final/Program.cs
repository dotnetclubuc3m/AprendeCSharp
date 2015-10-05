using System;

namespace BolaMagica
{
    class Program
    {
        static void Main(string[] args)
        {
            // Guardamos el viejo color del texto de la consola por si se quiere reutilizar
            ConsoleColor oldColor = Console.ForegroundColor;

            // Ejemplo de cambio de color del texto de cosola
            Console.ForegroundColor = ConsoleColor.Green;
            
            // Pequeño resumen de la app
            Console.WriteLine("Bienvenido a AprendeCSharp 2 - Aplicación de Bola Mágica");

            Random random = new Random();
            //Bucle infinito
            while (true)
            {
                Console.Write("Pregunta lo que te inquieta: ");
                string question = Console.ReadLine();
                if( question.ToLower() == "quit")
                {
                    //Terminamos el bloque si el usuario escribe quit
                    break;
                }
                Console.Write("Answer: ");
                //Selecciona un número aleatorio del 0 al 5
                switch( random.Next(5) )
                {
                    case 0:
                        Console.WriteLine("Si!");
                        break;
                    case 1:
                        Console.WriteLine("Diablos no!");
                        break;
                    case 2:
                        Console.WriteLine("Quizás.");
                        break;
                    case 3:
                        Console.WriteLine("Pero que c#@$*es?");
                        break;
                    case 4:
                        Console.WriteLine("No entiendo la pregunta?!?!");
                        break;
                }
            }

            // Restore the old foreground color
            Console.ForegroundColor = oldColor;
        }
    }
}
