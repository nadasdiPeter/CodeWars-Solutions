using System;
using System.Collections.Generic;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_RotateArray : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nRotate array:");
         var data = new object[] { 1, 2, 3, 4, 5 };
         List<KataTest> test_package = new List<KataTest>
         {
            new KataTest(Rotate(data, 1), new object[] { 5, 1, 2, 3, 4 }),
            new KataTest(Rotate(data, 2), new object[] { 4, 5, 1, 2, 3 }),
            new KataTest(Rotate(data, 3), new object[] { 3, 4, 5, 1, 2 }),
            new KataTest(Rotate(data, 4), new object[] { 2, 3, 4, 5, 1 }),
            new KataTest(Rotate(data, 5), new object[] { 1, 2, 3, 4, 5 }),
            new KataTest(Rotate(data, 0), new object[] { 1, 2, 3, 4, 5 }),
            new KataTest(Rotate(data, -1), new object[] { 2, 3, 4, 5, 1 }),
            new KataTest(Rotate(data, -2), new object[] { 3, 4, 5, 1, 2 }),
            new KataTest(Rotate(data, -3), new object[] { 4, 5, 1, 2, 3 }),
            new KataTest(Rotate(data, -4), new object[] { 5, 1, 2, 3, 4 }),
            new KataTest(Rotate(data, -5), new object[] { 1, 2, 3, 4, 5 }),
         };
         Verify(test_package);
      }

      private object[] Rotate(object[] array, int n)
      {
         object[] rotated_array = new object[array.Length];
         array.CopyTo(rotated_array, 0);

         int shift = n % array.Length;
         int index = (shift > 0) ? (array.Length - shift) : (0 - shift);

         for (int i = 0; i < array.Length; i++)
         {
            rotated_array[i] = array[index];
            index = ((index + 1) > (array.Length - 1)) ? (0) : (index + 1);
         }

         return rotated_array;
      }
   }
}
