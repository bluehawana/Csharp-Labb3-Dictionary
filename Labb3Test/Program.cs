using System;
using Labb3Library;

namespace Labb3Test
{
    class Program
    {
        static void Main()
        {
            /*
            WordList wordList = new WordList("mylist", "English", "Swedish");
            wordList.Add("dog", "hund");
            wordList.Add("cat", "katt");
            wordList.Save();
            */

         //   var lists = WordList.GetLists();
            

            var wordList = WordList.LoadList("mylist");
            //wordList.Add("bird", "fågel");
            //var count = wordList.Count();
            //wordList.Save();

            wordList.List(1, words =>
            {
                foreach (var word in words)
                {
                    Console.WriteLine(word);
                }
            });

            var wordToPractice = wordList.GetWordToPractice();
            Console.WriteLine("Translate the word: " + wordToPractice.Translations[0] + " from " + wordList.Languages[wordToPractice.FromLanguage] + " to " + wordList.Languages[wordToPractice.ToLanguage]);
            string userTranslation = Console.ReadLine();

            if (userTranslation == wordToPractice.Translations[1])
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine("Incorrect. The correct translation is: " + wordToPractice.Translations[1]);
            }
        }
    }
}