using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogDeleterConsole
{
    interface IFileProcessor
    {
        bool GivendDirectoryExists(string path);
        void DeleteFiles(string path, string[] ArrayOfFileExtensions, bool LoggingFlag);
    }
}
