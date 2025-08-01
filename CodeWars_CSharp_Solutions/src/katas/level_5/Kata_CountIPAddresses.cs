using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_CountIPAddresses : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nCount IP4 addresses:");
         List<KataTest> test_package = new List<KataTest>
         {
            new KataTest(IpsBetween("10.0.0.0","10.0.0.50" ), (long)50),
            new KataTest(IpsBetween("10.0.0.50","10.0.0.0" ), (long)50),
            new KataTest(IpsBetween("10.0.0.50","10.0.0.50"), (long)0),
            new KataTest(IpsBetween("20.0.1.0","20.0.0.10" ), (long)246),
            new KataTest(IpsBetween("20.1.1.0","20.0.3.10" ), (long)65014),
            new KataTest(IpsBetween("0.0.0.0", "255.255.255.255"),(1L << 32) - 1L),
         };
         Verify(test_package);
      }

      private long IpsBetween(string ipA, string ipB)
      {
         var a = ipA.Split('.').Select(x => int.Parse(x)).ToArray();
         var b = ipB.Split('.').Select(x => int.Parse(x)).ToArray();
         return Math.Abs(((a[0] * (long)16777216) + (a[1] * (long)65536) + (a[2] * 256) + a[3]) - ((b[0] * (long)16777216) + (b[1] * (long)65536) + (b[2] * 256) + b[3]));
      }
   }
}