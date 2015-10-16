using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace BorysenkoKlassXML
{
    class Program
    {
        static int ID = 0;
        static void Main(string[] args)
        {
            List<Farmac> Apteka = new List<Farmac>();
       Sclad.First();
       Sclad.Get_Elem(ref Apteka);
      
     
            
       Contr.Prov(Apteka);   

       string[] menu = { "Просмотр и покупка ", "Удаление\\Редактирование", "Остатки", "Инвентаризация", "Просм убытков" };
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
                       int pos1 = 0;
                     
                       while (true)
                       {
                           
                           Menu(pos1, "", Apteka);
                           var key1 = Console.ReadKey();
                           pos1 = menycount(key1, Apteka.Count(), pos1);
                           if (key1.Key == ConsoleKey.Enter)
                           {
                               Sclad.Set_elem(Apteka[pos1].get_id());
                               Sclad.Get_Elem(ref Apteka);
                               Contr.Contr_sclad(Apteka[pos1].get_id());
                               Console.WriteLine("Еденица проданна!!!");
                               Console.ReadKey();
                           
                           }
                           else if (key1.Key == ConsoleKey.Escape)
                           {
                               break;
                           }

                           Console.Clear();

                       }


                       break;
                   case 1:
                        int pos2 = 0;

                        while (true)
                        { 
                          

                            Menu(pos2, "", Apteka);
                            var key2 = Console.ReadKey();
                            bool del =false;
                            pos2 = menycount(key2, Apteka.Count(), pos2);
                            if (key2.Key == ConsoleKey.Enter)
                            {
                                 string[] menuEdit = { "Цена","Количество","Фирма","Delete" }; 
                                 int pos3 = 0;

                                 while (true)
                                 {
                                    Sclad.Get_Elem(ref Apteka);

                                     Menu(pos3, "", menuEdit);
                                     var key3 = Console.ReadKey();
                                     pos3 = menycount(key3, menuEdit.Length, pos3);
                                     if (key3.Key == ConsoleKey.Enter)
                                     {
                                         switch (pos3) { 
                                             case 0:
                                                 double price=0;

                                                 while (!double.TryParse(Console.ReadLine(), out price)) ;
                                                 if (Sclad.Edit_elem("price", price.ToString(), Apteka[pos2].get_id())) {
                                                     Console.WriteLine("Успешно обновленно");
                                                 }

                                                 break;
                                             case 1:
                                                 int q=0;

                                                 while (!int.TryParse(Console.ReadLine(), out q)) ;
                                                 if(Sclad.Edit_elem("quantity", q.ToString(), Apteka[pos2].get_id())) {
                                                     Console.WriteLine("Успешно обновленно");
                                                 }

                                                 break;
                                             case 2:
                                                 string cantry;

                                                 cantry=Console.ReadLine();
                                                 if(Sclad.Edit_elem("cantry", cantry, Apteka[pos2].get_id())) {
                                                     Console.WriteLine("Успешно обновленно");
                                                 }

                                                 break;
                                             case 3:
                                                 Sclad.Del_elem(Apteka[pos2].get_id());
                                                 Menu(0, "", Apteka);
                                                 del = true;
                                                 break;
                                         
                                         }

                                     }
                                     else if (key3.Key == ConsoleKey.Escape)
                                     {
                                         break;
                                     }
                                     else if (del)
                                     {
                                         del = false;
                                         break;
                                     }

                                     Console.Clear();
                                 }

                                

                            }
                            else if (key2.Key == ConsoleKey.Escape)
                            {
                                break;
                            }

                            Console.Clear();
                        
                        
                        
                        }



                       break;
                   case 2:

                       foreach(Farmac F in Apteka){
                      Console.WriteLine(F.Print());
                          
                       } Console.ReadKey();
                       break;
                   case 3:
                      Console.WriteLine(Contr.Get_quant(0));
                      Console.ReadKey();
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

        class Contr
        {
            static public string Get_quant(int id) { 
                XmlDocument xDoc = new XmlDocument();
                var path = Environment.CurrentDirectory;

               
                try
                { 
                
                xDoc.Load(@"../../Contr.xml");

                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                    {
                        if (node.Attributes.Count > 0)
                        {
                            
                            XmlNode attr = node.Attributes.GetNamedItem("id");
                            if (attr != null && attr.Value == id.ToString())
                            {
                             
                                foreach (XmlNode childNode in node.ChildNodes)
                                {
                                    
                                    foreach (XmlNode chi in childNode.ParentNode)
                                    {
                                       
                                        if (chi.Name == "quantity")
                                        {


                                            return chi.InnerText; ;
                                        }else return "NULL";
                                    }




                                }

                            }else return "NULL";


                        }
                        else return "NULL";
                    
                    }

                   
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
                return "NULL";
            }
            static public void Contr_sclad(int id) {
                XmlDocument xDoc = new XmlDocument();
                var path = Environment.CurrentDirectory;


                try
                {
                    xDoc.Load(@"../../Contr.xml");

                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                    {

                        if (node.Attributes.Count > 0)
                         {
                             XmlNode attr = node.Attributes.GetNamedItem("id");
                             if (attr != null && attr.Value == id.ToString()) { 
                                 foreach (XmlNode childNode in node.ChildNodes)
                                 {
                                    
                                         foreach (XmlNode chi in childNode.ParentNode)
                                         {
                                             if (chi.Name == "quantity")
                                             {
                                                 int temp = int.Parse(chi.InnerText);
                                                 temp--;
                                                 if (temp <= 0)
                                                 {
                                                     xRoot.RemoveChild(node);
                                                     xDoc.Save(@"../../Contr.xml");
                                                     break;
                                                 }

                                                 XmlText quantText = xDoc.CreateTextNode(temp.ToString());
                                                 XmlNode nodeT = chi.LastChild;
                                                 chi.RemoveChild(nodeT);
                                                 chi.AppendChild(quantText);
                                                 xDoc.Save(@"../../Contr.xml");
                                             }
                                         }

                                     


                                 }
                             
                             }
                             
                             
                             }

                     

                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            
            
            }
            static public void Prov(List<Farmac> f)
            {
                XmlDocument Xc = new XmlDocument();
                Xc.Load(@"../../Contr.xml");

                XmlElement xCRoot = Xc.DocumentElement;
                xCRoot.RemoveAll();
                Xc.Save(@"../../Contr.xml");

                foreach (Farmac F in f)
                {
                    XmlElement tovElem = Xc.CreateElement("tovar");
                    XmlAttribute idAttr = Xc.CreateAttribute("id");
                    XmlElement quantityElem = Xc.CreateElement("quantity");

                    XmlText idText = Xc.CreateTextNode(F.get_id().ToString());
                    XmlText quantityText = Xc.CreateTextNode(F.quantity.ToString());

                    idAttr.AppendChild(idText);
                    quantityElem.AppendChild(quantityText);

                    tovElem.Attributes.Append(idAttr);
                    tovElem.AppendChild(quantityElem);

                    xCRoot.AppendChild(tovElem);


                    Xc.Save(@"../../Contr.xml");
                }
            }

        }
        class Sclad
        {

            static public bool Edit_elem(string name, string volue,int id) {
        
                XmlDocument xDoc = new XmlDocument();
                var path = Environment.CurrentDirectory;
                try
                {
                    xDoc.Load(@"../../Farm.xml");

                    XmlElement xRoot = xDoc.DocumentElement;


                    foreach (XmlNode node in xRoot)
                    {
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            if (childNode.Name == "id" && int.Parse(childNode.InnerText) == id)
                            {
                        if (node.Attributes.Count > 0)
                        {
                            XmlNode attr = node.Attributes.GetNamedItem(name);
                            if (attr != null){

                                XmlText Text = xDoc.CreateTextNode(volue);

                                //XmlAttribute priceAttr = xDoc.CreateAttribute(name);
                                XmlNode nodeT = attr.LastChild;
                                attr.RemoveChild(nodeT);
                                attr.AppendChild(Text); 

                                xDoc.Save(@"../../Farm.xml");
                                return true;
                            }
                               
                        }
                        foreach (XmlNode childNode1 in childNode.ParentNode)
                        {
                            if (childNode1.Name == name) {
                                XmlText Text = xDoc.CreateTextNode(volue);
                                XmlNode nodeT = childNode1.LastChild;
                                childNode1.RemoveChild(nodeT);
                                childNode1.AppendChild(Text);

                                xDoc.Save(@"../../Farm.xml");
                                return true;
                            
                            }
                            if (childNode1.Name == "maker" || childNode1.Name == "date")
                            {
                                foreach (XmlNode childNodeNod in childNode1)
                                {
                                    if (childNodeNod.Name == name)
                                    {
                                        XmlText Text = xDoc.CreateTextNode(volue);
                                        XmlNode nodeT = childNodeNod.LastChild;
                                        childNodeNod.RemoveChild(nodeT);
                                        childNodeNod.AppendChild(Text);

                                        xDoc.Save(@"../../Farm.xml");
                                        return true;
                                    }
                                }                            
                            }
                        
                        }
                        }
                    }
                    }

                    return false;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
                return false;
        }


            static public void Del_elem(int id) { 
                XmlDocument xDoc = new XmlDocument();
                var path = Environment.CurrentDirectory;


                try
                {
                    xDoc.Load(@"../../Farm.xml");

                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                    {
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            if (childNode.Name == "id" && int.Parse(childNode.InnerText) == id) {
                                Console.WriteLine("Еденица удалена!!!");
                                Console.ReadKey();
                                xRoot.RemoveChild(node);
                                xDoc.Save(@"../../Farm.xml");
                                break;
                            
                            }
                        
                        
                        }
                    
                    
                    
                    }

                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }


            
            }

            static public void Get_elem_id(int id)
            {
            XmlDocument xDoc = new XmlDocument();
                var path = Environment.CurrentDirectory;
                        string N = "";
                        string P = "";
                        string C = "";
                        string F = "";
                        string dM = "";
                        string dF = "";
                        string Id = "";
                        string Q = "";

                try
                {
                    xDoc.Load(@"../../Farm.xml");
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                    {
                       

                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            if (childNode.Name == "id" && int.Parse(childNode.InnerText) == id) {
                                if (node.Attributes.Count > 0)
                                {
                                    XmlNode attr = node.Attributes.GetNamedItem("name");
                                    if (attr != null)
                                      
                                        N = attr.Value;
                                    XmlNode attr1 = node.Attributes.GetNamedItem("price");
                                    if (attr1 != null)
                                      
                                        P = attr1.Value;
                                
                                }
                                foreach (XmlNode childNode1 in node.ChildNodes)
                                {
                                    if (childNode1.Name == "id")
                                        //Console.WriteLine($"Двигатель: {childNode.InnerText}");
                                        Id = childNode1.InnerText;
                                    if (childNode1.Name == "quantity")
                                        //Console.WriteLine($"Цвет : {childNode.InnerText}");
                                        Q = childNode1.InnerText;
                                    if (childNode1.Name == "maker")
                                    {
                                        foreach (XmlNode childNodeNod in childNode1)
                                        {
                                            if (childNodeNod.Name == "cantry")
                                            {
                                                C = childNodeNod.InnerText;
                                            }
                                            if (childNodeNod.Name == "firma")
                                            {
                                                F = childNodeNod.InnerText;
                                            }
                                        }
                                    }
                                    if (childNode1.Name == "date")
                                    {
                                        foreach (XmlNode childNodeNod in childNode1)
                                        {
                                            if (childNodeNod.Name == "dateM")
                                            {
                                                dM = childNodeNod.InnerText;
                                            }
                                            if (childNodeNod.Name == "dateF")
                                            {
                                                dF = childNodeNod.InnerText;
                                            }
                                        }
                                    }


                                }
                            
                            }
                        }

                    }

                    Console.WriteLine("   Название " +N+ "   Цена " +P+ "  Фирма " +F+ "");
                  

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }


            }
            static public void Set_elem(int id)
            {
                XmlDocument xDoc = new XmlDocument();
                var path = Environment.CurrentDirectory;


                try {
                    xDoc.Load(@"../../Farm.xml");

                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                    {
                        

                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            if (childNode.Name == "id" && int.Parse(childNode.InnerText) == id) {
                                foreach (XmlNode chi in childNode.ParentNode) {
                                    if (chi.Name == "quantity") {
                                        int temp=int.Parse(chi.InnerText);
                                        temp--;
                                        if (temp <= 0) {
                                            xRoot.RemoveChild(node);
                                            xDoc.Save(@"../../Farm.xml");
                                            break;
                                        }

                                        XmlText quantText = xDoc.CreateTextNode(temp.ToString());
                                        XmlNode nodeT = chi.LastChild;
                                        chi.RemoveChild(nodeT);
                                        chi.AppendChild(quantText);
                                        xDoc.Save(@"../../Farm.xml");
                                    }
                                }
                            
                            }
                        
                        
                        }
                    
                    }

                
                }catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            
            }

            static public void Get_Elem(ref List<Farmac> f)
            {
                f=new List<Farmac>();
                XmlDocument xDoc = new XmlDocument();
                var path = Environment.CurrentDirectory;
                try
                {
                    xDoc.Load(@"../../Farm.xml");

                    XmlElement xRoot = xDoc.DocumentElement;
                    

                     foreach (XmlNode node in xRoot)
                     {
                         string N="";
                         double P=0;
                         string C="";
                         string F="";
                         string dM="";
                         string dF="";
                         int Id=0;
                         int Q=0;
                         
                         if (node.Attributes.Count > 0)
                         {
                             XmlNode attr = node.Attributes.GetNamedItem("name");
                             if (attr != null)
                             //    Console.WriteLine(attr.Value);
                             //Console.ReadKey();
                                 N = attr.Value;
                              XmlNode attr1 = node.Attributes.GetNamedItem("price");
                             if (attr1 != null)
                             //    Console.WriteLine(attr1.Value);
                             //Console.ReadKey();
                                 P = double.Parse(attr1.Value);
                         }

                         foreach (XmlNode childNode in node.ChildNodes)
                         {
                             if (childNode.Name == "id")
                                 //Console.WriteLine($"Двигатель: {childNode.InnerText}");
                                 Id= int.Parse(childNode.InnerText);
                             if (childNode.Name == "quantity")
                                 //Console.WriteLine($"Цвет : {childNode.InnerText}");
                                 Q =int.Parse(childNode.InnerText);
                             if (childNode.Name == "maker") {
                                 foreach (XmlNode childNodeNod in childNode)
                                 {
                                     if (childNodeNod.Name == "cantry") {
                                         C = childNodeNod.InnerText;
                                     }
                                     if (childNodeNod.Name == "firma")
                                     {
                                         F = childNodeNod.InnerText;
                                     }
                                 }
                             }
                             if (childNode.Name == "date")
                             {
                                 foreach (XmlNode childNodeNod in childNode)
                                 {
                                     if (childNodeNod.Name == "dateM")
                                     {
                                         dM = childNodeNod.InnerText;
                                     }
                                     if (childNodeNod.Name == "dateF")
                                     {
                                         dF = childNodeNod.InnerText;
                                     }
                                 }
                             }


                         }
                           Farmac temp = new Farmac(Id,N,F,C,dM,dF,P,Q);
                           f.Add(temp); 
                     }
                  
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
            static public void First()
            {
                string[] f_name = { "Fly cold", "Aspirin", "Balzam", "Panadol", "Papazol", "Fitomix", "VitaminA", "Zelyonka", "Mumiyo", "Spirt", "H2O3", "Pantenol" };
                string[] f_cantry = { "Russia", "England", "India", "Germany", "Ucraina", "Niderlandu", "Belorussiya", "Vietnam", "Russia", "France", "Algir", "Litva" };
                string[] f_firma = { "Bolshevik", "BritFarm", "Akipaki", "Bayer", "Darnica", "FitoWorld", "BelZdor", "Sinsu", "UralRudnik", "Vida", "Xerword", "VIK" };
                string[] f_price = { "22,22", "34", "10,12", "567,05", "6,12", "35,22", "45", "54", "15", "92", "56", "87" };
                string[] f_dateM = { "08.14", "03.15", "10.10", "12.14", "04.12", "01.12", "02.13", "07.14", "02.15", "02.13", "09.10", "05.10" };
                string[] f_dateF = { "08.24", "03.20", "10.17", "12.19", "04.22", "01.18", "02.23", "07.19", "02.20", "02.18", "09.16", "05.23" };
                Random rand = new Random();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(@"../../Farm.xml");
                XmlElement xR = xDoc.DocumentElement;

                xR.RemoveAll();

                xDoc.Save(@"../../Farm.xml");


                for (int i = 0; i < 12; i++)
                {
                    try
                    {
                        XmlDocument X = new XmlDocument();
                        X.Load(@"../../Farm.xml");

                        XmlElement xRoot = X.DocumentElement;

                        XmlElement farmElem = X.CreateElement("farm");
                        XmlAttribute priceAttr = X.CreateAttribute("price");
                        XmlAttribute farmAttr = X.CreateAttribute("name");
                        XmlElement idElem = X.CreateElement("id");
                        XmlElement makerElem = X.CreateElement("maker");
                        XmlElement cantryElem = X.CreateElement("cantry");
                        XmlElement firmaElem = X.CreateElement("firma");
                        XmlElement dateElem = X.CreateElement("date");
                        XmlElement dateMElem = X.CreateElement("dateM");
                        XmlElement dateFElem = X.CreateElement("dateF");
                        XmlElement quantityElem = X.CreateElement("quantity");

                        XmlText nameText = X.CreateTextNode(f_name[i]);
                        XmlText priceText = X.CreateTextNode(f_price[i]);
                        XmlText idText = X.CreateTextNode(ID++.ToString());
                        XmlText catryText = X.CreateTextNode(f_cantry[i]);
                        XmlText fimaText = X.CreateTextNode(f_firma[i]);
                        XmlText dateMText = X.CreateTextNode(f_dateM[i]);
                        XmlText dateFText = X.CreateTextNode(f_dateF[i]);
                        XmlText quantityFText = X.CreateTextNode(rand.Next(10, 100).ToString());


                        farmAttr.AppendChild(nameText);
                        priceAttr.AppendChild(priceText);
                        idElem.AppendChild(idText);
                        cantryElem.AppendChild(catryText);
                        firmaElem.AppendChild(fimaText);
                        dateMElem.AppendChild(dateMText);
                        dateFElem.AppendChild(dateFText);
                        quantityElem.AppendChild(quantityFText);

                        farmElem.Attributes.Append(farmAttr);
                        farmElem.Attributes.Append(priceAttr);
                        farmElem.AppendChild(idElem);
                        makerElem.AppendChild(cantryElem);
                        makerElem.AppendChild(firmaElem);
                        farmElem.AppendChild(makerElem);
                        dateElem.AppendChild(dateMElem);
                        dateElem.AppendChild(dateFElem);
                        farmElem.AppendChild(dateElem);
                        farmElem.AppendChild(quantityElem);

                        xRoot.AppendChild(farmElem);


                        X.Save(@"../../Farm.xml");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            }

        }
        class ContrSkl{
            public int id;
            public int quntity;
            public ContrSkl(int I,int Q) {
                id = I;
                quntity = Q;
            }
        }
        class Farmac {
            private int id;
            protected string name;
            protected string maker;
            protected string country;
            protected string dateM;
            protected string dateF;
            public double price;
            public int quantity;
            public Farmac() {
                id = ID++;
                name = "";
                maker = "";
                country = "";
                dateM = "";
                dateF = "";
                price = 0;
                quantity = 0;
            }
            public Farmac(int I,string N,string M, string C,string dM,string dF,double P,int Q) {
                id = I;
                name = N;
                maker = M;
                country = C;
                dateM = dM;
                dateF = dF;
                price = P;
                quantity = Q;

            }
            public int get_id(){
                return id;
            }
            public string get_name()
            {
                return name;
            }
            public override string ToString() {
                return name+" Производитель "+maker+". Страна "+country+". Цена "+price+". Срок годности "+dateF;
            
            }
            public string Print() {
                return name + ". Количество " + quantity;
            }
            public bool Invent(int Q) {
                if (quantity == Q)
                {
                    return true;
                }
                else {
                    return false;
                }
            
            }


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
        public static int menycount(ConsoleKeyInfo key, int count, int pos)
        {
            if (key.Key == ConsoleKey.UpArrow)
                pos = pos <= 0 ? count - 1 : --pos;
            else if (key.Key == ConsoleKey.DownArrow)
                pos = pos >= count - 1 ? 0 : ++pos;
            return pos;
        }
        private static void Menu(int pos, string inf, List<Farmac> m)
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
