using System;

namespace Aufgabe09_SaveQuestions
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            
        }

        static void Serialize(string addQuestions, int addAnswers, string answersOfUser, int score)
        {
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(addQuestions.GetType());
            FileStream str = new FileStream(@"C:\Users\Admin\Documents\GitHub\Modulare Softwareentwicklung im Frontend\Modulare-Softwareentwicklung-im-Frontend\SoftwareDesign-Aufgaben\Aufgabe 08_QuizCodetest.xml", FileMode.Open);
            x.Serialize(str, addQuestions);

            System.Xml.Serialization.XmlSerializer y = new System.Xml.Serialization.XmlSerializer(addAnswers.GetType());
            y.Serialize(str, addAnswers);

            System.Xml.Serialization.XmlSerializer z = new System.Xml.Serialization.XmlSerializer(answersOfUser.GetType());
            z.Serialize(str, answersOfUser);

            Program p = new Program();
            p.QuizMenu(score);
        }


    }
}
