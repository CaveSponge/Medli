﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Medli.Applications
{
    class AppLauncher
    {
        public static string AppTitle;
        public static string AppDesc;
        public static string AppAuthor;
        public static void app_author(string file)
        {
            string[] readlines = File.ReadAllLines(file);
            Console.WriteLine("Application Author:");
            AppAuthor = readlines[2].Substring(7);
            Console.WriteLine(AppAuthor);
        }
        public static void app_desc(string file)
        {
            string[] readlines = File.ReadAllLines(file);
            Console.WriteLine("Application Description:");
            AppDesc = readlines[1].Substring(5);
            Console.WriteLine(AppDesc);
        }
        public static void app_title(string file)
        {
            string[] readlines = File.ReadAllLines(file);
            Console.WriteLine("Application Title:");
            AppTitle = readlines[0].Substring(6);
            Console.WriteLine(AppTitle);
        }
        private static void Verify(string file)
        {
            List<string> readlines = new List<string>();
            var lines = File.ReadAllLines(file);
            foreach (var line in lines) readlines.Add(line);
            if (readlines[0].StartsWith("Title="))
            {
                if (readlines[1].StartsWith("Desc="))
                {
                    if (readlines[2].StartsWith("Author="))
                    {
                        if (readlines[readlines.Count - 1].StartsWith("EOF"))
                        {
                            Execute(file);
                        }
                        else
                        {
                            Console.WriteLine("EOF expected: Valid Medli Applications finish with EOF");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Author expected: No author information on line 3");
                    }
                }
                else
                {
                    Console.WriteLine("Description expected: No application description on line 2");
                }
            }
            else
            {
                Console.WriteLine("Title expected: No application title on line 1");
            }
        }
        public static void PreExecute(string file)
        {
            if (file.EndsWith(".ma"))
            {
                Verify(file);
            }
            else
            {
                Console.WriteLine("Not a valid Medli Application!");
                Console.WriteLine("Medli Application filenames end in .ma");
            }
        }
        private static void ClearDraw()
        {
            Console.CursorTop = 1;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine(AppTitle);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorTop = 3;
        }
        private static void Execute(string file)
        {
            string[] readlines = File.ReadAllLines(file);
            AppTitle = readlines[0].Substring(6);
            AppDesc = readlines[1].Substring(5);
            AppAuthor = readlines[2].Substring(7);
            ClearDraw();
            foreach (string line in readlines)
            {
                if (line.StartsWith("Title="))
                {
                    AppTitle = line.Remove(0, 6);
                }
                else if (line.StartsWith("Desc="))
                {
                    AppDesc = line.Remove(0, 5);
                }
                else if (line.StartsWith("Author="))
                {
                    AppAuthor = line.Remove(0, 7);
                }
                else if (line == "clear")
                {
                    ClearDraw();
                }
                else if (line == "EOF")
                {

                }
                else
                {
                    if (Console.CursorTop == 23)
                    {
                        ClearDraw();
                    }
                    Shell.cmd(line);
                }
            }
        }
    }
}
