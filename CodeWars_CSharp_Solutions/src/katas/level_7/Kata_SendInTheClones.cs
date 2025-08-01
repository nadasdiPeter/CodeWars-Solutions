using System;
using System.Collections.Generic;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_SendInTheClones : Kata
   {
      protected override void RunKata()
      {
         /* Testcases */
         Console.WriteLine("\nSend in the clones:");
         List<KataTest> test_package = new List<KataTest>
         {
            new KataTest(SendInTheClones(0), new int[] { 1, 0 }),
            new KataTest(SendInTheClones(5), new int[] { 16, 57 }),
            new KataTest(SendInTheClones(10), new int[] { 512, 2036 }),
         };
         Verify(test_package);
      }

      private int[] SendInTheClones(int kataPerDay)
      {
         int clones = 1;
         int completed_katas = 0;
         int clone_power = kataPerDay;

         for (int i = 0; i < kataPerDay; i++)
         {
            completed_katas += clones * clone_power;
            clones = clones * 2;
            clone_power--;
         }

         clones = (clones == 1) ? 1 : clones / 2;

         return new int[] { clones, completed_katas };
      }
   }
}
