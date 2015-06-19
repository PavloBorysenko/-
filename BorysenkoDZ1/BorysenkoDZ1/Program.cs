using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorysenkoDZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            //*********** Задача №1 ***************

            int sum = 0;
            int[] masA = new int[10] { 9, 2, 7, 35, 5, 6, 7, 5, 40, 10 };
            int[] masB = new int[10];
            for (int i = 0; i < masA.Length; i++)
            {
                sum = 0;
                for (int j = i; j < masA.Length; j++)
                {

                    sum += masA[j];
                }
                masB[i] = sum / (masA.Length - i);
            }
            Console.WriteLine("Исходный массив: ");
            foreach (int i in masA)
            {
                Console.Write(i + "\t");
            }
            Console.WriteLine("Результат: ");
            foreach (int i in masB)
            {
                Console.Write(i + "\t");
            }

            //**********************Конец  1 задачи*********************

            //*********** *************Задача №2 ***************

            //int N = 10;
            //int[] masA = new int[N];
            //int plus = 0, minus = 0;
            //for (int i = 0; i < masA.Length; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        masA[i] = -(i + 1);
            //    }
            //    else
            //    {
            //        masA[i] = (i + 1);

            //    }
            //}

            //foreach (int pos in masA)
            //{
            //    if (pos > 0)
            //    {
            //        plus++;
            //    }
            //    else
            //    {
            //        minus++;
            //    }
            //}
            //Console.WriteLine("Исходный массив:    " );
            //foreach (int i in masA)
            //{
            //    Console.Write(i + "\t");

            //}
            //Console.WriteLine();
            //int[] masB = new int[plus];
            //int[] masC = new int[minus];
            ////Console.WriteLine(masB.Length);
            ////Console.WriteLine(masC.Length);
            //int b = 0, c = 0;
            //for (int i = 0; i < masA.Length; i++)
            //{
            //    if (masA[i] > 0)
            //    {

            //        masB[b++] = masA[i];
            //    }
            //    else
            //    {

            //        masC[c++] = masA[i];
            //    }
            //}
            //Console.WriteLine("Длина массива В: " + masB.Length);
            //foreach (int i in masB)
            //{
            //    Console.Write(i + "\t");

            //}
            //Console.WriteLine();
            //Console.WriteLine("Длина массива С: " + masC.Length);
            //foreach (int i in masC)
            //{
            //    Console.Write(i + "\t");
            //}
            //Console.WriteLine();

            //**********************Конец  2 задачи*********************


            //*********** *************Задача №3 ***************
            //string[] mas = { "fred", "cat", "led", "lizy", "cat", "led", "son", "pop", "pop", "zero", "fred" };
            //foreach (string i in mas)
            //{
            //    Console.Write(i + " | ");

            //}
            //Console.WriteLine();
            //int count = 0, temp = 0;
            //for (int i = 0; i < mas.Length; i++)
            //{
            //    for (int j = 0; j < mas.Length; j++)
            //    {
            //        if (mas[i] == mas[j] && j != i)
            //        {
            //            temp++;
            //        }
            //    }
            //    if (temp == 0)
            //    {
            //        count++;
            //    }
            //    temp = 0;
            //}
            //Console.WriteLine("Найденно " + count + " различных элементов!");

            //**********************Конец  3 задачи*********************

            //*********** *************Задача №4 **********************
            //int M = 6, N = 5, tI = 0, tJ = 0;
            //double sr = 0, sum = 0, min = 0;
            //Random rand = new Random();


            //double[,] mas = new double[M, N];
            //for (int i = 0; i < M; i++)
            //{
            //    for (int j = 0; j < N; j++)
            //    {
            //        mas[i, j] = rand.Next(0, 1000);
            //    }
            //}

            //for (int i = 0; i < M; i++)
            //{
            //    Console.WriteLine();
            //    for (int j = 0; j < N; j++)
            //    {
            //        Console.Write(mas[i, j] + "\t");
            //        sum += mas[i, j];
            //    }
            //}
            //sr = sum / (N * M);
            //min = Math.Abs(sr - mas[0, 0]);
            //for (int i = 0; i < M; i++)
            //{
            //    for (int j = 0; j < N; j++)
            //    {
            //        if (min > Math.Abs(sr - mas[i, j]))
            //        {
            //            min = Math.Abs(sr - mas[i, j]);
            //            tI = i;
            //            tJ = j;
            //        }

            //    }
            //}
            //Console.WriteLine();
            //Console.WriteLine("Среднее число " + sr);
            //Console.WriteLine("Ближайший элемент это " + mas[tI, tJ] + " строка: " + (tI + 1) + " Столбец:  " + (tJ + 1));


            //**********************Конец  4 задачи*********************

            //*********** *************Задача №5 **********************
            //string str = "    ДАНА СТРОКА, СОСТОЯЩАЯ ИЗ РУССКИХ СЛОВ, \n НАБРАНЫХ ЗАГЛАВНЫМИ БУКВАМИ И РАЗДЕЛЕННЫХ ПРОБЕЛАМИ";
            //int count = 0;
            //Console.WriteLine("Исходная строка: \n"+str);
            //Console.WriteLine();
            //char[] charSeparators = new char[] { ' ' };
            //string[] result = str.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
            //Console.Write("Введите символ для поиска: ");
            //char T = (char)Console.Read();
            //string TT = "";
            //TT = char.ToString(T);
            //string TU = TT.ToUpper();

            //foreach (string item in result)
            //{
            //    if (item.IndexOf(TU) >= 0)
            //    {
            //        count++;
            //    }
            //}
            //Console.WriteLine("Всего слов:  " + result.Length);
            //Console.WriteLine("Слов с данным символом: " + count);


            //**********************Конец  5 задачи*********************


            //*********** *************Задача №6 и №7**********************


            //string cod = "Это очень секретное послание! а это - поверка пределов А а Я я";
            //string rez="",rezN="";
            //int K = 0;
            
            //Console.Write("Введите коэффициент шифрования ");
            //K=Console.Read();
           
            //Console.WriteLine();
            //foreach (char ch in cod) {
            //int shifr =(int)ch;
            //if (shifr >= 1040 && shifr <= 1103) {
            //    shifr += K-48;
            //    if (shifr > 1103) {
            //        shifr -= 64;
            //    }
            //} 
                
            //    rez=rez+(char)shifr;
            //}
            //Console.Write("Начальная строка: ");
            //Console.WriteLine(cod);
            //Console.Write("       Результат: ");
            //Console.WriteLine(rez);


            //foreach (char ch in rez) {
            //    int shifr = (int)ch;
            //    if (shifr >= 1040 && shifr <= 1103)
            //    {
            //        shifr -= K - 48;
            //        if (shifr < 1040)
            //        {
            //            shifr += 64;
            //        }
            //    }

            //    rezN = rezN + (char)shifr;
            //}
            //Console.WriteLine();
            //Console.Write("     Расшифровка:");
            //Console.WriteLine(rezN);
            //**********************Конец  6 и 7 задачи*********************
        }

    }
}