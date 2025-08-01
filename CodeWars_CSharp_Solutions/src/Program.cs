using CodeWars_CSharp_Solutions.src.kyus;
using System;

namespace CodeWars_CSharp_Solutions
{
   class Program
   {
      static void Main(string[] args)
      {
         /* Run all katas */
         //new Kyu().Run_AllKata();

         /* Run only specific kata */
         new Kyu().Run_SpecificKata();

         /* Keep the consolewindow open till any key pressed */
         Console.ReadKey();
      }
   }
}
