using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorysenkoDZ3
{
    class Program
    {
        static void Main(string[] args)
        {
           // Console.BackgroundColor = ConsoleColor.DarkYellow;
            T_book spr=new T_book();
            Abon t = new Abon();
            Abon t1 = new Abon();
            Abon t2= new Abon();
            Abon t3 = new Abon();
            t.name= "Василий";
            t.numero= 923345271;
            spr.book[0]=t;
            t1.name ="Станислав";
            t1.numero = 67543;
            spr.book[1] = t1;
            t2.name =  "Александр";
            t2.numero =9999999;
            spr.book[2] = t2;
            t3.name =  "Лев";
            t3.numero = 10001;
            spr.book[3] = t3;

          
         
             var pos = 0;
             while (true)
             {
                 Menu(pos);
                 var key = Console.ReadKey();
                 if (key.Key == ConsoleKey.UpArrow)
                     pos = pos <= 0 ? 2 : --pos;
                 else if (key.Key == ConsoleKey.DownArrow)
                     pos = pos >= 2 ? 0 : ++pos;
                 else if (key.Key == ConsoleKey.Enter)
                 {
                     switch (pos) { 
                         case 0:
                             spr.llamo();
                             break;
                         case 1:
                             
                             Console.Clear();
                             int pos2 = 0;
                             while (true) { 
                              int count=spr.Count();
                             if (count == 0) {
                                 Console.WriteLine( "Справочник пуст!!!");
                                 Console.ReadKey();
                                 break;
                             }
                             Menu2(pos2,count, spr);
                             var key1 = Console.ReadKey();
                             if (key1.Key == ConsoleKey.UpArrow)
                                 pos2 = pos2 <= 0 ? count-1 : --pos2;
                             else if (key1.Key == ConsoleKey.DownArrow)
                             {
                                 pos2 = pos2 >= count-1 ? 0 : ++pos2;
                             }
                             else if (key1.Key == ConsoleKey.Enter) {
                                 spr.getAb(pos2);
                             }
                             else if (key1.Key == ConsoleKey.Backspace)
                             {
                                 spr.menAb(pos2);
                             }
                             else if (key1.Key == ConsoleKey.Escape)
                             {
                                 Menu(0);
                                 break;
                             }
                             Console.Clear();
                             }
                             break;
                         case 2:
                             //int con=spr.Count();
                            // spr.book[con].R_numero();
                             spr.Add();
                             break;
                     }
                 }
                 else if (key.Key == ConsoleKey.Escape)
                 { break; }

                     //MessageBox.Show("Пункт меню " + (pos+1));
                 Console.Clear();
             }
        }
        private static void Menu(int pos)
        {
            var menu = new[] { "1. Набор номера", "2. Набор и удаление номера", "3. Сохранить номер" };

            var curColor = Console.BackgroundColor;
            for (int i = 0; i < 3; i++)
            {
                if (i == pos)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(menu[i]);
                    Console.BackgroundColor = curColor;
                }
                else
                {
                    Console.WriteLine(menu[i]);
                }
            }
        }
        public class Abon {
            public string name = "";
            public int numero = 0;
            public void R_nombre()
            {
                try
                {
                    Console.WriteLine( "Введите имя абонента!" );
                    string n = "";
                    n = Console.ReadLine();
                    char c=n[0];
                    if (n.Length > 16) {
                        throw new Exception("Имя слишком длинное!");
                    }else if(n.Length<3){
                        throw new Exception("Имя слишком короткое!");
                    }else if(char.IsDigit(n[0])){
                        throw new Exception("Не допустимо начало имени с числа!");
                    }
                     name=n;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message+" Попробуйте еще");
                    Console.ReadKey();
                    R_nombre();
                }
                }
            public void R_numero() {
                try
                {
                    Console.WriteLine("Введите номер");
                    int nu = 0;
                    // nu=int.Parse(Console.ReadLine());
                    if (!int.TryParse(Console.ReadLine(), out nu)) {
                        throw new Exception("Недопустимый символ !");
                    }
                    else if(nu>9999999){
                     throw new Exception("Номер слишком длинный!");
                    }
                    else if (nu < 10000)
                    {
                        throw new Exception("Номер слишком короткий!");
                    }
                   
                    numero=nu;
                    
                }
                catch (Exception ex) {

                    Console.WriteLine(ex.Message +"  Попробуйте еще!");
                   Console.ReadKey();
                    R_numero();
                   
                }

            }
            
        }



        public class T_book {
           public Abon[] book = new Abon[200];
            public void Add(){
                int c = 0;
                
                try
                {
                    for (int i = 0; i < 200; i++)
                    {
                        if (book[i] == null)
                        {
                            Abon t = new Abon();
                            t.R_nombre();
                            t.R_numero();
                            book[i]=t;
                            
                            c++;
                            break;
                        }
                        
                    }
                    if (c == 0) {
                            throw new Exception("Память справочника заполнена!");
                            
                        }
                }catch(Exception ex){
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                Console.WriteLine("Номер успешно добавлен!");
                Console.ReadKey();
            }
            public void menAb(int z) {

                book[z] = null;
                int s= Count();
                Abon[] temp = new Abon[s];
                int x = 0;
                foreach (Abon t in book) {
                    if (t != null) {
                        temp[x++] = t;
                    }
                }
                for (int i = 0; i < book.Length; i++) {
                    if (i < temp.Length)
                    {
                        book[i] = temp[i];
                    }
                    else {
                        book[i] = null;
                    }

                }


            }
            public void getAb(int z) {
                Console.WriteLine("\n  Вы звоните абоненту "+book[z].name+"\n  На номер:"+book[z].numero );
                Console.ReadKey();
            }

            public void llamo() {
                try
                {
                    Console.WriteLine("Введите номер");
                    int nu = 0;
                    // nu=int.Parse(Console.ReadLine());
                    if (!int.TryParse(Console.ReadLine(), out nu))
                    {
                        throw new Exception("Недопустимый символ!Или невероятнно длинный номер)))");
                   }
                    else if (nu > 9999999)
                    {
                        throw new Exception("Номер слишком длинный!");
                        
                    }
                    else if (nu < 10000)
                    {
                        throw new Exception("Номер слишком короткий!");
                        
                    }
                    
                    Console.WriteLine("Вы дозванились на номер :"+nu); ;
                        Console.ReadKey();
            
                        
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message + "  Попробуйте еще!");
                    Console.ReadKey();
                }
                
            }
            public int Count() {
                int count = 0;
                foreach (Abon t in book) {
                    if (t != null) {
                        count++;
                    }
                }
                return count;
            }
        
        }
      
        private static void Menu2(int pos,int c, T_book m)
        {
            Console.WriteLine("[Enter]-позвонить.  [backspace]- удалить. [esc]-главное меню \n");
           

            var curColor = Console.BackgroundColor;
            for (int i = 0; i < c; i++)
            {
                if (i == pos)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(m.book[i].name+" Телефон: "+m.book[i].numero);
                    Console.BackgroundColor = curColor;
                }
                else
                {
                    Console.WriteLine(m.book[i].name + " Телефон: " + m.book[i].numero);
                }
            }
        }
    }
}
