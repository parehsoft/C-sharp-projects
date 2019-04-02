using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogDeleterConsole
{
    class FileProcessorImplementation : IFileProcessor
    {
        private bool ShellIDeleteAllFiles(string[] ArrayOfFileExtensions, bool loggingFlag)
        {
            foreach (string extension in ArrayOfFileExtensions)
            {
                if (extension == "*")
                {
                    if (loggingFlag == true)
                    { Console.WriteLine("Asterisk is present, deleting all files."); }
                    return true;
                }
            }

            return false;
        }
        
        private void ProcessDirectory(string targetDirectory, string[] ArrayOfFileExtensions, bool loggingFlag, bool deleteAllFiles) 
        {
            List<string> ListOfFiles = new List<string>();

            if (deleteAllFiles == true)
            {
                foreach (string file in Directory.GetFiles(targetDirectory))
                {
                    ListOfFiles.Add(file);
                }
            }
            else
            {
                foreach (string item in ArrayOfFileExtensions)
                {
                    string fileExtnesion = "*." + item;
                    foreach (string file in Directory.GetFiles(targetDirectory, fileExtnesion))
                    {
                        ListOfFiles.Add(file);
                    }
                }
            }

            if (ListOfFiles.Count == 0)
            {
                if (loggingFlag == true)
                { Console.WriteLine(" No files to delete."); }
            }

            string[] fileEntries = ListOfFiles.ToArray();
            foreach (string fileEntry in fileEntries)
            {
                if (loggingFlag == true)
                { Console.Write(Path.GetFileName(fileEntry) + " - "); }

                try
                {
                    File.Delete(fileEntry);
                    if (loggingFlag == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("OK");
                        Console.ResetColor();
                    }
                }
                catch (IOException)
                {
                    if (loggingFlag == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("not deleted");
                        Console.ResetColor();
                    }
                }
                catch (Exception exc)
                {
                    if (loggingFlag == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("not deleted, special exception:");
                        Console.WriteLine(exc.Message);
                        Console.ResetColor();
                    }
                }
            }

            // Recurse into subdirectories of this directory.
            string [] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                if (loggingFlag == true)
                {Console.WriteLine("\n- Processing files in " + subdirectory + " :"); }

                ProcessDirectory(subdirectory, ArrayOfFileExtensions, loggingFlag, deleteAllFiles);
            }    
        }
        
        public bool GivendDirectoryExists(string path)
        {
            if (Directory.Exists(path) == true)
            { return true; }

            return false;
        }

        public void DeleteFiles(string path, string[] ArrayOfFileExtensions, bool loggingFlag)
        {
            if (loggingFlag == true)
            { Console.WriteLine("Processing files in: " + path + " :"); }

            bool deleteAllFiles = false;
            if (ShellIDeleteAllFiles(ArrayOfFileExtensions, loggingFlag) == true)
            {
                deleteAllFiles = true;
            }

            ProcessDirectory(path, ArrayOfFileExtensions, loggingFlag, deleteAllFiles);
        }
    }
}
