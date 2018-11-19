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
        string text = "My name is Tugba Atici";

        Console.WriteLine(string.Join(" ", text.Split(' ').Reverse()));
        
    }
}
}