﻿using System;
using System.IO;
using Medli.System;
using Medli.SysInternal;
using Medli.GUI;
using Medli.Command_db.Commands;

namespace Medli.Applications
{
    class Shell
    { 
        public static void Cmd(string input)
        {
            var command = input.ToLower();
            string[] cmd_args = input.Split(' ');
            if (command == "cd ..")
            {
                try
                {
                    if (MEnvironment.current_dir == MEnvironment.root_dir)
                    {
                        Console.WriteLine("Cannot go up any more levels!");
                    }
                    else
                    {
                        var pos = MEnvironment.current_dir.LastIndexOf('\\');
                        if (pos >= 0)
                        {
                            MEnvironment.current_dir = MEnvironment.current_dir.Substring(0, pos) + @"\";
                        }
                        /*                        
                        var dir = FSfunc.fs.GetDirectory(Kernel.current_dir);
                        string p = dir.mParent.mName;
                        if (!string.IsNullOrEmpty(p))
                        {
                            Kernel.current_dir = p;
                        }
                        */
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandler.Init(0, ex.Message, false, "");
                }
            }
            else if (command.StartsWith("cd "))
            {
                Fsfunc.cd(cmd_args[1]);
            }
            else if (command == "color")
            {
                Console.WriteLine("Usage: color <background> <foreground>");
            }
            else if (command.StartsWith("color "))
            {
                ColorChanger.ChangeBGC(cmd_args[1]);
                ColorChanger.ChangeFGC(cmd_args[2]);
            }
            else if (command.StartsWith("launch "))
            {
                Console.Clear();
                AppLauncher.PreExecute(cmd_args[1]);
            }
            else if (command == "fsinit")
            {
                MEnvironment.PressAnyKey();
                Console.Clear();
                Init.InitMain();
            }
            else if (command == "reinstall")
            {
                try
                {
                    //Directory.Delete(Kernel.root_dir + @"\" + KernelVariables.username);
                    Fsfunc.delfile(KernelVariables.usrinfo);
                    Fsfunc.delfile(KernelVariables.pcinfo);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Reinstallation failed!");
                    Console.WriteLine("Reason: " + ex.Message);
                }
                Console.WriteLine("Press any key to reboot...");
                Console.ReadKey(true);
                Sysfunc.reboot();

            }
            else if (command == "time")
            {
                MedliTime.printTime();
            }
            else if (command == "date")
            {
                MedliTime.printDate();
            }
            /*
            else if (command == "day")
            {
                DateTime.Now.Day.ToString();
            }
            */
            else if (command.StartsWith("run "))
            {
                if (!File.Exists(MEnvironment.current_dir + cmd_args[1]))
                {
                    InvalidCommand(command.Remove(0, 4), 2);
                }
                else
                {
                    Mdscript.Execute(MEnvironment.current_dir + cmd_args[1]);
                }
            }
            else if (command == "rm")
            {
                Console.WriteLine("Usage: rm <file or directory>");
            }
            else if (command.StartsWith("rm "))
            {
                if (cmd_args[1] == "-r")
                {
                    try
                    {
                        Fsfunc.deldir(cmd_args[2]);
                    }
                    catch (Exception exdir)
                    {
                        Console.WriteLine(exdir.Message);
                        //InvalidCommand(cmd_args[2], 2);
                    }
                }
                else
                {
                    try
                    {
                        Fsfunc.delfile(cmd_args[1]);
                    }
                    catch (Exception exfile)
                    {
                        Console.WriteLine(exfile.Message);
                        //InvalidCommand(cmd_args[1], 2);
                    }
                }
                try
                {
                    Fsfunc.deldir(cmd_args[1]);
                }
                catch (Exception ex)
                {
                    try
                    {
                        Fsfunc.delfile(cmd_args[1]);
                    }
                    catch (Exception exfile)
                    {
                        Console.WriteLine(exfile.Message);
                        Console.WriteLine(ex.Message);
                        InvalidCommand(cmd_args[1], 2);
                    }
                }
            }
            else if (command == "getram")
            {
                Sysfunc.ram();
            }
            else if (command == "dir")
            {
                Fsfunc.dir();
            }
            else if (command.StartsWith("miv "))
            {
                MIV.StartMIV(cmd_args[1]);
            }
            else if (command == "miv")
            {
                MIV.StartMIV();
            }
            else if (command == "reboot")
            {
                Sysfunc.reboot();
            }
            else if (command == "shutdown")
            {
                Sysfunc.shutdown();
            }
            else if (command == "logout")
            {
                Accounts.UserManagement.UserLogin();
            }
            else if (command == "panic")
            {
                ErrorHandler.Init(1, "Medli received the 'panic' command, Nothing's gonna happen.", false, "");
            }
            else if (command.StartsWith("panic"))
            {
                if (cmd_args[1] == "critical")
                {
                    ErrorHandler.Init(0, "Medli received the 'panic critical' command , Nothings gonna happen", true, "User-invoked panic");
                }
                else if (cmd_args[1] == "userlvl")
                {
                    ErrorHandler.Init(1, "Medli received the 'panic userlvl' command, Nothing's gonna happen.", false, "");
                }
            }
            else if (command == "getvol")
            {
                Fsfunc.getVols();
            }
            else if (command == "startm")
            {
                MUI.Init();
            }
            else if (command == "cowsay")
            {
                Cowsay.Cow("Say something using 'Cowsay <message>'");
                Console.WriteLine(@"You can also use 'cowsay -f' tux for penguin, cow for cow and 
sodomized-sheep for, you guessed it, a sodomized-sheep");
            }
            else if (command.StartsWith("cowsay"))
            {
                if (cmd_args[1] == "-f")
                {
                    if (cmd_args[2] == "cow")
                    {
                        Cowsay.Cow(command.Remove(0, cmd_args[0].Length + cmd_args[1].Length + cmd_args[2].Length + 3));
                    }
                    else if (cmd_args[2] == "tux")
                    {
                        Cowsay.Tux(command.Remove(0, cmd_args[0].Length + cmd_args[1].Length + cmd_args[2].Length + 3));
                    }
                    else if (cmd_args[2] == "sodomized-sheep")
                    {
                        Cowsay.SodomizedSheep(command.Remove(0, cmd_args[0].Length + cmd_args[1].Length + cmd_args[2].Length + 3));
                    }
                }
                else
                {
                    Cowsay.Cow(command.Substring(7));
                }
            }
            else if (command == "mkdir")
            {
                Console.WriteLine("Usage: mkdir <directory>");
            }
            else if (command.StartsWith("mkdir "))
            {
                Fsfunc.mkdir(cmd_args[1]);
            }
            else if (command == "switch_to")
            {
                Console.WriteLine("Please enter a terminal to switch to by using switch_to 'number 1-4'.");
            }
            else if (command.StartsWith("switch_to "))
            {
                if (cmd_args[1] == "1")
                {
                    ShellInfo.shell1.Run(1);
                }
                else if (cmd_args[1] == "2")
                {
                    ShellInfo.shell1.Run(2);
                }
                else if (cmd_args[1] == "3")
                {
                    ShellInfo.shell1.Run(3);
                }
                else if (cmd_args[1] == "4")
                {
                    ShellInfo.shell1.Run(4);
                }
                else
                {
                    Console.WriteLine("Please enter a terminal to switch to by using switch_to 'number 1-4'.");
                }
            }
            else if (command == "switch_to")
            {
                Console.WriteLine("Please enter a terminal to switch to.");
            }
            else if (command.StartsWith("alias "))
            {
                Console.WriteLine("Aliases are cleared over reboot. W.I.P");
                throw new NotImplementedException();
            }
            else if (command == "clear")
            {
                Sysfunc.clearScreen();
            }
            else if (command.StartsWith("cedit "))
            {
                Cpedit.Run(cmd_args[1]);
            }
            else if (command.StartsWith("devenv "))
            {
                if (command.EndsWith(".ma"))
                {
                    MIDE.Run(cmd_args[1]);
                }
                else
                {
                    Console.WriteLine("The IDE may only be used to create (.ma) Medli application files.");
                }
            }
            else if (command == "cview")
            {
                Console.WriteLine("cview Usage: cview <file>");
            }
            else if (command.StartsWith("cview "))
            {
                Cpview.ViewFile(cmd_args[1]);
            }
            else if (input == "lock")
            {
                Console.WriteLine("Missing password - try again.");
            }
            else if (input.StartsWith("lock"))
            {
                bool locked = true;
                while (locked == true)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Clear();
                    Console.WriteLine("Insert correct password: ");
                    string pwd = Console.ReadLine();
                    if (pwd == MEnvironment.usrpass)
                    {
                        Console.WriteLine("Correct - unlocking system");
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Black;
                        locked = false;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect password - try again.");
                        locked = true;
                    }
                }
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (command.StartsWith("echo "))
            {
                try
                {
                    Console.WriteLine(command.Remove(0, 5));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (command == "help")
            {
                GetHelp.Full();
            }
            else if (command.StartsWith("help "))
            {
                if (cmd_args[1] == "1" || cmd_args[1] == "app")
                {
                    GetHelp.Pages(1);
                }
                else if (cmd_args[1] == "2" || cmd_args[1] == "fs")
                {
                    GetHelp.Pages(2);
                }
                else if (cmd_args[1] == "3" || cmd_args[1] == "sys")
                {
                    GetHelp.Pages(3);
                }
                else if (cmd_args[1] == "specific")
                {
                    GetHelp.Specific(cmd_args[2]);
                }
                else
                {
                    GetHelp.Full();
                }
            }
            else if (command == "ver")
            {
                KernelVariables.Ver();
            }
            else if (command == "")
            {

            }
            else
            {
                InvalidCommand(command, 1);
            }
        }
        public static void InvalidCommand(string args, int errorlvl)
        {
            if (errorlvl == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(args);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" is not a valid command, see 'help' for a list of commands");
            }
            else if (errorlvl == 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("The file ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(args);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" could not be found!");

            }
            else if (errorlvl == 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (errorlvl == 4)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
        }
    }
}
