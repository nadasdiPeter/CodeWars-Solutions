using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars_CSharp_Solutions.src.katas
{
   class Kata_PullYourWordsTogether : Kata
   {
      protected override void RunKata()
      {
         Console.WriteLine("\nPull your words together:");
         List<KataTest> test_package = new List<KataTest> /* Testcases */
         {
            new KataTest(Sentencify(new string[] {"i", "am", "an", "AI"}), "I am an AI."),
            new KataTest(Sentencify(new string[] {"yes"}), "Yes."),
            new KataTest(Sentencify(new string[] {"i'm","afraid","I","can't","let","you","do","that"}), "I'm afraid I can't let you do that."),
            new KataTest(Sentencify(new string[] {}), ""),
         };
         Verify(test_package);
      }

      public string Sentencify(string[] words)
      {
         if(words != null && words.Length != 0)
         {
            words[0] = words[0].First().ToString().ToUpper() + words[0].Remove(0, 1);
            return string.Join(" ", words) + ".";
         }
         return "";
      }
   }
}