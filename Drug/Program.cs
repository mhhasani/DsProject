using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
class Program
{
    public static string random_string()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, 10)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static int random_num(int len)
    {
        Random rnd = new Random();
        int num = rnd.Next();
        return Convert.ToInt32(num % len);
    }
    public static void create_random_alergie_and_effect(string name, string[] drugs, string[] alergies, string[] effects, string[] new_eff)
    {
        string al1 = " ; " + "(" + name + "," + "+)";
        string al2 = " ; " + "(" + name + "," + "-)";
        int rand = 0;
        for (int i = 0; i < 5; i++)
        {
            rand = random_num(alergies.Length);
            if (i % 2 == 0)
                alergies[rand] += al1;
            else
                alergies[rand] += al2;
        }

        bool add_to_new_eff = false;
        for (int j = 0; j < 3; j++)
        {
            rand = random_num(effects.Length);
            string str_rand = random_string();
            string drug = effects[rand].Split(" : ")[0];
            string first_eff_name = "(" + drug + "," + "Eff_" + str_rand + ")";
            string eff_name = " ; (" + drug + "," + "Eff_" + str_rand + ")";
            string eff_drug = " ; (" + name + "," + "Eff_" + str_rand + ")";

            for (int i = 0; i < effects.Length; i++)
            {
                if (effects[i].Split(" : ")[0] == drug)
                {
                    effects[i] += eff_drug;
                    break;
                }
            }

            if (!add_to_new_eff)
            {
                for (int i = 0; i < new_eff.Length; i++)
                {
                    if (new_eff[i] == "")
                    {
                        add_to_new_eff = true;
                        new_eff[i] = name + " : " + first_eff_name;
                        break;
                    }
                }
                continue;
            }

            for (int i = 0; i < new_eff.Length; i++)
            {
                if (new_eff[i] != "")
                {
                    if (new_eff[i].Split(" : ")[0] == name)
                    {
                        new_eff[i] += eff_name;
                        break;
                    }
                }
            }
        }
    }
    public static void create_drug(string name, string price, string[] drugs, string[] new_drug, string[] new_eff, string[] alergies, string[] effects)
    {
        for (int i = 0; i < new_drug.Length; i++)
        {
            if (new_drug[i] == "" || new_drug[i] == null)
            {
                new_drug[i] = name + " : " + price;
                break;
            }
        }
        create_random_alergie_and_effect(name, drugs, alergies, effects, new_eff);
    }
    public static int serach_in_drugs(string name, string[] drugs)
    {
        for (int i = 0; i < drugs.Length; i++)
        {
            string[] drug_and_price = drugs[i].Split(" : ");
            if (drug_and_price[0] == name)
            {
                System.Console.WriteLine(drugs[i]);
                return i;
            }
        }
        return -1;
    }
    public static int serach_in_drugs_num(string name, string[] drugs)
    {
        for (int i = 0; i < drugs.Length; i++)
        {
            string[] drug_and_price = drugs[i].Split(" : ");
            if (drug_and_price[0] == name)
            {
                return i;
            }
        }
        return -1;
    }
    public static void serach_in_effects(string name, string[] effects)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            string[] drug_and_effect = effects[i].Split(" : ");
            if (effects[i].Split(" : ")[0] == name)
            {
                string[] eff = drug_and_effect[1].Split(" ; ");
                if (eff != null)
                {
                    for (int j = 0; j < eff.Length; j++)
                    {
                        System.Console.WriteLine(eff[j]);
                    }
                }
                else
                {
                    string res = "no drug has effect on " + name;
                    System.Console.WriteLine(res);
                }
                break;
            }
        }
    }
    public static int serach_in_effects_num(string name, string[] effects)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            string[] drug_and_effect = effects[i].Split(" : ");
            if (effects[i].Split(" : ")[0] == name)
            {
                return i;
            }
        }
        return -1;
    }
    public static void serach_in_alergies(string name, string[] alergies)
    {
        for (int i = 0; i < alergies.Length; i++)
        {
            string[] dis_and_drug = alergies[i].Split(" : ");
            string[] drug = dis_and_drug[1].Split(" ; ");
            for (int j = 0; j < drug.Length; j++)
            {
                string[] dr = drug[j].Split(",");
                if ("(" + name == dr[0])
                {
                    string res = "(" + dis_and_drug[0] + "," + dr[1];
                    System.Console.WriteLine(res);
                    break;
                }
            }
        }
    }
    public static int serach_in_alergies_num(string name, string[] alergies)
    {
        for (int i = 0; i < alergies.Length; i++)
        {
            string[] drug_and_effect = alergies[i].Split(" : ");
            if (alergies[i].Split(" : ")[0] == name)
            {
                return i;
            }
        }
        return -1;
    }
    public static bool search_drug(string name, string[] drugs, string[] effects, string[] alergies, string[] new_drug, string[] new_eff, string[] new_al)
    {
        int is_in_drug = serach_in_drugs(name, drugs);
        int is_in_new_drug = -1;

        if (is_in_drug == -1)
        {
            is_in_new_drug = serach_in_drugs(name, new_drug);
        }
        if (is_in_drug != -1)
        {
            serach_in_effects(name, effects);
            serach_in_alergies(name, alergies);
            return true;
        }
        else if (is_in_new_drug != -1)
        {
            serach_in_effects(name, new_eff);
            serach_in_alergies(name, alergies);
            return true;
        }
        return false;
    }
    public static bool delete_from_drugs(string name, string[] drugs)
    {
        for (int i = 0; i < drugs.Length; i++)
        {
            string[] drug_and_price = drugs[i].Split(" : ");
            if (drug_and_price[0] == name)
            {
                drugs[i] = "";
                return true;
            }
        }
        return false;
    }
    public static string drug_interaction(int Drug1, int Drug2, string[] effects)
    {
        try
        {
            string[] drug_1 = effects[Drug1].Split(" : ")[1].Split(" ; ");
            string[] drug_2 = effects[Drug2].Split(" : ")[1].Split(" ; ");
            for (int i = 0; i < drug_1.Length; i++)
            {
                for (int j = 0; j < drug_2.Length; j++)
                {
                    if (drug_1[i].Split(",")[1] == drug_2[j].Split(",")[1])
                    {
                        return drug_1[i].Split(",")[1].Split(")")[0];
                    }
                }
            }
            return "No";
        }
        catch (Exception)
        {
            return "No";
        }
    }
    public static string dis_interaction(int alergie, int drug, string[] alergies, string[] drugs)
    {
        string[] dis_and_drug = alergies[alergie].Split(" : ");
        string[] Drugs = dis_and_drug[1].Split(" ; ");
        string d = drugs[drug].Split(" : ")[0];
        for (int j = 0; j < Drugs.Length; j++)
        {
            string[] dr = Drugs[j].Split(",");
            if ("(" + d == dr[0] && "-)" == dr[1])
            {
                return d;
            }
        }
        return "No";
    }
    public static void delete_from_effects(string name, string[] effects)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            string[] drug_and_effect = effects[i].Split(" : ");
            if (effects[i].Split(" : ")[0] == name)
            {
                effects[i] = "";
                continue;
            }
            try
            {
                string[] eff = drug_and_effect[1].Split(" ; ");
                if (eff != null)
                {
                    for (int j = 0; j < eff.Length; j++)
                    {
                        if (eff[j].Split(",")[0] == "(" + name)
                        {
                            eff[j] = "";
                            break;
                        }
                    }
                    drug_and_effect[1] = "";
                    for (int j = 0; j < eff.Length; j++)
                    {
                        if (eff[j] != "")
                        {
                            drug_and_effect[1] += eff[j] + " ; ";
                        }
                    }
                    if (drug_and_effect[1] == "")
                    {
                        effects[i] = "";
                        continue;
                    }
                    drug_and_effect[1] += "end";
                    effects[i] = drug_and_effect[0] + " : " + drug_and_effect[1].Split(" ; end")[0];
                }
            }
            catch (Exception)
            {
                continue;
            }
        }
    }
    public static void delete_from_alergies(string name, string[] alergies)
    {
        for (int i = 0; i < alergies.Length; i++)
        {
            string[] dis_and_alergie = alergies[i].Split(" : ");
            try
            {
                string[] eff = dis_and_alergie[1].Split(" ; ");
                if (eff != null)
                {
                    for (int j = 0; j < eff.Length; j++)
                    {
                        if (eff[j].Split(",")[0] == "(" + name)
                        {
                            eff[j] = "";
                            break;
                        }
                    }
                    dis_and_alergie[1] = "";
                    for (int j = 0; j < eff.Length; j++)
                    {
                        if (eff[j] != "")
                        {
                            dis_and_alergie[1] += eff[j] + " ; ";
                        }
                    }
                    if (dis_and_alergie[1] == "")
                    {
                        alergies[i] = "";
                        continue;
                    }
                    dis_and_alergie[1] += "end";
                    alergies[i] = dis_and_alergie[0] + " : " + dis_and_alergie[1].Split(" ; end")[0];
                }
            }
            catch
            {
                continue;
            }
        }
    }
    public static bool delete_drug(string name, string[] drugs, string[] effects, string[] alergies, string[] new_drug, string[] new_eff, string[] new_al)
    {
        bool is_in_drug = false;
        bool is_in_new_drug = delete_from_drugs(name, new_drug);

        if (!is_in_new_drug)
        {
            is_in_drug = delete_from_drugs(name, new_drug);
        }
        if (is_in_drug || is_in_new_drug)
        {
            delete_from_effects(name, effects);
            delete_from_effects(name, new_eff);
            delete_from_alergies(name, alergies);
            return true;
        }
        return false;
    }
    public static void create_random_alergie(string name, string[] new_al, string[] drugs)
    {
        Random rnd = new Random();
        int num1 = rnd.Next();
        int num2 = rnd.Next();
        int len = drugs.Length;
        num1 = Convert.ToInt32(num1 % len);
        num2 = Convert.ToInt32(num2 % len);
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
    public static bool search_in_dis(string name, string[] diseases)
    {
        for (int i = 0; i < diseases.Length; i++)
        {
            if (diseases[i] == name)
            {
                System.Console.WriteLine(name);
                return true;
            }
        }
        return false;
    }
    public static bool search_in_dis_num(string name, string[] diseases)
    {
        for (int i = 0; i < diseases.Length; i++)
        {
            if (diseases[i] == name)
            {
                return true;
            }
        }
        return false;
    }
    public static void serach_in_alergies_2(string name, string[] alergies)
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
                    string res = "no drug has effect on " + name;
                    System.Console.WriteLine(res);
                }
                break;
            }
        }
    }
    public static bool search_dis(string name, string[] diseases, string[] alergies, string[] new_dis, string[] new_al)
    {
        bool is_in_dis = search_in_dis(name, diseases);
        bool is_in_new_dis = false;
        if (!is_in_dis)
        {
            is_in_new_dis = search_in_dis(name, new_dis);
        }
        if (is_in_dis)
        {
            serach_in_alergies_2(name, alergies);
            return true;
        }
        else if (is_in_new_dis)
        {
            serach_in_alergies_2(name, new_al);
            return true;
        }
        return false;
    }
    public static bool delete_dis(string name, string[] diseases, string[] alergies, string[] new_dis, string[] new_al)
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
            return true;
        }

        if (!(dis_deleted && al_deleted))
        {
            for (int i = 0; i < diseases.Length; i++)
            {
                if (diseases[i] == name)
                {
                    diseases[i] = "";
                    dis_deleted = true;
                    break;
                }
            }
            for (int i = 0; i < alergies.Length; i++)
            {
                string[] alergie_and_dis = alergies[i].Split(" : ");
                if (alergie_and_dis[0] == name)
                {
                    alergies[i] = "";
                    al_deleted = true;
                    break;
                }
            }
        }
        if (!dis_deleted)
        {
            return false;
        }
        return true;
    }
    public static void save(string[] diseases, string[] alergies, string[] drugs, string[] effects, string[] new_dis, string[] new_al, string[] new_drug, string[] new_eff)
    {
        File.Delete(@"../datasets/diseases.txt");
        File.Delete(@"../datasets/alergies.txt");
        File.Delete(@"../datasets/effects.txt");
        File.Delete(@"../datasets/drugs.txt");
        StreamWriter sw_dis = new StreamWriter(@"../datasets/diseases.txt");
        StreamWriter sw_al = new StreamWriter(@"../datasets/alergies.txt");
        StreamWriter sw_eff = new StreamWriter(@"../datasets/effects.txt");
        StreamWriter sw_drugs = new StreamWriter(@"../datasets/drugs.txt");

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

        for (int i = 0; i < drugs.Length; i++)
        {
            if (drugs[i] != "")
            {
                sw_drugs.WriteLine(drugs[i]);
            }
        }
        for (int i = 0; i < new_drug.Length; i++)
        {
            if (new_drug[i] != "" && new_drug[i] != null)
            {
                sw_drugs.WriteLine(new_drug[i]);
            }
        }
        sw_drugs.Close();

        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i] != "")
            {
                sw_eff.WriteLine(effects[i]);
            }
        }
        for (int i = 0; i < new_eff.Length; i++)
        {
            if (new_eff[i] != "" && new_eff[i] != null)
            {
                sw_eff.WriteLine(new_eff[i]);
            }
        }
        sw_eff.Close();

        diseases = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
        alergies = System.IO.File.ReadAllLines(@"../datasets/alergies.txt");
        effects = System.IO.File.ReadAllLines(@"../datasets/effects.txt");         
        drugs = System.IO.File.ReadAllLines(@"../datasets/drugs.txt");       
        new_dis = new string [100];
        new_al = new string [100];
        new_eff = new string [100];
        new_drug = new string [100];
        for (int i = 0; i < new_dis.Length; i++)
        {
            new_dis[i] = "";
            new_al[i] = "";
            new_drug[i] = "";
            new_eff[i] = "";
        }
    }
    public static void Main()
    {

        string[] diseases = System.IO.File.ReadAllLines(@"../datasets/diseases.txt");
        string[] alergies = System.IO.File.ReadAllLines(@"../datasets/alergies.txt");
        string[] drugs = System.IO.File.ReadAllLines(@"../datasets/drugs.txt");
        string[] effects = System.IO.File.ReadAllLines(@"../datasets/effects.txt");
        string[] new_dis = new string[100];
        string[] new_al = new string[100];
        string[] new_drug = new string[100];
        string[] new_eff = new string[100];
        for (int i = 0; i < new_dis.Length; i++)
        {
            new_dis[i] = "";
            new_al[i] = "";
            new_drug[i] = "";
            new_eff[i] = "";
        }

        while (true)
        {
            Console.Clear();
            System.Console.WriteLine("please write a number:");
            System.Console.WriteLine("1. Create Drug");
            System.Console.WriteLine("2. Delete Drug");
            System.Console.WriteLine("3. Search Drug");
            System.Console.WriteLine("4. Create Disease");
            System.Console.WriteLine("5. Delete Disease");
            System.Console.WriteLine("6. Search Disease");
            System.Console.WriteLine("7. Check for drug interactions in a prescription");
            System.Console.WriteLine("8. Calculate the cost of prescription drugs");
            System.Console.WriteLine("9. Check for the absence of alergies in a patient's prescription");
            System.Console.WriteLine("10. General increase or decrease in drug prices");
            System.Console.WriteLine("11. Save Changes Into DataSets");
            System.Console.WriteLine("12. quit");

            int request;
            while (true)
            {
                try
                {
                    request = Convert.ToInt32(Console.ReadLine());
                    if (request > 12 || request < 1)
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("your request must be an int 1 to 12");
                }
            }

            if (request == 1)
            {
                System.Console.Write("please write your Drug's name: ");
                string drug = Console.ReadLine();
                System.Console.Write("please write price of this drug: ");
                int price;
                while (true)
                {
                    try
                    {
                        price = Convert.ToInt32(Console.ReadLine());
                        if (price <= 0)
                        {
                            throw new Exception();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("price must be int and positive number");
                    }
                }


                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();
                try
                {
                    create_drug(drug, Convert.ToString(price), drugs, new_drug, new_eff, alergies, effects);
                    System.Console.WriteLine("Drug succesfully created!");
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Error!");
                }

                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 2)
            {
                System.Console.Write("please write your Drug's name: ");
                string drug;
                while (true)
                {
                    try
                    {
                        drug = Console.ReadLine();
                        if (drug == "")
                        {
                            throw new Exception();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("please enter an input");
                    }
                }

                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();

                bool deleted = delete_drug(drug, drugs, effects, alergies, new_drug, new_eff, new_al);
                if (deleted)
                    System.Console.WriteLine("Drug succesfully deleted!");
                else
                {
                    System.Console.WriteLine("Drug not found!");
                }
                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 3)
            {
                System.Console.Write("please write your Drug's name: ");
                string drug;
                while (true)
                {
                    try
                    {
                        drug = Console.ReadLine();
                        if (drug == "")
                        {
                            throw new Exception();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("please enter an input");
                    }
                }

                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();

                bool search = search_drug(drug, drugs, effects, alergies, new_drug, new_eff, new_al);
                if (!search)
                    System.Console.WriteLine("Drug not found!");
                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 4)
            {
                System.Console.Write("please write your Disease's name: ");
                string disease = Console.ReadLine();

                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();
                try
                {
                    create_dis(disease, new_dis, new_al, drugs);
                    System.Console.WriteLine("Disease succesfully created!");
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Error!");
                }

                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 5)
            {
                System.Console.Write("please write your Disease's name: ");
                string disease;
                while (true)
                {
                    try
                    {
                        disease = Console.ReadLine();
                        if (disease == "")
                        {
                            throw new Exception();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("please enter an input");
                    }
                }

                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();

                bool deleted = delete_dis(disease, diseases, alergies, new_dis, new_al);
                if (deleted)
                    System.Console.WriteLine("Disease succesfully deleted!");
                else
                {
                    System.Console.WriteLine("Disease not found!");
                }
                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 6)
            {
                System.Console.Write("please write your Disease's name: ");
                string disease;
                while (true)
                {
                    try
                    {
                        disease = Console.ReadLine();
                        if (disease == "")
                        {
                            throw new Exception();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("please enter an input");
                    }
                }

                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();

                bool search = search_dis(disease, diseases, alergies, new_dis, new_al);
                if (!search)
                    System.Console.WriteLine("Disease not found!");
                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 7)
            {
                System.Console.Write("please write Drug's count: ");
                int drug_num;
                while (true)
                {
                    try
                    {
                        drug_num = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("drug number must be positive int");
                    }
                }
                System.Console.WriteLine("please write Drugs's name: ");

                int[] Drugs = new int[drug_num];

                for (int i = 0; i < drug_num; i++)
                {
                    string drug = Console.ReadLine();
                    int eff = serach_in_effects_num(drug, effects);
                    int search = serach_in_drugs_num(drug, drugs);
                    if (eff != -1)
                        Drugs[i] = eff;
                    else if (search == -1)
                    {
                        Drugs[i] = -1;
                        string res = drug + " not found!";
                        System.Console.WriteLine(res);
                    }
                    else
                    {
                        Drugs[i] = -1;
                    }
                }

                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();
                bool flag = false;
                for (int i = 0; i < drug_num; i++)
                {
                    for (int j = i; j < drug_num; j++)
                    {
                        if (i != j && Drugs[i] != -1 && Drugs[j] != -1)
                        {
                            string di = drug_interaction(Drugs[i], Drugs[j], effects);
                            if (di != "No")
                            {
                                string res = effects[Drugs[i]].Split(" : ")[0] + " + " + effects[Drugs[j]].Split(" : ")[0] + " -> " + di;
                                System.Console.WriteLine(res);
                                flag = true;
                            }
                        }
                    }
                }
                if (!flag)
                {
                    System.Console.WriteLine("None of the drugs interact with each other!");
                }
                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 8)
            {
                System.Console.Write("please write Drug's count: ");
                int drug_num;
                while (true)
                {
                    try
                    {
                        drug_num = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("your drug number must be positive");
                    }
                }
                System.Console.WriteLine("please write Drugs's name: ");

                int[] Drugs = new int[drug_num];

                for (int i = 0; i < drug_num; i++)
                {
                    string drug = Console.ReadLine();
                    int dr_and_pr = serach_in_drugs_num(drug, drugs);
                    if (dr_and_pr != -1)
                        Drugs[i] = dr_and_pr;
                    else
                    {
                        Drugs[i] = -1;
                        string res = drug + "not found!";
                        System.Console.WriteLine(res);
                    }
                }

                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();
                int price = 0;
                for (int i = 0; i < drug_num; i++)
                {
                    if (Drugs[i] != -1)
                    {
                        price += Convert.ToInt32(drugs[Drugs[i]].Split(" : ")[1]);
                    }
                }
                System.Console.Write("price: ");
                System.Console.WriteLine(price);
                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 9)
            {
                System.Console.Write("please write Disease's count: ");
                int dis_num;
                while (true)
                {
                    try
                    {
                        dis_num = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("your disease number must be positive");
                    }
                }
                System.Console.WriteLine("please write Disease's name: ");

                int[] Dis = new int[dis_num];

                for (int i = 0; i < dis_num; i++)
                {
                    string dis = Console.ReadLine();
                    int al = serach_in_alergies_num(dis, alergies);
                    bool is_in_dis = search_in_dis_num(dis, diseases);
                    if (al != -1)
                        Dis[i] = al;
                    else if (!is_in_dis)
                    {
                        Dis[i] = -1;
                        string res = dis + "not found!";
                        System.Console.WriteLine(res);
                    }
                    else
                    {
                        Dis[i] = -1;
                    }
                }

                System.Console.Write("please write Drug's count: ");
                int drug_num;
                while (true)
                {
                    try
                    {
                        drug_num = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("your drug number must be positive");
                    }
                }
                System.Console.WriteLine("please write Drugs's name: ");

                int[] Drugs = new int[drug_num];

                for (int i = 0; i < drug_num; i++)
                {
                    string drug = Console.ReadLine();
                    int dr = serach_in_drugs_num(drug, drugs);
                    if (dr != -1)
                        Drugs[i] = dr;
                    else
                    {
                        Drugs[i] = -1;
                        string res = drug + " not found!";
                        System.Console.WriteLine(res);
                    }
                }
                bool flag = false;
                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();
                for (int i = 0; i < dis_num; i++)
                {
                    for (int j = i; j < drug_num; j++)
                    {
                        if (Dis[i] != -1 && Drugs[j] != -1)
                        {
                            string di = dis_interaction(Dis[i], Drugs[j], alergies, drugs);
                            if (di != "No")
                            {
                                string res = drugs[Drugs[j]].Split(" : ")[0] + " has bad effect on " + alergies[Dis[i]].Split(" : ")[0];
                                System.Console.WriteLine(res);
                                flag = true;
                            }
                        }
                    }
                }
                if (!flag)
                {
                    System.Console.WriteLine("None of the drugs has bad effect on this diseases!");
                }
                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 10)
            {
                System.Console.Write("Please enter the inflation rate: ");
                int inflation_rate;
                while (true)
                {
                    try
                    {
                        inflation_rate = Convert.ToInt32(Console.ReadLine());
                        if (inflation_rate < -99)
                        {
                            throw new Exception();
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("inflation_rate must be int and bigger than -99%");
                    }
                }

                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();

                for (int i = 0; i < drugs.Length; i++)
                {
                    int price = Convert.ToInt32(drugs[i].Split(" : ")[1]);
                    price = price + price * inflation_rate / 100;
                    drugs[i] = drugs[i].Split(" : ")[0] + " : " + Convert.ToString(price);
                }

                for (int i = 0; i < new_drug.Length; i++)
                {
                    if (new_drug[i] != "")
                    {
                        int price = Convert.ToInt32(new_drug[i].Split(" : ")[1]);
                        price = price + price * inflation_rate / 100;
                        new_drug[i] = new_drug[i].Split(" : ")[0] + " : " + Convert.ToString(price);
                    }
                }

                System.Console.WriteLine("Inflation rates were applied to drugs!");

                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }

            else if (request == 11)
            {
                DateTime first = DateTime.Now;
                Process proc = Process.GetCurrentProcess();
                try
                {
                    save(diseases, alergies, drugs, effects, new_dis, new_al, new_drug, new_eff);
                    System.Console.WriteLine("changes succesfully saved!");
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Error!");
                }
                System.Console.WriteLine("--------------");
                System.Console.Write("memory: ");
                System.Console.WriteLine(proc.PrivateMemorySize64);
                DateTime last = DateTime.Now;
                TimeSpan diff = last.Subtract(first);
                System.Console.Write("time: ");
                System.Console.WriteLine(diff);
                System.Console.WriteLine("--------------");
            }
            
            else if (request == 12)
            {
                break;
            }
            
            System.Console.Write("press any key to continue: ");
            Console.ReadKey();
        }
    }
}