/* В одномерном массиве, состоящем из N вещественных элементов, вычислить:
сумму положительных элементов массива
произведение элементов массива, расположенных между максимальным по модулю и минимальным по модулю элементами.
Упорядочить элементы массива по убыванию.
*/

using System;

namespace lab1
{
}

class Program
{
    static void Main()
    {
        // Задаем размерность массива
        int N = 10;

        // Создаем массив и заполняем его случайными числами
        double[] array = new double[N];
        Random random = new Random();

        for (int i = 0; i < N; i++)
        {
            array[i] = random.NextDouble() * random.Next(-100, 100);
        }

        // Выводим исходный массив
        Console.WriteLine("Исходный массив:");
        PrintArray(array);

        // Считаем сумму положительных элементов массива
        double sumPositive = 0;
        foreach (double element in array)
        {
            if (element > 0)
            {
                sumPositive += element;
            }
        }

        // Находим максимальный и минимальный по модулю элементы
        double maxAbs = array[0];
        double minAbs = array[0];

        for (int i = 1; i < N; i++)
        {
            if (Math.Abs(array[i]) > Math.Abs(maxAbs))
            {
                maxAbs = array[i];
            }
            
            if (Math.Abs(array[i]) < Math.Abs(minAbs))
            {
                minAbs = array[i];
            }
        }

        // Находим индексы максимального и минимального элементов
        int maxIndex = Array.IndexOf(array, maxAbs);
        int minIndex = Array.IndexOf(array, minAbs);

        // Вычисляем произведение элементов, расположенных между максимальным и минимальным по модулю элементами
        double ans = 1;

        if (maxIndex < minIndex)
        {
            for (int i = maxIndex + 1; i < minIndex; i++)
            {
                ans *= array[i];
            }
        }
        else
        {
            for (int i = minIndex + 1; i < maxIndex; i++)
            {
                ans *= array[i];
            }
        }

        // Упорядочиваем элементы массива по убыванию
        Array.Sort(array);
        Array.Reverse(array);

        // Выводим результаты
        Console.WriteLine("Сумма положительных элементов массива: " + sumPositive);
        Console.WriteLine("Произведение элементов между максимальным и минимальным по модулю: " + ans);
        Console.WriteLine("Упорядоченный массив по убыванию:");
        PrintArray(array);
    }

    static void PrintArray(double[] array)
    {
        foreach (double element in array)
        {
            Console.Write(element + " ");
        }

        Console.WriteLine();
    }
}






        
        
    
