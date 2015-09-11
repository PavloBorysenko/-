using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorysenkoDZBiocrizis
{
    class Program
    {
        static void Main(string[] args)
        {
            var point1 = new ArrayList(5);
            var point2 = new ArrayList(5);
            string[,] pole = new string[10, 10];
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    pole[i, j] = " ";
                }
            }
            pole[0, 0] = "#";
            pole[0, 9] = "*";
            point1.Add(new Point(0, 0));
            point2.Add(new Point(0, 9));
            for (; ; )
            {
                Xod(ref pole,ref point1, "#");
                Print(pole, point1, point2);
                if (point1.Count > 50) {
                System.Console.WriteLine( "Победа красных");
                break;
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения!");
                Console.ReadKey();
                Console.Clear();
                Xod(ref pole, ref point2, "*");
                Print(pole, point1, point2);
                 if (point2.Count > 50)
                {
                    System.Console.WriteLine("Победа зеленых");
                    break;
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения!");
                
                Console.ReadKey();
                Console.Clear();
            }
        }



        class Point {
            public int x = 0;
            public int y = 0;

            public Point(int X, int Y) {
                x = X;
                y = Y;
            }
            public Point Get_point(){
                Random R = new Random();
                for (; ; ) {
                    int ch = R.Next(0, 4);
                    switch (ch) { 
                        case 0:
                            if (x - 1 >= 0) { 
                            Point P=new Point(x-1,y);
                            return P;
                            }

                            break;
                        case 1:
                            if (x + 1 <= 9)
                            {
                                Point P = new Point(x + 1, y);
                                return P;
                            }

                            break;
                        case 2:
                            if (y - 1 >= 0)
                            {
                                Point P = new Point(x , y-1);
                                return P;
                            }
                            break;
                        case 3:
                            if (y + 1 <= 9)
                            {
                                Point P = new Point(x , y+1);
                                return P;
                            }
                            break;
                    
                    }
                
                
                }
            
            
            }

        
        }

        static public void Xod(ref string[,] pole, ref ArrayList point , string s ) {
            Random rand = new Random();
            int prob = 0;
            int popul = rand.Next(1, 4);
            for (int i = 0; i < popul; ) {
                var P = (point[rand.Next(0, point.Count)] as Point).Get_point();
                if (pole[P.x, P.y] != "#" && pole[P.x, P.y] != "*") {

                    pole[P.x, P.y] = s;
                    point.Add(P);
                    i++;
                }
                prob++;
                if (prob > 500) break;
            }
        
        
        }

       static public void Print(string[,] pole, ArrayList b1,ArrayList b2 ) {
           System.Console.WriteLine();
           var curColor = Console.BackgroundColor;
           for (int i = 0; i < 10; i++) {
               for (int j = 0; j < 10; j++) {
                   if (pole[i, j] == "#")
                   {
                       Console.BackgroundColor = ConsoleColor.Red;
                       System.Console.Write(pole[i, j]);
                       Console.BackgroundColor = curColor;
                   }
                   else if (pole[i, j] == "*") {
                       Console.BackgroundColor = ConsoleColor.DarkGreen;
                       System.Console.Write(pole[i, j]);
                       Console.BackgroundColor = curColor;
                   }
                   else System.Console.Write(pole[i, j]);



               }
               System.Console.WriteLine();
           
           }
           System.Console.WriteLine("#-"+b1.Count);
           System.Console.WriteLine("*-" + b2.Count);

        }

    }
}
