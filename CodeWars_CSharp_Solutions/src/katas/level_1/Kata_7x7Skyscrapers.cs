using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars_CSharp_Solutions.src.katas
{
   public class Kata_7x7Skyscrapers : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\n7x7 Skyscrapers");

         var clues1_7x7 = new[] { 7, 0, 0, 0, 2, 2, 3, 0, 0, 3, 0, 0, 0, 0, 3, 0, 3, 0, 0, 5, 0, 0, 0, 0, 0, 5, 0, 4 };
         var clues2_7x7 = new[] { 0, 2, 3, 0, 2, 0, 0, 5, 0, 4, 5, 0, 4, 0, 0, 4, 2, 0, 0, 0, 6, 5, 2, 2, 2, 2, 4, 1 };

         var expected1_7x7 = new[] { new[] { 1, 5, 6, 7, 4, 3, 2 },
                                     new[] { 2, 7, 4, 5, 3, 1, 6 },
                                     new[] { 3, 4, 5, 6, 7, 2, 1 },
                                     new[] { 4, 6, 3, 1, 2, 7, 5 },
                                     new[] { 5, 3, 1, 2, 6, 4, 7 },
                                     new[] { 6, 2, 7, 3, 1, 5, 4 },
                                     new[] { 7, 1, 2, 4, 5, 6, 3 } };

         var expected2_7x7 = new[] { new[] { 7, 6, 2, 1, 5, 4, 3 },
                                     new[] { 1, 3, 5, 4, 2, 7, 6 },
                                     new[] { 6, 5, 4, 7, 3, 2, 1 },
                                     new[] { 5, 1, 7, 6, 4, 3, 2 },
                                     new[] { 4, 2, 1, 3, 7, 6, 5 },
                                     new[] { 3, 7, 6, 2, 1, 5, 4 },
                                     new[] { 2, 4, 3, 5, 6, 1, 7 } };

         List<KataTest> test_package = new List<KataTest>
         {
            new KataTest(Skyscrapers.SolvePuzzle(clues1_7x7), expected1_7x7), // Test 1
            new KataTest(Skyscrapers.SolvePuzzle(clues2_7x7), expected2_7x7), // Test 1
         };

         Verify(test_package);
      }

      public class Skyscrapers
      {
         public static readonly int SIZE = 7;

         public class Riddle
         {
            public class Cell
            {
               public int Row { get; set; }
               public int Col { get; set; }
               public Cell(int x, int y)
               {
                  Row = x;
                  Col = y;
               }
            }

            public class Line
            {
               public int Clue_A { get; set; }
               public int Clue_B { get; set; }
               public bool Finished { get; set; }
               public List<Cell> Cells { get; set; }
               public List<int> Unfinished_Clues { get; set; } = Range.ToList();

               public Line(int clueA, int clueB, List<Cell> members)
               {
                  Clue_A = clueA;
                  Clue_B = clueB;
                  Cells = members;
                  Finished = false;
               }

               public void AddFinishedClue(int clue)
               {
                  Unfinished_Clues.Remove(clue);
                  if (Unfinished_Clues.Count == 0) Finished = true;
               }
            }

            public int[][] Solution { get; }
            public List<Line> Lines { get; set; } = new List<Line>();
            private static readonly int[] Range = Enumerable.Range(1, SIZE).ToArray();
            private static int[][][] Possibilities;

            public Riddle(int[] clues)
            {
               /* Initialization of possibility matrix */
               Possibilities = new int[][][]
               {
               new int[][] { Range, Range, Range, Range, Range, Range, Range },
               new int[][] { Range, Range, Range, Range, Range, Range, Range },
               new int[][] { Range, Range, Range, Range, Range, Range, Range },
               new int[][] { Range, Range, Range, Range, Range, Range, Range },
               new int[][] { Range, Range, Range, Range, Range, Range, Range },
               new int[][] { Range, Range, Range, Range, Range, Range, Range },
               new int[][] { Range, Range, Range, Range, Range, Range, Range }
               };

               /* Initialization of solution matrix */
               Solution = Range.Select(r => new int[7] { 0, 0, 0, 0, 0, 0, 0 }).ToArray();

               /* Initialization of COLUMBs */
               for (int col = 0; col < SIZE; col++)
               {
                  List<Cell> members = new List<Cell>();
                  for (int row = 0; row < SIZE; row++)
                     members.Add(new Cell(row, col));
                  Line l = new Line(clues[col], clues[((3 * SIZE) - 1) - col], members); // (3*SIZE)-1) = 17
                  Lines.Add(l);
               }

               /* Initialization of ROWs */
               for (int row = 0; row < SIZE; row++)
               {
                  List<Cell> members = new List<Cell>();
                  for (int col = 0; col < SIZE; col++)
                     members.Add(new Cell(row, col));
                  Line l = new Line(clues[((4 * SIZE) - 1) - row], clues[SIZE + row], members); // (4*SIZE)-1) = 23; 
                  Lines.Add(l);
               }

               /* Trigger Solver */
               Solve();
            }

            List<Line> GetCols() => Lines.GetRange(0, SIZE);
            List<Line> GetRows() => Lines.GetRange(SIZE, SIZE);

            public bool IsSolved() => Lines.Where(l => l.Finished == true).Count() == Lines.Count();

            public void RemoveClue_Cell(int row, int col, int clue)
            {
               var t = Possibilities[row][col].ToList();
               if (t.Contains(clue) && t.Count != 1)
               {
                  t.Remove(clue);
                  Possibilities[row][col] = t.ToArray();
               }
            }

            public void RemoveClue_RowCol(int row, int col, int clue)
            {
               for (int index = 0; index < SIZE; index++)
               {
                  RemoveClue_Cell(row, index, clue);
                  RemoveClue_Cell(index, col, clue);
               }
            }

            public void SetClue(int row, int col, int clue)
            {
               if (Solution[row][col] == 0)
               {
                  RemoveClue_RowCol(row, col, clue);
                  Lines[col].AddFinishedClue(clue);
                  Lines[SIZE + row].AddFinishedClue(clue);
                  Possibilities[row][col] = new int[] { clue };
                  Solution[row][col] = clue;
               }
            }

            public bool Verify(int clue, List<int> cells)
            {
               if (cells.Distinct().Count() == SIZE)
               {
                  int insight = 1;
                  int highest_building = cells[0];
                  for (int i = 1; i < cells.Count; i++)
                     if (cells[i] > highest_building)
                     {
                        insight++;
                        highest_building = cells[i];
                     }
                  return (clue == insight);
               }
               else // Not valid combination because the buildings are not the combination of 1,2,3,4,5,6,7
                  return false;
            }

            public bool Verify(Line l)
            {
               List<int> sum = new List<int>();
               foreach (Cell c in l.Cells)
                  sum.AddRange(Possibilities[c.Row][c.Col].ToList());
               if (sum.Distinct().Count() != SIZE) return false;

               if (l.Clue_A == 0 && l.Clue_B == 0)
                  return true; // No reason to iterate further because there are no clues to compare.

               for (int a_index = 0; a_index < Possibilities[l.Cells[0].Row][l.Cells[0].Col].Length; a_index++)
               {
                  for (int b_index = 0; b_index < Possibilities[l.Cells[1].Row][l.Cells[1].Col].Length; b_index++)
                  {
                     if (Possibilities[l.Cells[1].Row][l.Cells[1].Col][b_index] != Possibilities[l.Cells[0].Row][l.Cells[0].Col][a_index])
                        for (int c_index = 0; c_index < Possibilities[l.Cells[2].Row][l.Cells[2].Col].Length; c_index++)
                        {
                           if (Possibilities[l.Cells[0].Row][l.Cells[0].Col][a_index] != Possibilities[l.Cells[2].Row][l.Cells[2].Col][c_index] &&
                               Possibilities[l.Cells[1].Row][l.Cells[1].Col][b_index] != Possibilities[l.Cells[2].Row][l.Cells[2].Col][c_index])
                              for (int d_index = 0; d_index < Possibilities[l.Cells[3].Row][l.Cells[3].Col].Length; d_index++)
                              {
                                 if (Possibilities[l.Cells[0].Row][l.Cells[0].Col][a_index] != Possibilities[l.Cells[3].Row][l.Cells[3].Col][d_index] &&
                                     Possibilities[l.Cells[1].Row][l.Cells[1].Col][b_index] != Possibilities[l.Cells[3].Row][l.Cells[3].Col][d_index] &&
                                     Possibilities[l.Cells[2].Row][l.Cells[2].Col][c_index] != Possibilities[l.Cells[3].Row][l.Cells[3].Col][d_index])
                                    for (int e_index = 0; e_index < Possibilities[l.Cells[4].Row][l.Cells[4].Col].Length; e_index++)
                                    {
                                       if (Possibilities[l.Cells[0].Row][l.Cells[0].Col][a_index] != Possibilities[l.Cells[4].Row][l.Cells[4].Col][e_index] &&
                                           Possibilities[l.Cells[1].Row][l.Cells[1].Col][b_index] != Possibilities[l.Cells[4].Row][l.Cells[4].Col][e_index] &&
                                           Possibilities[l.Cells[2].Row][l.Cells[2].Col][c_index] != Possibilities[l.Cells[4].Row][l.Cells[4].Col][e_index] &&
                                           Possibilities[l.Cells[3].Row][l.Cells[3].Col][d_index] != Possibilities[l.Cells[4].Row][l.Cells[4].Col][e_index])
                                          for (int f_index = 0; f_index < Possibilities[l.Cells[5].Row][l.Cells[5].Col].Length; f_index++)
                                          {
                                             if (Possibilities[l.Cells[0].Row][l.Cells[0].Col][a_index] != Possibilities[l.Cells[5].Row][l.Cells[5].Col][f_index] &&
                                                 Possibilities[l.Cells[1].Row][l.Cells[1].Col][b_index] != Possibilities[l.Cells[5].Row][l.Cells[5].Col][f_index] &&
                                                 Possibilities[l.Cells[2].Row][l.Cells[2].Col][c_index] != Possibilities[l.Cells[5].Row][l.Cells[5].Col][f_index] &&
                                                 Possibilities[l.Cells[3].Row][l.Cells[3].Col][d_index] != Possibilities[l.Cells[5].Row][l.Cells[5].Col][f_index] &&
                                                 Possibilities[l.Cells[4].Row][l.Cells[4].Col][e_index] != Possibilities[l.Cells[5].Row][l.Cells[5].Col][f_index])
                                                for (int g_index = 0; g_index < Possibilities[l.Cells[6].Row][l.Cells[6].Col].Length; g_index++)
                                                {

                                                   if (Possibilities[l.Cells[0].Row][l.Cells[0].Col][a_index] != Possibilities[l.Cells[6].Row][l.Cells[6].Col][g_index] &&
                                                         Possibilities[l.Cells[1].Row][l.Cells[1].Col][b_index] != Possibilities[l.Cells[6].Row][l.Cells[6].Col][g_index] &&
                                                         Possibilities[l.Cells[2].Row][l.Cells[2].Col][c_index] != Possibilities[l.Cells[6].Row][l.Cells[6].Col][g_index] &&
                                                         Possibilities[l.Cells[3].Row][l.Cells[3].Col][d_index] != Possibilities[l.Cells[6].Row][l.Cells[6].Col][g_index] &&
                                                         Possibilities[l.Cells[4].Row][l.Cells[4].Col][e_index] != Possibilities[l.Cells[6].Row][l.Cells[6].Col][g_index] &&
                                                         Possibilities[l.Cells[5].Row][l.Cells[5].Col][f_index] != Possibilities[l.Cells[6].Row][l.Cells[6].Col][g_index])
                                                   {
                                                      List<int> cells = new List<int>() { Possibilities[l.Cells[0].Row][l.Cells[0].Col][a_index],
                                                                                             Possibilities[l.Cells[1].Row][l.Cells[1].Col][b_index],
                                                                                             Possibilities[l.Cells[2].Row][l.Cells[2].Col][c_index],
                                                                                             Possibilities[l.Cells[3].Row][l.Cells[3].Col][d_index],
                                                                                             Possibilities[l.Cells[4].Row][l.Cells[4].Col][e_index],
                                                                                             Possibilities[l.Cells[5].Row][l.Cells[5].Col][f_index],
                                                                                             Possibilities[l.Cells[6].Row][l.Cells[6].Col][g_index] };

                                                      bool A = (l.Clue_A != 0) ? Verify(l.Clue_A, cells) : true;
                                                      cells.Reverse();
                                                      bool B = (l.Clue_B != 0) ? Verify(l.Clue_B, cells) : true;

                                                      if (A == true && B == true)
                                                         return true; // There is at least one valid combination.
                                                   }
                                                }
                                          }
                                    }
                              }
                        }
                  }
               }
               return false; // No valid combination found.
            }

            public int GetNumberOfCellsWhereCluePossible(Line l, int clue)
            {
               int count = 0;
               foreach (Cell c in l.Cells)
                  if (Possibilities[c.Row][c.Col].Contains(clue))
                     count++;
               return count;
            }

            public void FindSingleClues()
            {
               foreach (Line l in Lines)
                  foreach (int c in Range)
                     if (1 == GetNumberOfCellsWhereCluePossible(l, c))
                        foreach (Cell cell in l.Cells)
                           if (Possibilities[cell.Row][cell.Col].Contains(c))
                              SetClue(cell.Row, cell.Col, c);

               for (int row = 0; row < SIZE; row++)
                  for (int col = 0; col < SIZE; col++)
                     if (Possibilities[row][col].Length == 1)
                        SetClue(row, col, Possibilities[row][col][0]);

               foreach (Line l in Lines)
               {
                  if (l.Finished == false)
                  {
                     bool completed = true;
                     foreach (Cell c in l.Cells)
                     {
                        if (Possibilities[c.Row][c.Col].Length != 1)
                        {
                           completed = false;
                           break;
                        }
                     }
                     if (completed)
                        l.Finished = true;
                  }
               }
            }

            public void TestCellValuesAgainstClues()
            {
               foreach (Line l in Lines)
                  if (l.Clue_A != 0 || l.Clue_B != 0)
                     foreach (Cell cell in l.Cells)
                        if (Possibilities[cell.Row][cell.Col].Count() > 1)
                        {
                           foreach (int tested_clue in Possibilities[cell.Row][cell.Col])
                           {
                              int[] original_possibilities = Possibilities[cell.Row][cell.Col];
                              int[] temp = new int[] { tested_clue };
                              Possibilities[cell.Row][cell.Col] = temp;

                              if (Verify(l) == false) // no possible solution found
                              {
                                 var o = original_possibilities.ToList();
                                 o.Remove(tested_clue);
                                 original_possibilities = o.ToArray();

                                 if (original_possibilities.Count() == 1)
                                    SetClue(cell.Row, cell.Col, original_possibilities[0]);
                              }

                              Possibilities[cell.Row][cell.Col] = original_possibilities;
                           }
                        }
            }

            public void Print()
            {
               for (int i = 0; i < SIZE; i++)
               {
                  if (i == 0)
                  {
                     foreach (Line l in GetCols())
                        Console.Write("         [" + l.Clue_A + "]   ");
                     Console.WriteLine();
                  }
                  Console.Write("[" + Lines[SIZE + i].Clue_A + "]   ");
                  for (int j = 0; j < SIZE; j++)
                  {
                     for (int z = 0; z < Possibilities[i][j].Length; z++)
                     {
                        Console.Write(Possibilities[i][j][z] + ",");
                     }
                     for (int spaces = Possibilities[i][j].Length * 2; spaces < 15; spaces++)
                        Console.Write(" ");
                  }
                  Console.Write("   [" + Lines[SIZE + i].Clue_B + "]");
                  Console.WriteLine();
                  if (i == SIZE - 1)
                  {
                     foreach (Line l in GetCols())
                        Console.Write("         [" + l.Clue_B + "]   ");
                     Console.WriteLine();
                  }
               }
               for (int spaces = 0; spaces < 15 * 7; spaces++)
                  Console.Write("-");

               Console.WriteLine();
               Console.ReadLine();
            }

            private void Solve()
            {
               while (IsSolved() == false)
               {
                  TestCellValuesAgainstClues();
                  FindSingleClues();
                  //Print();
               }

               for (int row = 0; row < SIZE; row++)
                  for (int col = 0; col < SIZE; col++)
                     Solution[row][col] = Possibilities[row][col][0];
            }
         }

         public static int[][] SolvePuzzle(int[] clues)
         {
            Riddle r = new Riddle(clues);
            return r.Solution;
         }
      }
   }
}
