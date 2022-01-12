using System;
using System.Diagnostics;

class Program 
{
    public static bool serach_in_drugs(string name, string[] drugs)
    {
        for (int i = 0; i < drugs.Length; i++)
        {
            string[] drug_and_price = drugs[i].Split(" : ");            
            if (drug_and_price[0] == name)
            {
                System.Console.WriteLine(drugs[i]);
                return true;
            }                
        }
        return false;
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
                    string res = "no drug has effect on " + name ;
                    System.Console.WriteLine(res);
                }
                break;
            }
        }            
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
    public static void serach_drug(string name, string[] drugs, string[] effects, string[] alergies, string[] new_drug, string[] new_eff, string[] new_al)
    {
        bool is_in_drug = serach_in_drugs(name, drugs);
        bool is_in_new_drug = false;
        
        if (!is_in_drug)
        {
            is_in_new_drug = serach_in_drugs(name, new_drug);          
        }
        if (is_in_drug)
        {
            serach_in_effects(name, effects);    
            serach_in_alergies(name, alergies);  
        }
        else if (is_in_new_drug)
        {
            serach_in_effects(name, new_eff);
            serach_in_alergies(name, new_al);              
        }
        else
        {
            string res = name + " not found in drugs!";
            System.Console.WriteLine(res);
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
                    string res = "no drug has effect on " + name ;
                    System.Console.WriteLine(res);
                }
                break;
            }
        }            
    }
    public static void serach_dis(string name, string[] diseases, string[] alergies, string[] new_dis, string[] new_al)
    {
        bool is_in_dis = search_in_dis(name, diseases);
        bool is_in_new_dis = false;
        if (!is_in_dis)
        {
            is_in_new_dis= search_in_dis(name, new_dis);        
        }
        if (is_in_dis)
        {
            serach_in_alergies_2(name, alergies);         
        }
        else if (is_in_new_dis)
        {
            serach_in_alergies_2(name, new_al);                    
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
        string[] new_drug = new string[10];
        string[] new_eff = new string[10];
        for (int i = 0; i < new_dis.Length; i++)
        {
            new_dis[i] = "";
            new_al[i] = "";
            new_drug[i] = "";
            new_eff[i] = "";
        }
        serach_drug("Drug_imuraovqcy", drugs, effects, alergies, new_drug, new_eff, new_al);
        create_dis("Dis_alsjdlkasj", new_dis, new_al, drugs);
        serach_dis("Dis_alsjdlkasj", diseases, alergies, new_dis, new_al);
        delete_dis("Dis_mvkepinytj", diseases, alergies, new_dis, new_al);
        serach_dis("Dis_utjolpmtkr", diseases, alergies, new_dis, new_al);
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