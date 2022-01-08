using System;
class Drug 
{
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
        read_from_txt();
    }
}

