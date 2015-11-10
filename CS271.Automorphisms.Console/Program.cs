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
            //WritePermutations(results);

            // Now that we have the Permutations, let's check for the automorphisms
            List<List<string>> automorphisms = results.Where(IsAutomorphism).ToList();

            //System.Console.WriteLine();
            //automorphisms.ForEach(x => System.Console.WriteLine(string.Join("", x)));
            //System.Console.WriteLine();

            //System.Console.WriteLine("Number of Automorphisms: " + automorphisms.Count);

            // Convert to Cycle Notation
            List<string> automorphicCycles = automorphisms.Select(perm => permutations.ToCycleNotation(results.FirstOrDefault(), perm)).ToList();

            // Write cycles to the Console
            WriteCycles(automorphicCycles);

            // Force console to remain open
            System.Console.ReadLine();
        }

        /// <summary>
        /// Specifies whether the provided permutation is an automorphism of the Fano Plane Permutations.
        /// </summary>
        /// <param name="permutation">The permutation derived to check.</param>
        /// <returns>Boolean specifying whether the permutation is an automorphism.</returns>
        private static bool IsAutomorphism(List<string> permutation)
        {
            Permutations permutations = new Permutations();
            List<List<string>> permAsSetOf3 = permutations.PermToSetsOfThree(permutation);
            // Now the permutation is in {1,2,3},{3,4,5},{5,6,7} format
            List<List<List<string>>> fanoPlanePerm = FanoPlane.FanoOriginPermutation();

            foreach (List<List<string>> fanoPerm in fanoPlanePerm) // For each Fano Perm
            {
                bool membersMatch = false;
                for (int i = 0; i < 3; i++) // For each Member in Perms
                {
                    // Loop of each of 3 members
                    List<string> fanoMember = fanoPerm[i];
                    List<string> permMember = permAsSetOf3[i];
                    bool charsMatch = true;
                    for (int j = 0; j < 3; j++) // For each character in Members
                    {
                        // Loop of each character in member
                        if (!fanoMember.Contains(permMember[j]))
                        {
                            charsMatch = false;
                        }
                    }

                    membersMatch = charsMatch;
                }

                if (membersMatch)
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