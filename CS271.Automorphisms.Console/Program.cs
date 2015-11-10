using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

            // Create FanoPlane Collection
            List<List<string>> fanoPlaneList = FanoPlane.GenerateCollection();

            // Now that we have the Permutations, let's check for the automorphisms
            List<List<string>> automorphisms = results.Where(permutation => IsAutomorphism(ref fanoPlaneList, permutation)).ToList();

            automorphisms.ForEach(x => System.Console.WriteLine(string.Join("", x)));

            System.Console.WriteLine("Number of Automorphisms: " + automorphisms.Count);

            // Convert to Cycle Notation
            //List<string> automorphicCycles = automorphisms.Select(x => permutations.ToCycleNotation(x)).ToList();

            // Write cycles to the Console
            //WriteCycles(automorphicCycles);

            // Force console to remain open
            System.Console.ReadLine();
        }

        /// <summary>
        /// Specifies whether the provided permutation is an automorphism of the specified origin set.
        /// </summary>
        /// <param name="origin">The original set.</param>
        /// <param name="permutation">The permutation derived from the origin.</param>
        /// <returns>Boolean specifying whether the permutation is an automorphism.</returns>
        private static bool IsAutomorphism(ref List<List<string>> origin, List<string> permutation)
        {
            Permutations permutations = new Permutations();
            List<List<string>> permAsSetOf3 = permutations.PermToSetsOfThree(permutation);
            // Now the permutation is in {1,2,3},{3,4,5},{5,6,7} format
            List<List<List<string>>> fanoPlanePerm = FanoPlane.FanoOriginPermutation();
            List<List<string>> firstFanoPerm = fanoPlanePerm[0];

            foreach (List<List<string>> fanoPerm in fanoPlanePerm)
            {
                bool memberMatch = false;
                for (int i = 0; i < 3; i++)
                {
                    
                    // Loop of each of 3 members
                    List<string> fanoMember = fanoPerm[i];
                    List<string> permMember = permAsSetOf3[i];
                    bool charMatch = true;
                    for (int j = 0; j < 3; j++)
                    {
                        // Loop of each character in member
                        if (!fanoMember.Contains(permMember[j]))
                        {
                            charMatch = false;
                        }
                    }

                    memberMatch = charMatch;
                }
                if (memberMatch)
                {
                    return true;
                }
                
            }

            return false;
        }

        

        /// <summary>
        /// Attempts to collect a valid input from the User, handling empty, invalid, and exit values.
        /// </summary>
        /// <returns>String representing the User's validated input.</returns>
        private static string CollectInput()
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
        private static void WritePermutations(IReadOnlyCollection<List<string>> results)
        {
            foreach (List<string> permutation in results)
            {
                System.Console.WriteLine(string.Join("", permutation));
            }
            System.Console.WriteLine(Environment.NewLine + "Number of Permutations: " + results.Count);
        }

        /// <summary>
        /// Writes all cycles in the specified collection to the Console.
        /// </summary>
        /// <param name="cycles">The list of cycles as strings to write.</param>
        private static void WriteCycles(IReadOnlyCollection<string> cycles)
        {
            foreach (string cycle in cycles)
            {
                System.Console.WriteLine(cycle);
            }

            System.Console.WriteLine(Environment.NewLine + "Number of Cycles: " + cycles.Count);
        }
    }
}