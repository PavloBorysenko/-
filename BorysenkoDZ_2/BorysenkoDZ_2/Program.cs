using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BorysenkoDZ_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int m=-1;
            Poezd obj = new Poezd("146-Д");
            while(m!=0 )
            {
                Console.WriteLine("1-Добавить вагон. 2-убрать вагон. 3-добавить пассажиров. 4-отнять пасcажиров. \n 5-убрать вагон по номеру. 6-Езда");
                
                while (!int.TryParse(Console.ReadLine(), out m)) ;
               
                switch(m)
                {   
                    case 1:
                    obj.AddVag();
                    
                      break;
                    case 2:
                      obj.MenVag();
                     
                      break;   
                    case 3:
                        int N, S;
                        Console.WriteLine("Введите номер вагона: ");
                        while (!int.TryParse(Console.ReadLine(), out N)) ;
                        Console.WriteLine("Введите количество  пассажиров: ");
                        while (!int.TryParse(Console.ReadLine(), out S)) ;
                        obj.AddPasPZD(N, S);
                      break;
                    case 4:
                      
                          int Nu, Su;
                          Console.WriteLine("Введите номер вагона: ");
                          while (!int.TryParse(Console.ReadLine(), out Nu)) ;
                          Console.WriteLine("Введите количество  пассажиров: ");
                          while (!int.TryParse(Console.ReadLine(), out Su)) ;
                              obj.MenPasPZD(Nu, Su);
                      
                      break;
                    case 5:
                      int v;
                        Console.WriteLine("Введите номер вагона: ");
                        while (!int.TryParse(Console.ReadLine(), out v)) ;
                      obj.MenVag(v);
                      
                      break;
                    case 6:
                      obj.Ezda();
                      break;
               }
              


            }
        }

        public class vag {
            public int pas = 0;
            public double Kz = 0;
            public int num = 0;

            public void AddPas(int p) {
                if (pas + p <= 36) {
                    pas += p;

                    Kz = 0.027 * (double)pas;
                  //Console.WriteLine("{0:f2}", Kz);
                }
                else Console.WriteLine("**************Столько мест нет!**********\n");
            }
            public void MenPas(int p) {
                if (pas - p >= 0) {
                   
                    pas -= p;
                    Kz = 0.027 * (double)pas;
                }
                else Console.WriteLine("************В вагоне нет столько пасcажиров!**********\n");
            }
        }

        public class Poezd { 
       public vag[] PZD=new  vag[12];
     double Pow = 0;
        int Spd = 0,Sum=0;
        string Name="";
        public Poezd(string name) {
            Name = name;
        
        }
        public void AddPasPZD(int n ,int kol) {
            if (PZD[n - 1] != null) {
                PZD[n - 1].AddPas(kol);
            }
            else Console.WriteLine("Такого вагона нет!\n");
        
        }
        public void MenPasPZD(int n, int kol)
        { 
            if (PZD[n - 1] != null)
            {
               
                PZD[n - 1].MenPas(kol);
            }
            else Console.WriteLine("Такого вагона нет!\n");

        }
        public void AddVag() {
            int ind = 0;
            for (int i = 0; i < PZD.Length; i++) {
                if (PZD[i] == null) { 
                vag temp=new vag();
                Console.WriteLine("Сколько пассажиров посадить?");
                int pas = int.Parse(Console.ReadLine());
                temp.AddPas(pas);
                temp.num = i + 1;
                PZD[i] = temp;
                ind++;
                Console.WriteLine("ВАГОН ДОБАВЛЕН!");
                break;
                }
            }
            if (ind == 0) { Console.WriteLine("Состав укомплектован!!!\n"); }
        }
        public void MenVag() {
            int ind = 0;
            for (int i = PZD.Length-1; i >= 0; i--) {
                if (PZD[i] != null) {
                    PZD[i] = null;
                    ind++;
                    Console.WriteLine("ВАГОН УБРАН!");
                    break;
                }
            }
            if (ind == 0) Console.WriteLine("В составе нет ни одного вагона!\n");
        }
        public void MenVag(int n) {
            if (n <= 12 && n > 0) {
                if (PZD[n - 1] != null)
                {
                    PZD[n - 1] = null;
                    Console.WriteLine("ВАГОН УБРАН!");
                }
                else Console.WriteLine("Такого вагона нет!\n");
            }
            else Console.WriteLine("Не корректное значение!\n");
        }
        public void Ezda() {
            Sum = 0;
            Pow = 0;
            int person = 0;
            foreach (vag item in PZD) {
                if (item != null) { 
                    Sum++;
                    Pow += item.Kz * 10 + 10.0;
                   // Console.WriteLine(item.Kz);
                    person += item.pas;
                    Console.Write("[№:{0} Пас:{1}]*",item.num,item.pas);
                }
            }
            Spd =(int)(60+60 *(1-(Pow / 240)));
            Console.WriteLine("\n");
            Console.WriteLine("Поезд №:"+Name);
            Console.WriteLine("Количество вагонов:"+Sum);
            Console.WriteLine("Количество пасcажиров:" + person);
            Console.WriteLine("Потребляемая мощность: " + Pow+"*10 кВт");
            Console.WriteLine("Максимальная скорость:" + Spd+" км/ч");
            Console.WriteLine("*****************************************");
            Console.WriteLine("\n");
}

        }
    }
}
