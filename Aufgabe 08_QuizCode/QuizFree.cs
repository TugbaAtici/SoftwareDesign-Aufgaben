using System;

namespace Aufgabe08_QuizCode
{
    class QuizFree : Quizelement
    {
        public void AnswerQuizFree(int score)
        {
            Quizelement quizFree = new Quizelement();
            quizFree.question = "Führe die Redewendung fort: Wer den Cent nicht ehrt,...";
            
            Console.WriteLine(quizFree.question);

            Console.WriteLine("Deine Antwort ist: ");
            string rightWord = Console.ReadLine();

            if (rightWord == "ist den Euro nicht wert")
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