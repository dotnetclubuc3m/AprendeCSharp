using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;
using System.Media;

//
//  Nombre de aplicación: PC Borracho
//  Descripción: Aplicación que genera fallos en el ratón y teclado y movimientos de entrada y genera sonidos del sistema y diálogos falsos para confundir al usuario
//  Puntos:
//    1) Hilos
//    2) System.Windows.Forms namespace & assembly
//    3) Aplicaciones en segundo pano
//    4) Regiones en el código
//

namespace PcBorracho
{
    class Program
    {
        public static Random _random = new Random();

        public static int _startupDelaySeconds = 10;
        public static int _totalDurationSeconds = 10000000;

        #region Main
        /// <summary>
        /// Entry point for prank application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("PC Borracho por: Jerry (aka. Barnacules)");

            // Comprueba los arguments y asigna los nuevos valores
            if( args.Length >= 2 )
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            // Crea todos los hilos y manipula todos los inputs y outputs del sistema
            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);
            Console.WriteLine("Esperemos 10 segundos hasta que empiece el pedo.");
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            // Iniciamos todos los hilos
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            while( future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            Console.WriteLine("Comienza la resaca.");

            // Destruimos todos los hilos y terminamos de ejecutar la aplicación
            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();
        }

        #endregion

        #region Thread Functions
        /// <summary>
        /// Este hilo afectará aleatoriamente al movimiento del ratón para molestar al usuario final
        /// </summary>
        public static void DrunkMouseThread()
        {
            Console.WriteLine("Ratón borracho Iniciado");

            int moveX = 0;
            int moveY = 0;

            while(true)
            {
                // Console.WriteLine(Cursor.Position.ToString());

                if (_random.Next(100) > 50)
                {
                    // Genera las posiciones aleatorias para mover el cursr en X e Y
                    moveX = _random.Next(20) - 10;
                    moveY = _random.Next(20) - 10;

                    // Cambia la posición del cursor del ratón a unas coordenadas aleatorias
                    Cursor.Position = new System.Drawing.Point(
                        Cursor.Position.X + moveX,
                        Cursor.Position.Y + moveY);
                }

                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Este método generará salidas de teclado aleatorias para molestar al usuario
        /// </summary>
        public static void DrunkKeyboardThread()
        {
            Console.WriteLine("Teclado Borracho Iniciado");

            while (true)
            {
                if (_random.Next(100) > 95)
                {
                    // Genera una letra mayúscula aleatoria
                    char key = (char)(_random.Next(25) + 65);

                    // 50/50 lo crea en minúscula
                    if (_random.Next(2) == 0)
                    {
                        key = Char.ToLower(key);
                    }

                    SendKeys.SendWait(key.ToString());
                }

                Thread.Sleep(_random.Next(500));
            }
        }

        /// <summary>
        /// Este método creará sonidos aleatorios para 
        /// </summary>
        public static void DrunkSoundThread()
        {
            Console.WriteLine("DrunkSoundThread Started");

            while (true)
            {
                // Determina si vamos a reproducir sonido esta vez en el bucle (20% son raros)
                if (_random.Next(100) > 0)
                {
                    // Selecciona un número aleatorio.
                    switch(_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
                    }
                }
                
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Este hilo hace saltar notificaciones para hacer sentir más loco al usuario
        /// </summary>
        public static void DrunkPopupThread()
        {
            Console.WriteLine("Popup Borracho Iniciado");

            while (true)
            {
                // Cada 10 segundo ejecuta el hilo y el 10% de las veces ejecuta el código
                if (_random.Next(100) > 90)
                {
                    // Decide que mensaje mostrar
                    switch(_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show(
                               "Internet Explorer ha dejado de funcionar",
                                "Internet Explorer",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show(
                               "Tu sistema está funcionando bajo mínimos",
                                "Microsoft Windows",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;
                    }
                }

                Thread.Sleep(10000);
            }
        }
        #endregion
    }
}
