//Author:   Mohammed Efaz
//Date:     2023.05.05
//Title:    simulation until one gas completely perishes from the atmosphere

using System;
using TextFile;
using System.Collections.Generic;
using System.Globalization;

namespace assignment2_efaz
{
    class Program
    {
        static void Main()
        {
            //taking the name of the file
            string name = "";
            int temp = 0;
            TextFileReader reader = null!;
            while(temp==0)
            {
                try
                {
                    Console.WriteLine("Enter the name of the file: ");
                    name = Console.ReadLine();
                    reader = new(name);
                    temp = 1;
                }
                catch(FileNotFoundException)
                {
                    Console.WriteLine("File does not exist.");
                }

            }

            //Console.Clear();

            //reading the file
            //adding the layers
            reader.ReadLine(out string line);
            int n = int.Parse(line);
            List<Layer> layers = new();
            
            for (int i = 0; i < n; ++i)
            {
                char[] separators = new char[] { ' ', '\t' };
                Layer layer = null!;

                if (reader.ReadLine(out line))
                {
                    string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    char ch = char.Parse(tokens[0]);
                    double p = double.Parse(tokens[1]);

                    switch (ch)
                    {
                        case 'Z': layer = new Ozone(ch, p); break;
                        case 'X': layer = new Oxygen(ch, p); break;
                        case 'C': layer = new CarbonDioxide(ch, p); break;
                    }
                }
                layers.Add(layer);
            }

            //reading the file
            //adding the atmospheric variables
            //simulating the program
            reader.ReadLine(out line);
            int lineIndex = 0;
            int simulation = 0;
            bool isPerished = false;
            string m = line;
            
            List<IAtmosphere> atmospheres = new();
            do
            {
                int temp2 = layers.Count;
                Console.WriteLine($"\nSimulation: {++simulation}");
                for (int j = 0; j < layers.Count; ++j)
                {
                    char c = m[lineIndex++ % m.Length];
                    switch (c)
                    {
                        case 'T': atmospheres.Add(new Thunderstorm()); break;
                        case 'S': atmospheres.Add(new Sunshine()); break;
                        case 'O': atmospheres.Add(new Other()); break;
                    }

                    /*try { layers[j].Simulate(ref layers, atmospheres[round - 1]); } catch (zeroLayerException) { Console.WriteLine("Length cannot be negative."); }*/
                    layers[j].Simulate(ref layers, atmospheres[simulation - 1]);
                    if (!layers[j].notPerish())
                    {
                        layers.RemoveAt(j);
                    }
                    if (temp2 > layers.Count)
                    {
                        temp2 = layers.Count;
                        j--;
                    }
                }

                bool z = false, x = false, c2 = false;

                foreach(var layer in layers)
                {
                    if(layer.GetType() == typeof(Ozone)) z = true;
                    else if(layer.GetType() == typeof(Oxygen)) x = true;
                    else if(layer.GetType() == typeof(CarbonDioxide)) c2 = true;
                }
                if(!z || !x || !c2) isPerished = true;

                foreach (Layer layer in layers)
                {
                    Console.Write($"{layer.Name} {layer.Thickness}\n");
                    //if (layer.NotPerish())
                    //{
                    //    Console.WriteLine($"{layer.Name} {Math.Round(layer.Thickness, 2)}");
                    //}
                    //else if (!layer.NotPerish())
                    //{
                    //    Console.WriteLine("A gas component has completely perished from the atmosphere.");
                    //    isPerished = true;
                    //    break;
                    //}
                    //Console.WriteLine();
                }
            } while (!isPerished);
            Console.WriteLine($"\nOne gas has completely perished. Simulation over.");
        }
    }
}