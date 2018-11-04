using System;

namespace Praktikum_24._10._2018
{
    class Program
    {
        static void Main(string[] args)
        {
            int DecimalToHexal(int dec)
            {
                int result = 0;
                int factor = 1;
                while (dec != 0)
                {
                    int digit = dec % 6;
                    dec /= 6;
                    result += factor * digit;
                    factor *= 10;
                }
                return result;
            }
        }
    }
}
