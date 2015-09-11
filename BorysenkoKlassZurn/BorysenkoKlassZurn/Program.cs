using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timer = System.Threading.Timer;
namespace BorysenkoKlassZurn
{
    class Program
    {
        static int i = 0;
        static ArrayList m3 = new ArrayList(5);
        static Post zhur = new Post();
   
        static void Main(string[] args)
        {
            Timer timer;
            timer = new Timer(AddCar);
            string[] m1 = new[] { "1. Подписаться", "2. Мои журналы", "3. Ждем выхода журналов", "4. Остановить выхода журналов" };
            string[] m2 = new[] {"1. Мурзилка","2. Вокруг света","3. Риск","4. АвтоМир","5. CHIP","6. Спорт","7. Юный нейрохерург","8. Зимбабве сегодня","9. Журнал"};
            zhur.RegisterPostHendler(Show_Post);

           
            Console.Clear();
            int pos = 0;
            int pos1 = 0;
            int pos2 = 0;
            while (true)
            {


                Menu(pos, m1);
                var key1 = Console.ReadKey();
                if (key1.Key == ConsoleKey.UpArrow)
                {
                    pos = pos <= 0 ? m1.Length - 1 : --pos;
                  
                }
                else if (key1.Key == ConsoleKey.DownArrow)
                {
                    pos = pos >= m1.Length - 1 ? 0 : ++pos;
                  
                }
                else if (key1.Key == ConsoleKey.Enter)
                {
                    switch (pos)
                    {
                        case 0:
                            while (true)
                            {
                                Console.Clear();
                                Menu(pos1, m2);
                                key1 = Console.ReadKey();
                                if (key1.Key == ConsoleKey.UpArrow)
                                {
                                    pos1 = pos1 <= 0 ? m2.Length - 1 : --pos1;

                                }
                                else if (key1.Key == ConsoleKey.DownArrow)
                                {
                                    pos1 = pos1 >= m2.Length - 1 ? 0 : ++pos1;

                                }
                                else if (key1.Key == ConsoleKey.Enter)
                                {
                                    m3.Add(m2[pos1]);
                                }
                                else if (key1.Key == ConsoleKey.Escape)
                                {

                                    break;
                                }

                                Console.Clear();
                            }


                            break;
                        case 1:

                            while (true)
                            {
                                Console.Clear();
                                Menu1(pos2, m3);
                                key1 = Console.ReadKey();
                                if (key1.Key == ConsoleKey.UpArrow)
                                {
                                    pos2 = pos2 <= 0 ? m3.Count - 1 : --pos2;

                                }
                                else if (key1.Key == ConsoleKey.DownArrow)
                                {
                                    pos2= pos2 >= m3.Count - 1 ? 0 : ++pos2;


                                }
                                else if (key1.Key == ConsoleKey.Enter)
                                {
                                    if (m3.Count > 0)
                                    {
                                        m3.RemoveAt(pos2);
                                    }
                                    else {
                                        Console.WriteLine("Вы не подписались ни на один журнал!");
                                        Console.ReadKey();
                                    }
                                }
                                else if (key1.Key == ConsoleKey.Escape)
                                {
                                    

                                    break;
                                }

                                Console.Clear();
                            }

                            break;
                        case 2:
                            
                            
                           
                              
                             timer.Change(500, 5000);



                            break;
                        case 3:


                                timer.Dispose();
                           


                            break;

                    }


                }
                else if (key1.Key == ConsoleKey.Escape)
                {

                    break;
                }
                Console.Clear();
            }

        }


         public static void AddCar(Object stateInfo)
        {
           
            zhur.postWeat(m3);
            
            }

        private static void Show_Post(string message)
         {
            
            Console.WriteLine(message);
          
        }

        public class Post{

           

            public delegate void PostEngineHandler(string msgFromCaller);

            private PostEngineHandler EngineHandler;

            public void RegisterPostHendler(PostEngineHandler PostEngine)
            {
                EngineHandler += PostEngine;
            }



            public void postWeat(ArrayList m)
            {
                int count = m.Count;
           
                if (count >0)
                {
                    i++; 
                    if (i >= count) {
                        i = 0;
                    }

                    if (EngineHandler != null)
                        EngineHandler(("Вам пришёл журнал" + m[i]));
                }
                else
                {
                    if (EngineHandler != null)
                        EngineHandler("Вы не выписываете журналы!");
                }
            }
        
        
        }

        private static void Menu(int pos, string[] m)
        {
            Console.WriteLine("[ESC] Назад. Enter- Выбрать.  ");
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
            Console.WriteLine("Вы выписали "+m3.Count+" журналов.");

        }

        private static void Menu1(int pos, ArrayList m)
        {
            Console.WriteLine("[ESC] Назад.  Enter- отписаться. ");
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
            Console.WriteLine("Вы выписали " + m3.Count + " журналов.");
          
        }

    }
}
