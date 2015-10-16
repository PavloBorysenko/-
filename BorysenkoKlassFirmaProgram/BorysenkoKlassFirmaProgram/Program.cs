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
namespace BorysenkoKlassFirmaProgram
{
    class Program
    {   static Firma WEBProf = new Firma(20000);
        static Timer timer;
        static void Main(string[] args)
        {
            string[] meny = { "Нанять работника", "Уволить работника", "Взять заказ", "Разработка" };
            
            List<Programmer> ListProf = new List<Programmer>();
            ListProf = Programmer.Insert();
           Birzha BirzhRab = new Birzha();

           
           

            int pos=0;
            WEBProf.addSotru += FirmaAddDellsotrud;
            WEBProf.delSotru += FirmaAddDellsotrud;
            WEBProf.StatusZak += FirmaMesag;
            WEBProf.infoZak += Proces;
            WEBProf.InfoBank += InfoBank;
          while (true)
            {
                Menu(pos, WEBProf.getbank().ToString(), meny);
                var key = Console.ReadKey();
                pos = menycount(key, meny.Length, pos);
                if (key.Key == ConsoleKey.Enter)
                {
                    switch (pos) { 
                        case 0:
                            
                            int pos1 = 0;
                            while (true)
                            {
                                Console.Clear();
                                Menu(pos1, WEBProf.getbank().ToString(), ListProf);
                                var key1 = Console.ReadKey();
                                pos1 = menycount(key1, ListProf.Count, pos1);
                                if (key1.Key == ConsoleKey.Enter)
                                {

                                    WEBProf.Add_sotrud(ListProf[pos1]);

                                }
                                else if (key1.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }

                                Console.Clear();
                            }
                            break;
                        case 1:
                            
                            int pos3 = 0;
                            while (true)
                            {
                                Console.Clear();
                                Menu(pos3, WEBProf.getbank().ToString(), WEBProf.get_sotrud());
                                var key1 = Console.ReadKey();
                                pos3 = menycount(key1, WEBProf.get_sotrud().Count, pos3);
                                if (key1.Key == ConsoleKey.Enter)
                                {

                                    WEBProf.Del_sotrud(pos3);

                                }
                                else if (key1.Key==ConsoleKey.Escape){
                                    break;
                                }


                                Console.Clear();
                            }
                            break;
                        case 2:
                           
                            int pos2 = 0;
                            while (true)
                            {
                                Console.Clear();
                                Menu(pos2, WEBProf.getbank().ToString(), BirzhRab.mas);
                                var key2 = Console.ReadKey();
                                pos2 = menycount(key2, BirzhRab.mas.Count, pos2);
                                
                                if (key2.Key == ConsoleKey.Enter)
                                {
                                    WEBProf.Add_zakaz(BirzhRab.mas[pos2]);

                                  
                                }
                                else if (key2.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }
                                Console.Clear();
                            }
                            break;
                        case 3:
                            if (WEBProf.StatZAK())
                            {
                                timer = new Timer(Go);
                                timer.Change(500, 5000);
                            }
                            else
                            {
                                Console.WriteLine("Нет заказа или не выполнен прошлый");
                                Console.ReadKey();
                            }
                            break;
                    }

                }
                Console.Clear();
          }



        }
        public static void Go(Object stateInfo)
        {
            WEBProf.Action();
            
        }


        public class Person
        {
            protected string name;
            protected int age;
            Person() {
                name = "";
                age=0;
            }
           protected Person(string N,int A) {
                name =N;
                age=A;
            }

        
        }

        public class Programmer : Person
        {
           private string skills;
           private int KPD;
           private int zarp;
           public Programmer(string N, int A, string S, int K,int Z)
               : base(N, A) {
                   skills = S;
                   KPD = K;
                   zarp = Z;
           
           }
            public int get_kpd(){
                return KPD;
            }
            public int get_zarpl()
            {
                return zarp;
            }
            public string get_skills() {
                return skills;
            }
            public string get_name()
            {
                return name;
            }

            static public List<Programmer> Insert()
            {
                List<Programmer> mas = new List<Programmer>();
                Random rand = new Random();
                string[] name = { "Александр", "Евгений", "Виктория", "Василий", "Артем", "Николай", "Светлана", "Елизавета", "Дмитрий", "Захар" };
                string[] prof = { "Верстальщик", "РНР-программер", "JavaScript-программер", "SEO-спец", "Дизайнер", "Универсал", "Руковдитель проекта", "Практикант" };
                for (int i = 0; i < rand.Next(5, 20); i++)
                {
                    Programmer temp = new Programmer(name[rand.Next(0, 10)], rand.Next(18, 40), prof[rand.Next(0, 8)], rand.Next(40, 150), rand.Next(600, 5000));
                    mas.Add(temp);
                }

                return mas;
            }
        }






        public class Birzha
        {
            public List<Zakaz> mas=new List<Zakaz>();
            Random rand = new Random();
            string[] rab = { "Сайт визитка","Калькулятор","Рефакторинг  сайта","Приложение на айфон","Сайт на WordPress",
                               "Исправление плагина","Интернет магазин","Перенос сайта","Верстка","Приложение на Андроид" };
            public Birzha() {
                for (int i = 0; i < rand.Next(3,20); i++) {
                    Zakaz temp = new Zakaz(rand.Next(500, 10000), rab[rand.Next(0, 10)], rand.Next(200, 1000));
                    mas.Add(temp);
                
                }


            }
        
        
        }

        public class Zakaz
        {
            private int stoim;
            private string nazw{ get; set; }
            public int slog;
            public int nomslog;
            bool status;
            public Zakaz() {
                stoim = 0;
                nazw = "Нет заказов";
                nomslog = slog = 0;
                status = false;
            
            }

            public Zakaz(int S, string N, int Sl) {
                stoim = S;
                nazw = N;
                nomslog = slog = Sl;
                status = true;
            
            }
            public bool get_status() {
                return status;
            }
            public int Proc(int kpd){
                slog -= kpd;
                if (slog <= 0)
                {
                    
                    nazw = "Нет заказов";
                    nomslog = slog = 0;
                    status = false;
                    timer.Dispose();
                    return stoim;
                }
                
                    

                    return 0;
                

                
            
            }
            public string info(){
                string info = nazw + " Стоимость: " +stoim+ ".  Процесс" + slog;
                return info;
            }
            //static  public List<Zakaz> Insert(){
            //public List<Zakaz> mas=new List<Zakaz>();
            //Random rand = new Random();
            //string[] rab = { "Сайт визитка","Калькулятор","Рефакторинг  сайта","Приложение на айфон","Сайт на WordPress",
            //                   "Исправление плагина","Интернет магазин","Перенос сайта","Верстка","Приложение на Андроид" };
           
            //}
        
        }
       
        public class FirmaEventArgs : EventArgs
        {
            public readonly string msg1;
          public readonly  List<Programmer>Sotr;
       
             public readonly Zakaz zakaz;
            public readonly int count;
            public readonly int proc;
            public readonly int nom;
            public readonly int bank;

            public FirmaEventArgs(string message, List<Programmer> S, Zakaz Z,int C)
            {
                msg1 = message;
                Sotr=S;
                zakaz = Z;
                count = C;

            }
            public FirmaEventArgs(string message, List<Programmer> S)
            {

                msg1 = message;
                Sotr = S;
                
            }
            public FirmaEventArgs(int P, int N)
            {

                proc = P;
                nom = N;


            }
            public FirmaEventArgs(int B)
            {

                bank = B;


            }

            public FirmaEventArgs(string message)
            {

                msg1 = message;
               

            }
        }
        private static void FirmaAddDellsotrud(object sender, FirmaEventArgs e)
        {
            Console.WriteLine(e.msg1);
            Console.WriteLine("Текущий список работников");
            foreach (Programmer item in e.Sotr)
            {
                Console.WriteLine(item.get_name() + " Специал: " + item.get_skills() + " Зарплата: " + item.get_zarpl() + " КПД-" + item.get_kpd());

            }
            Console.ReadKey();

        }
        private static void FirmaMesag(object sender, FirmaEventArgs e)
        {

            Console.WriteLine(e.msg1);
            Console.ReadKey();
        }
        private static void Proces(object sender, FirmaEventArgs e)
        {
            float p;
            float n;
            p = e.proc;
            n = e.nom;



            Console.WriteLine("Осталось "+(int)((p*100)/n)+"%");
            Console.ReadKey();
        }
        private static void InfoBank(object sender, FirmaEventArgs e)
        {

            Console.WriteLine(" Ваш счет"+e.bank);
         
        }

        class Firma
        {

            public delegate void FirmaEngineHandler(object sender, FirmaEventArgs e);

            public event FirmaEngineHandler addSotru;
            public event FirmaEngineHandler delSotru;
            public event FirmaEngineHandler StatusZak;
            public event FirmaEngineHandler infoZak;
            public event FirmaEngineHandler InfoBank;

           
            protected int Pow=0;
            private int bank_count;
            public List<Programmer> sotrudn = new List<Programmer>();
            private Zakaz Rabota;
            public Firma(int C) {
                Programmer Ya = new Programmer("Я", 30, "Boss", 75,1000);
                sotrudn.Add(Ya);
                Rabota = new Zakaz();
                bank_count = C;
                 
            }
            public void Add_sotrud(Programmer now){

                sotrudn.Add(now);
                    if (addSotru != null){
                    addSotru(this,new FirmaEventArgs(String.Format("Добавлен сотрудник "+now.get_name()), sotrudn));
            }
            }

            public void Del_sotrud(int pos)
            {

                sotrudn.RemoveAt(pos);
                if (addSotru != null)
                {
                    delSotru(this, new FirmaEventArgs(String.Format("Уволен сотрудник " + sotrudn[pos].get_name()), sotrudn));
                }

            }
            public void Add_zakaz(Zakaz Z) {

                if (Rabota.get_status())
                {
                    if (StatusZak != null)
                    {
                        StatusZak(this, new FirmaEventArgs(String.Format("Вы еще не закончили заказ " + Rabota.info())));
                        //event NO
                    }
                }
                else {
                    
                    Rabota = Z;
                    if (StatusZak != null)
                    {
                        StatusZak(this, new FirmaEventArgs(String.Format("Вы взяли заказ " + Rabota.info())));
                    }
                }
            
            }

            public int getbank() {
                return bank_count;

            }

           public  void Action() {


               if (Rabota.get_status())
               {
                   if (bank_count < 0) {
                       StatusZak(this, new FirmaEventArgs(String.Format("Вы банкрот!!!")));
                       return;
                   }
                   for (int i = 0; i < sotrudn.Count; i++)
                   {
                       Pow += sotrudn[i].get_kpd();
                   }
                   int zarpl = 0;
                   for (int i = 0; i < sotrudn.Count; i++)
                   {
                       zarpl += sotrudn[i].get_zarpl();
                   }
                   bank_count -= zarpl;
                   int den = Rabota.Proc(Pow);
                   Pow = 0;
                   InfoBank(this, new FirmaEventArgs(bank_count));

                   if (den > 0)
                   {


                       bank_count += den;
                       if (StatusZak != null)
                       {
                           StatusZak(this, new FirmaEventArgs(String.Format("Вы выполнили заказ и получили " + den)));
                           //EVENT
                       }
                   }
                   else
                   {
                       if (infoZak != null)
                       {
                           infoZak(this, new FirmaEventArgs(Rabota.slog, Rabota.nomslog));
                       }
                   }

               }
            }
           public bool StatZAK() {
               return Rabota.get_status();

           }
         
            public List<Programmer> get_sotrud()
            {
                return sotrudn;
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
        private static void Menu(int pos, string inf, List<Programmer> m)
        {
            Console.WriteLine("[esc]- Назад");
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine();
            var curColor = Console.BackgroundColor;
            for (int i = 0; i < m.Count; i++)
            {
                if (i == pos)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(m[i].get_name()+" Специал: "+m[i].get_skills()+" Зарплата: "+m[i].get_zarpl()+" КПД-"+m[i].get_kpd());
                    Console.BackgroundColor = curColor;
                }
                else
                {
                    Console.WriteLine(m[i].get_name() + " Специал: " + m[i].get_skills() + " Зарплата: " + m[i].get_zarpl() + " КПД-" + m[i].get_kpd());
                }
            }
            Console.WriteLine();
            Console.WriteLine("______________________________________________________________");

            Console.WriteLine(inf);
        }
         private static void Menu(int pos, string inf, List<Zakaz> m)
        {
            Console.WriteLine("[esc]- Назад");
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine();
            var curColor = Console.BackgroundColor;
            for (int i = 0; i < m.Count; i++)
            {
                if (i == pos)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(m[i].info());
                    Console.BackgroundColor = curColor;
                }
                else
                {
                    Console.WriteLine(m[i].info());
                }
            }
            Console.WriteLine();
            Console.WriteLine("______________________________________________________________");

            Console.WriteLine(inf);
        }


    }
}
