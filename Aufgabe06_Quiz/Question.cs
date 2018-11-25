using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe06_Quiz
{
    class Question
    {
        public string question;
        public string[] answers;
        public bool isCorrect;
        public string inputAnswer;
        private int correctIndex;
        private string questionType;
        public static string trueAndFalse = "TF";
        public static string multipleChoice = "MC";


        /// <summary>
        /// </summary>
        /// <param name="q"></param>
        /// <param name="answers"></param>
        /// <param name="correctAnswer"></param>
        public Question(string q, string[] answersList, string typeOfQuestion, int correctAnswer)
        {
            question = q;
            questionType = typeOfQuestion;
            if(questionType == multipleChoice)
                answers = new string[4];
            else if(questionType == trueAndFalse)
                answers = new string[2];
            
            for(int i = 0; i < answersList.Length; i++)
            {
                this.answers[i] = answersList[i];
            }

            correctIndex = correctAnswer;
        }


        public bool Ask()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(question);
            Console.ForegroundColor = ConsoleColor.White;
            if (questionType == multipleChoice)
            {
                Console.WriteLine("Gebe eine Antwort aus den folgenden Möglichkeiten ein...");
            }
            else
            {
                Console.WriteLine("Gebe bitte 'true' oder 'false' ein... ");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < answers.Length; i++)
            {
                Console.WriteLine(answers[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("--------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            inputAnswer = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;


            if (inputAnswer == answers[correctIndex])
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Richtig!");
                isCorrect = true;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Drücken eine beliebige Taste, um zur nächsten Frage zu gelangen..");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Falsch, weitergehen!");
                isCorrect = false;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Drücken eine beliebige Taste, um zur nächsten Frage zu gelangen..");
                Console.ReadKey();
            }
            
            return isCorrect;
        }

        public void PrintQuestion()
        {
            Console.WriteLine(question);
            if (questionType == multipleChoice)
            {
                Console.WriteLine("Gebe eine Antwort aus den folgenden Möglichkeiten ein...");
            }
            else
            {
                Console.WriteLine("Gebe bitte 'true' oder 'false' ein.... ");
            }

            for (int i = 0; i < answers.Length; i++)
            {
                Console.WriteLine(answers[i]);
            }
        }
    }
}