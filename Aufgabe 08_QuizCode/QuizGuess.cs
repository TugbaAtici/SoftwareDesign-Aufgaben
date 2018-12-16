using System;

namespace Aufgabe08_QuizCode
{
    class QuizGuess : Quizelement
    {
        public void AnswerQuizGuess(int score)
        {
            Quizelement quizGuess = new Quizelement();
            quizGuess.question = "Schätze, wie viele Studenten die HFU hat.";
            
            Console.WriteLine(quizGuess.question);
            Console.WriteLine("Gib eine Zahl ein:");
            double number = double.Parse(Console.ReadLine());

            double toleranceMin = 6687 * 0.09;
            double toleranceMax = 6687 * 1.1;

            if (number >= toleranceMin && number <= toleranceMax)
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