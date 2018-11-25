using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe06_Quiz
{
    static class Results
    {
        public static List<Question> firstResults = new List<Question>();
        public static List<Question> finalResults = new List<Question>();


        public static void AddResult(Question questionResult)
        {
            firstResults.Add(questionResult);
        }

        public static void AddFinalResult(Question question)
        {
            finalResults.Add(question);
        }

        public static void RunSecondAttempt()
        {
            Console.Clear();
            Console.WriteLine("Zweiter Versuch");

            for (int i = 0; i < firstResults.Count; i++)
            {
                Console.WriteLine("\n--------------------------------");
                Console.WriteLine("\nQuestion " + (1 + i).ToString());

                if(firstResults[i].isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nRichtig");
                    Console.ForegroundColor = ConsoleColor.White;
                    firstResults[i].PrintQuestion();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Deine Antwort: " + firstResults[i].inputAnswer);
                    Console.ForegroundColor = ConsoleColor.White;
                    AddFinalResult(firstResults[i]);
                    Console.WriteLine("Drücken eine beliebige Taste, um zur nächsten Frage zu gelangen.");
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Falsch, bitte versuche es erneut.");
                    Console.ForegroundColor = ConsoleColor.White;
                    if (firstResults[i].Ask())
                    {
                        AddFinalResult(firstResults[i]);
                        Console.WriteLine("Drücken eine beliebige Taste, um zur nächsten Frage zu gelangen.");
                    }
                    else
                    {
                        AddFinalResult(firstResults[i]);
                    }

                    Console.WriteLine("\n--------------------------------");
                }

                Console.Clear();
            }
        }
    }
}