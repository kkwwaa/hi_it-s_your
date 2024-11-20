using System;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;



namespace lab4
{
    internal class Program
    {
        public static int Convert(string input, int l, int r)
        {
            int n;
            bool isConvert = Int32.TryParse(input, out n) && r >= n && n >= l;
            while (isConvert == false)
            {
                Console.WriteLine("Попробуйте еще раз");
                input = Console.ReadLine();
                isConvert = Int32.TryParse(input, out n) && r >= n && n >= l;
            }
            return n;
        }

        static void Main(string[] args)
        {
            int[] array = [];
            bool exit = false;
            int n = 0;

            while (!exit)
            {
                #region menu
                Console.WriteLine("Главное меню:");
                Console.WriteLine("1. Сформировать массив");
                Console.WriteLine("2. Распечатать массив");
                Console.WriteLine("3. Удалить все элементы больше среднего арифметического элементов массива");
                Console.WriteLine("4. Добавить К элементов в начало массива");
                Console.WriteLine("5. Сдвинуть циклически на M элементов вправо");
                Console.WriteLine("6. Поиск первого четного элемента в массиве");
                Console.WriteLine("7. Выполнить сортировку простым включением для массива");
                Console.WriteLine("8. Выход");
                Console.Write("Выберите пункт: ");
                #endregion 

                int choice = Convert(Console.ReadLine(), 1, 9);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Выберите способ формирования массива:");
                        Console.WriteLine("1. Случайные числа");
                        Console.WriteLine("2. Ввод с клавиатуры");
                        int subChoice = int.Parse(Console.ReadLine());

                        switch (subChoice)
                        {
                            case 1:
                                Console.Write("Введите размер массива: ");
                                n = Convert(Console.ReadLine(), 0, int.MaxValue);
                                array = new int[n];
                                Random rand = new Random();
                                for (int i = 0; i < n; i++)
                                    array[i] = rand.Next(-100, 100);
                                break;

                            case 2:
                                Console.Write("Введите размер массива: ");
                                n = Convert(Console.ReadLine(), 0, int.MaxValue);
                                array = new int[n];
                                for (int i = 0; i < n; i++)
                                {
                                    Console.WriteLine($"Введите {i + 1} элемент массива:");
                                    array[i] = Convert(Console.ReadLine(), int.MinValue, int.MaxValue);
                                }
                                break;

                            default:
                                Console.WriteLine("Неверный выбор");
                                break;
                        }
                        break;

                    case 2:
                        if (array.Length != 0)
                        {
                            Console.WriteLine("Элементы массива:");
                            foreach (var item in array)
                                Console.Write(item + " ");
                            Console.WriteLine();
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 3:
                        if (array.Length != 0)
                        {
                            // Вычисление среднего арифметического
                            double avg = 0;
                            foreach (int x in array)
                            {
                                avg += x;
                            }
                            avg /= array.Length;

                            // Подсчёт количества элементов, меньших среднего
                            int count = 0;
                            foreach (int x in array)
                            {
                                if (x < avg)
                                {
                                    count++;
                                }
                            }

                            // Создание нового массива
                            int[] newArray = new int[count];
                            int index = 0;
                            foreach (int x in array)
                            {
                                if (x < avg)
                                {
                                    newArray[index++] = x;
                                }
                            }

                            // Вывод нового массива
                            Console.WriteLine("Новый массив из элементов, меньших среднего арифметического:");
                            foreach (int x in newArray)
                            {
                                Console.Write($"{x} ");
                            }

                            array = newArray; // нужно заменить исходный массив
                        }
                        else
                        {
                            Console.WriteLine("Массив не задан.");
                        }
                        break;

                    case 4:
                        if (true)
                        {
                            n = array.Length;
                            Console.WriteLine("Введите число K");
                            int k = Convert(Console.ReadLine(), 0, int.MaxValue);
                            int[] arr_add = new int[n + k];

                            Console.WriteLine("1. Случайные числа");
                            Console.WriteLine("2. Ввод с клавиатуры");
                            subChoice = int.Parse(Console.ReadLine());

                            switch (subChoice)
                            {
                                case 2:
                                    for (int i = 0; i < n + k; i++)
                                    {
                                        if (i < k) 
                                        {
                                            Console.WriteLine($"Введите элемент для добавления в массив:");
                                            arr_add[i] = Convert(Console.ReadLine(), int.MinValue, int.MaxValue);
                                        }
                                        else arr_add[i] = array[i - k];
                                    }
                                    foreach (int x in arr_add) { Console.Write($"{x} "); }
                                    array = arr_add;
                                    break;

                                case 1:
                                    Random rand = new Random();
                                    for (int i = 0; i < n + k; i++)
                                    {
                                        if (i < k) arr_add[i] = rand.Next(-100, 100); 
                                        else arr_add[i] = array[i - k];
                                    }
                                    foreach (int x in arr_add) { Console.Write($"{x} "); }
                                    array = arr_add;
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 5:
                        if (array.Length != 0)
                        {
                            n = array.Length;
                            Console.WriteLine("Введите число M");
                            int m = Convert(Console.ReadLine(), 0, int.MaxValue);
                            int[] arr_sd = new int[n];

                            for (int i = 0; i < n; i++)
                            {
                                arr_sd[i] = array[(n - m + i) % n];
                            }
                            foreach (int x in arr_sd) { Console.Write($"{x} "); }
                            array = arr_sd;
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 6:
                        int cnt = 0;
                        if (array.Length != 0)
                        {
                            n = array.Length;
                            for (int i = 0; i < n; i++)
                            {
                                if (array[i] %2== 0)
                                {
                                    Console.WriteLine($"Первый четный элемент в массиве {array[i]} находится на позиции {i + 1}, было произведено {i+1} сравнений");
                                    cnt++;
                                    break;
                                }
                            }
                            if (cnt == 0) Console.WriteLine("Четных элементов не обнаружено");
                        }
                        else Console.WriteLine("Массив не задан.");
                        break;

                    case 7:
                        if (array.Length != 0)
                        {
                            n = array.Length;

                            for (int i = 1; i < n; i++)
                            {
                                int key = array[i];
                                int j = i - 1;

                                // Сдвигаем элементы массива, которые больше ключа, на одну позицию вперёд
                                while (j >= 0 && array[j] > key)
                                {
                                    array[j + 1] = array[j];
                                    j--;
                                }

                                // Вставляем ключ на своё место
                                array[j + 1] = key;
                            }

                            // Вывод отсортированного массива
                            foreach (int x in array)
                            {
                                Console.Write($"{x} ");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Массив не задан.");
                        }
                        break;

                    case 8:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный выбор");
                        break;

                }
                Console.WriteLine();
            }
        }
    }
}
