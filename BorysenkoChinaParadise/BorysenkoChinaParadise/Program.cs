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
namespace BorysenkoChinaParadise
{
    class Program
    {

        
        static int ID = 0;
        static Timer timer;
        static  Linia linia = new Linia();
        static  Sclad sclad_coml = new Sclad();
        static  Sclad_ustr sclad_ustr = new Sclad_ustr();
        static Proizv startProiz = new Proizv();
        static bool yes = true;
        static List<Statistic> All_st = new List<Statistic>();
        static List<Statistic> toDay_st = new List<Statistic>();
        static string path = @"D:\statistic.bin";
        static string path_c = @"D:\compl.bin";
        static string path_u = @"D:\Ustr.bin";
        static void Main(string[] args)
        {
           // Console.WriteLine(DateTime.Today); 


            FileMen.Get_statistic();
           
           List<Ustroist> Total_ustr = new List<Ustroist>();
         List<Compl> Total_compl = new List<Compl>();
         FileMen.Get_info_c(Total_compl);
         FileMen.Get_info_u(Total_ustr);
         startProiz.Stop_conveer += Eve_net_compl;
         startProiz.Prod_compl += Eve_complet_ustr;
         startProiz.Stop_linea += Eve_net_zak;
         startProiz.Norma_kompl += Eve_complet_linea;
         startProiz.Sklad_full += Eve_full_sclad;
         startProiz.Stop_sluch += Eve_sluch;
          

            string[] menu = {  "Отдел ИТР [Просмотр/Создать/Pедактировать]", "Производтво","Склад комплектующих","Склад готовой продукции" };
            string[] menu_proiz = { "Запустить", "Остановить", "Создать линию", "Вся статистика ","Статистика сегодня" };
            string[] menu_com_uctr = { "Комплектующие", "Устройство" };
            int pos = 0;
            while (true)
            {
                Menu(pos, "", menu);
                var key = Console.ReadKey();
                pos = menycount(key, menu.Length, pos);


                if (key.Key == ConsoleKey.Enter)
                {
                    switch (pos) { 
                        case 0:
                            Console.Clear();
                            int pos1 = 0;
                            while (true)
                            {
                                Menu(pos1, "", menu_com_uctr);
                                var key1 = Console.ReadKey();
                                pos1 = menycount(key1, menu_com_uctr.Length, pos1);

                                if (key1.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    switch (pos1) { 
                                        case 0:
                                            int pos2 = 0;
                                            while (true)
                                            {
                                                string info = " Tab-создать комлектующую.\n Enter-редоктировать комплектующую.";
                                                Menu(pos2,info, Total_compl);
                                                var key2 = Console.ReadKey();
                                                pos2 = menycount(key2, Total_compl.Count, pos2);
                                                if (key2.Key == ConsoleKey.Enter)
                                                {
                                                    Total_compl[pos2].edit_coml();
                                                    FileMen.Set_info_c(Total_compl);
                                                }
                                                else if (key2.Key == ConsoleKey.Tab)
                                                {
                                                    Total_compl.Add(Compl.crear());
                                                    FileMen.Set_info_c(Total_compl);
                                                }
                                                else if (key2.Key == ConsoleKey.Escape)
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
                                                string info = "Space-Просмотр\n Tab-создать изделие\n Enter-редоктировать изделие.";
                                                Menu(pos3, info,Total_ustr);
                                                var key3 = Console.ReadKey();
                                                pos3 = menycount(key3, Total_ustr.Count, pos3);
                                                if (key3.Key == ConsoleKey.Enter)
                                                {
                                                    Console.WriteLine("Введите новое название изделия ");
                                                    string n = Console.ReadLine();
                                                    Total_ustr[pos3].edit_ustr(n, Compl.edit_compl(Total_compl, Total_ustr[pos3].complet));
                                                    FileMen.Set_info_u(Total_ustr);
                                                }
                                                else if (key3.Key == ConsoleKey.Tab)
                                                {
                                                    Console.WriteLine("Введите название изделия ");
                                                    string n = Console.ReadLine();
                                                    List<Compl> temp=new List<Compl>();
                                                    Ustroist now=new Ustroist( Compl.chois(Total_compl,temp),n);
                                                    Total_ustr.Add(now);
                                                    FileMen.Set_info_u(Total_ustr);
                                                }
                                                else if (key3.Key == ConsoleKey.Spacebar)
                                                {
                                                    Total_ustr[pos3].Print();
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

                            break;
                        case 1:
                             Console.Clear();
                            int pos_proiz = 0;
                            while (true)
                            {
                                Menu(pos_proiz, "", menu_proiz);
                                var key_proiz = Console.ReadKey();
                                pos_proiz = menycount(key_proiz, menu_proiz.Length, pos_proiz);
                               // pos_proiz = menycount(key_proiz , menu_proiz.Length, pos_proiz);

                                if (key_proiz.Key == ConsoleKey.Enter)
                                {
                                    switch (pos_proiz) { 
                                        case 0:
                                            try {
                                                if (yes)
                                                {
                                                    timer = new Timer(Start_proc);
                                                    timer.Change(500, 5000);
                                                    yes = false;
                                                }
                                                else {
                                                    Console.WriteLine("Конвеер уже работает!!!");
                                                }
                                    
                                            }
                                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                                            Console.ReadKey();
                                            break;
                                        case 1:
                                            try {

                                                timer.Dispose();
                                                yes = true;
                                            }
                                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                                            Console.ReadKey();
                                            break;
                                        case 2:
                                            linia.creat_linea(Total_ustr);
                                            break;
                                        case 3:
                                            double t = 0;
                                            foreach (Statistic item in All_st) {
                                                Console.WriteLine(item);
                                                t += item.price;
                                            }
                                            Console.WriteLine("Всего произ товров на "+t);
                                            Console.ReadKey();
                                            break;
                                        case 4:
                                            double t1 = 0;
                                            foreach (Statistic item in toDay_st) {
                                                Console.WriteLine(item);
                                                t1 += item.price;
                                            }
                                            Console.WriteLine("Сегодня произ товров на " + t1);
                                            Console.ReadKey();


                                            break;
                                    
                                    }
                                }
                                else if (key_proiz.Key == ConsoleKey.Escape)
                                {
                                    break;
                                }

                                Console.Clear();
                            }


                            break;
                        case 2:

                                            int pos4 = 0;
                                            while (true)
                                            {
                                                Console.Clear();
                                                string info = "Tab-Пополнить склад комплектующих. ";
                                                Menu(pos4, info,sclad_coml.sclad);
                                                var key4 = Console.ReadKey();
                                                pos4 = menycount(key4, Total_ustr.Count, pos4);
                                                if (key4.Key == ConsoleKey.Tab)
                                                {

                                                    int chois=Compl.Get_comlect(Total_compl);
                                                    if (chois != -1)
                                                    {
                                                        int quant = 0;
                                                        Console.WriteLine("Сколько единиц? ");
                                                        while (!int.TryParse(Console.ReadLine(), out quant)) ;
                                                        for (int i = 0; i < quant;i++ )
                                                        {
                                                            sclad_coml.Add_sclad(Total_compl[chois]);
                                                        }
                                                    }
                                                    else {
                                                        Console.WriteLine("Вы не выбрали ни одной комплектужщей!!!");
                                                        Console.ReadKey();
                                                    }


                                                 } 
                                                else if (key4.Key == ConsoleKey.Escape)
                                                {
                                                    break;
                                                }


                                                Console.Clear();
                                            }
                            break;
                        case 3:
                          
                            sclad_ustr.Vende_ustr();
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
      
        
         
   

        
        public static void Start_proc(Object stateInfo)
        {
         
         


            startProiz.Start();

        }
        class Compl {
            public string name { get; set; }
            public double price { get; set; }
            protected int id;
            DateTime Tim = new DateTime();

       

            public Compl(string N, double P) {
                name = N;
                price = P;
                
                id = (ID++) + Tim.Second;
            }
            public Compl(string N, double P,int I)
            {
                name = N;
                price = P;

                id = I;
            }
            public Compl() {
                name = "";
                price = 0;
                
                id = (ID++) +Tim.Second;
            }
            public int get_id(){
            return id;
            }
            static public Compl crear() {
                Console.WriteLine("Введите название комплектующей ");
                string n=Console.ReadLine();
                Console.WriteLine("Введите цену комплектующей ");
                double p = 0;
                while (!double.TryParse(Console.ReadLine(), out p)) ;
                Compl temp =new Compl(n,p);
                return temp;
            
            }
            public void edit_coml(){
                Console.WriteLine("Введите   новое название комплектующей ");
                string n = Console.ReadLine();
                Console.WriteLine("Введите новую цену комплектующей ");
                double p = 0;
                while (!double.TryParse(Console.ReadLine(), out p)) ;
                name = n;
                price = p;
  
            }
            static public List<Compl> edit_compl(List<Compl> compl, List<Compl> temp)
            {
                int pos = 0;
                while (true)
                {
                    Console.Clear();
                    Menu(pos, "Комлектующие вашего устройства\nEnter-добавить комплектующую.\nBackspace-Удалить комплектующую", temp);
                    var key = Console.ReadKey();
                    pos = menycount(key, temp.Count, pos);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        temp = Compl.chois(compl, temp);
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        temp.RemoveAt(pos);
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }


                    Console.Clear();

                }
           
            

                return temp;
            }


            static public int Get_comlect(List<Compl> temp) {
                int pos = 0;
                while (true)
                {
                    Console.Clear();
                    Menu(pos, "Выберите комплектующие для пополнения склада", temp);
                    var key = Console.ReadKey();
                    pos = menycount(key, temp.Count, pos);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        return pos;
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        
                        break;
                    }


                    Console.Clear();
                }
                return -1;
            }


            static public List<Compl> chois(List<Compl> compl, List<Compl> temp)
            {
                List<Compl> listCom=new List<Compl>();
                foreach (Compl item in compl) {
                    listCom.Add(item);
                }
                //List<Compl> temp = new List<Compl>();
                    int pos = 0;
                    while (true)
                    {
                        Console.Clear();
                        Menu(pos, "Выберите комплектующие для этого устройства", listCom);
                        var key = Console.ReadKey();
                        pos = menycount(key, listCom.Count, pos);

                        if (key.Key == ConsoleKey.Enter)
                        {
                            temp.Add(listCom[pos]);
                            Console.WriteLine(listCom[pos]+" добавлен в устройство.");
                            Console.ReadKey();
                            listCom.RemoveAt(pos);
                        }
                        else if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }


                        Console.Clear();

                    }
                    return temp;
            
            }
            public override string ToString()
            {
                return "Название: " + name + " Цена: " + price;
            }

        }
        class Real_compl:Compl{
        public int count;
            public Real_compl(Compl temp){
                name=temp.name;
                price=temp.price;
                id=temp.get_id();
                count=1;
            }
        public bool Igul(int I){
            try{
           if(id==I){
               count++;
           return true;
           }else{
           return false;
           }
            }catch(Exception e){
            Console.WriteLine(e.Message);
                Console.ReadKey();
                return false;
            }
        }
        public override string ToString()
        {
            return "Название: " + name + " Количество: " +count;
        }
       
        }
        class Sclad {
           public List<Real_compl> sclad =new List<Real_compl>();
           public int count_sclad = 10;
           int max = 10000;

           public void Del_compl(int id_d){
               for (int i = 0; i < sclad.Count; i++) {
                   if (sclad[i].get_id() == id_d) {
                       sclad.RemoveAt(i);
                   }
               }
           }
           public bool Get_compl(int Id) {

               for (int i = 0; i < sclad.Count; i++) {
                   if (sclad[i].get_id() == Id) {

                       sclad[i].count--;
                       if (sclad[i].count <= 0) {
                           sclad.RemoveAt(i);
                       }
                       return true;

                   }

               
               }
               return false;
               ////int i_count = 0;
               //foreach (Real_compl item in sclad) {
               //    if (item.get_id() == id)
               //    {
               //        --item.count;
               //        if (item.count == 0)
               //        {
               //            Del_compl(id);
               //        }
               //        return true;
               //    }
               //    //else {
               //    //    i_count++;
               //    //}
               
               //}
               ////if (i_count >= sclad.Count) {
               ////    return false;
               ////}
               //return false;
           } 
           public void Add_sclad(Compl temp) {
               int i = 0;
               foreach (Real_compl item in sclad) {
                   if (item.Igul(temp.get_id()))
                   {
                       if (count_sclad > max)
                       {
                           //trow event
                           break;
                       }
                       else
                       {
                           count_sclad++;
                           break;
                       }
                   }
                   else {
                       i++;
                   }
                  
               
               }
               if (i >= sclad.Count) {
                   if (count_sclad > max)
                   {
                       //trow event

                   }
                   else {
                       Real_compl T = new Real_compl(temp);
                       sclad.Add(T);
                       count_sclad++;
                   }
               }
           
           }


        
        }
        class Norma {
            public int id;
            public int quant;
            public Norma(int I, int N) {
                id = I;
                quant = N;
            }

        }
        class Linia {
            public List<Ustroist> linea = new List<Ustroist>();
            public List<Norma> norm = new List<Norma>();
            public void creat_linea(List<Ustroist> Mas) { 
                List<Ustroist> mas=new List<Ustroist>();
                foreach (Ustroist item in Mas) {
                    mas.Add(item);
                }

                    int pos = 0;
                    string info = "";
                    while (true)
                    {
                        info = "Стоит в производстве: \n";
                        foreach (Ustroist item in linea)
                        {
                            info+=item+" В очериди на произ:"+get_count(item.get_id())+"\n";
                        }
                        Console.Clear();
                        Menu(pos, info,mas);
                        var key = Console.ReadKey();
                        pos = menycount(key, mas.Count, pos);

                        if (key.Key == ConsoleKey.Enter)
                        {
                            if (mas.Count != 0)
                            {
                                Console.WriteLine("Введите количество для производства ");
                                int nor = 0;
                                while (!int.TryParse(Console.ReadLine(), out nor)) ;

                                Norma temp = new Norma(mas[pos].get_id(), nor);

                                norm.Add(temp);

                                linea.Add(mas[pos]);
                                mas.RemoveAt(pos);
                            } Console.WriteLine("НЕТ Устроиств!");
                        }
                        else if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }


                        Console.Clear();
                    }

            }
            public void linea_null(int I) {
                //for (int i = 0; i < linea.Count; i++)
                //{
                //    if (id == linea[i].get_id())
                //    {
                //        linea.RemoveAt(i);
                //    }
                //}
                for (int i = 0; i < norm.Count; i++)
                {
                    if (I == norm[i].id)
                    {

                        norm[i].quant = 0;
                    }
                }
            }
            public int get_count(int I) {
               
                foreach (Norma n in norm) {
                    if (n.id == I) {

                        return n.quant;
                    }
                }
                return 0;
            }
            public bool men_norm(int Id)
            {

                for (int i = 0; i < norm.Count; i++)
                {
                    if (norm[i].id == Id)
                    {

                        norm[i].quant--;
                        if (norm[i].quant <= 0)
                        {
                            norm.RemoveAt(i);
                        }
                        return true;

                    }


                }
                return false;
            }
            //public void men_norm(int Id) {
                
            //    for (int i = 0; i < norm.Count; i++) {

            //        if (norm[i].id == Id)
            //        {
            //            norm[i].quant--;

            //        }
                    
            //    }
               
            //}
            public void stop(int id) {
                for (int i = 0; i < linea.Count; i++)
                {
                    if (id == linea[i].get_id())
                    {
                        linea.RemoveAt(i);
                    }
                }
                for (int i = 0; i < norm.Count; i++)
               {
                   if (id == norm[i].id)
                    {
                      norm.RemoveAt(i);
                  }
              }
            
            }
        }

        class Ustroist { 
        public List<Compl> complet=new List<Compl>();
        public string name { get; set; }
        public double total_pr { get; set; }
        private int count = 0;
        protected int id;
        DateTime Tim = new DateTime();
          public Ustroist(List<Compl>C,string N) {
              complet = C;
              name = N;
              total_pr = 0;
              foreach (Compl item in complet) {
                  total_pr += item.price;
                  count++;

              }
              id = (ID++) + Tim.Second;
          }
          public Ustroist() {
              name = "";
              id = (ID++) + Tim.Second;
          }
          public int get_id() {
              return id;
          }
          public void edit_ustr(string N){
              name = N;
          }
          public void edit_ustr(Compl C) {
              complet.Add(C);
              total_pr += C.price;
              count++;
          }
          public void edit_ustr(int pos)
          {
              double temp=complet[pos].price;
              total_pr -= temp;
              complet.RemoveAt(pos);
              count--;
              
          }
          public void edit_ustr(string n, List<Compl> l) {
              name = n;
              count = 0;
              total_pr = 0;
              foreach (Compl item in l)
              {
                  total_pr += item.price;
                  count++;

              }
              complet = l;
          }
         

          public override string ToString()
          {
              return "Название: " + name + " Цена: " + total_pr+" Всего комплектующих: "+count;
          }
          public void Print() {
              Console.WriteLine("Изделие: "+name);
              Console.WriteLine("Состоит из");
              foreach (Compl item in complet) {
                  Console.WriteLine(item);
              
              }
              Console.WriteLine("Цена: "+total_pr);
              Console.ReadKey();

          }  

            
        }

        class Real_ustr : Ustroist
        {
            public int count;
            public Real_ustr(Ustroist temp)
            {
                name = temp.name;
                total_pr = temp.total_pr;
                id = temp.get_id();
                foreach (Compl item in temp.complet)
                {
                    complet.Add(item);
                }
                count = 1;
            }
            public bool Igul(int I)
            {
                try
                {
                    if (id == I)
                    {
                        count++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    return false;
                }
            }
            

            public override string ToString()
            {
                return "Название: " + name + " Количество: " + count;
            }
        }



        class Sclad_ustr
        {
            public List<Real_ustr> sclad = new List<Real_ustr>();
            public int count_sclad = 0;
            int max = 100;


            public bool free() {
                if (max <= count_sclad) {
                    return false;
                }else
                return true;

            }

            public void Add_sclad(Ustroist temp)
            {
                int i = 0;
                foreach (Real_ustr item in sclad)
                {
                    if (item.Igul(temp.get_id()))
                    {
                        if (count_sclad > max)
                        {
                            //trow event
                            break;
                        }
                        else
                        {
                            count_sclad++;
                            break;
                        }
                    }
                    else
                    {
                        i++;
                    }


                }
                if (i >= sclad.Count)
                {
                    if (count_sclad > max)
                    {
                        //trow event

                    }
                    else
                    {
                        Real_ustr T = new Real_ustr(temp);
                        sclad.Add(T);
                        count_sclad++;
                    }
                }

            }
            public void Vende_ustr()
            {
                Console.Clear();
                 int pos = 0;
                 while (true)
                 {
                     Console.Clear();
                     string info = "Выберите что продать\n Свободного места на складе"+(max-count_sclad);
                     Menu(pos, info, sclad);
                     var key = Console.ReadKey();
                     pos = menycount(key, sclad.Count, pos);

                     if (key.Key == ConsoleKey.Enter)
                     {
                         Console.WriteLine("Вы получили "+sclad[pos].count*sclad[pos].total_pr+"");
                         Console.ReadKey();
                         count_sclad -= sclad[pos].count;
                         sclad.RemoveAt(pos);
                     }
                     else if (key.Key == ConsoleKey.Escape)
                     {
                         break;
                     }


                     Console.Clear();
                 }


            }


        }
        class FirmaEventArgs : EventArgs
        {
            public readonly string msg;
            public readonly Ustroist ustr;
            public readonly int sluch;
            public readonly Linia linea_eve;
           


            public FirmaEventArgs(string str,  Ustroist u )
            {
                msg = str;
                ustr = u;
            }
            public FirmaEventArgs(string str)
            {
                msg = str;
               
            }

            public FirmaEventArgs(string str, Ustroist u,Linia l)
            {
                msg = str;
                ustr = u;
                linea_eve = l;
            }
            public FirmaEventArgs(int s, Ustroist u, Linia l)
            {
                sluch = s;
                ustr = u;
                linea_eve = l;
            }


        }

        private static void Eve_net_compl(object sender, FirmaEventArgs e) {
           
            Console.WriteLine("Конвеер  по прозводству "+e.ustr.name+" остановлен по причине ");
            Console.WriteLine(e.msg);
            Console.WriteLine();
           // timer.Dispose();
          //  yes = true;
           // Console.WriteLine("Конвеер стоп");
            //Console.ReadKey();
        }
        private static void Eve_complet_ustr(object sender, FirmaEventArgs e)
        {
            //seif statis
            Statistic temp = new Statistic(DateTime.Today.ToString("D")+"  "+e.ustr.name, e.ustr.total_pr);
            toDay_st.Add(temp);
            All_st.Add(temp);
            FileMen.Set_statistic();
            Console.WriteLine(e.msg);
          
        }
        private static void Eve_complet_linea(object sender, FirmaEventArgs e)
        {   //seif statis
            //e.linea_eve.stop(e.ustr.get_id());
            Console.WriteLine(e.msg);
            Console.WriteLine();
        }
        private static void Eve_full_sclad(object sender, FirmaEventArgs e)
        {
            e.linea_eve.linea.Clear();
            e.linea_eve.norm.Clear();
            ////stop timer
            timer.Dispose();
            Console.WriteLine("Конвеер стоп");
            yes = true;
            Console.WriteLine(e.msg);
            Console.WriteLine();
            
        }
        private static void Eve_net_zak(object sender, FirmaEventArgs e)
        {
            try
            {
                //stop timer
                timer.Dispose();
                Console.WriteLine("Конвеер стоп");
                yes = true;
                //Console.ReadKey();
                Console.WriteLine(e.msg);
                Console.WriteLine();
               
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
        private static void Eve_sluch(object sender, FirmaEventArgs e)
        {
            string[] sl = { "Хинь Суаню упал на ногу бутерброт, мы останавливаем конвеер с ",
                              "Этот продукт не прошол стандарты китайского качества, снимаем с производства ",
                              "Что бы вы не делали получается Apple 8, надо остановить линию ",
                              "Сегодня День Рождение Мао ДзеДуна. Оснанавливаем  ",
                              "Стачка на линии ",
                              "Все работники ушли на производство Abibas, останавливаем производство ",
                              "Все работники ушли на производство Abibas, останавливаем производство "};

            e.linea_eve.linea_null(e.ustr.get_id());
            Console.WriteLine(sl[e.sluch]+e.ustr.name);
            Console.WriteLine();
            //Console.ReadKey();
        }

        class Proizv {
            Random rand = new Random();
            public delegate void FirmaEngineHandler(object sender, FirmaEventArgs e);
            
            public event FirmaEngineHandler Stop_conveer;
            public event FirmaEngineHandler Prod_compl;
            public event FirmaEngineHandler Stop_sluch;
            public event FirmaEngineHandler Stop_linea;
            public event FirmaEngineHandler Norma_kompl;
            public event FirmaEngineHandler Sklad_full;

            public void Start()
            {
                try
                {




                    if (linia.linea.Count > 0)
                    {
                        for (int i = 0; i < linia.linea.Count; i++)
                        {
                            if (rand.Next(0, 10) == 1)
                            {

                                Stop_sluch(this, new FirmaEventArgs(rand.Next(0, 6), linia.linea[i], linia));
                               // linia.linea_null(linia.linea[i].get_id());
                                break;
                            }
                           
                            
                            if (sclad_ustr.free())
                            {
                                bool run = true;
                                for (int j = 0; j < linia.linea[i].complet.Count; j++)
                                {

                                    if (!sclad_coml.Get_compl(linia.linea[i].complet[j].get_id()))
                                    {
                                        Stop_conveer(this, new FirmaEventArgs(String.Format("Не хватает комплектующих " + linia.linea[i].complet[j].name), linia.linea[i], linia));
                                       // linia.linea.RemoveAt(i);
                                        linia.linea_null(linia.linea[i].get_id());
                                        run = false;
                                        break;
                                    }
                                    
                                }
                                if (run)
                                {   
                                    Prod_compl(this, new FirmaEventArgs(String.Format("Вы произвели единицу " + linia.linea[i].name), linia.linea[i], linia));

                                    sclad_ustr.Add_sclad(linia.linea[i]);
                                    
                                    linia.men_norm(linia.linea[i].get_id());
                                    Console.WriteLine("Осталось произвести " + linia.get_count(linia.linea[i].get_id()));
                                   
                                    if (linia.get_count(linia.linea[i].get_id()) <= 0)
                                    {
                                        Norma_kompl(this, new FirmaEventArgs(String.Format("Заказ на производство " + linia.linea[i].name + " выполнен!!!"), linia.linea[i], linia));
                                    }
                                }
                            }
                            else {
                                Sklad_full(this, new FirmaEventArgs(String.Format("Нет места на складе "), linia.linea[i], linia));
                                
                                //linia.linea.Clear();
                            }
                        }

                  
                    }
                    else
                    {
                        Stop_linea(this, new FirmaEventArgs(String.Format(" У вас нет ни одного заказа! ")));
                    }
                    for (int i = 0; i < linia.linea.Count; i++) {
                        if (linia.get_count(linia.linea[i].get_id()) <= 0)
                        {
                            linia.stop(linia.linea[i].get_id());
                    }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                }
            
        }

        class Statistic {
            public string name;
            public double price;
            public Statistic(string n, double p) {

              
                name = n;
                price = p;
            }
            public override string ToString()
            {
                return  name + " Цена: " + price;
            }

        }


        class FileMen {

            static public void Get_info_c(List<Compl> m) {
                try
                {

                    m.Clear();

                    using (BinaryReader r = new BinaryReader(File.Open(path_c, FileMode.OpenOrCreate)))
                    {
                        while (r.PeekChar() > -1)
                        {
                            var temp = new Compl(r.ReadString(), r.ReadDouble(),r.ReadInt32());




                            m.Add(temp);
                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            
            
            }
            static public void Get_info_u(List<Ustroist> m)
            {
                try
                {

                    m.Clear();

                    using (BinaryReader r = new BinaryReader(File.Open(path_u, FileMode.OpenOrCreate)))
                    {
                        while (r.PeekChar() > -1)
                        { 
                            var tem = new List<Compl>();
                            var s = r.ReadString();
                            var i = r.ReadInt32();
                            for (var j = 0; j < i;j++ )
                            {
                                tem.Add(new Compl(r.ReadString(), r.ReadDouble(),r.ReadInt32()));

                            }
                            var temp = new Ustroist(tem, s);

                            m.Add(temp); 
                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


            }
            static public void Set_info_c(List<Compl> m) {
                try
                {

                    using (BinaryWriter w = new BinaryWriter(File.Open(path_c, FileMode.OpenOrCreate)))
                    {
                        foreach (Compl item in m)
                        {
                            w.Write(item.name);
                            w.Write(item.price);
                            w.Write(item.get_id());

                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            
            }
            static public void Set_info_u(List<Ustroist> m)
            {
                try
                {

                    using (BinaryWriter w = new BinaryWriter(File.Open(path_u, FileMode.OpenOrCreate)))
                    {
                        foreach (Ustroist item in m)
                        {
                            w.Write(item.name);
                            w.Write(item.complet.Count);
                            foreach (Compl item1 in item.complet) {
                                w.Write(item1.name);
                                w.Write(item1.price);
                                w.Write(item1.get_id());
                            }
                           
                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            static public void Get_statistic() {

                try
                {
                    


                    using (BinaryReader r = new BinaryReader(File.Open(path, FileMode.OpenOrCreate)))
                    {
                        while (r.PeekChar() > -1)
                        {
                            var temp = new Statistic(r.ReadString(), r.ReadDouble());
                     

                           
                            
                            All_st.Add(temp);
                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            
            }
            static public void Set_statistic() {

                try
                {

                    using (BinaryWriter w = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                    {
                        foreach (Statistic item in All_st)
                        {
                            w.Write(item.name);
                            w.Write(item.price);
                          
                            
                        }

                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
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

        private static void Menu(int pos, string inf, List<Compl> m)
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
        private static void Menu(int pos, string inf, List<Real_compl> m)
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
        private static void Menu(int pos, string inf, List<Ustroist> m)
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
        private static void Menu(int pos, string inf, List<Real_ustr> m)
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
