using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseSentence
{
class Program
{
    static void Main(string[] args)
    {
        //string text = "My name is Tugba Atici";
        LettersReversed(args);
        OrderOfWordsReversed(args);
        AllReversed(args);
        
    }

    static void LettersReversed(String[] args)
        {
            string input = "";

            for(int i = 0; i < args.Length; i++)
            {
                for(int j = args[i].Length - 1; j >= 0; j--)
                {
                    input = input + args[i][j];
                }

                input = input + " ";
            }

            Console.WriteLine(input);
        }

     static void OrderOfWordsReversed(String[] args)
        {
            string input = "";

            for(int i = args.Length -1; i >= 0; i--)
            {
                input = input + args[i] + " ";
            }
            Console.WriteLine(input);
        }

        static void AllReversed(String[] args)
        {
            string input = "";

            for(int i = args.Length -1; i >= 0; i--)
            {
                for(int j = args[i].Length - 1; j >= 0; j--)
                {
                    input = input + args[i][j];
                }

                input = input + " ";
            }

            Console.WriteLine(input);
        }      
}
}