using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using System.Data.SQLite;
using System.Data;
public class Calculator
{
    private static SQLiteConnection m_dbConn;
    private static SQLiteCommand m_sqlCmd;
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

    private static bool Create_SQLLite(ref String S)
    {
        if (!File.Exists("mem.sqlite"))
        {
            SQLiteConnection.CreateFile("mem.sqlite");
        }

        try
        {
            m_dbConn = new SQLiteConnection("Data Source=mem.sqlite;Version=3;");
            m_dbConn.Open();
            m_sqlCmd = m_dbConn.CreateCommand();
            m_sqlCmd.Connection = m_dbConn;

            m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Memory (id INTEGER PRIMARY KEY AUTOINCREMENT, num INTEGER)";//создадим если не создана
            m_sqlCmd.ExecuteNonQuery();
            m_sqlCmd.CommandText = "DELETE FROM Memory";//Очистим
            m_sqlCmd.ExecuteNonQuery();
            return true;
        }
        catch (SQLiteException ex)
        {
            S = "Error: " + ex.Message;
            return false;
        }
    }
    private static bool Connect_SQLLite(ref String S)
    {
        if (!File.Exists("mem.sqlite"))
        {
            S = "No database mem.sqlite";
            return false;
        }
        try
        {
            m_dbConn = new SQLiteConnection("Data Source=mem.sqlite;Version=3;");
            m_dbConn.Open();
            m_sqlCmd = m_dbConn.CreateCommand();
            m_sqlCmd.Connection = m_dbConn;
            return true;
        }
        catch (SQLiteException ex)
        {
            S = "Error: " + ex.Message;
            return false;
        }
    }
    private static bool Read_SQLLite(ref String S)
    {
        DataTable table = new DataTable();
        String sqlQuery;

        if (m_dbConn.State != ConnectionState.Open)
        {
            S = "Open connection with SQLLite database";
            return false;
        }
        Mem.Clear();
        try
        {
            sqlQuery = "SELECT * FROM Memory";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    Mem.Add(Convert.ToInt32(row["num"]));
                }
            }
            else
            {
                S = "Database is empty";
                return false;
            }
            return true;
        }
        catch (SQLiteException ex)
        {
            S = "Error: " + ex.Message;
            return false;
        }
    }

    private static bool Write_SQLlite(ref String S)
    {
        if (m_dbConn.State != ConnectionState.Open)
        {
            S = "Open connection with database";
            return false;
        }
        try
        {
            foreach (int num in Mem)
            {
                m_sqlCmd.CommandText = "INSERT INTO Memory ('num') values ('" + num.ToString() + "')";
                m_sqlCmd.ExecuteNonQuery();
            }
            return true;
        }
        catch (SQLiteException ex)
        {
            S = "Error: " + ex.Message;
            return false;
        }
    }
    public bool SaveSQLLite(ref String S)
    {
        if (Create_SQLLite(ref S))
        {
            return Write_SQLlite(ref S);
        }
        else
        {
            return false;
        }
    }
    public bool LoadSQLLite(ref String S)
    {
        if (Connect_SQLLite(ref S))
        {
            if (Read_SQLLite(ref S))
            {
                if (Mem.Count > 0)
                {
                    inputnum = false;//вводим операцию
                    lastmem = Mem.Count - 1;
                    return true;
                }
                else
                {
                    inputnum = true;//вводим число
                    lastmem = -1;
                    return false;
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
    }
    public bool SaveXML()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
            StreamWriter writer = new StreamWriter("mem.xml");
            serializer.Serialize(writer, Mem);
            writer.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool LoadXML()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
            TextReader reader = new StreamReader("mem.xml");
            Mem = (List<int>)serializer.Deserialize(reader);
            reader.Close();
            if (Mem.Count > 0)
            {
                inputnum = false;//вводим операцию
                lastmem = Mem.Count - 1;
                return true;
            }
            else
            {
                inputnum = true;//вводим число
                lastmem = -1;
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    public bool SaveJSON()
    {
        try
        {
            string json = JsonSerializer.Serialize(Mem);
            File.WriteAllText("mem.json", json);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool LoadJSON()
    {
        try
        {
            string data = File.ReadAllText("mem.json");
            Mem = JsonSerializer.Deserialize<List<int>>(data);
            if (Mem.Count > 0)
            {
                inputnum = false;//вводим операцию
                lastmem = Mem.Count - 1;
                return true;
            }
            else
            {
                inputnum = true;//вводим число
                lastmem = -1;
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public bool Calculate(String S)// S строка введённая пользователем
    {
        if (S.Length == 0)
        {
            return false;
        }
        if (S == "ss")
        {
            if (!SaveSQLLite(ref S))
            {
                return false;
            }
        }
        else if (S == "ls")
        {
            if (!LoadSQLLite(ref S))
            {
                return false;
            }

        }
        else if (S == "sj")
        {
            if (Mem.Count > 0)
            {
                if (!SaveJSON())
                {
                    return false;
                }
            }
        }
        else if (S == "lj")
        {
            if (!LoadJSON())
            {
                return false;
            }
        }
        else if (S == "sx")
        {
            if (Mem.Count > 0)
            {
                if (!SaveXML())
                {
                    return false;
                }
            }
        }
        else if (S == "lx")
        {
            if (!LoadXML())
            {
                return false;
            }
        }
        else if (S == "+" || S == "-" || S == "/" || S == "*")
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
            Mem[id - 1] = value;
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
                lastmem = Mem.Count - 1;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

}
