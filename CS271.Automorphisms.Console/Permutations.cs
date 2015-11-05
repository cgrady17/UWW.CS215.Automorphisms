using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

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
        /// <param name="currentPermutation">The current permutations</param>
        /// <param name="results">The collection into which each permutation is loaded.</param>
        /// <param name="nextPosition">The next position in the temporary permutations collection.</param>
        /// <remarks>
        /// Keep in mind, all permutations are themselves a collection of T.
        /// </remarks>
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

        /// <summary>
        /// Converts the specified permutation to it's respective cycle notation as a single string.
        /// </summary>
        /// <param name="permutation">The permutation to convert to cycle notation.</param>
        /// <returns>String representing the cycle notation of the specified permutation.</returns>
        public string ToCycleNotation(List<string> permutation)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> PermutationAs3SetsOf3()
        {
            string[] members = new string[3]
            {
                "1,2,3",
                "3,4,5",
                "5,6,7"
            };

            List<List<string>> permMembers =
                members.Select(member => member.Split(',')).Select(digits => digits.ToList()).ToList();

            return permMembers;
        }
    }

    public static class FanoPlane
    {
        public static List<List<string>> GenerateCollection()
        {
            string[] members = new string[7]
            {
                "1,2,3",
                "1,5,4",
                "1,6,7",
                "2,5,7",
                "2,4,6",
                "3,5,6",
                "3,4,7"
            };

            List<List<string>> fanoMembers = members.Select(member => member.Split(',')).Select(digits => digits.ToList()).ToList();

            return fanoMembers;
        } 
    }

    //public static class PermutationExtensions
    //{
    //    private static string ToCycleNotation(this IReadOnlyList<string> permutation, IReadOnlyList<string> originalPermutation)
    //    {
    //        var mapping = new Dictionary<int, string>();

    //        for(int i = 0; i < originalPermutation.Count; i++)
    //        {
    //            mapping.Add(i, originalPermutation[i]);
    //        }

    //        List<HashSet<string>> cycles = new List<HashSet<string>>();
    //        HashSet<string> cycle = new HashSet<string>();

    //        string key = null;
    //        int keyInt = 0;
    //        int index = 0;

    //        while (mapping.Count != 0)
    //        {
    //            if (key == null)
    //            {
    //                if (cycles.Count != 0)
    //                {
    //                    cycles.Add(cycle);
    //                }

    //                cycle = new HashSet<string>();
    //                mapping.Keys.GetEnumerator().MoveNext();
    //                key = mapping.Values.GetEnumerator().Current;
    //                keyInt = mapping.Keys.GetEnumerator().Current;
    //            }
    //            else
    //            {
    //                key = permutation[index];
    //                mapping.Remove(keyInt);
    //                if (!cycle.Add(key) || index == null)
    //                {
    //                    key = null;
    //                }
    //            }


    //        }

    //        if (cycle.Count != 0)
    //        {
    //            cycles.Add(cycle);
    //        }

    //        return ToCycles(cycles);
    //    }

    //    private static string ToCycles(IEnumerable<IEnumerable<string>> collections)
    //    {
    //        StringBuilder sb = new StringBuilder();

    //        foreach (IEnumerable<string> collection in collections)
    //        {
    //            sb.Append(ToCycle(collection));
    //        }

    //        return sb.ToString();
    //    }

    //    private static string ToCycle(IEnumerable<string> collection)
    //    {
    //        StringBuilder sb = new StringBuilder("(");
    //        string separator = "";

    //        foreach (string s in collection)
    //        {
    //            sb.Append(separator).Append(s);
    //            separator = " ";
    //        }

    //        sb.Append(")");

    //        return sb.ToString();
    //    }
    //}
}