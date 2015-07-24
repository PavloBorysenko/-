using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorysenkoKlass5
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Rasa myRasa;
            dosp myDosp=dosp.koga;
            Orug[] orug = new Orug[] {
             new Mech(20),
             new Pistol(20),
             new Posox(20)
            };
            int i = 0;
            int j = 0;
            string Name;
            string CPU = "Супостат";
            string T = "Выберите расу";
            string T2 = "Выберите профессию";
            string T3 = "Вы имеете право выбрать любой доспех или оружие, но только что-то одно. \nОставшийся опции  выберутся рандомно. ";
            string T4 = "Выберите доспехи";
            string T5 = "Выберите оружие. Повышенный урон при соответствуещем умении";
          
            string[] m1 = new string[] { "Человек", "Гоблин ", "Эльф" };
            string[] m2 = new string[] { "Воин ", "Маг", "Вор " };
            string[] m3 = new string[] { "Доспех", "Оружие" };
            string[] m4 = new string[] { "Кожаный (хорош для охоты на куриц)", "Железный (хороший выбор) ","Чешуя дракона(для серьезного квеста)" };
            string[] m5 = new string[] { "Меч (в хороших руках, хороший урон)","Пистоль(подлое оружие)" , "Посох (грозен при знании магии) " };
            string[] m6 = new string[] { "Критичный удар (возможность макс. урона. но раскрывате оборону) ", "Обычный удар( урон и защита сбалансированы)", "Оккуратный удар ( удар с с макс. вероятностью защиты)" };
            int pos = 0;
            Console.WriteLine("Введите имя");
            Name = Console.ReadLine();
            Console.Clear();
            while (true)
            {
                Menu(pos, T, m1);
                var key = Console.ReadKey();
                pos=menycount( key, m1.Length, pos );
                if (key.Key == ConsoleKey.Enter) {
                    myRasa = ChoRas(pos);
                    Console.Clear();
                    break;
                }
                Console.Clear();
            }
            pos = 0;
            while (true)
            {
                Menu(pos, T2, m2);
                var key = Console.ReadKey();
                pos = menycount(key, m2.Length, pos);
                if (key.Key == ConsoleKey.Enter)
                {
                    i = pos;
                    Console.Clear();
                    break;
                }
                Console.Clear();
            }
    
            pos = 0;
            int chos = 0;
            while (true)
            {
                Menu(pos, T3, m3);
                var key = Console.ReadKey();
                pos = menycount(key, m3.Length, pos);
                if (key.Key == ConsoleKey.Enter)
                {
                   
                    chos = pos;
                    Console.Clear();
                    break;
                }
                Console.Clear();
            }
            pos = 0;
            if (chos == 0)
            {
                
                while (true)
                {
                    Menu(pos, T4, m4);
                    var key = Console.ReadKey();
                    pos = menycount(key, m4.Length, pos);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        j = rand.Next(0, 3);
                        myDosp = ChoDosp(pos);
                        
                        Console.Clear();
                        break;
                    }
                    Console.Clear();
                }
            }
            else if (chos == 1)
            {
                while (true)
                {
                    Menu(pos, T5, m5);
                    var key = Console.ReadKey();
                    pos = menycount(key, m5.Length, pos);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        j = pos;
                        myDosp = ChoDosp(rand.Next(0, 3));
                      
                        Console.Clear();
                        break;
                    }
                    Console.Clear();
                }
            }
            
            Pers[] person = new Pers[]{
              new Warrior(Name,getRas(myRasa),(int)myRasa,myDosp,ChoSchlem(rand.Next(0,3)),ChoSapog(rand.Next(0,3)),10,20,orug[j]),
              new Warlock(Name,getRas(myRasa),(int)myRasa,myDosp,ChoSchlem(rand.Next(0,3)),ChoSapog(rand.Next(0,3)),15,15,orug[j]),
              new Thief(Name,getRas(myRasa),(int)myRasa,myDosp,ChoSchlem(rand.Next(0,3)),ChoSapog(rand.Next(0,3)),10,20,orug[j])
           };

           Rasa pcRas = ChoRas(rand.Next(0, 3));
           int x = rand.Next(0, 3);
            Pers[] cpu = new Pers[]{
              new Warrior(CPU,getRas(pcRas),(int)pcRas,ChoDosp(rand.Next(0,3)),ChoSchlem(rand.Next(0,3)),ChoSapog(rand.Next(0,3)),20,10,orug[j]),
              new Warlock(CPU,getRas(pcRas),(int)pcRas,ChoDosp(rand.Next(0,3)),ChoSchlem(rand.Next(0,3)),ChoSapog(rand.Next(0,3)),15,15,orug[j]),
              new Thief(CPU,getRas(pcRas),(int)pcRas,ChoDosp(rand.Next(0,3)),ChoSchlem(rand.Next(0,3)),ChoSapog(rand.Next(0,3)),10,20,orug[j])
           };
            Console.WriteLine("Да начнется бой!!!!");
            Console.ReadKey();
            Console.Clear();
           while (true)
           {
               int d1 = 0;
               int d2 = 0;
               string T6= person[i].Print() + "\n**************************\n" + cpu[x].Print();
               Menu(pos, T6, m6);
               var key = Console.ReadKey();
               pos = menycount(key, m6.Length, pos);
               if (key.Key == ConsoleKey.Enter)
               {
                  switch(pos){
                      case 0:
                          d1 = rand.Next(5, 20);
                          d2 = rand.Next(5,30);
                          break;
                      case 1:
                          d1 = 0;
                          d2 = 0;
                          break;
                      case 2:
                          d1 = rand.Next(-10,0);
                       
                          d2 = rand.Next(-10,0);
                          break;
                  }
                  Console.WriteLine("Ваш удар!");
                   Console.WriteLine("C БОНУСОМ за вид удара "+ d1);
                 
                  cpu[x].ud(person[i].uron+d1);
                 if (cpu[x].life <= 0) {
                     T6 = person[i].Print() + "\n**************************\n" + cpu[x].Print();
                     Console.Clear();
                     Menu(pos, T6, m6);
                     Console.WriteLine("**********************************");
                    Console.WriteLine(" Персонаж " + person[i].name + " выиграл!!!");
                    break;
                }
                  
                  Console.WriteLine("Удар Противника!");
                  Console.WriteLine("БОНУС противнику за твою защиту " + d2);
                   person[i].ud(cpu[x].uron + d2);
                  
                    if (person[i].life <= 0) {
                        Console.Clear();
                        T6 = person[i].Print() + "\n**************************\n" + cpu[x].Print();
                        Menu(pos, T6, m6);

                        Console.WriteLine("**********************************");
            Console.WriteLine(" Персонаж " + cpu[x].name + " выиграл!!!");
                    break;
                }
                  
               }
               Console.Clear();
           }
         
           
        }
        public static int menycount(ConsoleKeyInfo key, int count,int pos ) {
            if (key.Key == ConsoleKey.UpArrow)
                pos = pos <= 0 ?  count-1  : --pos;
            else if (key.Key == ConsoleKey.DownArrow)
                pos = pos >=  count-1  ? 0 : ++pos;
            return pos;
        }

        public static Rasa ChoRas(int pos) { 
          
            switch (pos) { 
                case 0:
                     return Rasa.Human;
                    break;
                case 1:
                    return Rasa.Goblin;
                    break;
                case 2:
                    return Rasa.Elf;
                    break;
                    
            }

          return Rasa.Human;
        }
        public static string getRas(Rasa r)
        {
           

            switch (r)
            {
                case Rasa.Human:
                    return "Человек";
                    break;
                case Rasa.Goblin :
                    return "Гоблин";
                    break;
                case Rasa.Elf:
                    return "Эльф";
                    break;

            }

            return "X-men";
        }
        public static dosp ChoDosp(int pos)
        {
            Rasa temp;

            switch (pos)
            {
                case 0:
                    return dosp.koga;
                    break;
                case 1:
                    return dosp.still;
                    break;
                case 2:
                    return dosp.dragon;
                    break;

            }

            return dosp.koga;
        }
        public static shlem ChoSchlem(int pos)
        {
            shlem temp;

            switch (pos)
            {
                case 0:
                    return shlem.koga;
                    break;
                case 1:
                    return shlem.still;
                    break;
                case 2:
                    return shlem.magic;
                    break;

            }

            return shlem.koga;
        }

        public static sapog ChoSapog(int pos)
        {
            

            switch (pos)
            {
                case 0:
                    return sapog.kedu;
                    break;
                case 1:
                    return sapog.still;
                    break;
                case 2:
                    return sapog.adidas;
                    break;

            }

            return sapog.kedu;
        }

        public enum Rasa
        {
            Human = 100,
            Goblin = 150,
            Elf= 90
        }
        public enum dosp { 
            koga=10,
            still=20,
            dragon=30
        }
       public enum shlem
        {
            koga = 10,
            still = 20,
            magic = 30
        }
       public  enum sapog
        {
            kedu = 10,
            still = 20,
            adidas = 30
        }
        public abstract class Pers {
            public string name;
            protected string rasa;
            public int life;
            public int ProtDsp=0;
            public dosp dosp;
           public shlem shlem;
           public sapog sapog;
            public Orug orug;
            protected string cat;
            public int uron;
            public string info;
            protected Pers(string n,string r, int l,dosp d,shlem s,sapog sa,Orug o ) {
                name = n;
                rasa = r;
                life = l;
                dosp = d;
                shlem = s;
                sapog = sa;
                orug = o;
              
                switch (shlem)
                {
                    case shlem.koga:
                        ProtDsp += (int)shlem.koga;
                        break;
                    case shlem.still:
                        ProtDsp += (int)shlem.still;
                        break;
                    case shlem.magic:
                        ProtDsp += (int)shlem.magic;
                        break;
                }
                switch (dosp)
                {
                    case dosp.koga:
                        ProtDsp += (int)dosp.koga;
                        break;
                    case dosp.still:
                        ProtDsp += (int)dosp.still;
                        break;
                    case dosp.dragon:
                        ProtDsp += (int)dosp.dragon;
                        break;
                }
                switch (sapog)
                {
                    case sapog.kedu:
                        ProtDsp += (int)sapog.kedu;
                        break;
                    case sapog.still:
                        ProtDsp += (int)sapog.still;
                        break;
                    case sapog.adidas:
                        ProtDsp += (int)sapog.adidas;
                        break;
                }
            }
            public virtual void ud(int x) {
                life=life - x;
            }
            public virtual string Print() {
                return "Имя: " + name + " Раса: " + rasa + "  Жизнь: " + life + "\n Шлем: " + shlem + " Доспех: " + dosp + "  Сапоги: " + sapog ;
            }
        }
        public class Warrior : Pers {
            public int protect;
            public int power;
           
            public Warrior(string n, string r, int l, dosp d, shlem s, sapog sa, int pow,int prot,  Orug o)
                : base(n, r, l, d, s, sa,o)
            {
                protect = prot + ProtDsp;
                power = pow;
                cat = "war";

                if (orug.cat == cat)
                {
                    uron = orug.uron + power;
                    info = orug.uron + "+ за прав. оружие БОНУС " + pow;
                }
                else {
                    uron = orug.uron;
                    info = orug.uron.ToString();} 
            }
            public override void ud(int x)
            {
                int s = x - (protect / 10);
              
                Console.WriteLine("Урон  -" + s);
                Console.ReadKey();

                base.ud(s);
            }
            public override string Print()
            {
                return base.Print() + "\n профессия  ВОИН.   урон: " + info + "   Общая защита: " + protect;
            }
        }
        public class Warlock : Pers
        {
            public int powZakl;
            public int mana;
           
           
            public Warlock(string n, string r, int l, dosp d, shlem s, sapog sa, int pow, int man, Orug o)
                : base(n, r, l, d, s, sa,o)
            {
                powZakl = pow;
                mana = man + ProtDsp;
                cat = "mag";
                if (orug.cat == cat) {
                    uron = orug.uron + powZakl;
                    info=orug.uron +"+ БОНУС "+ powZakl;
                }
                 else {
                    uron = orug.uron;
                    info = orug.uron.ToString();} 
            }
            public override void ud(int x)
            {
                int s = x - (mana / 10);
               
                Console.WriteLine("Урон  -"+s);
                Console.ReadKey();
                
                base.ud(s);
            }
            public override string Print() {
                return base.Print() + "\n профессия  МАГ.    урон: " + info + "   Общая защита: " + mana;
            }

        }
            public class Thief : Pers
            {
                public int skrut;
                public int pricel;

                public Thief(string n, string r, int l, dosp d, shlem s, sapog sa, int pri, int skr,  Orug o)
                    : base(n, r, l, d, s, sa,o)
                {
                    skrut = skr + ProtDsp;
                    pricel = pri;
                    cat = "vor";
                    if (orug.cat == cat)
                    {
                        uron = orug.uron + pricel;
                        info = orug.uron + "+ БОНУС " + pricel;
                    }
                    else
                    {
                        uron = orug.uron;
                        info = orug.uron.ToString();
                    } 
                }
                public override void ud(int x)
                {
                    int s = x - (skrut / 10);

                    Console.WriteLine("Урон  -" + s);
                    Console.ReadKey();

                    base.ud(s);
                }
                public override string Print()
                {
                    return base.Print() + "\n профессия  ВОР .  урон: " + info + "   Общая защита: " + skrut;
                }

            }
            public abstract class Orug {
               public int uron;
               public string cat;
            protected Orug(int ur){
                uron = ur;
            }
            }
            public class Mech : Orug {
                
                public Mech(int ur) : base(ur) {
                    cat = "war";
                }
            }
            public class Posox : Orug
            {

                public Posox(int ur)
                    : base(ur)
                {
                    cat = "mag";
                }
            }
            public class Pistol : Orug
            {

                public Pistol(int ur)
                    : base(ur)
                {
                    cat = "vor";
                }
            }
            private static void Menu(int pos, string inf, string[] m)
            {
               
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
