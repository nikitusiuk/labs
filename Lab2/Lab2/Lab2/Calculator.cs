using System;
using System.Collections.Generic;
public class Calculator
{
    private static List<int> Mem = new List<int>();//Список 
    private int lastmem = -1;//последний номер операции когда число сохраняли в mem
    private char lastoper = '+';//последняя операция
    public bool inputnum = true;//true вводим число, false вводим операцию
    public List<int> GetMem()
    {
        return Mem;
    }
    public String GetMem(int id)//вычисленное значение по индексу
    {
        if (id == 0 && lastmem != -1 && Mem.Count > 0)
        {
            return Mem[lastmem].ToString();
        }
        else
        if (id >= 1 && id <= Mem.Count)
        {
            return Mem[id - 1].ToString();
        }
        else
        {
            return "";
        }
    }
    public String GetLastMem()//последнее вычисленное значение
    {
        return GetMem(0);
    }

    public bool Calculate(String S)// S строка введённая пользователем
    {
        if (S == "+" || S == "-" || S == "/" || S == "*")
        {
            if (!inputnum)
            {
                lastoper = S[0];
                inputnum = true;
            }
            else
            {
                Console.WriteLine("Error. You have to enter number!");
                return false;
            }
        }
        else if (S[0] == '#')
        {
            S = S.Substring(1);
            int num;
            if (int.TryParse(S, out num))
            {
                if (num >= 1 && num <= Mem.Count)
                {
                    lastmem = num - 1;
                    Console.WriteLine("[#" + (lastmem + 1).ToString() + "]=" + Mem[lastmem].ToString());
                    inputnum = false;
                }
                else
                {
                    Console.WriteLine("Error. No mem state " + num.ToString());
                    return false;
                }
            }
        }
        else if (inputnum)
        {
            int num;
            if (int.TryParse(S, out num))
            {
                if (lastmem == -1)
                {
                    lastmem++;
                    if (lastmem < Mem.Count)
                    {
                        Mem[lastmem] = num;
                    }
                    else
                    {
                        Mem.Add(num);
                    }
                    Console.WriteLine("[#" + (lastmem + 1).ToString() + "]=" + num.ToString());
                    inputnum = false;
                }
                else
                {
                    switch (lastoper)
                    {
                        case '+':
                            num = Mem[lastmem] + num;
                            lastmem++;
                            if (lastmem < Mem.Count)
                            {
                                Mem[lastmem] = num;
                            }
                            else
                            {
                                Mem.Add(num);
                            }
                            Console.WriteLine("[#" + (lastmem + 1).ToString() + "]=" + num.ToString());
                            inputnum = false;
                            break;
                        case '-':
                            num = Mem[lastmem] - num;
                            lastmem++;
                            if (lastmem < Mem.Count)
                            {
                                Mem[lastmem] = num;
                            }
                            else
                            {
                                Mem.Add(num);
                            }
                            Console.WriteLine("[#" + (lastmem + 1).ToString() + "]=" + num.ToString());
                            inputnum = false;
                            break;
                        case '*':
                            num = Mem[lastmem] * num;
                            lastmem++;
                            if (lastmem < Mem.Count)
                            {
                                Mem[lastmem] = num;
                            }
                            else
                            {
                                Mem.Add(num);
                            }
                            Console.WriteLine("[#" + (lastmem + 1).ToString() + "]=" + num.ToString());
                            inputnum = false;
                            break;
                        case '/':
                            if (num == 0)
                            {
                                Console.WriteLine("Error. You have to enter not 0!");
                                return false;
                            }
                            else
                            {
                                num = Mem[lastmem] / num;
                                lastmem++;
                                if (lastmem < Mem.Count)
                                {
                                    Mem[lastmem] = num;
                                }
                                else
                                {
                                    Mem.Add(num);
                                }
                                Console.WriteLine("[#" + (lastmem + 1).ToString() + "]=" + num.ToString());
                                inputnum = false;
                            }
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error. You have to enter number!");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Error. You have to enter operation!");
            return false;
        }
        return true;
    }

}
