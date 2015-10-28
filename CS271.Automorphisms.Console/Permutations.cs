using System.Collections.Generic;
using System.Linq;

namespace CS271.Automorphisms.Console
{
    /// <summary>
    /// Library of methods for generating permutations from a set.
    /// </summary>
    public class Permutations
    {
        /// <summary>
        /// Generates the permutations from the specified set.
        /// </summary>
        /// <typeparam name="T">The type of each item in the set.</typeparam>
        /// <param name="items">The specified set from which to generate permutations.</param>
        /// <returns>List of permutations, each a List of <typeparamref name="T"/></returns>
        public List<List<T>> GeneratePermutations<T>(IReadOnlyList<T> items)
        {
            // Instantiate array of T
            T[] currentPermutation = new T[items.Count];

            // Instantiate array of booleans used to specify whether 
            // the current permutation is in the selection already
            bool[] inSelection = new bool[items.Count];

            // Instantiate the result list
            List<List<T>> results = new List<List<T>>();

            // Call PermuteItems to load the permutations into results
            PermuteItems(items, inSelection, currentPermutation, results, 0);

            return results;
        }

        /// <summary>
        /// Recursively generate the permutations, loading them into the specified results collection.
        /// </summary>
        /// <typeparam name="T">The type of the items collection.</typeparam>
        /// <param name="items">The set from which to generate permutations.</param>
        /// <param name="inSelection">Collection of booleans.</param>
        /// <param name="currentPermutation">Collection of the already generated permutations, if any.</param>
        /// <param name="results">The collection into which each permutation is loaded.</param>
        /// <param name="nextPosition">The next position in the temporary permutations collection.</param>
        private static void PermuteItems<T>(IReadOnlyList<T> items, IList<bool> inSelection, IList<T> currentPermutation, ICollection<List<T>> results,
            int nextPosition)
        {
            // Add the intermediate permutations collection to the results
            // if the specified next position is the same as the number
            // of items in the source set. Otherwise, loop through and
            // add the permutations to the intermediate collection
            if (nextPosition == items.Count)
            {
                results.Add(currentPermutation.ToList());
            }
            else
            {
                // Loop through each item in the source set
                for (int i = 0; i < items.Count; i++)
                {
                    // Skip this iteration if already in the selection
                    if (inSelection[i]) continue;

                    // Set this iteration to true
                    inSelection[i] = true;
                    // Add this item from the source set to the intermediate collection
                    currentPermutation[nextPosition] = items[i];

                    // Re-call PermuteItems with nextPosition stepped up
                    PermuteItems(items, inSelection, currentPermutation, results, nextPosition + 1);

                    // Set this iteration back to false
                    inSelection[i] = false;
                }
            }
        }
    }
}