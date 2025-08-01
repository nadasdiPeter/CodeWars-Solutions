using System;
using System.Collections.Generic;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_GoingToTheCinema : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nGoing to the cinema:");
         List<KataTest> test_package = new List<KataTest> /* Testcases */
         {
            new KataTest(Movie(500, 15, 0.90), 43),
            new KataTest(Movie(100, 10, 0.95), 24),
            new KataTest(Movie(  0, 10, 0.95),  2),
         };
         Verify(test_package);
      }

      private int Movie(int card, int ticket, double perc )
      {
         int index = 0;
         double systemA = 0;
         double systemB = card;
         double systemB_ticket = ticket;

         do
         {
            index++;
            systemA += ticket;
            systemB_ticket = systemB_ticket * perc;
            systemB += systemB_ticket;
         } while (systemA <= Math.Ceiling(systemB));

         return index;
      }
   }
}