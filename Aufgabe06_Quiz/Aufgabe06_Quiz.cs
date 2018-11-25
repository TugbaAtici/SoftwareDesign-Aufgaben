using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe06_Quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            IntroGreeting();

            List<Question> question = new List<Question> {
                //MultipleChoice type question
                new Question("Wie oft wurde Deutschland Fußball-Weltmeister", new string[] { "1", "2", "3", "4", "5", "6" }, Question.multipleChoice, 3),
                //true/false type question
                //new Question("2 + 2 does not equal 4.", new string[] { "true", "false" }, Question.trueAndFalse, 1),
                new Question("Halloween stammt ursprünglich aus ...", new string[] { "Irland", "Norwegen", "Vereinigten Staaten", "Deutschland", "Mexiko", "Japan" }, Question.multipleChoice, 0),
                new Question("Wie lang ist der Äquator", new string[] { "15k km", "6k km", "2.5k km", "28k km", "9 km", "40k km" }, Question.multipleChoice, 5),
                new Question("Wann war der erste Mensch auf dem Mond", new string[] { "1888", "1792", "1902", "1698", "1969", "1996" }, Question.multipleChoice, 4),
                new Question("Für welche Kategorie gibt es keinen Nobelpreis?", new string[] { "Mathematik", "Physik", "Literatur", "Friedensbemühungen", "Chemie", "Medizin" }, Question.multipleChoice, 0),
            };

            
            for(int i = 0; i < question.Count; i++)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Question #" + (1 + i).ToString());
                if (question[i].Ask())
                {
                    Results.AddResult(question[i]);
                    Console.WriteLine("Drücke eine beliebige Taste um fortzufahren");
                    //Console.WriteLine("--------------------------------");
                }
                else
                {
                    Results.AddResult(question[i]);
                }
                Console.Clear();
            }

            Console.WriteLine("--------------------------------");
            int tempScore = 0;

            for(int i = 0; i < Results.firstResults.Count; i++)
            {
                if(Results.firstResults[i].isCorrect)
                {
                    tempScore++;
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Dein jetziger Punktestand beträgt: " + tempScore + "/" + Results.firstResults.Count.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Drücke eine beliebige Taste um fortzufahren...");
            Console.ReadKey();

            Results.RunSecondAttempt();

            Console.WriteLine("--------------------------------");
            Console.WriteLine("Ende!");

            int tempFinalScore = 0;

            for (int i = 0; i < Results.finalResults.Count; i++)
            {
                if (Results.finalResults[i].isCorrect)
                {
                    tempFinalScore++;
                }
            }

            Console.WriteLine("Dein engültiger Punktestand beträgt: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(tempFinalScore + "/" + Results.finalResults.Count.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            if (tempFinalScore > 5)
            {
                Console.WriteLine("Gut!");
            }
            else
            {
                Console.WriteLine("Versuche es erneut...");
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Drücke eine beliebige Taste um zu beenden...");
            Console.ReadKey();
        }


        public static void IntroGreeting()
        {
            
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Es gibt fünf Fragen zu beantworten.");
            Console.WriteLine("Nach dem ersten Versuch bekommst du einen zweiten.");
            Console.WriteLine("Du musst nur die Frage beantworten, die du falsch beantwortet hast.");
            Console.WriteLine("Bist du bereit?");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Drücke eine beliebige Taste um das Quiz zu starten...");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

    }
}