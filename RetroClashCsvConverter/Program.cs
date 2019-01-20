using System;
using System.IO;
using System.Linq;
using RetroClashCsvConverter.Extensions;

namespace RetroClashCsvConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "RetroClash CSV Converter v0.1";

            Console.WriteLine(
                "\r\n________     _____             ______________             ______  \r\n___  __ \\______  /_______________  ____/__  /_____ __________  /_ \r\n__  /_/ /  _ \\  __/_  ___/  __ \\  /    __  /_  __ `/_  ___/_  __ \\\r\n_  _, _//  __/ /_ _  /   / /_/ / /___  _  / / /_/ /_(__  )_  / / /\r\n/_/ |_| \\___/\\__/ /_/    \\____/\\____/  /_/  \\__,_/ /____/ /_/ /_/ \r\n                                                                  \r\n");

            Console.SetOut(new Prefixed());

            Console.WriteLine("Converting...");

            if (!Directory.Exists("CSV Input"))
            {
                Directory.CreateDirectory("CSV Input");

                Console.WriteLine("Input Directory was not found so it has been created.");
            }

            if (!Directory.Exists("CS Output"))
            {
                Directory.CreateDirectory("CS Output");

                Console.WriteLine("Output Directory was not found so it has been created.");
            }

            if (Directory.GetFiles("CSV Input").Any())
            {
                var files = Directory.GetFiles("CSV Input");

                foreach (var file in files)
                {
                    if (Path.GetExtension(file) != ".csv") continue;
                    var header = File.ReadLines(file).ToList()[0].Replace("\"", "").Split(',');
                    var types = File.ReadLines(file).ToList()[1].Replace("\"", "").Split(',');

                    new CsWriter(Path.GetFileNameWithoutExtension(file), header, types);

                    Console.WriteLine($"File {Path.GetFileNameWithoutExtension(file)} has been exported.");
                }
            }
            else
            {
                Console.WriteLine("No CSV File has been found.");
            }

            Console.ReadKey();
        }
    }
}