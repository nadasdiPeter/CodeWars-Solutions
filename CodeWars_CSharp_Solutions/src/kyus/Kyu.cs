using CodeWars_CSharp_Solutions.src.katas;
using System.Linq;
using System.Collections.Generic;

namespace CodeWars_CSharp_Solutions.src.kyus
{
   class Kyu
   {
      private static List<Kata> Kyu1_package = new List<Kata>();
      private static List<Kata> Kyu2_package = new List<Kata>();
      private static List<Kata> Kyu3_package = new List<Kata>();
      private static List<Kata> Kyu4_package = new List<Kata>();
      private static List<Kata> Kyu5_package = new List<Kata>();
      private static List<Kata> Kyu6_package = new List<Kata>();
      private static List<Kata> Kyu7_package = new List<Kata>();
      private static List<Kata> Kyu8_package = new List<Kata>();
      private static List<Kata> Kyu_unreated_package = new List<Kata>();

      private List<List<Kata>> Kyu_packages = new List<List<Kata>> { Kyu1_package, Kyu2_package, Kyu3_package, Kyu4_package, Kyu5_package, Kyu6_package, Kyu7_package, Kyu8_package };

      public Kyu()
      {
         Kyu_Level1();
         Kyu_Level2();
         Kyu_Level3();
         Kyu_Level4();
         Kyu_Level5();
         Kyu_Level6();
         Kyu_Level7();
         Kyu_Level8();
      }

      private void Kyu_Level1()
      {
         Add_Kata(new Kata_7x7Skyscrapers(), 1);
      }

      private void Kyu_Level2()
      {
      }

      private void Kyu_Level3()
      {
      }

      private void Kyu_Level4()
      {
         Add_Kata(new Kata_BoggleWordChecker(), 4);
      }

      private void Kyu_Level5()
      {
         Add_Kata(new Kata_WeightSort(), 5);
         Add_Kata(new Kata_CountIPAddresses(), 5);
      }

      private void Kyu_Level6()
      {
         Add_Kata(new Kata_RotateArray(), 6);
         Add_Kata(new Kata_FindTheMine(), 6);
      }

      private void Kyu_Level7()
      {
         Add_Kata(new Kata_SendInTheClones(),   7);
         Add_Kata(new Kata_LogicalCalc(),       7);
         Add_Kata(new Kata_TheRejectFunction(), 7);
         Add_Kata(new Kata_PullYourWordsTogether(), 7);
         Add_Kata(new Kata_GoingToTheCinema(), 7);
         Add_Kata(new Kata_DigitsExplosion(), 7);
      }

      private void Kyu_Level8()
      {
         Add_Kata(new Kata_MergeTwoSortedArraysIntoOne(), 8);
      }

      public void Add_Kata(Kata kata, int kyu_level) => Kyu_packages[kyu_level - 1].Add(kata);

      public void Run_AllKata()
      {
         foreach (var package in Kyu_packages)
            foreach (var kata in package)
               kata.Start();
      }

      public void Run_SpecificKata() => Kyu5_package.Last().Start();
   }
}
