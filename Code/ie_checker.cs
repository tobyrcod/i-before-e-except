using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IBeforeE
{
    class Program
    {
        static string directory = "Wherever you have saved words_alpha.txt";
        static string allWordsFile = "/words_alpha.txt";
        static string ruleFollowerFile = "/rule_follower_words.txt";
        static string ruleBreakerFile = "/rule_breaker_words.txt";
        static string ignoreFile = "/ignore_words.txt";

        static List<string> ruleFollowerWords = new List<string>();
        static List<string> ruleBreakerWords = new List<string>();
        static List<string> ignoreWords = new List<string>();

        static void Main(string[] args)
        {
            string[] allWords = File.ReadAllLines(directory + allWordsFile);
            Console.WriteLine(allWords.Length);
        }

        static void GenerateWordFiles() {
            Console.WriteLine("Loading words...");
            string[] allWords = File.ReadAllLines(directory + allWordsFile);
            Console.WriteLine("All words loaded successfully");

            Console.WriteLine("Analysing words...");
            for (int i = 0; i < allWords.Length; i++)
            {
                AnalyseWord(allWords[i], out List<int> catagory);
                if (catagory.Count == 0)
                {
                    //Ignore
                    ignoreWords.Add(allWords[i]);
                }
                else
                {
                    for (int j = 0; j < catagory.Count; j++)
                    {
                        int code = catagory[j];
                        if (code == 1 || code == 4)
                        {
                            //Good
                            ruleFollowerWords.Add(allWords[i]);
                        }
                        else
                        {
                            //Bad
                            ruleBreakerWords.Add(allWords[i]);
                        }
                    }
                }
            }
            Console.WriteLine("Analysed all words successfully");

            Console.WriteLine("Writing rule follow words to file...");
            File.WriteAllLines(directory + ruleFollowerFile, ruleFollowerWords);
            Console.WriteLine("Successfully saved rule follow words to file");

            Console.WriteLine("Writing rule break words to file...");
            File.WriteAllLines(directory + ruleBreakerFile, ruleBreakerWords);
            Console.WriteLine("Successfully saved rule break words to file");

            Console.WriteLine("Writing ignore words to file...");
            File.WriteAllLines(directory + ignoreFile, ignoreWords);
            Console.WriteLine("Successfully saved ignore words to file");
        }

        static void AnalyseWord(string word, out List<int> wordCatagory)
        {
            //Sort into 1 of 4 catagories
            //return 1 -> ie before c
            //return 2 -> ie after c
            //return 3 -> ei before c
            //return 4 -> ei after c

            wordCatagory = new List<int>();

            if (word.Length <= 2)
                return;

            for (int i = 1; i < word.Length - 1; i++)
            {
                if (word[i] == 'i')
                {
                    if (word[i + 1] == 'e')
                    {
                        wordCatagory.Add((word[i - 1] != 'c') ? 1 : 2);
                    }
                }

                if (word[i] == 'e')
                {
                    if (word[i + 1] == 'i')
                    {
                        wordCatagory.Add((word[i - 1] != 'c') ? 3 : 4);
                    }
                }
            }
        }
    }
}
