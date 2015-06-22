using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KlasCScharp1
{
    class Program
    {  
        static int count=1;
       
        
        static void Main(string[] args)
    {  
            Univer U = new Univer();
            for(int i=0; i<4;i++){
             string N;
             int S;
             Console.Write("Ведите имя: ");
             N=Console.ReadLine();
             Console.WriteLine();
             Console.Write("Введите средний бал: ");
             S = int.Parse(Console.ReadLine());
             U.AddS(N, S);
             Console.WriteLine();
             Console.WriteLine("***********************");
            }
            U.Print();
            Console.WriteLine("номер ID для удаления: ");
            int I=int.Parse(Console.ReadLine());
            U.Del(I);
            Console.WriteLine();
            Console.WriteLine("Результат******************");
            Console.WriteLine();
              U.Print();
              Console.WriteLine();
              Console.WriteLine("Проверка на переполнение ******************");
              Console.WriteLine();
              for (int i = 0; i < 2; i++)
              {
                  string N;
                  int S;
                  Console.Write("Ведите имя: ");
                  N = Console.ReadLine();
                  Console.WriteLine();
                  Console.Write("Введите средний бал: ");
                  S = int.Parse(Console.ReadLine());
                  U.AddS(N, S);
                  Console.WriteLine();
                  Console.WriteLine("***********************");
                 
              }
              Console.WriteLine();
              Console.WriteLine("Результат******************");
              Console.WriteLine();
              U.Print();


        }
        public class Univer {
            public Student[] listS = new Student[4];

            public void AddS( string N, int sr) {
                int c = 0;
                Student S = new Student();
                Zap(out S, N, sr);
                for (int i = 0; i < listS.Length; i++) {
                    if (listS[i] == null) {
                        c++;
                        listS[i] = S;
                        break;
                    }
                   
                }
                if (c == 0) {
                    Console.WriteLine();
                    Console.WriteLine("****************************\n********Група заполнена!**********\n*********************************");
                    Console.WriteLine();
                }
            }
            public void Print() {
                int c = 0;
           
                foreach (Student x in listS) {
                    if (x != null)
                    {
                        Console.WriteLine("_________________________");
                        Console.WriteLine("Имя: " + x.name);
                        Console.WriteLine("Ид: " + x.id);
                        Console.WriteLine("Средний бал: " + x.SrBal);
                        Console.WriteLine("Дата: " + x.dt);
                        Console.WriteLine("_________________________");
                        c++;
                    }
                    
                }
                if (c == 0) {
                    Console.WriteLine("Нет Записей!!!");
                }
            
            }
            public void Del(int ID) {
                for (int i = 0; i < listS.Length; i++) {
                    if (listS[i] != null) {
                        if (listS[i].id == ID) {
                            listS[i] = null;
                            break;
                        }
                    }
                }
            }

        }


        public class Student
        {
            public int id;
            public string name;
            public int SrBal;
            public DateTime dt;

        }
        static void inecial(out int id)
        {
            id = count++;
        }
        static void Zap(out Student S, string N, int sr) {

            Student S1 = new Student();
            inecial(out S1.id);
            S1.name = N;
            S1.SrBal = sr;
            S1.dt = new DateTime(2015, 06, 21);
            S = S1;
        }
           

    }

}
