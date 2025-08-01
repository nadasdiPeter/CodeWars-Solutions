using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_BoggleWordChecker : Kata
   {
      public char[][] DeepCopyBoard(char[][] original) => original.Select(row => row.ToArray()).ToArray(); // Its needed because the board otherwise passed by reference and modified by each TEST.

      protected override void RunKata()
      {
         Console.WriteLine("\nBoggle Word Checker");

         char[][] board_4x4 =
         {
            new []{'E','A','R','A'},
            new []{'N','L','E','C'},
            new []{'I','A','I','S'},
            new []{'B','Y','O','R'}
         };

         char[][] board_16x16 =
{
            new []{'E','A','R','A','E','A','R','A','E','A','R','A','E','A','D','F'},
            new []{'N','L','E','C','E','B','B','A','A','A','R','A','E','Q','C','H'},
            new []{'I','A','I','S','E','C','R','A','S','N','R','A','E','Y','K','Y'},
            new []{'B','Y','O','R','E','A','R','W','K','B','R','A','E','A','T','S'},
            new []{'E','A','R','A','E','Y','R','W','E','H','R','A','E','P','W','P'},
            new []{'N','L','E','C','E','A','R','O','K','K','R','A','E','A','Q','U'},
            new []{'I','A','I','S','E','W','H','L','E','A','R','O','E','O','P','R'},
            new []{'B','Y','O','R','E','A','R','L','P','L','R','L','E','L','L','T'},
            new []{'E','A','R','A','U','A','K','A','O','A','R','K','E','A','D','Z'},
            new []{'N','L','E','C','E','F','R','S','U','Y','R','J','E','A','X','F'},
            new []{'I','A','I','S','E','A','P','R','G','Z','R','H','E','Z','N','H'},
            new []{'B','Y','O','R','P','A','N','V','D','Q','R','G','E','A','F','J'},
            new []{'E','A','R','A','E','K','M','A','E','W','R','D','E','U','R','K'},
            new []{'N','L','E','C','J','A','M','J','f','R','R','S','E','A','R','L'},
            new []{'I','A','I','S','E','A','R','A','E','T','R','A','E','A','R','M'},
            new []{'B','Y','O','R','K','A','Z','A','x','Z','R','Y','C','V','B','N'},
         };

         List<KataTest> test_package = new List<KataTest>
         {
            new KataTest(new Boggle(DeepCopyBoard(board_4x4),"C").Check(), true),                              // Test 1
            new KataTest(new Boggle(DeepCopyBoard(board_4x4),"EAR").Check(), true),                            // Test 2
            new KataTest(new Boggle(DeepCopyBoard(board_4x4),"EARS").Check(), false),                          // Test 3
            new KataTest(new Boggle(DeepCopyBoard(board_4x4),"BAILER").Check(), true),                         // Test 4
            new KataTest(new Boggle(DeepCopyBoard(board_4x4),"RSCAREIOYBAILNEA").Check(), true),               // Test 5
            new KataTest(new Boggle(DeepCopyBoard(board_4x4),"CEREAL").Check(), false),                        // Test 6
            new KataTest(new Boggle(DeepCopyBoard(board_4x4),"ROBES").Check(), false),                         // Test 7
            new KataTest(new Boggle(DeepCopyBoard(board_16x16),"BAEAPARAPARAEYCFHYSPUR").Check(), true),       // Test 8
         };

         Verify(test_package);
      }
   }

   public class Boggle
   {
      private Point cell = new Point(int.MaxValue, int.MaxValue);
      private char[][] board;
      private string word;

      public Boggle(char[][] board, string word)
      {
         this.board = board;
         this.word = word;
      }

      public void SetCell(Point p) => cell = p;

      public void SetBoard(Point p, char c) => board[p.X][p.Y] = c;

      private List<Point> Get_Neighbors(Point nP)
      {
         Func<Point, bool> Valid = delegate (Point x)
         {
            return (x.X >= 0 && x.X < board.Length && x.Y >= 0 && x.Y < board[0].Length && board[x.X][x.Y] == word.First());
         };

         List<Point> neighbors = new List<Point>();

         if (cell.X == int.MaxValue && cell.Y == int.MaxValue) // First call
         {
            for (int i = 0; i < board.Length; i++)
               for (int j = 0; j < board[i].Length; j++)
                  if (board[i][j] == word.First())
                     neighbors.Add(new Point(i, j));
         }
         else
         {
            if (Valid(new Point(nP.X - 1, nP.Y))) neighbors.Add(new Point(nP.X - 1, nP.Y));
            if (Valid(new Point(nP.X - 1, nP.Y - 1))) neighbors.Add(new Point(nP.X - 1, nP.Y - 1));
            if (Valid(new Point(nP.X - 1, nP.Y + 1))) neighbors.Add(new Point(nP.X - 1, nP.Y + 1));
            if (Valid(new Point(nP.X + 1, nP.Y))) neighbors.Add(new Point(nP.X + 1, nP.Y));
            if (Valid(new Point(nP.X + 1, nP.Y - 1))) neighbors.Add(new Point(nP.X + 1, nP.Y - 1));
            if (Valid(new Point(nP.X + 1, nP.Y + 1))) neighbors.Add(new Point(nP.X + 1, nP.Y + 1));
            if (Valid(new Point(nP.X, nP.Y - 1))) neighbors.Add(new Point(nP.X, nP.Y - 1));
            if (Valid(new Point(nP.X, nP.Y + 1))) neighbors.Add(new Point(nP.X, nP.Y + 1));
         }

         return neighbors;
      }

      public bool Check()
      {
         if (word == "") return true;
         foreach (Point p in Get_Neighbors(cell))
         {
            var b = new Boggle(board, word.Remove(0, 1));
            b.SetBoard(p, ' ');
            b.SetCell(p);
            if (b.Check() == true)
               return true;
            else
               SetBoard(p, word.First()); // need to set back because board passed by reference
         }
         return false;
      }
   }
}
