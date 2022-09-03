using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
{
    internal class Program
    {
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
        static void FileDel(string path, int timer)
        {
            if (Directory.Exists(path))
            {
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    try 
                    {
                        DirectoryInfo directory = new DirectoryInfo(dir);
                        var dirlastuse = directory.LastAccessTime;
                        TimeSpan td = DateTime.Now - dirlastuse;
                        bool dirdelete = false;
                        if (td.TotalMinutes > timer)
                            dirdelete = true;
                        var files = directory.GetFiles();
                        foreach (var file in files)
                        {
                            try
                            {
                                var lastuse = file.LastAccessTime;
                                TimeSpan t = DateTime.Now - lastuse;
                                if (t.TotalMinutes > timer)
                                {
                                    Console.WriteLine(file.FullName);
                                    Console.WriteLine(t.TotalMinutes);
                                    file.Delete();
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message.ToString());
                            }
                            Console.WriteLine();
                        }
                        files = directory.GetFiles();
                        if ((files.Length == 0) && dirdelete)
                        {
                            Console.WriteLine(directory.FullName);
                            Console.WriteLine(td.TotalMinutes);
                            directory.Delete(true);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message.ToString());
                    }
                    FileDel(dir, timer);
                }
                
    
                var rootfiles = Directory.GetFiles(path);
                foreach (var file in rootfiles)
                {   
                    FileInfo f = new FileInfo(file);
                    var lastuse = f.LastAccessTime;
                    TimeSpan t = DateTime.Now - lastuse;
                    if (t.TotalMinutes > timer)
                    {
                        Console.WriteLine(f.FullName);
                        f.Delete();
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
            int timer = 1;
            string path = args[0];
            FileDel(path, timer);
            
        }
    }
}

          
         

