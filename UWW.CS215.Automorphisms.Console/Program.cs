using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UWW.CS215.Automorphisms.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isFirstRun = true;
            while (true)
            {
                if (isFirstRun)
                {
                    System.Console.WriteLine("UWW CS215 Fano Plane Automorphism Finder" + Environment.NewLine + "Authored by Connor Grady, Grant Jones, and Collin Stolpa");
                }
                else
                {
                    System.Console.WriteLine();
                }
                string input = CollectInput();

                string[] items = new string[7] { "1", "2", "3", "4", "5", "6", "7" };

                Permutations permutations = new Permutations();
                List<List<string>> results = permutations.GeneratePermutations(items.ToList());
                //WritePermutations(results);

                switch (input)
                {
                    case "OutputPerms":
                        WritePermutations(results);
                        break;

                    case "OutputAutos":
                        // Now that we have the Permutations, let's check for the automorphisms
                        List<List<string>> automorphisms = results.Where(IsAutomorphism).ToList();

                        // Convert to Cycle Notation
                        List<string> automorphicCycles = automorphisms.Select(perm => permutations.ToCycleNotation(results.FirstOrDefault(), perm)).ToList();

                        // Write cycles to the Console
                        WriteCycles(automorphicCycles);
                        break;

                    default:
                        System.Console.WriteLine("ERROR: No valid selection detected. Please try again...");
                        break;
                }

                isFirstRun = false;
            }
        }

        /// <summary>
        /// Specifies whether the provided permutation is an automorphism of the Fano Plane Permutations.
        /// </summary>
        /// <param name="permutation">The permutation derived to check.</param>
        /// <returns>Boolean specifying whether the permutation is an automorphism.</returns>
        private static bool IsAutomorphism(List<string> permutation)
        {
            Permutations permutations = new Permutations();
            // Get the permutation as a set of 7 sets of 3 characters
            List<List<string>> permGroups = permutations.PermTo7SetsOf3(permutation);

            // Get the Fano Plane collection
            List<List<string>> fanoGroups = FanoPlane.GenerateCollection();

            // This will contain the result of all the "true" method comparisons
            // If there are 7 "true" bools at the end, then it is an automorphism
            List<bool> groupBools = new List<bool>(7);

            // For each 3-member group of the permutation
            foreach (List<string> permGroup in permGroups)
            {
                // True if there's at least one 3-member group in the fano plane collection that matches
                // this 3-member group of the permutation
                bool atLeastOneGroupMatches = false;

                // For each 3-member group in the fano collection, for the first group that matches all 3 members (regardless of order),
                // set atLeastOneGroupMatches = true, remove that 3-member group from the fano collection, and break from the loop
                foreach (List<string> fanoGroup in from fanoGroup in fanoGroups let charsMatch = permGroup.All(fanoGroup.Contains) where charsMatch select fanoGroup)
                {
                    atLeastOneGroupMatches = true;
                    fanoGroups.Remove(fanoGroup);
                    break;
                }

                // Add whatever the value of atLeastOneGroupMatches to the bool collection
                groupBools.Add(atLeastOneGroupMatches);
            }

            // Return a boolean whose value is true if the count of all "true" booleans in the bool collection is 7
            return groupBools.Count(x => x) == 7;
        }

        /// <summary>
        /// Attempts to collect a valid input from the User, handling empty, invalid, and exit values.
        /// </summary>
        /// <returns>String representing the User's validated input.</returns>
        private static string CollectInput()
        {
            // Ask for the list
            System.Console.WriteLine(Environment.NewLine + "Select what you would like to ouput:");
            System.Console.WriteLine("A) Permutations of a Fano Plane Collection (1,2,3,4,5,6,7)");
            System.Console.WriteLine("B) Automorphisms of the Permutations of a Fano Plane Collection");
            System.Console.WriteLine("Exit) Stop and exit the Program.");
            System.Console.Write("Your choice: ");
            // Collect the input
            string input = System.Console.ReadLine();
            // Remove all whitespace from input if not null or empty
            input = !string.IsNullOrEmpty(input) ? input.Replace(" ", "").Trim() : input;

            // Handle null, empty, exit, and invalid inputs
            if (string.IsNullOrEmpty(input))
            {
                System.Console.WriteLine("ERROR: You must provide a selection! Try again...");
                CollectInput();
            }
            else if (input.ToLower() == "exit")
            {
                System.Console.WriteLine("Goodbye!");
                System.Threading.Thread.Sleep(500);
                Environment.Exit(0);
            }
            else if (input.ToUpper() != "A" && input.ToUpper() != "B")
            {
                System.Console.WriteLine("ERROR: Sorry, " + input + " is not a valid selection. Try again...");
                CollectInput();
            }

            // Return the input
            Debug.Assert(input != null, "input != null");
            return input.ToUpper() == "A" ? "OutputPerms" : "OutputAutos";
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

            SaveCollectionToFile(results.Select(x => string.Join("", x)).ToList(), "Permutations");
        }

        /// <summary>
        /// Writes all cycles in the specified collection to the Console.
        /// </summary>
        /// <param name="cycles">The list of cycles as strings to write.</param>
        private static void WriteCycles(IList<string> cycles)
        {
            foreach (string cycle in cycles)
            {
                System.Console.WriteLine(cycle);
            }

            System.Console.WriteLine(Environment.NewLine + "Number of Cycles: " + cycles.Count);

            SaveCollectionToFile(cycles, "Automorphisms");
        }

        /// <summary>
        /// Asks the User if they would like to save the specified output to a local file, handling it if they do.
        /// </summary>
        /// <param name="lines">The collection of lines to output to a file.</param>
        /// <param name="outputType">The type of the collection of lines (i.e. Permutations or Automorphisms)</param>
        private static void SaveCollectionToFile(IList<string> lines, string outputType)
        {
            System.Console.Write(Environment.NewLine + "Would you like to write the previous output to a file? (Y or N) ");
            string input = System.Console.ReadLine();
            input = !string.IsNullOrEmpty(input) ? input.Replace(" ", "").Trim().ToUpper() : input;
            if (string.IsNullOrEmpty(input))
            {
                System.Console.WriteLine("ERROR: You must provide a selection! Try again...");
                SaveCollectionToFile(lines, outputType);
            }
            else if (input != "Y" && input != "N")
            {
                System.Console.WriteLine("ERROR: Sorry, " + input + " is not a valid selection. Try again...");
                SaveCollectionToFile(lines, outputType);
            }

            if (input != "Y") return;

            lines.Insert(0, "UWW CS215 Automorphism Finder | " + outputType + " Output | Generated: " + DateTime.Now);
            lines.Add(Environment.NewLine + "Number of " + outputType + ": " + (lines.Count - 1));
            // Save to local file
            File.Delete("Output.txt");
            File.WriteAllLines("Output.txt", lines);

            System.Console.WriteLine("SUCCESS! Output saved to \"Output.txt\" at same location as this Program.");
        }
    }
}