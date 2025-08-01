using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_MergeTwoSortedArraysIntoOne : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nMerge two sorted arrays into one:");

         List<KataTest> test_package = new List<KataTest> /* Testcases */
         {
            new KataTest(MergeArrays(new[] {1, 2, 3, 4}, new[] {5, 6, 7, 8}),new[] {1, 2, 3, 4, 5, 6, 7, 8}),
            new KataTest(MergeArrays(new[] {1, 2, 3, 4, 5, 6}, new[] {5, 6, 7, 8}),new[] {1, 2, 3, 4, 5, 6, 7, 8}),
            new KataTest(MergeArrays(new[] {1, 2, 3, 4, 9, 10}, new[] {5, 6, 7, 8}),new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}),
         };

         Verify(test_package);
      }

      private int[] MergeArrays(int[] arr1, int[] arr2) => arr1.Union(arr2).Distinct().OrderBy(x => x).ToArray();
   }
}
