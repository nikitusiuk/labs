using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();//Объект Калькулятор
            String S;//строка для ввода
            Console.WriteLine("Usage:");
            Console.WriteLine("when a first symbol on line is ‘>’ – enter operand(number)");
            Console.WriteLine("when a first symbol on line is ‘@’ – enter operation");
            Console.WriteLine("operation is one of ‘+’, ‘-‘, ‘/’, ‘*’ or");
            Console.WriteLine("‘#’ followed with number of evaluation step");
            Console.WriteLine("‘s’ save to file (sj JSON sx XML ss SQLlite)");
            Console.WriteLine("‘l’ load from file (lj JSON lx XML ls SQLlite)");
            Console.WriteLine("‘q’ to exit");
            while (true)
            {
                if (calculator.inputnum)
                {
                    Console.Write(">");
                }
                else
                {
                    Console.Write("@");
                }
                S = Console.ReadLine();
                if (S.Length > 0)
                {
                    if (S == "q")
                    {
                        break;
                    }
                    calculator.Calculate(S);//Передаём в калькулятор введённую строку
                }
            }
        }
    }
}
