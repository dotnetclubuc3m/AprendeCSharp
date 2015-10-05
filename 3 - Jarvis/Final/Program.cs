using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;

namespace Jarvis
{
    class Program
    {
        private static SpeechSynthesizer synth = new SpeechSynthesizer();

        // 
        //  DONDE TODA LA MAGIA OCURRE!!
        //  
        static void Main(string[] args)
        {
            // Lista de mensajes que apareceran si la CPU esta al máximo
            List<string> cpuMaxedOutMessages = new List<string>();
            cpuMaxedOutMessages.Add("PELIGRO: Joder tu CPU está a punto de arder!");
            cpuMaxedOutMessages.Add("PELIGRO: O dios mio para por favor");
            cpuMaxedOutMessages.Add("PELIGRO: Deja de descargar porno, me estas sobrecargando");
            cpuMaxedOutMessages.Add("PELIGRO: Tu CPU está persiguiendo ardillas");
            cpuMaxedOutMessages.Add("Alerta roja! Alerta roja! Alerta roja! Alerta roja! ME HE TIRADO UN PEDO");

            // Un daaado como el de dragones y mazmorras
            Random rand = new Random();

            // Esto dara la vienvenida al usuario con un mensaje de voz
            synth.Speak("Bienvenido a Jarvis que es mejor que cortana, y lo sabes!");

            #region My Performance Counters
            // Nos da el porcentaje de la cpu
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            perfCpuCount.NextValue();

            // Da informacion de la memoria libre en MB
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            perfMemCount.NextValue();

            // Nos da el tiempo que lleva el pc encendido
            PerformanceCounter perfUptimeCount = new PerformanceCounter("System", "System Up Time");
            perfUptimeCount.NextValue();
            #endregion

            TimeSpan uptimeSpan = TimeSpan.FromSeconds(perfUptimeCount.NextValue());
            string systemUptimeMessage = string.Format("El sistema lleva encendido {0} dias {1} horas {2} minutos {3} segundos",
                (int)uptimeSpan.TotalDays,
                (int)uptimeSpan.Hours,
                (int)uptimeSpan.Minutes,
                (int)uptimeSpan.Seconds
                );

            // Lee el tiempo que lleva encendido el pc
            JarvisHabla(systemUptimeMessage, VoiceGender.Male, 2);

            int speechSpeed = 1;
            bool isChromeOpenedAlready = false;

            // un while infinito
            while(true)
            {
                // Almacena los valores que queremos en un int
                int currentCpuPercentage = (int)perfCpuCount.NextValue();
                int currentAvailableMemory = (int)perfMemCount.NextValue();

                // Cada segundo escribimos estos datos en la consola
                Console.WriteLine("Carga de la CPU        : {0}%", currentCpuPercentage);
                Console.WriteLine("Memoria disponible: {0}", currentAvailableMemory);

                // Solo entra cuando la CPU tiene mas de un 80% de uso
                #region Logic
                if ( currentCpuPercentage > 80 )
                {
                    if (currentCpuPercentage == 100)
                    {
                        // Esto impide que la velocidad sea demasiado rapido
                        string cpuLoadVocalMessage = cpuMaxedOutMessages[rand.Next(5)];

                        if (isChromeOpenedAlready == false)
                        {
                            OpenWebsite("https://www.imagineware.org");
                            isChromeOpenedAlready = true;
                        }
                        JarvisHabla(cpuLoadVocalMessage, VoiceGender.Male, speechSpeed);
                    }
                    else
                    {
                        string cpuLoadVocalMessage = String.Format("La carga actual de la cpu {0} porciento", currentCpuPercentage);
                        JarvisHabla(cpuLoadVocalMessage, VoiceGender.Female, 5);
                    }
                }
                #endregion

                // Solo cuando tenemos menos de 1gb de memoria libre
                if (currentAvailableMemory < 1024)
                {
                    // Hablamos con el tts los megas libres
                    string memAvailableVocalMessage = String.Format("Tienes actualmente {0} megabytes de memoria libre", currentAvailableMemory);
                    JarvisHabla(memAvailableVocalMessage, VoiceGender.Male, 10);
                }

                Thread.Sleep(1000);
            } // Fin del while
        }

        /// <summary>
        /// Habla con la voz seleccionada
        /// </summary>
        /// <param name="message"></param>
        /// <param name="voiceGender"></param>
        public static void JarvisHabla(string message, VoiceGender voiceGender)
        {
            synth.SelectVoiceByHints(voiceGender);
            synth.Speak(message);
        }

        /// <summary>
        /// Habla con la voz y velocidad seleccionadas
        /// </summary>
        /// <param name="message"></param>
        /// <param name="voiceGender"></param>
        /// <param name="rate"></param>
        public static void JarvisHabla(string message, VoiceGender voiceGender, int rate)
        {
            synth.Rate = rate;
            JarvisHabla(message, voiceGender);
        }

        // Abre una web
        public static void OpenWebsite(string URL)
        {
            Process p1 = new Process();
            p1.StartInfo.FileName = "chrome.exe";
            p1.StartInfo.Arguments = URL;
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            p1.Start();
        }
    }
}
