using System;

namespace Aufgabe09_SaveQuestions
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            
        }

        static void Serialize(string addUserQuestion, int addHowManyAnswers, string userAnswer1, int score)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(addUserQuestion.GetType());
            FileStream str = new FileStream(@"C:\Users\Admin\Documents\GitHub\Modulare Softwareentwicklung im Frontend\Modulare-Softwareentwicklung-im-Frontend\SoftwareDesign-Aufgaben\Aufgabe 08_QuizCodetest.xml", FileMode.Open);
            x.Serialize(str, addUserQuestion);

            System.Xml.Serialization.XmlSerializer y = new System.Xml.Serialization.XmlSerializer(addHowManyAnswers.GetType());
            y.Serialize(str, addHowManyAnswers);

            System.Xml.Serialization.XmlSerializer z = new System.Xml.Serialization.XmlSerializer(userAnswer1.GetType());
            z.Serialize(str, userAnswer1);

            Program p = new Program();
            p.QuizMenu(score);
        }


    }
}
