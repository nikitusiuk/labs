using System;
namespace lab1._2
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            // Создание и заполнение матрицы случайными числами
            int[,] matrix = MakeNew(5, 5, -10, 10);

            Console.WriteLine("Исходная матрица:");
            Print(matrix);

            // Вычисление количества столбцов без нулевых элементов
            int columnCount = NotZero(matrix);
            Console.WriteLine($"Количество столбцов без нулевых элементов: {columnCount}");

            // Перестановка строк в соответствии с характеристиками
            Sorting(matrix);

            Console.WriteLine("Матрица после перестановки строк:");
            Print(matrix);
        }

        // Метод для генерации матрицы случайных чисел
        private static int[,] MakeNew(int rows, int columns, int minValue, int maxValue)
        {
            Random random = new Random();
            int[,] matrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = random.Next(minValue, maxValue + 1);
                }
            }

            return matrix;
        }

        // Метод для печати матрицы
        static void Print(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Метод для подсчета количества столбцов без нулевых элементов
        static int NotZero(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            int columnCount = 0;

            for (int j = 0; j < columns; j++)
            {
                bool hasZero = false;

                for (int i = 0; i < rows; i++)
                {
                    if (matrix[i, j] == 0)
                    {
                        hasZero = true;
                        break;
                    }
                }

                if (!hasZero)
                {
                    columnCount++;
                }
            }

            return columnCount;
        }

        // Метод для расчета характеристик и перестановки строк
        static void Sorting(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            int[] characteristics = new int[rows];

            // Расчет характеристик
            for (int i = 0; i < rows; i++)
            {
                int characteristic = 0;

                for (int j = 0; j < columns; j++)
                {
                    int element = matrix[i, j];

                    if (element > 0 && element % 2 == 0)
                    {
                        characteristic += element;
                    }
                }

                characteristics[i] = characteristic;
            }

            // Перестановка строк
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < rows - i - 1; j++)
                {
                    if (characteristics[j] > characteristics[j + 1])
                    {
                        SwapRows(matrix, j, j + 1);
                        SwapValues(characteristics, j, j + 1);
                    }
                }
            }
        }

        // Метод для обмена строк матрицы
        static void SwapRows(int[,] matrix, int rowIndex1, int rowIndex2)
        {
            int columns = matrix.GetLength(1);

            for (int j = 0; j < columns; j++)
            {
                int temp = matrix[rowIndex1, j];
                matrix[rowIndex1, j] = matrix[rowIndex2, j];
                matrix[rowIndex2, j] = temp;
            }
        }

        // Метод для обмена значений в массиве
        static void SwapValues(int[] array, int index1, int index2)
        {
            int temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }
}
