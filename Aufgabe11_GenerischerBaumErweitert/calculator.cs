using System;

namespace calculator 
{
    delegate void ReportProgressMethod(int progress);
    class Calculator
    {
        public event ReportProgressMethod ProgressMethod;
        public Calculator()
        {
            ProgressMethod += OutputProgressInConsole;
        }
        public void CalculateSomething()
        {
            for (int i = 0; i <= 150; i++)
            {
                if (i == 0 || i == 50 || i == 75 || i == 100 || i == 150)
                {
                    ProgressMethod(i);
                }
            }
        }
        public void OutputProgressInConsole(int progress)
        {
            Console.WriteLine(progress + " %");
        }
    }
}