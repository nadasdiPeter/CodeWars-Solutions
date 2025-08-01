using System.Collections;
using System.Linq;

namespace CodeWars_CSharp_Solutions.src.katas
{
   enum Test_result_t { PASSED, FAILED, UNTESTED }

   public class KataTest
   {
      private object mSolution;
      private object mTestResult;
      private Test_result_t mVerificationResult = Test_result_t.UNTESTED;

      public object Solution { get => mSolution; set => mSolution = value; }
      public object TestResult { get => mTestResult; set => mTestResult = value; }
      internal Test_result_t VerificationResult { get => mVerificationResult; set => mVerificationResult = value; }

      public KataTest(object testResult, object solution)
      {
         TestResult = testResult;
         Solution = solution;
      }

      public bool Equal() => AreEqual(TestResult, Solution);

      private bool AreEqual(object a, object b)
      {
         if (ReferenceEquals(a, b))
            return true;

         if (a == null || b == null)
            return false;

         if (a.GetType() != b.GetType())
            return false;

         if (a is IEnumerable enumerable1 && b is IEnumerable enumerable2) // If the objects are IEnumerable (list, array, etc.)
         {
            var aList = enumerable1.Cast<object>().ToList();
            var bList = enumerable2.Cast<object>().ToList();

            if (aList.Count != bList.Count)
               return false;

            for (int i = 0; i < aList.Count; i++)
            {
               if (AreEqual(aList[i], bList[i]) == false)
                  return false;
            }

            return true;
         }
         else
         {
            return a.Equals(b);
         }
      }
   }
}
