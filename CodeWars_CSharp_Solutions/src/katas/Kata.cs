using CodeWars_CSharp_Solutions.src.katas;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace CodeWars_CSharp_Solutions
{
   public abstract class Kata
   {
      private void Print(KataTest kt, int index)
      {
         Console.WriteLine("   - " + index + ". Test: " + (kt.VerificationResult == Test_result_t.FAILED ? "Failed" : "Passed"));
         if( kt.VerificationResult == Test_result_t.FAILED )
         {
            if (kt.TestResult is IEnumerable enumerable1 && kt.Solution is IEnumerable enumerable2) // If the objects are IEnumerable (list, array, etc.)
            {
               Console.Write("      -> Answer: ");
               foreach (var l in enumerable1.Cast<object>().ToList())
                  Console.Write(l + ", ");

               Console.Write("\n      -> Solution:   ");
               foreach (var l in enumerable2.Cast<object>().ToList())
                  Console.Write(l + ", ");
               Console.WriteLine();
            }
            else
               Console.WriteLine("      -> Answer:   {0}\n      -> Solution: {1}", kt.TestResult, kt.Solution);
         }
      }

      public void Start()
      {
         var watch = System.Diagnostics.Stopwatch.StartNew();

         RunKata();

         watch.Stop();
         Console.WriteLine("   - ExecutionTime: " + watch.ElapsedMilliseconds + " (ms)");
      }

      protected abstract void RunKata();

      protected virtual void Verify(List<KataTest> test_package)
      {
         int index = 1;

         foreach (KataTest t in test_package)
         {
            t.VerificationResult = t.Equal() ? Test_result_t.PASSED : Test_result_t.FAILED;
            if(t.VerificationResult == Test_result_t.FAILED)
               Print(t,index);
            index++;
         }
            
         if( test_package.Where(x => x.VerificationResult == Test_result_t.FAILED).Count() == 0 )
            Console.WriteLine( "   - Passed test(s): " + test_package.Count + "/" + test_package.Count);
      }
   }
}