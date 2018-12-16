using System;

namespace Aufgabe08_QuizCode
{
    class QuizSingle : Quizelement
    {


       public void AnswerQuizSingle(int score)
       {
           Quizelement quizSingle = new Quizelement();
           quizSingle.question = "Wer war der erste Bundeskanzler der BRD?";

            string[] answers = new string[4];
            answers[0] = "Barak Obama";
            answers[1] = "Helmut Kohl";
            answers[2] = "Konrad Adenauer";
            answers[3] = "Angela Merkel";

           Console.WriteLine(quizSingle.question);

           Console.WriteLine(answers[0]);
           Console.WriteLine(answers[1]);
           Console.WriteLine(answers[2]);
           Console.WriteLine(answers[3]);

           Console.WriteLine("Bitte wähle die richtige Antwort:");
           int answerChoice = int.Parse(Console.ReadLine());

           if (answerChoice == 3)
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