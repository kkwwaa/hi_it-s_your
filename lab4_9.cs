using System;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;



namespace lab4
{
    internal class Program
    {
        static int BinSearch(int[] array, int num)
        {
            int l = 0, r = array.Length, mid = 0, count = 0;
            while (l <= r)
            {
                mid = (l + r) / 2;
                if (array[mid] == num) return count + 1;
                else if (array[mid] < num) l = mid + 1;
                else r = mid - 1;
                count += 2;
            }
            return -1;
        }
        static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);
                QuickSort(array, left, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, right);
            }
        }
        static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }

            Swap(array, i + 1, right);
            return i + 1;
        }
        static void Swap(int[] array, int a, int b)
        {
            int temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }
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
                Console.WriteLine("8. Бинарный поиск в массиве с использованием быстрой сортировки");
                Console.WriteLine("9. Выход");
                Console.Write("Выберите пункт: ");
                #endregion 

                int choice = Convert(Console.ReadLine(), 1, 9);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Выберите способ формирования массива:");
                        Console.WriteLine("1. Случайные числа");
                        Console.WriteLine("2. Ввод с клавиатуры");
                        int subChoice = Convert(Console.ReadLine(), 1,2);

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
                                if (x <= avg)
                                {
                                    count++;
                                }
                            }

                            // Создание нового массива
                            int[] newArray = new int[count];
                            int index = 0;
                            foreach (int x in array)
                            {
                                if (x <= avg)
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
                            subChoice = Convert(Console.ReadLine(), 1,2);

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
                        // Проверяем, не пустой ли массив, так как операция сдвига невозможна для пустого массива.
                        if (array.Length != 0)
                        {
                            // Получаем длину массива.
                            n = array.Length;
                    
                            // Запрашиваем у пользователя значение сдвига M.
                            Console.WriteLine("Введите число M");
                    
                            // Читаем значение, введенное пользователем, и приводим его к числу. 
                            // Используем метод Convert для безопасной обработки ввода.
                            int m = Convert(Console.ReadLine(), 0, int.MaxValue);
                    
                            // Приводим значение M к диапазону индексов массива (остаток от деления на длину массива).
                            // Это важно, чтобы избежать лишних циклов, если M больше длины массива.
                            m %= n;
                    
                            // Создаем новый массив, куда будет записан результат сдвига.
                            int[] arr_sd = new int[n];
                    
                            // Выполняем циклический сдвиг:
                            // Каждый элемент нового массива берем из старого массива по индексу, который определяется формулой:
                            // `(n + i - m) % n`.
                            // Формула работает так:
                            // - (i - m): для текущего элемента i смещаем индекс на M влево.
                            // - (n + ...): прибавляем длину массива, чтобы избежать отрицательных индексов.
                            // - % n: ограничиваем индекс в пределах длины массива.
                            for (int i = 0; i < n; i++)
                            {
                                arr_sd[i] = array[(n + i - m) % n];
                            }
                    
                            // Выводим результат на экран: новый массив после сдвига.
                            foreach (int x in arr_sd)
                            {
                                Console.Write($"{x} ");
                            }
                    
                            // Перезаписываем исходный массив на сдвинутый.
                            array = arr_sd;
                        }
                        else
                        {
                            // Если массив не задан, выводим предупреждение.
                            Console.WriteLine("Массив не задан.");
                        }
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
                                    Console.WriteLine($"Первый четный элемент в массиве {array[i]} находится на позиции {i + 1}");
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
                        if (array.Length != 0)
                        {
                            n = array.Length;
                            Console.WriteLine("Введите число X");
                            int num = Convert(Console.ReadLine(), int.MinValue, int.MaxValue);
                            int[] array_srt = array;
                            QuickSort(array_srt, 0, n - 1);

                            int count = BinSearch(array_srt, num);
                            if (count > -1) Console.WriteLine($"Элемент {num} найден, было проведено {count} сравнений");
                            else Console.WriteLine($"Элемент {num} в массиве не найден");
                        }
                        else
                            Console.WriteLine("Массив не задан.");
                        break;

                    case 9:
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
