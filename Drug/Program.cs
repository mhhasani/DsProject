using System;
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
            System.Console.Write(drug.name);
            System.Console.Write(" : ");
            System.Console.Write(drug.price);
            System.Console.WriteLine("");
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
    static void read_from_txt()
    {
        DateTime first = DateTime.Now;
        string[] lines = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
        int c = 0;
        foreach (string line in lines)
        {
            System.Console.WriteLine(line);
            if (line == "Dis_xmbjdyijco")
            {
                System.Console.WriteLine(line);
                break;      
            }
            c++;
        }
        System.Console.WriteLine(c);
        // Console.WriteLine("Press any key to exit.");
        DateTime last = DateTime.Now;
        TimeSpan diff = last.Subtract(first);
        // System.Console.WriteLine(first);
        // System.Console.WriteLine(last);
        System.Console.WriteLine(diff);
    }
    public static void Main()
    {
        string[] drugs = System.IO.File.ReadAllLines(@"../datasets/drugs.txt");
        string[] diseases = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
        string[] effects = System.IO.File.ReadAllLines(@"../datasets/effects.txt");
        string[] alergies = System.IO.File.ReadAllLines(@"../datasets/alergies.txt");

        DrugStore DStore = new DrugStore();
        DStore.create_drug_store(drugs);
        DStore.read("Drug_ijnhdnezld");
    }
}

