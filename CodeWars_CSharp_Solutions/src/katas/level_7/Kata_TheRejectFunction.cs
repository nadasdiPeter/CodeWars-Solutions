using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_TheRejectFunction : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nThe reject function:");
         List<KataTest> test_package = new List<KataTest>
         {
            new KataTest(Reject(new int[] { 1, 2, 3, 4, 5, 6 }, (n) => n % 2 == 0), new int[] { 1, 3, 5 }),
         };
         Verify(test_package);
      }

      private int[] Reject(int[] array, Func<int, bool> predicate) => array.Where( x => !predicate(x)).ToArray();
   }
}
