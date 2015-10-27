using System.Collections.Generic;

namespace CS271.Automorphisms.Console
{
    public class Program
    {
        private static List<char[]> permsList; 

        public static void Main(string[] args)
        {
            permsList = new List<char[]>();
            const string input = "1234567";
            char[] inputChars = input.ToCharArray();
            GetPermutations(inputChars);
            permsList.ForEach(System.Console.WriteLine);
            System.Console.ReadLine();
        }

        private static void Swap(ref char a, ref char b)
        {
            if (a == b)
            {
                return;
            }

            a ^= b;
            b ^= a;
            a ^= b;
        }

        public static void GetPermutations(char[] list)
        {
            // Set x to the length of the char list - 1
            int x = list.Length - 1;
            // Run GetPermutations
            GetPermutations(list, 0, x);
        }

        private static void GetPermutations(char[] list, int k, int m)
        {
            // If k == m, add to the perms list, otherwise swap k and i, re-run, then swap again
            if (k == m)
            {
                permsList.Add(list);
            }
            else
            {
                // Loop through all characters
                for (int i = k; i <= m; i++)
                {
                    // Swap k and i
                    Swap(ref list[k], ref list[i]);
                    // Recursively run with k stepped by 1
                    GetPermutations(list, k + 1, m);
                    // Swap k and i again
                    Swap(ref list[k], ref list[i]);
                }
            }
        }
    }
}