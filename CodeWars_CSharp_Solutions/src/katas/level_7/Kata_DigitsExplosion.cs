using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_DigitsExplosion : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nDigits explosion:");
         List<KataTest> test_package = new List<KataTest> /* Testcases */
         {
            new KataTest(Explode("321"), "333221"),
            new KataTest(Explode("123"), "122333"),
            new KataTest(Explode("0"), ""),
         };
         Verify(test_package);
      }

      private string Explode(string s)
      {
         /*string v = "";
         foreach( char c in s )
         {
            v += new string(c, int.Parse(c + ""));
         }
         return v;*/

         return string.Join(string.Empty,s.ToArray().Select(x => new string(x, int.Parse(x + ""))));
      }
   }
}
