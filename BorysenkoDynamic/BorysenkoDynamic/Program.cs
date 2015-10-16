using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BorysenkoDynamic
{
    class Program
    {
        static void Main(string[] args)

        {   Plener plan1 = new Plener { model = "AN-2", maxHeiht = 3000, Massa = 1 };
            Plener plan2 = new Plener { model = "IL-76", maxHeiht = 8000, Massa = 20 };
            Plener plan3 = new Plener { model = "SU-25", maxHeiht = 11000, Massa = 5 };
           
            List<Plener> planer=new List<Plener>();
            planer.Add(plan1);
            planer.Add(plan2);
            planer.Add(plan3);

            Ship ship1 = new Ship { name = "Pilar", water_p = 10, massa = 3 };
            Ship ship2 = new Ship { name = "Beda", water_p = 15, massa = 6 };
            Ship ship3 = new Ship { name = "Titanic", water_p = 300000, massa = 50 };

            List<Ship> ship =new List<Ship>();
            ship.Add(ship1);
            ship.Add(ship2);
            ship.Add(ship3);

            Track track1 = new Track { marka = "Zil", massa = 5, speed = 140 };
            Track track2 = new Track { marka = "MAN", massa = 8, speed = 160 };
            Track track3 = new Track { marka = "DAF", massa = 15, speed = 170 };
            List<Track> track = new List<Track>();
            track.Add(track1);
            track.Add(track2);
            track.Add(track3);

            string[] menu = { "Самолет ", "Судно", "Грузовик" };
            int pos = 0;
            while (true)
            {
                Menu(pos, "", menu);
                var key = Console.ReadKey();
                pos = menycount(key, menu.Length, pos);
                if (key.Key == ConsoleKey.Enter)
                {
                    switch (pos)
                    {

                        case 0:
                           
                            try
                            {

                                Assembly asm = Assembly.LoadFrom("PlanMath.dll");
                                Type t = asm.GetType("PlanMath.Functions", true, true);
                                // создаем экземпляр класса Functions
                                object obj = Activator.CreateInstance(t);

                                // получаем метод класса
                                MethodInfo addMethod = t.GetMethod("Add");

                                //Вызываем метод, передаем ему значения.
                                foreach (Plener item in planer) {
                                 Console.WriteLine(item);
                                 object result = addMethod.Invoke(obj, new object[] {item.Massa,item.maxHeiht});
                                 Console.WriteLine("Максимальная высота груженый " + result);
                                
                                }
                               


                                Console.ReadKey();
                            }
                            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                            break;
                        case 1:
                            try
                            {

                                Assembly asm = Assembly.LoadFrom("ShipMath.dll");
                                Type t = asm.GetType("ShipMath.Functions", true, true);
                                // создаем экземпляр класса Functions
                                object obj = Activator.CreateInstance(t);

                                // получаем метод класса
                                MethodInfo addMethod = t.GetMethod("Add");

                                //Вызываем метод, передаем ему значения.
                                foreach (Ship item in ship)
                                {
                                    Console.WriteLine(item);
                                    object result = addMethod.Invoke(obj, new object[] { item.massa, item.water_p });
                                    Console.WriteLine("Водоизмещение груженый " + result);

                                }



                                Console.ReadKey();
                            }
                            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                            break;
                        case 2:

                            try
                            {

                                Assembly asm = Assembly.LoadFrom("TrackMath.dll");
                                Type t = asm.GetType("TrackMath.Functions", true, true);
                                // создаем экземпляр класса Functions
                                object obj = Activator.CreateInstance(t);

                                // получаем метод класса
                                MethodInfo addMethod = t.GetMethod("Add");

                                //Вызываем метод, передаем ему значения.
                                foreach (Track item in track)
                                {
                                    Console.WriteLine(item);
                                    object result = addMethod.Invoke(obj, new object[] { item.massa, item.speed });
                                    Console.WriteLine("Максимальная высота груженый " + result);

                                }



                                Console.ReadKey();
                            }
                            catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                            break;
                    }

                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                Console.Clear();


            }


        }


        class Plener
        {

            public int maxHeiht { get; set; }
            public string model { get; set; }
            public int Massa { get; set; }
            public override string ToString() {
                return "Модель самолета "+model+" .Макс высота "+maxHeiht+" . Макс загруженность "+Massa;
            }
        }
        class Ship
        {
            public string name { get; set; }
            public int water_p { get; set; }
            public int massa { get; set; }
            public override string ToString()
            {
                return "Название коробля " + name + " .Водоизмещение " + water_p + " . Макс загруженность " + massa;
            }
        }
        class Track
        {
            public string marka { get; set; }
            public int speed { get; set; }
            public int massa { get; set; }
            public override string ToString()
            {
                return "Марка авто " + marka + " .Скорость " + speed + " . Макс загруженность " + massa;
            }
        }
        public static int menycount(ConsoleKeyInfo key, int count, int pos)
        {
            if (key.Key == ConsoleKey.UpArrow)
                pos = pos <= 0 ? count - 1 : --pos;
            else if (key.Key == ConsoleKey.DownArrow)
                pos = pos >= count - 1 ? 0 : ++pos;
            return pos;
        }
        private static void Menu(int pos, string inf, string[] m)
        {
            Console.WriteLine("[esc]- Назад");
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
