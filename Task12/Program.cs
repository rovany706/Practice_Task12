using System;
using System.Collections.Generic;
using System.Linq;
using InputHelper;

namespace Task12
{
    class Program
    {
        private static Random rand = new Random(); //ДСЧ

        private static void PrintArray(int[] array)
        {
            foreach (int num in array)
                Console.Write($"{num} ");
            Console.WriteLine();
        }

        private static int[] ShellSort(int[] array, out int comparisions, out int moves)
        {
            int step = array.Length / 2;
            int j;
            moves = comparisions = 0;

            while (step > 0)
            {
                for (int i = 0; i < array.Length - step; i++)
                {
                    j = i;
                    while (j >= 0)
                    {
                        comparisions++;
                        if (array[j] > array[j + step])
                        {
                            int temp = array[j];
                            array[j] = array[j + step];
                            moves++;
                            array[j + step] = temp;
                            moves++;
                            j -= step;
                        }
                        else
                            break;
                    }
                }

                step /= 2;
            }

            return array;
        }

        private static int[] BucketSort(int[] array, out int comparisions, out int moves)
        {
            moves = comparisions = 0;

            if (array.Length == 1)
                return array;

            // Поиск элементов с максимальным и минимальным значениями
            int maxNumber = array[0];
            int minNumber = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                comparisions++;
                if (array[i] > maxNumber)
                    maxNumber = array[i];

                comparisions++;
                if (array[i] < minNumber)
                    minNumber = array[i];
            }

            //Массив карманов
            List<int>[] bucket = new List<int>[maxNumber - minNumber + 1];

            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }

            // Занесение значений в пакеты
            for (int i = 0; i < array.Length; i++)
            {
                moves++;
                bucket[array[i] - minNumber].Add(array[i]);
            }

            // Восстановление элементов в исходный массив
            // из карманов в порядке возрастания значений
            int position = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        moves++;
                        array[position] = bucket[i][j];
                        position++;
                    }
                }
            }

            return array;
        }

        static void Main()
        {
            Console.WriteLine("Задача 12\n=================");
            Console.WriteLine("Условие задачи:\nВыполнить сравнение двух предложенных методов сортировки одномерных массивов, \n" +
                              "содержащих n элементов, по количеству пересылок и сравнений.\n" +
                              "Провести анализ методов сортировки для трех массивов:\n" +
                              "упорядоченного по возрастанию, упорядоченного по убыванию и неупорядоченного.\n\n" +
                              "Используемые методы сортировки:\n" +
                              "Сортировка Шелла\n" +
                              "Блочная (корзинная) сортировка\n" +
                              "=================");

            int size = Input.ReadInt("Введите размер массивов: ", min: 1);

            int[] defaultArray = new int[size]; // неупорядоченный массив
            int[] sortedAscArray = new int[size]; //массив, упорядоченный по возрастанию
            int[] sortedDescArray = new int[size]; //массив, упорядоченный по убыванию

            //генерация массивов
            for (int i = 0; i < size; i++)
            {
                defaultArray[i] = rand.Next(-100, 101);
                sortedAscArray[i] = rand.Next(-100, 101);
                sortedDescArray[i] = rand.Next(-100, 101);
            }

            //сортировка по возрастанию и убыванию
            sortedAscArray = sortedAscArray.OrderBy(num => num).ToArray();
            sortedDescArray = sortedDescArray.OrderByDescending(num => num).ToArray();

            //вывод массивов
            Console.WriteLine("Создано 3 массива, которые заполнены целыми числами в диапазоне [-100; 100]");
            Console.WriteLine("Неупорядоченный массив:");
            PrintArray(defaultArray);

            Console.WriteLine("Массив, упорядоченный по возрастанию:");
            PrintArray(sortedAscArray);

            Console.WriteLine("Массив, упорядоченный по убыванию:");
            PrintArray(sortedDescArray);

            Console.WriteLine("Нажмите Enter, чтобы начать сортировку Шэлла");
            Console.ReadLine();

            #region ShellSort

            int comparisions, moves;
            Console.WriteLine("\nСортировка Шелла");
            int[] tempArray;//переменная для хранения отсортированных массивов

            tempArray = ShellSort(defaultArray, out comparisions, out moves);
            Console.WriteLine("Отсортированный неупорядоченный массив:");
            PrintArray(tempArray);
            Console.WriteLine($"Количество сравнений: {comparisions}, количество перемещений: {moves}");

            tempArray = ShellSort(sortedAscArray, out comparisions, out moves);
            Console.WriteLine("Отсортированный, упорядоченный по возрастанию массив:");
            PrintArray(tempArray);
            Console.WriteLine($"Количество сравнений: {comparisions}, количество перемещений: {moves}");

            tempArray = ShellSort(sortedDescArray, out comparisions, out moves);
            Console.WriteLine("Отсортированный, упорядоченный по убыванию массив:");
            PrintArray(tempArray);
            Console.WriteLine($"Количество сравнений: {comparisions}, количество перемещений: {moves}");


            Console.WriteLine("\nНажмите Enter, чтобы начать блочную сортировку");
            Console.ReadLine();

            #endregion

            #region BucketSort
            Console.WriteLine("\nБлочная сортировка");

            tempArray = BucketSort(defaultArray, out comparisions, out moves);
            Console.WriteLine("Отсортированный неупорядоченный массив:");
            PrintArray(tempArray);
            Console.WriteLine($"Количество сравнений: {comparisions}, количество перемещений: {moves}");

            tempArray = BucketSort(sortedAscArray, out comparisions, out moves);
            Console.WriteLine("Отсортированный, упорядоченный по возрастанию массив:");
            PrintArray(tempArray);
            Console.WriteLine($"Количество сравнений: {comparisions}, количество перемещений: {moves}");

            tempArray = BucketSort(sortedDescArray, out comparisions, out moves);
            Console.WriteLine("Отсортированный, упорядоченный по убыванию массив:");
            PrintArray(tempArray);
            Console.WriteLine($"Количество сравнений: {comparisions}, количество перемещений: {moves}");
            #endregion
        }
    }
}
