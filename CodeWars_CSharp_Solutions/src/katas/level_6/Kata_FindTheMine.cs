using System;
using System.Collections.Generic;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_FindTheMine : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nFind the mine:");
         List<KataTest> test_package = new List<KataTest>
         {
            new KataTest(MineLocation(new int[,] {{1, 0}, {0, 0}}),new Tuple<int, int>(0, 0)),
            new KataTest(MineLocation(new int[,] {{1, 0, 0}, {0, 0, 0}, {0, 0, 0}}),new Tuple<int, int>(0, 0)),
            new KataTest(MineLocation(new int[,] {{0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 1, 0}, {0, 0, 0, 0}}),new Tuple<int, int>(2, 2)),
         };
         Verify(test_package);
      }

      private Tuple<int, int> MineLocation(int[,] field)
      {
         for (int i = 0; i < field.GetLength(0); i++)
            for (int j = 0; j < field.GetLength(1); j++)
               if (field[i, j] == 1)
                  return new Tuple<int, int>(i, j);
         return null;
      }
   }
}
