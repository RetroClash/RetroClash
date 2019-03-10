using System;
using System.IO;
using NLog;
using RetroClash.Logic;

namespace RetroClash
{
    public class Logger
    {
        private static NLog.Logger _logger;

        public Logger()
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            _logger = LogManager.GetCurrentClassLogger();
        }

        public static void Log(object message, Enums.LogType logType = Enums.LogType.Info)
        {
            switch (logType)
            {
                case Enums.LogType.Info:
                {
                    _logger.Info(message);

                    Console.WriteLine($"[{logType.ToString()}] {message}");
                    break;
                }

                case Enums.LogType.Warning:
                {
                    _logger.Warn(message);

                    if (Configuration.Debug)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"[{logType.ToString()}] {message}");
                        Console.ResetColor();
                    }
                    break;
                }

                case Enums.LogType.Error:
                {
                    _logger.Error(message);

                    if (Configuration.Debug)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[{logType.ToString()}] {message}");
                        Console.ResetColor();
                    }
                    break;
                }

                case Enums.LogType.Debug:
                {
                    if (Configuration.Debug)
                    {
                        _logger.Debug(message);

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"[{logType.ToString()}] {message}");
                        Console.ResetColor();
                    }
                    break;
                }
            }
        }
    }
}