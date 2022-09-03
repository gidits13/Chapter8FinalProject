using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    public class Students
    {
        public string Name;
        public string Group;
        public DateTime BirthDate;
        

    }
    internal class Program
    {
        static void studensList(string path)
        {
           if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Students[] deserilizePeople = (Students[])formatter.Deserialize(fs);

                    foreach (Students p in deserilizePeople)
                    {
                        Console.WriteLine($"Имя: {p.Name} --- Возраст: {p.Group}");
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string path = @"c:\2\Students.dat";
            studensList(path);
            Console.WriteLine("Hello, World!");
        }
    }
}