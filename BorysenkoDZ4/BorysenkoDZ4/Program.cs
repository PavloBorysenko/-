using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorysenkoDZ4
{
    class Program
    {
        static void Main(string[] args)
        {
            Tienda mag = new Tienda();
            Coche temp1 = new Coche();
            Coche temp2 = new Coche();
            Coche temp3 = new Coche();
            temp1.brend = "BMW";
            temp1.nombre = "X1";
            temp1.cuesta = 12000;
            temp2.brend = "Opel";
            temp2.nombre = "Astra";
            temp2.cuesta = 8000;
            temp3.brend = "KIA";
            temp3.nombre = "H1";
            temp3.cuesta = 6000;
            mag.mas[0] = temp1;
            mag.mas[1] = temp2;
            mag.mas[2] = temp3;
           
            int ch1 =-1, ch2 = -1;
            //mag = mag + 2;
            Console.Clear();
            int pos2 = 0;
            while (true)
            {
                int count = mag.mas.Count();
              
                Menu2(pos2, mag);
                var key1 = Console.ReadKey();
                if (key1.Key == ConsoleKey.UpArrow)
                    pos2 = pos2 <= 0 ? count - 1 : --pos2;
                else if (key1.Key == ConsoleKey.DownArrow)
                {
                    pos2 = pos2 >= count - 1 ? 0 : ++pos2;
                }
                else if (key1.Key == ConsoleKey.Backspace)
                {
                    mag.Update(pos2);

                }
                else if (key1.Key == ConsoleKey.Tab)
                {
                    if (ch1 == -1) {
                        ch1 = pos2;
                        Console.WriteLine("Сравнить "+mag[pos2].Show()+" c ...?\n Выберите еще одно авто для сравнения.");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    else if (ch1 != -1 && ch2 == -1) {
                        ch2 = pos2;
                        if (mag[ch1] < mag[ch2])
                        {
                            Console.WriteLine("Это авто {0} Mod:{1} стоит меньше чем {2} Mod:{3}", mag[ch1].brend, mag[ch1].nombre, mag[ch2].brend, mag[ch2].nombre);
                            Console.ReadKey();
                            ch1 = ch2 = -1;
                        }
                        else {
                            Console.WriteLine("Этo авто {2} Mod:{3} стоит меньше чем {0} Mod:{1}", mag[ch1].brend, mag[ch1].nombre, mag[ch2].brend, mag[ch2].nombre);
                            Console.ReadKey();
                            ch1 = ch2 = -1;
                        }
                    }


                }
                else if (key1.Key == ConsoleKey.Delete)
                {
                  
                    mag=mag-pos2;

                }
                else if (key1.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Вы купили "+mag[pos2].Show());
                    Console.ReadKey();

                }
                else if (key1.Key == ConsoleKey.Insert)
                {
                    int sum = 0;
                    Console.WriteLine("Введите количество добовлений: ");
                    while (!int.TryParse(Console.ReadLine(), out sum)) ;
                    if (sum < 0 || sum > 10) {
                        Console.WriteLine("Слишком много!");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    mag = mag + sum;
                }
                
                
                else if (key1.Key == ConsoleKey.Escape)
                {
                    
                    break;
                }
                Console.Clear();
            }


         
        }

        public class Coche{
            public string brend = "";
            public string nombre = "";
            public int cuesta = 0;
            public Coche() { 
               brend = "";
               nombre = "";
               cuesta = 0;
            }
            public void NewCoche() {
                try
                {
                    Console.WriteLine("Введите марку авто ");
                    string t = Console.ReadLine();
                    Console.WriteLine("Введите модель ");
                    string t1 = Console.ReadLine();


                    if (t.Length < 1 || t.Length > 16 || t1.Length < 1 || t1.Length > 16) {
                        throw new Exception("Не корректно заполнены данные названия или модели!");
                    }
                    int t2;
                    if (!int.TryParse(Console.ReadLine(), out t2))
                    {
                        throw new Exception("Недопустимый символ в цене!");
                    }

                  brend = t;
                nombre = t1;
                cuesta = t2;

                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    NewCoche();
                }
               
            }

            public static bool operator >(Coche i, Coche j)
            {
                if (i.cuesta > j.cuesta)
                {
                    return true;
                }
                else return false;
            }
            public static bool operator <(Coche i, Coche j)
            {
                if (i.cuesta < j.cuesta)
                {
                    return true;
                }
                else return false;
            }
            public  string Show() {
                string s = "";
                s = "Марка: " + this.brend + ". Модель:" + this.nombre + ". Цена: $" + this.cuesta;
                return s;
            }

        }
        public class Tienda {
           public Coche[] mas;

           public Tienda() {
               mas = new Coche[3];
              
           }
           public void Update(int pos) {
               Coche t = new Coche();
               t.NewCoche();
               mas[pos] = t;
           }
            public static Tienda operator +(Tienda T, int c){
          

                int x = T.mas.Length;
                //Console.WriteLine("X=" + x);
                //Console.ReadKey();
                Coche[] mT = new Coche[x + c];
                int i = 0;
                foreach (Coche item in T.mas)
                {
                    mT[i++] = item;
                    //Console.WriteLine("item=" + item);
                    //Console.ReadKey();
                }
                T.mas = new Coche[x + c];
                for (int j = x; j < c + x; j++)
                {
                    Coche temp = new Coche();
                    temp.NewCoche();
                    mT[j] = temp;
                }
                for (int z = 0; z < T.mas.Length; z++)
                {
                    T.mas[z] = mT[z];

                }
                    return T;
            }
            public static Tienda operator -(Tienda T, int c = 0)
            {
                int x = T.mas.Length;
                Coche[] mT = new Coche[x - 1];
                int j = 0;
                for (int i = 0; i < x; i++) {
                    if (i == c) continue;
                    mT[j++] = T.mas[i];
                }
                T.mas = new Coche[x -1];
                for (int z = 0; z < T.mas.Length; z++) {
                    T.mas[z] = mT[z];
                }

                return T;
            }
            
            public Coche this[int i]    // Indexer declaration
            {
                // get and set accessors
                get
                {

                    return mas[i];
                }
            }
        }

        private static void Menu2(int pos, Tienda m)
        {
            Console.WriteLine("[Enter]-Купить.   [insert]-добавить  [Backespace]-редактировать \n [Tab]-сравнить [Delete]- удалить. [esc]-Выход ");
            Console.WriteLine("______________________________________________________________") ;
            Console.WriteLine() ;
            var curColor = Console.BackgroundColor;
            for (int i = 0; i < m.mas.Length; i++)
            {
                if (i == pos)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(m[i].Show());
                    Console.BackgroundColor = curColor;
                }
                else
                {
                    Console.WriteLine(m[i].Show());
                }
            } 
            Console.WriteLine();
            Console.WriteLine("______________________________________________________________");
           
        }
    }
}
