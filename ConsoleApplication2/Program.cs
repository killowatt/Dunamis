using NDesk.Options;
using System;
using System.Collections.Generic;
using DunamisExamples.Examples;

namespace DunamisExamples
{
    class Program
    {
        public static Dictionary<string, BaseExample> examples = new Dictionary<string, BaseExample>()
        {
            { "cube", new CubeExample() }
        };

        static void Main(string[] args)
        {
            Console.Title = "Dunamis Examples";

            string exampleName = "";
            bool showExamples = false;
            OptionSet parser = new OptionSet()
            {
                { "e|example=", "the name of the example to run.", x => exampleName = x },
                { "examples", "list the examples that can be run.", x => showExamples = x != null }
            };

            try
            {
                parser.Parse(args);
            }
            catch(OptionException e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Try `DunamisExamples` for more info.");
                return;
            }

            if(showExamples)
            {
                Console.WriteLine("Examples: ");
                foreach (string s in examples.Keys)
                    Console.WriteLine("\t" + s);
                return;
            }

            BaseExample example;
            if (!examples.TryGetValue(exampleName, out example))
            {
                Console.WriteLine("Help: ");
                parser.WriteOptionDescriptions(Console.Out);
                Console.WriteLine("Error: example does not exist.");
                return;
            }

            example.Run();
        }
    }
}
