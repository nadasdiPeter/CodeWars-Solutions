using System;
using System.Collections.Generic;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_LogicalCalc : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nLogical calculator:");

         List<KataTest> test_package = new List<KataTest> /* Testcases */
         {
            new KataTest(LogicalCalc(new bool[] { true, true, true, false }, "AND"), false),
            new KataTest(LogicalCalc(new bool[] { true, true, true, false },  "OR"),  true),
            new KataTest(LogicalCalc(new bool[] { true, true, true, false }, "XOR"),  true),
         };

         Verify(test_package);
      }

      private bool LogicalCalc(bool[] bool_array, string operator_)
      {
         bool result = bool_array[0];
         for( int i=1; i< bool_array.Length; i++ )
            switch(operator_)
            {
               case "AND":
                  result &= bool_array[i];
                  break;
               case "OR":
                  result |= bool_array[i];
                  break;
               case "XOR":
                  result ^= bool_array[i];
                  break;
            }

         return result;
      }
   }
}
