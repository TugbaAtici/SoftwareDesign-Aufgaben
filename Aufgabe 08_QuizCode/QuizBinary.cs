using System;

namespace Aufgabe08_QuizCode
{
    class QuizBinary : Quizelement
    {
        public void AnswerQuizBinary(int score)
        {
            Quizelement quizBinary = new Quizelement();
            quizBinary.question = "Stimmt es, dass bei der Mischung von Gelb und Rot Orange ergeben?";
            
            Console.WriteLine(quizBinary.question);

            Console.WriteLine("Beantworte die Frage mit J bzw N");
            string UserInput = Console.ReadLine();

            if (UserInput == "J")
            {
                Console.WriteLine("Korrekt");
                score++;
            }

            else
            {
                Console.WriteLine("Falsch");
            }

            Program p = new Program();
            p.QuizMenu(score);
        }
    }
}