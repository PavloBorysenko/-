using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


using Timer = System.Threading.Timer;


namespace BorysenkoKlassAuto
{
    class Program

    {
        static ObservableCollection<Car> observable = new ObservableCollection<Car>();
        static Timer timer = new Timer(AddCar);
        static int contCar = 0;
        static void Main(string[] args)
        {
            string[] men = new[] { "1.Запуск конвеера", "2.Продажа авто", "3. Сохранить номер" };
            

            Console.Clear();
            int pos = 0;
            while (true)
            {
                

                Menu(pos, men);
                var key1 = Console.ReadKey();
                if (key1.Key == ConsoleKey.UpArrow)
                    pos = pos <= 0 ? men.Length - 1 : --pos;
                else if (key1.Key == ConsoleKey.DownArrow)
                {
                    pos = pos >= men.Length - 1 ? 0 : ++pos;
                }
                else if (key1.Key == ConsoleKey.Enter)
                {
                    switch (pos) { 
                        case 0:
                            
                            timer.Change(500, 1000);
                            break;
                        case 1:
                            observable.CollectionChanged += observable_CollectionChanged;
                              Console.Clear();
            int pos1 = 0;
            while (true)
            {


                Menu2(pos1, observable);
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                    pos1 = pos1 <= 0 ? observable.Count - 1 : --pos1;
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    pos1 = pos1 >= observable.Count - 1 ? 0 : ++pos1;
                }
                else if (key.Key == ConsoleKey.Enter)
                {

                    observable.RemoveAt(pos1);
                    
                }
               
                
                
                
               


                else if (key1.Key == ConsoleKey.Escape)
                {

                    break;
                }
                Console.Clear();
            }



                            break;

                    
                    
                    }


                }
                else if (key1.Key == ConsoleKey.Backspace)
                {

                    timer.Dispose();
                }
               
                
                
                
               


                else if (key1.Key == ConsoleKey.Escape)
                {

                    break;
                }
                Console.Clear();
            }



        }
        static void observable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //if(e.Action)
            Console.WriteLine(e.Action);
            //Console.WriteLine(e.OldItems);
            foreach (var item in e.OldItems)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }

        public static void AddCar(Object stateInfo)
        {
            observable.Add(new Car());
            Console.WriteLine("Машина готова!!!");
            if (observable.Count > 20) {
                timer.Dispose();
                Console.WriteLine("Гараж полон!");
            
            }
        
        }
        public class Car {
            string model;
            public Car() {
                contCar++;
                model = "Авто #" + contCar;
            }
            public override string ToString()
            {
                return String.Format("Выпуск {0}", this.model);
            }
        }

        private static void Menu(int pos, string[] m)
        {
            Console.WriteLine("[backspace]-Остановить таймер.   ");
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

        }
        private static void Menu2(int pos,ObservableCollection<Car> m)
        {
            Console.WriteLine("[Enter]-Продать.   ");
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine();
            var curColor = Console.BackgroundColor;
            for (int i = 0; i < m.Count; i++)
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

        }
    }
}
