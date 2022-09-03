using System;
using System.Collections.Generic;
using System.IO;

namespace Task2
{
    internal class Program
    {
        static bool CheckInputArgs(string[] args)
        {
            if (args.Length != 1)
                return false;
            if(!Directory.Exists(args[0]))
                return false;
            else return true;
        }
        static  decimal getDirTotalVolume(string path)
        {
            if(Directory.Exists(path))
            {
                decimal total = 0;
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                foreach (var f in files)
                {
                    try
                    {
                        FileInfo file = new FileInfo(f);
                        total += file.Length;
                        //Console.WriteLine(file.Name);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message.ToString());
                    }                    

                }
                foreach (var dir in dirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

                    total += getDirTotalVolume(dirInfo.FullName);
                }


                return total;
                   
            }
            return 0;   
        }
        static void Main(string[] args)
        {
            if (!CheckInputArgs(args))
            {
                Console.WriteLine("Ошибка во входных данных");
                return;

            }
            
            string path = args[0];
            var total=getDirTotalVolume(path);
            Console.WriteLine(total);
        }
    }
} 