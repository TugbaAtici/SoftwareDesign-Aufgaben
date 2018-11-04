using System;

namespace Praktikum_24._10._2018
{
    class Program
    {
        static void Main(string[] args)
        {
            int ConvertNumberFromSystemToSystem(int number, int fromSystem, int toSystem)
            {
                int result = 0;
                result = OtherToDecimal(number, fromSystem);
                result = DecimalToOther(result, toSystem);
                return result;
            }

            int DecimalToOther(int dec, int system)
            {
                int result = 0;
                int factor = 1;
                while (dec != 0)
                {
                    int digit = dec % system;
                    dec /= system;
                    result += factor * digit;
                    factor *= 10;
                }
                return result;
            }

            int OtherToDecimal(int other, int system)
            {
                int result = 0;
                int factor = 1;
                while (other != 0)
                {
                    int digit = other % 10;
                    other /= 10;
                    result += factor * digit;
                    factor *= system;
                }
                return result;
            }
        }
    }
}
