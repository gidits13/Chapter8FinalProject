using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }


    }
    internal class Program
    {
        static bool CheckInputArgs(string[] args)
        {
            if (args.Length != 1)
                return false;
            if (!File.Exists(args[0]))
                return false;
            else return true;
        }
        static  Student[] studensList(string path)
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Student[] listOfStudents =  (Student[])formatter.Deserialize(fs);

                    // foreach (Student s in listOfStudents)
                    // {
                    //    Console.WriteLine($"Имя: {s.Name} --- группа: {s.Group}----- ДР: {s.DateOfBirth} ");
                    //}
                    return listOfStudents;
                }
   
            }
            return null;
        }

        static void SortStudents(Student[] listOfStudents)
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            foreach (var s in listOfStudents)
            {
                if (!File.Exists(desktop+@"\"+s.Group+".txt"))
                {
                    using (StreamWriter sw = File.CreateText(desktop + @"\" + s.Group + ".txt"))
                    {
                        sw.WriteLine(s.Name + ", " + s.Group + ", " + s.DateOfBirth);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(desktop + @"\" + s.Group + ".txt"))
                    {
                        sw.WriteLine(s.Name + ", " + s.Group + ", " + s.DateOfBirth);
                    }
                }

            }
        }
        static void Main(string[] args)
        {
            if (!CheckInputArgs(args))
            {
                Console.WriteLine("Ошибка во входных данных");
                return;

            }
            string path = args[0];
            // string path = @"c:\2\Students.dat";
            var studentsList = studensList(path);
            SortStudents(studentsList);
            
        }
    }
}