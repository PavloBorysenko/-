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

namespace BorysenkoKlassFirma
{
    class Program
    {
        static Firma googl = new Firma();
        static void Main(string[] args)
        {
            googl.zarplata += OnFirmaEngineEvent;
            googl.Otchot += OnFirmaEngineEvent;
            googl.DenRog += OnFirmaEngineEvent;

            while (true) {
                Console.WriteLine("Нажмите любую клавишу для перехода на следующий месяц");
                Console.WriteLine();
               
                googl.Zarp();
                Console.ReadKey();
                Console.Clear();
            }

        }
        public struct Person {
           public string name;
           public int dr;
        }
        private static void OnFirmaEngineEvent(object sender, FirmaEventArgs e)
        {
            Console.WriteLine(" => {0}", e.msg);
           
        }
        public class FirmaEventArgs : EventArgs
        {
            public readonly string msg;


            public FirmaEventArgs(string message)
            {
                msg = message;
                
            }
        }

        class Firma {

            public delegate void FirmaEngineHandler(object sender, FirmaEventArgs e);

            public event FirmaEngineHandler zarplata;
            public event FirmaEngineHandler Otchot;
            public event FirmaEngineHandler DenRog;

            static int i = 1;
            public int year = 2015;
        public List<Person> mas=new List<Person>();
        public Firma() { 
        Person p1=new Person();
            p1.name="Dima";
            p1.dr = 3;
            Person p2 = new Person();
            p2.name = "Zina";
            p2.dr = 8;
            Person p3 = new Person();
            p3.name = "Vasya";
            p3.dr = 7;
            Person p4 = new Person();
            p4.name = "Frunze";
            p4.dr = 12;
            mas.Add(p1);
            mas.Add(p2);
            mas.Add(p3);
            mas.Add(p4);
        }
        public void Zarp() {

          
            if (i > 12) {
                i = 1;
                ++year;
            }
            
            foreach(Person item in mas){
                if (zarplata != null)
                {

                    zarplata(this, new FirmaEventArgs(item.name + " получил 1000 за " + (i) + " месяц  "+ year+" г."));
                }
                if (i % 3 == 0) {
                    if (Otchot != null)
                    {

                        Otchot(this, new FirmaEventArgs(item.name + " отчет сдал"));
                    }
                
                }
                if (i == item.dr)
                {
                    if (DenRog != null)
                    {

                        DenRog(this, new FirmaEventArgs(item.name + " С Днем Рождения!!!!"));
                    }

                }

            
             
            }
        i++;
        
        }
        
        
        }


    }
}
