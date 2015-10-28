using System;
using System.Collections.Generic;
using System.Linq;

namespace CS271.Automorphisms.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("UWW CS271 Automorphism Finder v1.0");
            string input = CollectInput();

            string[] items = input.Split(',');
            Permutations permutations = new Permutations();
            List<List<string>> results = permutations.GeneratePermutations(items.ToList());
            WritePermutations(results);

            // Now that we have the Permutations, let's check for the automorphisms

            
            // Force console to remain open
            System.Console.ReadLine();
        }

        /// <summary>
        /// Attempts to collect a valid input from the User, handling empty, invalid, and exit values.
        /// </summary>
        /// <returns>String representing the User's validated input.</returns>
        public static string CollectInput()
        {
            // Ask for the list
            System.Console.Write("Enter a comma-delimited list: ");
            // Collect the input
            string input = System.Console.ReadLine();
            // Remove all whitespace from input if not null or empty
            input = !string.IsNullOrEmpty(input) ? input.Replace(" ", "") : input;

            // Handle null, empty, exit, and invalid inputs
            if (string.IsNullOrEmpty(input))
            {
                System.Console.WriteLine("ERROR: You must provide a list...");
                CollectInput();
            }
            else if (input.ToLower() == "exit")
            {
                Environment.Exit(0);
            }
            else if (!input.Contains(','))
            {
                System.Console.WriteLine("ERROR: List must be comma-delimited...");
                CollectInput();
            }

            // Return the input
            return input;
        }

        /// <summary>
        /// Writes all permutations in the specified collection to the Console.
        /// </summary>
        /// <param name="results">The list of permutations (as a list of strings) to write.</param>
        public static void WritePermutations(List<List<string>> results)
        {
            foreach (List<string> combination in results)
            {
                System.Console.WriteLine(string.Join("", combination));
            }
            System.Console.WriteLine(Environment.NewLine + "Number of Permutations: " + results.Count);
        }
    }
}