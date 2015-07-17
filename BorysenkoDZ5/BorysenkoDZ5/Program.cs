using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorysenkoDZ5
{
    class Program
    {
          
        static void Main(string[] args)
        {
        int castle = 100; 

        Trebusche Os1 = new Trebusche(25, 200, 50);
        Balista Os2 = new Balista(10, 100, 70);
        Taran Os3 = new Taran(5,10,100);
       
        string[] m = new string[] { "Требуше", "Балиста", "Таран" };
        string[] m1 = new string[] { "Выстрел", "Перезарядить", "Вперед" ,"Назад"};


        string T="Выбирайте орудие!"  ;

        var pos = 0;
            while (true)
            {
                T = "Выбирайте орудие!";

                Menu(pos, T, m, castle);
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                    pos = pos <= 0 ? 2 : --pos;
                else if (key.Key == ConsoleKey.DownArrow)
                    pos = pos >= 2 ? 0 : ++pos;
               
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    
                    int pos1 = 0;
                    while (true)
                    {
                        T = Par(Os1, Os2, Os3, pos).Show();
                        Menu(pos1, T, m1, castle);
                        if (castle <= 0)
                        {
                            Console.WriteLine("Победа!\nКрепость разрушена!");
                            Console.ReadKey();
                            break;
                        }
                        int count = m1.Length;
                        var key1 = Console.ReadKey();
                        if (key1.Key == ConsoleKey.UpArrow)
                            pos1 = pos1 <= 0 ? count-1 : --pos1;
                        else if (key1.Key == ConsoleKey.DownArrow)
                            pos1 = pos1 >= count-1 ? 0 : ++pos1;

                        else if (key1.Key == ConsoleKey.Enter)
                        {
                            switch (pos1) { 
                                case 0:
                                    switch (pos)
                                    {
                                        case 0:
                                            castle = castle - (Os1 - 1);

                                            break;
                                        case 1:
                                            castle = castle - (Os2 - 1);

                                            break;
                                        case 2:
                                            castle = castle - (Os3 - 1);
                                            break;
                                    } 
                                    break;
                                case 1:
                                    switch (pos)
                                    {
                                        case 0:
                                            Os1=(Os1 + 1);

                                            break;
                                        case 1:
                                            Os2 =(Os2 + 1);

                                            break;
                                        case 2:
                                            Os3 = (Os3 + 1);
                                            break;
                                    } 
                                    break;

                                case 2:
                                    switch (pos)
                                    {
                                        case 0:
                                            Os1 = (Os1 > 1);

                                            break;
                                        case 1:
                                            Os2 = (Os2 >1);

                                            break;
                                        case 2:
                                            Os3 = (Os3 >1);
                                            break;
                                    } 
                                    break;
                                case 3:
                                    switch (pos)
                                    {
                                        case 0:
                                            Os1 = (Os1 < 1);

                                            break;
                                        case 1:
                                            Os2 = (Os2 < 1);

                                            break;
                                        case 2:
                                            Os3 = (Os3 < 1);
                                            break;
                                    } 
                                    break;
                            }


                        }
                        else if (key1.Key == ConsoleKey.Escape)
                        { break; }
                        

                        Console.Clear();
                    }
                   
                }
                else if (key.Key == ConsoleKey.Escape)
                { break; }
                if (castle <= 0)
                {
                
                    break;
                }
             
                Console.Clear();
            }



        }
      
     
        public static Osad Par(Trebusche t,Balista b, Taran ta, int i) {
            if (i == 0)
            {
                return t;
            }
            else if (i == 1)
            {
                return b;
            }
            else return ta;
            
        }
        
        public abstract class Osad {
        protected int pow;
        protected int dest;
        protected int toch;
       
        protected Osad(int p, int d, int t) {
            pow = p;
            dest = d;
            toch = t;

        }
      

        public virtual string Show()
        {
            string temp = "\nХарактеристики:\n Cила " + pow + "\n Дистанция " + dest + " м\n Точность " + toch+"%";
            return temp;
        }
        }
        public class Trebusche : Osad {
        public bool xod;
        public bool per;
        public bool redy;

            public Trebusche(int p, int d, int t) : base(p, d, t) {
                xod = false;
                per = true;
                redy = true;
            }
            public static Trebusche operator +(Trebusche T, int x){
                
                T.redy = true;
                Console.WriteLine("Орудие заряжено!");
                Console.ReadKey();
                return T;
            }
            public static int operator -(Trebusche T, int x)
            {
                Random rand = new Random();
                int uron=0;
            if (!T.redy) {
                Console.WriteLine("Орудие не заряжено");
            }
            else if (T.toch > rand.Next(0, 100))
            {
                Console.WriteLine("Попадание!");
                uron = T.pow;
                T.redy = false;
            }
            else
            {
                Console.WriteLine("Промах!");
                T.redy = false;
            }
            Console.ReadKey();
            return uron;
            }
            public static Trebusche operator >(Trebusche T,int i){
                Console.WriteLine("Орудие не может двигаться!");
                Console.ReadKey();
                return T;
            }
            public static Trebusche operator <(Trebusche T, int i)
            {
                Console.WriteLine("Орудие не может двигаться!");
                Console.ReadKey();
                return T;
            }

            public override string Show() {
                string temp = "Требуше \n Не может передвигаться, но очень мощное" + base.Show();
                return temp;
            }
        }
        public class Balista : Osad
        {
          public  bool xod;
          public  bool per;
          public  bool redy;

            public Balista(int p, int d, int t)
                : base(p, d, t)
            {
                xod = true;
                per = true;
                redy = true;
            }
            public static Balista operator +(Balista T,int i)
            {
                T.redy = true;
                Console.WriteLine("Орудие заряжено!");
                Console.ReadKey();
                return T;
            }
            public static int operator -(Balista T, int x)
            {
                Random rand = new Random();
                int uron = 0;
                if (!T.redy)
                {
                    Console.WriteLine("Орудие не заряжено");
                    Console.ReadKey();
                    return uron;
                }
                else if (T.toch > rand.Next(0, 100))
                {
                    T.redy = false;
                    Console.WriteLine("Попадание!");
                    uron = T.pow;

                }
                else
                {
                    T.redy = false;
                    Console.WriteLine("Промах!");
                }
                Console.ReadKey();
                return uron;
            }
            public static Balista operator >(Balista T, int i)
            {
               
                    Console.WriteLine("Маневр на сближение");
                    if (T.dest <= 30)
                    {
                        Console.WriteLine("Максимально близкая дистанция");
                       
                    }
                    else { T.dest -= 10;
                    T.toch += 3 ;
                    }
             
                Console.ReadKey();
                return T;
            }
            public static Balista operator <(Balista T, int i)
            {
               
                    Console.WriteLine("Маневр на удаление");
                    if (T.dest >= 100)
                    {
                        Console.WriteLine("Максимально дальняя дистанция");

                    }
                    else {
                        T.dest += 10;
                        T.toch -= 3 ;
                    }
              
                Console.ReadKey();
                return T;
            }
            public override string Show()
            {
                string temp = "Балиста \n Передвигается, но средней мощности" + base.Show();
                return temp;
            }
        }
        public class Taran: Osad
        {
           public bool xod;
           public bool per;
           public bool redy;

            public Taran(int p, int d, int t)
                : base(p, d, t)
            {
                xod = true;
                per = false;
                redy = true;
            }
            public static Taran operator +(Taran T, int x)
            {
                T.redy = true;
                Console.WriteLine("Орудие не нуждается в зарядке!");
                Console.ReadKey();
                return T;
            }
            public static int operator -(Taran T, int x)
            {    
                int uron = 0;
              
                if (T.dest < 1) {
                    Console.WriteLine("Попадание!");
                    uron = T.pow;
                }
                else Console.WriteLine("Подойдите ближе!");
                Console.ReadKey();
                return uron;
            }
            public static Taran operator >(Taran T, int i)
            {
                
            
                    Console.WriteLine("Маневр на сближение");
                    if (T.dest <= 0)
                    {
                        Console.WriteLine("Максимально близкая дистанция");
                        Console.ReadKey();
                        return T;
                    }
                    else T.dest -= 10;
              
                
                Console.ReadKey();
                return T;
            }
            public static Taran operator <(Taran T, int i)
            {
               
                    Console.WriteLine("Маневр на удаление");
                    if (T.dest >= 70)
                    {
                        Console.WriteLine("Максимально близкая дистанция");
                        Console.ReadKey();
                        return T;
                    }
                    else T.dest += 10;
                
                Console.ReadKey();
                return T;
            }

            public override string Show()
            {
                string temp = "Таран \n Не нуждается в перезарядке, но слабой мощности. Урон только в упор" + base.Show();
                return temp;
            }
        }

        private static void Menu(int pos, string inf,string[] m,int k)
        {
            Console.WriteLine(" [esc]-Выход ");
            Console.WriteLine("Прочность крепости:"+k);
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine();
            var curColor = Console.BackgroundColor;
            for (int i = 0; i < m.Length; i++)
            {
                if (i == pos)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(m[i]);
                    Console.BackgroundColor = curColor;
                }
                else
                {
                    Console.WriteLine(m[i]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("______________________________________________________________");

            Console.WriteLine(inf);
        }
    }
}
