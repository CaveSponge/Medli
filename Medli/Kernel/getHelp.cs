﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medli
{
    /// <summary>
    /// Help class, called by mshell
    /// </summary>
    public class getHelp
    {
        /*
         * A very primitive paging system (not RAM paging, I mean manpages etc...)
         * Tidied up a bit, might avoid the text-spilling-onto-next-line issue but certainly doesn't eliminate it
         * That's an issue which needs sorting, probs leave it until we go .NET Core
         */
         /// <summary>
         /// Creates multiple pages of 'help' so its not listed out in one go,
         /// makes for easier reading of available commands
         /// </summary>
         /// <param name="pageno"></param>
        public static void pages(int pageno)
        {
            if (pageno == 1)
            {
                Console.WriteLine("echo         \tPrints text to the console");
                Console.WriteLine("mkdir        \tMakes a directory");
                Console.WriteLine("dir          \tPrints a list of directories in the current directory");
                Console.WriteLine("cd           \tChanges the current directory");
                Console.WriteLine("clear        \tClears the screen");
                Console.WriteLine("cowsay <text>\tA little *nix easter egg ;)");
                Console.WriteLine("Help page 1 - Press any key...");
                Console.ReadKey(true);
                Console.WriteLine(" ");
            }
            else if (pageno == 2)
            {
                Console.WriteLine("cv <file>    \tPrints the contents of a file onto the screen.");
                Console.WriteLine("cp <file>    \tLaunches the text editor");
                Console.WriteLine("miv          \tLaunches the MIV advanced text editor");
                Console.WriteLine("reboot       \tReboots the system");
                Console.WriteLine("shutdown     \tCloses applications and powers down the system.");
                Console.WriteLine("shell2       \tLaunches the new shell (W.I.P)");
                Console.WriteLine("Help page 2 - Press any key...");
                Console.ReadKey(true);
                Console.WriteLine(" ");
            }
            else if (pageno == 3)
            {
                Console.WriteLine("run <file>   \tExecutes the script passed as <file>");
                Console.WriteLine("getram       \tGets the amount of system RAM in megabytes");
                Console.WriteLine("panic        \tStarts a harmless kernel panic");
                Console.WriteLine("Help page 3 - Press any key...");
                Console.ReadKey(true);
                Console.WriteLine(" ");
            }
            else
            {
                Console.Clear();
                full();
            }
        }
        /// <summary>
        /// prints out all help pages after each other
        /// </summary>
        public static void full()
        {
            pages(1);
            pages(2);
            pages(3);
        }
        /// <summary>
        /// Gets the detailed help for a specific command given to the method
        /// </summary>
        /// <param name="topic"></param>
        public static void specific(string topic)
        {
            if (topic == "mkdir")
            {
                Console.WriteLine("mkdir\tMakes a directory");
            }
            else if (topic == "panic" || topic == "panic critical")
            {
                Console.WriteLine("panic\tStarts a harmless kernel panic");
                Console.WriteLine("panic critical\tStarts a critical yet harmless kernel panic");
            }
            else if (topic == "cowsay")
            {
                Console.WriteLine("cowsay <text>\tA little *nix easter egg ;)");
            }
            else if (topic == "cv")
            {
                Console.WriteLine("cv <file>\tPrints the contents of a file onto the screen.");
            }
            else if (topic == "cp")
            {
                Console.WriteLine("cp <file>\tLaunches the text editor");
            }
            else if (topic == "miv")
            {
                Console.WriteLine("miv\tLaunches the MIV advanced text editor");
            }
            else if (topic == "reboot")
            {
                Console.WriteLine("reboot\tReboots the system");
            }
            else if (topic == "shutdown")
            {
                Console.WriteLine("shutdown\tCloses applications and powers down the system.");
            }
            else if (topic == "shell2")
            {
                Console.WriteLine("shell2\tLaunches the new shell (W.I.P)");
            }
            else if (topic == "clear")
            {
                Console.WriteLine("clear\tClears the screen");
            }
            else if (topic == "dir")
            {
                Console.WriteLine("dir\tPrints a list of directories in the current directory");
            }
            else if (topic == "cd")
            {
                Console.WriteLine("cd\tChanges the current directory");
            }
            else if (topic == "echo")
            {
                Console.WriteLine("echo\tPrints text to the console");
            }
            else if (topic == "getram")
            {
                Console.WriteLine("getram\tGets the amount of system RAM in megabytes");
            }
            else if (topic == "")
            {
                full();
            }
            else
            {
                Console.WriteLine(topic + ": Not a valid command.");
                full();
            }
        }
    }
}