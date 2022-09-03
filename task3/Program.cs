using System;
using System.Collections.Generic;
using System.IO;

namespace Task3
{
    internal class Program
    {
        static decimal getDirTotalVolume(string path)
        {
            if (Directory.Exists(path))
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
        static bool CheckInputArgs(string[] args)
        {
            if (args.Length != 1)
                return false;
            if (!args[0].Contains(@"\"))
                return false;
            if (args[0].Length < 4)
                return false;
            if (!Directory.Exists(args[0]))
                return false;
            else return true;
        }
        static void FileDel(string path, int timer, ref decimal size, ref int count)
        {
            if (Directory.Exists(path))
            {
                var dirs = Directory.GetDirectories(path);
                var files = Directory.GetFiles(path);
                foreach (var file  in files)
                {
                    var f = new FileInfo(file);
                    var lastuse = f.LastAccessTime;
                    TimeSpan t = DateTime.Now - lastuse;
                    if (t.TotalMinutes > timer)
                    {
                        count++;
                        size+=f.Length;
                        f.Delete();
                    }
                }
                foreach (var dir in dirs)
                {
                    var d = new DirectoryInfo(dir);
                    FileDel(d.FullName, timer, ref size, ref count);
                    var dirlastuse = d.LastAccessTime;
                    TimeSpan td = DateTime.Now - dirlastuse;
                    var dd = d.GetDirectories();
                    var ff = d.GetFiles();
                    if ((dd.Length == 0) && (ff.Length == 0) && (td.TotalMinutes > timer))
                    { d.Delete(true); }

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
            int timer = 1;
            string path = args[0];
            decimal size = 0;
            int count = 0;
            Console.WriteLine($"Исходный размер папки {getDirTotalVolume(path)} байт");
            FileDel(path, timer, ref size, ref count);
            Console.WriteLine("Освобождено {0} байт", size);
             Console.WriteLine("Удалено {0} файлов", count);
            Console.WriteLine("Текущий размер папки {0} байт", getDirTotalVolume(path));

        }
    }
}




