using System;

namespace Aufgabe08_QuizCode
{
    class QuizMultiple : Quizelement
    {
        public void AnswerQuizMultiple(int score)
        {
            Quizelement quizMultiple = new Quizelement();
            quizMultiple.question = "Wer war schon einmal Politiker?";
            
            string[] answers = new string[6];
            answers[0] = "Barak Obama";
            answers[1] = "Helmut Kohl";
            answers[2] = "Helene Fischer";
            answers[3] = "Justin Bieber";
            answers[4] = "Konrad Adenauer";
            answers[5] = "Elyas M'Barek";

            Console.WriteLine(quizMultiple.question);

            Console.WriteLine(answers[0]);
            Console.WriteLine(answers[1]);
            Console.WriteLine(answers[2]);
            Console.WriteLine(answers[3]);
            Console.WriteLine(answers[4]);
            Console.WriteLine(answers[5]);

            Console.WriteLine("Wähle die richtige Antwort:");
            
            string selectedAnswers = Console.ReadLine();
            string[] answer = selectedAnswers.Split(',');

            if (answer [0] == "1" && answer[1] == "2" && answer[2] == "4")
            {
                Console.WriteLine("Korrekt");
                score ++;
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