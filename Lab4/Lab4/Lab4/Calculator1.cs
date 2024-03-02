using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public class Calculator
    {
        public List<int> Mem = new List<int>();
        private bool inputnum = true;//true вводим число, false вводим операцию
        private int lastmem = -1;//последний номер операции когда число сохраняли в mem
        private char lastoper = '+';//последняя операция

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

        public bool Calculate(String S)
        {
            if (S.Length == 0)
            {
                return false;
            }

            if (S == "+" || S == "-" || S == "/" || S == "*")
            {
                if (!inputnum)
                {
                    lastoper = S[0];
                    inputnum = true;
                }
                else
                {
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
                        inputnum = false;
                    }
                    else
                    {
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
                                inputnum = false;
                                break;
                            case '/':
                                if (num == 0)
                                {
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
                                    inputnum = false;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public bool UpdateMem(int id, int value)
        {
            if (id >= 1 && id <= Mem.Count)
            {
                Mem[id-1] = value;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteMem(int id)
        {
            if (id >= 1 && id <= Mem.Count)
            {
                Mem.RemoveAt(id - 1);
                if (lastmem >= Mem.Count)
                {
                    lastmem = Mem.Count-1;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
