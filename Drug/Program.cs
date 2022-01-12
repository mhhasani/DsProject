using System;
using System.Diagnostics;

class Program 
{
    class Drug 
    {
        public string name; // name item (key)
        public int price;
        public Drug next; // next Drug in list
        public Drug(string Name, int Price)
        { // constructor
            name = Name;
            price = Price;
            next = null;
        }
    }
    class DrugStore
    {
        public Drug first; // reference to first Drug on list
        public string[] drugs = System.IO.File.ReadAllLines(@"../datasets/drugs.txt");
        public string[] effects = System.IO.File.ReadAllLines(@"../datasets/effects.txt");

        public DrugStore()
        { // constructor
            first = null;
        }
        public void create_drug_store()
        {
            for (int i = 0; i < drugs.Length; i++)
            {
                string[] NEW = drugs[i].Split(" : ");
                add(NEW[0],Convert.ToInt32(NEW[1]));
            }
        }
        public void print()
        { // display the list
            for (Drug current = first; current != null; current = current.next)
            {
                Console.Write(current.name);
                Console.Write(" ");
            }
            Console.Write("\n");
        }
        public void read(string name)
        {
            Drug drug = find(name);
            if (drug == null)
            {
                System.Console.Write(name);
                System.Console.Write(" not found!");
                System.Console.WriteLine("");
            }
            else
            {
                System.Console.Write(drug.name);
                System.Console.Write(" : ");
                System.Console.Write(drug.price);
                System.Console.WriteLine("");
            }

        }
        public void add(string name, int price)
        {
            Drug NEW = new Drug(name, price);
            if (first == null)
            {
                first = NEW;
            }
            else
            {
                NEW.next = first;
                first = NEW;
            }
        }
        public Drug find(string name)
        {
            Drug current = first;
            while (current != null && current.name != name)
            {
                current = current.next;
            }
            return current;
        }
        public void delete(string name)
        {
            Drug to_delete = find(name);
            if (to_delete == null)
            {
                System.Console.Write(name);
                System.Console.Write(" not found!");
                System.Console.WriteLine("");
            }
            else
            {
                if (to_delete.name == first.name)
                {
                    first = to_delete.next;
                    return;
                }
                Drug current = first;
                Drug c = current;
                while (current != null && current.name != to_delete.name)
                {
                    c = current;
                    current = current.next;
                }
                c.next = to_delete.next;
            }
        }
    }
    public static void create_random_alergie(string name, string[] new_al, string[] drugs)
    {
        Random rnd = new Random();
        int num1 = rnd.Next();
        int num2 = rnd.Next();
        int len = drugs.Length;
        num1 = Convert.ToInt32(num1 / len);
        num2 = Convert.ToInt32(num2 / len);
        string[] dis1 = drugs[num1].Split(" : ");
        string[] dis2 = drugs[num2].Split(" : ");
        string alergie = name + " : " + "(" + dis1[0] + "," + "+) ; " + "(" + dis2[0] + "," + "-)";
        for (int i = 0; i < new_al.Length; i++)
        {
            if (new_al[i] == "" || new_al[i] == null)
            {
                new_al[i] = alergie;
                break;
            }
        }            
    }
    public static void create_dis(string name, string[] new_dis, string[] new_al, string[] drugs)
    {
        for (int i = 0; i < new_dis.Length; i++)
        {
            if (new_dis[i] == "" || new_dis[i] == null)
            {
                new_dis[i] = name;
                break;
            }
        }
        create_random_alergie(name, new_al, drugs);
    }
    public static void serach_dis(string name, string[] diseases, string[] alergies, string[] new_dis, string[] new_al)
    {
        bool is_in_dis = false;
        bool is_in_new_dis = false;
        for (int i = 0; i < diseases.Length; i++)
        {
            if (diseases[i] == name)
            {
                is_in_dis = true;
                System.Console.WriteLine(name);
                break;
            }
        }
        if (!is_in_dis)
        {
            for (int i = 0; i < new_dis.Length; i++)
            {
                if (new_dis[i] == name)
                {
                    is_in_new_dis = true;
                    System.Console.WriteLine(name);
                    break;
                }
            }            
        }
        if (is_in_dis)
        {
            for (int i = 0; i < alergies.Length; i++)
            {
                string[] dis_and_al = alergies[i].Split(" : ");
                if (alergies[i].Split(" : ")[0] == name)
                {
                    string[] al = dis_and_al[1].Split(" ; ");
                    if (al != null)
                    {
                        for (int j = 0; j < al.Length; j++)
                        {
                            System.Console.WriteLine(al[j]);
                        }
                    }
                    else
                    {
                        string res = "no drug has effect on " + name ;
                        System.Console.WriteLine(res);
                    }
                    break;
                }
            }            
        }
        else if (is_in_new_dis)
        {
            for (int i = 0; i < new_al.Length; i++)
            {
                string[] dis_and_al = new_al[i].Split(" : ");
                if (new_al[i].Split(" : ")[0] == name)
                {
                    string[] al = dis_and_al[1].Split(" ; ");
                    if (al != null)
                    {
                        for (int j = 0; j < al.Length; j++)
                        {
                            System.Console.WriteLine(al[j]);
                        }
                    }
                    else
                    {
                        string res = "no drug has effect on " + name ;
                        System.Console.WriteLine(res);
                    }
                    break;
                }
            }            
        }
        else
        {
            string res = name + " not found in diseases!";
            System.Console.WriteLine(res);
        }

    }
    public static void delete_dis(string name, string[] diseases, string[] alergies, string[] new_dis, string[] new_al)
    {
        bool dis_deleted = false;
        bool al_deleted = false;
        for (int i = 0; i < new_dis.Length; i++)
        {
            if (new_dis[i] == name)
            {
                new_dis[i] = "";
                dis_deleted = true;
                break;
            }
        }
        if (dis_deleted)
        {
            for (int i = 0; i < new_al.Length; i++)
            {
                string[] alergie = new_al[i].Split(" : ");
                if (alergie[0] == name)
                {
                    new_al[i] = "";
                    al_deleted = true;
                    break;
                }
            }            
        }

        if (!(dis_deleted && al_deleted))
        {
            for (int i = 0; i < diseases.Length; i++)
            {
                if (diseases[i] == name)
                {
                    diseases[i] = "";
                    break;
                }
            }
            for (int i = 0; i < alergies.Length; i++)
            {
                string[] alergie_and_dis = alergies[i].Split(" : ");
                if (alergie_and_dis[0] == name)
                {
                    alergies[i] = "";
                    break;
                }
            }               
        }
    }
    public static void save_dis(string[] diseases, string[] alergies, string[] new_dis, string[] new_al)
    {
        File.Delete(@"../datasets/diseases.txt");
        File.Delete(@"../datasets/alergies.txt");
        StreamWriter sw_dis = new StreamWriter(@"../datasets/diseases.txt");
        StreamWriter sw_al = new StreamWriter(@"../datasets/alergies.txt");

        for (int i = 0; i < diseases.Length; i++)
        {
            if (diseases[i] != "")
            {
                sw_dis.WriteLine(diseases[i]);
            }
        }
        for (int i = 0; i < new_dis.Length; i++)
        {
            if (new_dis[i] != "" && new_dis[i] != null)
            {
                sw_dis.WriteLine(new_dis[i]);
            }
        }
        sw_dis.Close(); 

        for (int i = 0; i < alergies.Length; i++)
        {
            if (alergies[i] != "")
            {
                sw_al.WriteLine(alergies[i]);
            }
        }
        for (int i = 0; i < new_al.Length; i++)
        {
            if (new_al[i] != "" && new_al[i] != null)
            {
                sw_al.WriteLine(new_al[i]);
            }
        }
        sw_al.Close();  

        diseases = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
        alergies = System.IO.File.ReadAllLines(@"../datasets/alergies.txt");       
        new_dis = new string [10];
        new_al = new string [10];
    }
    public static void Main()
    {
        DateTime first = DateTime.Now;
        Process proc = Process.GetCurrentProcess();


        string[] diseases = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
        string[] alergies = System.IO.File.ReadAllLines(@"../datasets/alergies.txt");
        string[] drugs = System.IO.File.ReadAllLines(@"../datasets/drugs.txt");
        string[] effects = System.IO.File.ReadAllLines(@"../datasets/effects.txt");
        string[] new_dis = new string[10];
        string[] new_al = new string[10];
        create_dis("Dis_alsjdlkasj", new_dis, new_al, drugs);
        serach_dis("Dis_alsjdlkasj", diseases, alergies, new_dis, new_al);
        delete_dis("Dis_mvkepinytj", diseases, alergies, new_dis, new_al);
        serach_dis("Dis_tdeklrcfex", diseases, alergies, new_dis, new_al);
        save_dis(diseases, alergies, new_dis, new_al);


        System.Console.WriteLine("--------------");
        System.Console.Write("memory: ");
        System.Console.WriteLine(proc.PrivateMemorySize64); 

        DateTime last = DateTime.Now;
        TimeSpan diff = last.Subtract(first);
        System.Console.Write("time: ");
        System.Console.WriteLine(diff);
    }
}