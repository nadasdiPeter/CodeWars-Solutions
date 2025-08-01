using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars_CSharp_Solutions.src.katas
{
   public class Kata_WeightSort : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nWeight sort:");
         List<KataTest> test_package = new List<KataTest>
         {
            new KataTest(orderWeight("103 123 4444 99 2000"),"2000 103 123 4444 99"),
            new KataTest(orderWeight("2000 10003 1234000 44444444 9999 11 11 22 123"),"11 11 2000 10003 22 123 1234000 44444444 9999"),
         };
         Verify(test_package);
      }

      class Weight
      {
         public int realWeight = 0;
         public int transformedWeight = 0;
         public string number = "";

         public Weight(string num)
         {
            number = num;
            int.TryParse(number,out realWeight);
            foreach (char c in number)
               transformedWeight += int.Parse(c + "");
         }
      }

      public string orderWeight(string strng)
      {
         List<Weight> weights = new List<Weight>();
         foreach (var s in strng.Split(' '))
            weights.Add(new Weight(s));

         return string.Join(" ", weights.OrderBy(x => x.transformedWeight).ThenBy(x => x.number).Select(x => x.number));
      }
   }
}
