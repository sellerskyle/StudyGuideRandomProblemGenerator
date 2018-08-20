using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//TODO: Learn to parse from files and run from file
//      Use better save and load directory techniques,
//      dont use hard coded desktop
namespace StudyGuideRandomProblemGenerator
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            Console.Write("Do you want to load from a file? (y/n): ");
            string yesOrNo = Console.ReadLine();
            if (yesOrNo.Equals("y"))
            {
                Console.Write("Enter file name: ");
                string fileName = Console.ReadLine();
                Run(fileName);
            } else if (yesOrNo.Equals("n"))
            {
                Run();
            }
            
            
        }

        static void Run()
        {
            List<Chapter> chapters = new List<Chapter>();

            Console.Write("Enter first chapter: ");
            int firstChapter = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter last chapter: ");
            int lastChapter = Convert.ToInt32(Console.ReadLine());

            for (int i = firstChapter; i <= lastChapter; i++)
            {
                chapters.Add(new Chapter(i));
            }

            Console.Write("Enter Chapters being skipped: ");
            string skippedChapters = Console.ReadLine();

            //Parse skipped Chapters here into arraylist
            if (skippedChapters != "")
            {
                int[] skippedChaptersArray = skippedChapters.Split(' ').Select(p => int.Parse(p)).ToArray();
                int numChaptersRemoved = skippedChaptersArray.Length;
                //for each chapter to be removed, remove from chapters array
                for (int i = 0; i < skippedChaptersArray.Length; i++)
                {
                    for (int j = 0; j < chapters.Count; j++)
                    {
                        if (skippedChaptersArray[i]
                            == chapters[j].ChapterNumber)
                        {
                            chapters.RemoveAt(j);
                        }
                    }
                }
            }
            //for each chapter in the chapters array list, ask for number of problems in that chapter
            int numProblems;
            for (int i = 0; i < chapters.Count; i++)
            {
                Console.Write("Enter number of problems in chapter {0}: ", chapters[i].ChapterNumber);
                numProblems = Convert.ToInt32(Console.ReadLine());
                chapters[i].Problems = numProblems;
            }

            // q is escape key
            string userInput;
            do
            {
                string problem = GenerateRandomProblem(chapters);
                Console.Write(problem);
                userInput = Console.ReadLine();
                if (userInput.Equals("save"))
                {
                    SaveAsFile(chapters);
                }
            }
            while (!userInput.Equals("q"));
        }

        static void Run(string fileName)
        {
            List<Chapter> chapters = new List<Chapter>();

            string desktopDirectory = @"C:\Users\Kyle\Desktop\";
            string fullPath = desktopDirectory + fileName;

            string[] lines = File.ReadAllLines(fullPath);
            //StreamReader sr = File.OpenText(fullPath);

            //Regex regex = new Regex(@"(\d+)\s+(\d+)$");
            int chapter;
            int numProblems;
            foreach (string line in lines)
            {
                Chapter temp = new Chapter();

                Match match = Regex.Match(line, @"\d+");
                if (match.Success)
                {
                    chapter =  Int32.Parse(match.Value);
                } else
                {
                    chapter = 0;
                }

                match = match.NextMatch();
                if (match.Success)
                {
                    numProblems = Int32.Parse(match.Value);
                }
                else
                {
                    numProblems = 0;

                }

                temp.ChapterNumber = chapter;
                temp.Problems = numProblems;
                chapters.Add(temp);

            }

            // q is escape key
            string userInput;
            do
            {
                string problem = GenerateRandomProblem(chapters);
                Console.Write(problem);
                userInput = Console.ReadLine();
                if (userInput.Equals("save"))
                {
                    SaveAsFile(chapters);
                }
            }
            while (!userInput.Equals("q"));
        }
        

        static void PrintIntro()
        {
        }

        static string GenerateRandomProblem(List<Chapter> chapters)
        {
            string output = "";
            int index = rand.Next(chapters.Count);
            int chapter = chapters[index].ChapterNumber;
            int problemNum = rand.Next(1, chapters[index].Problems);
            output += chapter + "." + problemNum;
            return output;
        }

        static void SaveAsFile(List<Chapter> chapters)
        {
            string desktopDirectory = @"C:\Users\Kyle\Desktop\";
            Console.Write("Enter file to save: ");
            string fileName = Console.ReadLine();
            string fullPath = desktopDirectory + fileName;
            string[] chapterInfo = getChaptersAsTextArray(chapters);

            File.WriteAllLines(fullPath, chapterInfo);
        }

        static string[] getChaptersAsTextArray(List<Chapter> chapters)
            {
                string[] output = new string[chapters.Count];
                int i = 0;
                foreach (Chapter chapter in chapters)
                {
                int chap = chapter.ChapterNumber;
                int questions = chapter.Problems;
                string toAdd = chap + " " + questions;
                output[i] = toAdd;
                i++;
                }
                return output;
            }

        }

    }
