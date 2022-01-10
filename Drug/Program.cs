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
        public DrugStore()
        { // constructor
            first = null;
        }

        public void create_drug_store(string[] Drugs)
        {
            for (int i = 0; i < Drugs.Length; i++)
            {
                string[] NEW = Drugs[i].Split(" : ");
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

    class Disease
    {
        public string name;
        public string[] Drug_yes;
        public string[] Drug_no;
        public Disease next;
        public Disease(string Name)
        {
            name = Name;
            next = null;
        }
    }
    class AllDisease
    {
        public Disease first; // reference to first Drug on list
        public string[] diseases = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
        public string[] alergies = System.IO.File.ReadAllLines(@"../datasets/alergies.txt");

        public AllDisease()
        { // constructor
            first = null;
        }

        public void create_disease()
        {
            for (int i = 0; i < diseases.Length; i++)
            {
                add(diseases[i]);
            }
        }
        public void print()
        { // display the list
            for (Disease current = first; current != null; current = current.next)
            {
                Console.Write(current.name);
                Console.Write(" ");
            }
            Console.Write("\n");
        }

        public void read(string name)
        {
            Disease disease = find(name);
            if (disease == null)
            {
                System.Console.Write(name);
                System.Console.Write(" not found!");
                System.Console.WriteLine("");
            }
            else
            {
                System.Console.WriteLine(disease.name);
                if (disease.Drug_yes != null)
                {
                    System.Console.Write("Drug_yes: ");
                    for (int i = 0; i < disease.Drug_yes.Length; i++)
                    {
                        System.Console.Write(disease.Drug_yes[i]);
                        System.Console.Write(", ");
                    }
                    System.Console.WriteLine("");                    
                }
                if (disease.Drug_no != null)
                {
                    System.Console.Write("Drug_no: ");
                    for (int i = 0; i < disease.Drug_no.Length; i++)
                    {
                        System.Console.Write(disease.Drug_no[i]);
                        System.Console.Write(", ");
                    }
                    System.Console.WriteLine("");                    
                }
            }
        }


        public void add(string name)
        {
            Disease NEW = new Disease(name);
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

        public void add_alergie(string name)
        {
            Disease disease = find(name);
            if (disease == null)
            {
                System.Console.WriteLine("disease not found!");
                return;
            }

            List<string> drug_yes = new List<string>();
            List<string> drug_no = new List<string>();
            string[] dis_and_alergie;
            string[] alergie; 

            for (int i = 0; i < alergies.Length; i++)
            {
                drug_yes = new List<string>();
                drug_no = new List<string>();

                dis_and_alergie = alergies[i].Split(" : ");

                if (dis_and_alergie[0] != name)
                {
                    continue;
                }

                alergie = dis_and_alergie[1].Split(" ; ");
                string[] al;
                for (int j = 0; j < alergie.Length; j++)
                {
                    al = alergie[j].Substring(1,17).Split(",");
                    if (al[1] == "+")
                    {
                        drug_yes.Add(al[0]);
                    }
                    else
                    {
                        drug_no.Add(al[0]);
                    }
                }
                disease.Drug_yes = drug_yes.ToArray();
                disease.Drug_no = drug_no.ToArray();
            }
        }

        public void add_all_alergies()
        {
            System.Console.WriteLine("add alergies...");
            List<string> drug_yes = new List<string>();
            List<string> drug_no = new List<string>();
            string[] dis_and_alergie;
            string[] alergie; 
            Disease disease;

            for (int i = 0; i < alergies.Length; i++)
            {
                drug_yes = new List<string>();
                drug_no = new List<string>();

                dis_and_alergie = alergies[i].Split(" : ");
                disease = find(dis_and_alergie[0]);

                if (disease == null)
                {
                    break;
                }
                alergie = dis_and_alergie[1].Split(" ; ");
                string[] al;
                for (int j = 0; j < alergie.Length; j++)
                {
                    al = alergie[j].Substring(1,17).Split(",");
                    if (al[1] == "+")
                    {
                        drug_yes.Add(al[0]);
                    }
                    else
                    {
                        drug_no.Add(al[0]);
                    }
                }
                disease.Drug_yes = drug_yes.ToArray();
                disease.Drug_no = drug_no.ToArray();
            }
        }

        public Disease find(string name)
        {
            Disease current = first;
            while (current != null && current.name != name)
            {
                current = current.next;
            }
            return current;
        }

        public void delete_from_array(string name)
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
        public void save()
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
            sw_dis.Close(); 

            for (int i = 0; i < alergies.Length; i++)
            {
                if (alergies[i] != "")
                {
                    sw_al.WriteLine(alergies[i]);
                }
            }
            sw_al.Close();  
            diseases = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
            alergies = System.IO.File.ReadAllLines(@"../datasets/alergies.txt");       
        }

        public void delete(string name)
        {
            Disease to_delete = find(name);
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
                    delete_from_array(name);
                    save();
                    return;
                }
                Disease current = first;
                Disease c = current;
                while (current != null && current.name != to_delete.name)
                {
                    c = current;
                    current = current.next;
                }
                c.next = to_delete.next;
                delete_from_array(name);
                save();
            }
        }
    }
    public static void Main()
    {
        DateTime first = DateTime.Now;
        Process proc = Process.GetCurrentProcess();

        string[] drugs = System.IO.File.ReadAllLines(@"../datasets/drugs.txt");
        // string[] diseases = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
        string[] effects = System.IO.File.ReadAllLines(@"../datasets/effects.txt");
        // string[] alergies = System.IO.File.ReadAllLines(@"../datasets/alergies.txt");

        DrugStore DStore = new DrugStore();
        DStore.create_drug_store(drugs);

        AllDisease Diseases = new AllDisease();
        Diseases.create_disease();
        // Diseases.add_all_alergies();
        DStore.read("Drug_ucxnqwcpsf");
        DStore.delete("Drug_ucxnqwcpsf");
        DStore.read("Drug_ucxnqwcpsf");

        Diseases.add_alergie("Dis_iuirckvjxb");
        Diseases.read("Dis_iuirckvjxb");
        Diseases.delete("Dis_iuirckvjxb");
        Diseases.read("Dis_iuirckvjxb");
        Diseases.read("Dis_xmbjdyijco");
        Diseases.delete("Dis_xmbjdyijco");
        Diseases.read("Dis_xmbjdyijco");


        System.Console.WriteLine("--------------");
        System.Console.Write("memory: ");
        System.Console.WriteLine(proc.PrivateMemorySize64); 
        
        DateTime last = DateTime.Now;
        TimeSpan diff = last.Subtract(first);
        System.Console.Write("time: ");
        System.Console.WriteLine(diff);
    }
}