using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Threading;
using Sys = Cosmos.System;

namespace GraphicTest
{
    public class Kernel : Sys.Kernel
    {
        Canvas canvas;

        Sys.FileSystem.CosmosVFS fs = new Cosmos.System.FileSystem.CosmosVFS();

        public static void MoveFile(string file, string newpath)
        {
            try
            {
                File.Copy(file, newpath);
                File.Delete(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private readonly Bitmap bitmap = new Bitmap(10, 10,
                new byte[] { 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255,
                    23, 59, 88, 255, 0, 255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255, 23, 59, 88, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255, 153, 57, 12, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 0, 255, 243, 255, 153, 57, 12, 255, 23, 59, 88, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243,
                    255, 0, 255, 243, 255, 0, 255, 243, 255, 72, 72, 72, 255, 72, 72, 72, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 72, 72,
                    72, 255, 72, 72, 72, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 10, 66, 148, 255, 10, 66, 148, 255,
                    10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148,
                    255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, }, ColorDepth.ColorDepth32);

        protected override void BeforeRun()
        {
            // If all works correctly you should not really see this :-)
            Console.WriteLine("Cosmos booted successfully.");

            /*
            You don't have to specify the Mode, but here we do to show that you can.
            To not specify the Mode and pick the best one, use:
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            */
            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));

            // This will clear the canvas with the specified color.
            canvas.Clear(Color.Black);
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            VFSManager.GetDisks();
        }

        protected override void Run()
        {
            try
            {
                // A red Point
                canvas.DrawPoint(Color.Red, 69, 69);

                // Top horizontal line (moved right 5, up 10)
                canvas.DrawLine(Color.GreenYellow, 210, 155, 400, 155);

                // Left vertical line (moved right 5, up 10)
                canvas.DrawLine(Color.IndianRed, 210, 155, 210, 305);

                // Bottom horizontal line (moved right 5, up 10)
                canvas.DrawLine(Color.MintCream, 210, 305, 400, 305);

                Font font = PCScreenFont.Default;
                canvas.DrawString("Welcome to CollabOS", font, Color.White, 230, 380);
                Console.WriteLine("Welcome to CollabOS");
                canvas.Display(); // Required for something to be displayed when using a double buffered dri
                canvas.Display();


                Thread.Sleep(5000);

                canvas.Disable();// Required For Bootup Screen to be Removed 

                var available_space = fs.GetAvailableFreeSpace(@"0:\");

                // Start reading input in a loop
                while (true)
                {
                    Console.Write("root@CollabOS:~$ ");
                    var input = Console.ReadLine();

                    if (!(input == "hello" || input == "diskspace" || input == "help" || input == "crf" || input == "crd" || input == "ls" || input == "edit" || input == "mv" || input == "del" || input == "delD" || input == "rdfl"))
                    {
                        Console.WriteLine(input + " Command Not Found");
                    }
                    if (input == "hello")
                    {
                        Console.WriteLine("Hello! Welcome to Collab OS. Please type help for more commands.");
                    }
                    if (input == "diskspace")
                    {
                        Console.WriteLine("Available Free Space: " + available_space);
                    }
                    if (input == "help")
                    {
                        Console.WriteLine("hello: show a nice message and greet the user");
                        Console.WriteLine("diskspace: show the available diskspace");
                        Console.WriteLine("help: show this help message");
                        Console.WriteLine("crf: create a file");
                        Console.WriteLine("crd: create a directory");
                        Console.WriteLine("ls: list files and directories in a directory");
                        Console.WriteLine("edit: edit a file");
                        Console.WriteLine("mv: move a file or directory");
                        Console.WriteLine("del: delete a file");
                        Console.WriteLine("delD: delete a directory");
                        Console.WriteLine("rdfl: read a file");
                        Console.WriteLine("shutdown: shutdown the system");
                        Console.WriteLine("restart: restart the system");
                    }
                    if (input == "ls")
                    {
                        var files_list = Directory.GetFiles(@"0:\");
                        var directory_list = Directory.GetDirectories(@"0:\");

                        foreach (var file in files_list)
                        {
                            Console.WriteLine(file);
                        }
                        foreach (var directory in directory_list)
                        {
                            Console.WriteLine(directory);
                        }
                    }
                    if (input == "crf")
                    {
                        Console.WriteLine("What do you want the file to be named?");
                        string filename = Console.ReadLine();
                        try
                        {
                            var file_stream = File.Create(@"0:\" + filename);
                            Console.WriteLine("File " + filename + " created!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    if (input == "crd")
                    {
                        Console.WriteLine("What do you want to name your folder?");
                        string foldername = Console.ReadLine();
                        try
                        {
                            Directory.CreateDirectory(@"0:\" + foldername);
                            Console.WriteLine("Folder " + foldername + " created!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    if (input == "edit")
                    {
                        Console.WriteLine("What file do you want to edit?");
                        string filetoedit = Console.ReadLine();
                        Console.WriteLine("What would you like to add?");
                        string addition = Console.ReadLine();
                        try
                        {
                            File.WriteAllText(@"0:\" + filetoedit, addition);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    if (input == "del")
                    {
                        Console.WriteLine("What file do you want to delete?");
                        string filetoremove = Console.ReadLine();
                        try
                        {
                            File.Delete(@"0:\" + filetoremove);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    if (input == "delD")
                    {
                        Console.WriteLine("What folder do you want to delete?");
                        string directorytoremove = Console.ReadLine();
                        try
                        {
                            Directory.Delete(@"0:\" + directorytoremove);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    if (input == "mv")
                    {
                        Console.WriteLine("What file or folder do you want to move");
                        string objecttomove = Console.ReadLine();
                        Console.WriteLine("Where?");
                        string pathtomove = Console.ReadLine();
                        try
                        {
                            string start = @"0:\";
                            MoveFile(@"0:\" + objecttomove, start + pathtomove);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    if (input == "rdfl")
                    {
                        Console.WriteLine("Type the name of the file you want to read");
                        string filetoread = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(File.ReadAllText(@"0:\" + filetoread));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    if (input == "shutdown")
                    {
                        Console.WriteLine("GoodBye! We hope you had a good time using Collab OS.");
                        Sys.Power.Shutdown();
                    }
                    if (input == "restart")
                    {
                        Console.WriteLine("restarting...");
                        Sys.Power.Reboot();
                    }
                }
            }
            catch (Exception e)
            {
                Sys.Power.Shutdown();
            }
        }
    }
}
