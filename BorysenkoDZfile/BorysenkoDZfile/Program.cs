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

namespace BorysenkoDZfile
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\shipStaff.bin";
            string[] menu = {"Добавить","Удалить","Посмотреть" };
            List<Ship> port=new List<Ship>();

                           Ship temp = new Ship("Yoko",4);
                           port.Add(temp);
                            temp=new Ship("Linkoln",3);
                            port.Add(temp);
                            temp=new Ship("Nahimov",2);
                            port.Add(temp);
                            temp=new Ship("Avrora",2);
                            port.Add(temp);
                            temp=new Ship("York",1);
                            port.Add(temp);
                            temp=new Ship("Shimo",3);
                            port.Add(temp);
                            temp=new Ship("Strim",1);
                            port.Add(temp);
                            temp=new Ship("Golt",4);
                            port.Add(temp);
                            List<Ship> flot = new List<Ship>();
                            

            int pos1 = 0;
            while (true) {
                Menu(pos1, "", menu);
                var key1 = Console.ReadKey();
                pos1 = menycount(key1, menu.Length, pos1);

                if (key1.Key == ConsoleKey.Enter)
                {
                    switch (pos1) { 
                        case 0:
                            int pos2 = 0;
                            while (true) {
                                Menu(pos2, "", port);
                                var key2 = Console.ReadKey();
                                pos2 = menycount(key2, port.Count, pos2);
                                if (key2.Key == ConsoleKey.Enter)
                                {

                                    flot.Add(port[pos2]);
                                    flot[flot.Count - 1].set_ekep();
                                   

                                    try{

                                        using (BinaryWriter w = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate))) { 
                                        foreach(Ship s in flot){
                                            w.Write(s.name);
                                            w.Write(s.palub);
                                            w.Write(s.person);
                                            w.Write(s.snar);
                                        
                                        }
                                        
                                        }
                                    
                                    }catch(Exception e){
                                        Console.WriteLine(e.Message);
                                    }
                                   


                                }
                                else if (key2.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }

                                Console.Clear();
                            }
                            break;
                        case 1:

                            try
                            {
                                flot = new List<Ship>();
                                
                            
                                using (BinaryReader r=new BinaryReader(File.Open(path, FileMode.OpenOrCreate)))
                                {
                                    while (r.PeekChar() > -1) {
                                        var ship = new Ship(r.ReadString(), r.ReadInt32());
                                      
                                        ship.person = r.ReadInt32();
                                        ship.snar = r.ReadInt32();

                                        flot.Add(ship);
                                    }

                                }

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            int pos4 = 0;
                            while (true) {
                                Menu(pos4, "", flot);
                                var key4 = Console.ReadKey();
                                pos4 = menycount(key4, flot.Count, pos4);
                                if (key4.Key == ConsoleKey.Enter)
                                {
                                    flot.RemoveAt(pos4);
                                    try
                                    {

                                        using (BinaryWriter w = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                                        {
                                            foreach (Ship s in flot)
                                            {
                                                w.Write(s.name);
                                                w.Write(s.palub);
                                                w.Write(s.person);
                                                w.Write(s.snar);

                                            }

                                        }

                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                   
                                }
                                else if (key4.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }

                                Console.Clear();
                            }

                            break;
                        case 2:
                            int pos3 = 0;
                            try
                            {
                                flot = new List<Ship>();

                                using (BinaryReader r = new BinaryReader(File.Open(path, FileMode.OpenOrCreate)))
                                {
                                    while (r.PeekChar() > -1)
                                    {
                                        var ship = new Ship(r.ReadString(), r.ReadInt32());
                                        
                                        ship.person = r.ReadInt32();
                                        ship.snar = r.ReadInt32();

                                        flot.Add(ship);
                                    }

                                }

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }


                            while (true) {
                                Menu(pos3, "", flot);
                                var key3 = Console.ReadKey();
                                pos3 = menycount(key3, flot.Count, pos3);
                                if (key3.Key == ConsoleKey.Enter)
                                {

                                    flot[pos3].Show();
                                }
                                else if (key3.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }

                                Console.Clear();
                            }
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



        class Ship {
            public string name;
            public int palub;
            public int person;
            public int snar;
            //public Ship() {
            //    name = "";
            //    palub = 0;
            //    person = 0;
            //    snar = 0;
            
            //}
            public Ship(string N, int P)
            {
                name = N;
                palub = P;
                person = 0;
                snar = 0;

            }


            public void set_ekep() {
                Console.WriteLine("Введите количество экипажа : ");
                int ekip=0;
                while (!int.TryParse(Console.ReadLine(), out ekip)) ;
                person = ekip;
               
               
                    Console.WriteLine("Введите количество снарядов : ");
                    int s = 0;
                    while (!int.TryParse(Console.ReadLine(), out s)) ;
                    snar = s;
                
            }

            public override string ToString()
            {
                return "Название корабля:"+name;
            }
            public void Show() {

                Console.WriteLine("Название корабля:"+name+" Количество палуб: "+palub+"\n Экипаж: "+person+" чел. Снаряды: "+snar);
                Console.ReadKey();
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
        private static void Menu(int pos, string inf, List<Ship> m)
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
                    Console.WriteLine(m[i].ToString());
                    Console.BackgroundColor = curColor;
                }
                else
                {
                    Console.WriteLine(m[i].ToString());
                 
                }
            }
            Console.WriteLine();
            Console.WriteLine("______________________________________________________________");

            Console.WriteLine(inf);
        }
    }
}
