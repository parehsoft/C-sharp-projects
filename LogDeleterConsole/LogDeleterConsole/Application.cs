using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogDeleterConsole
{
    class Application
    {
        public int Run(string[] args)
        {
            Dictionary<string, string> DOfParameters = new Dictionary<string, string>();
            IParameterProcessor IPP = LogDeleterFactory.GetParameterProcessorImplementation('='); // '=' is the splitter
            try
            { DOfParameters = IPP.ParseParameters(args); }
            catch (System.ArgumentException)
            {
                Console.WriteLine("Some parameters were used more than once!");
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
            
            if (DOfParameters.Count == 0)
            { return 1; }

            bool pathFlag = false;
            bool extensionFlag = false;
            bool loggingFlag = true; // Logging of processed files is enabled by default.

            foreach (KeyValuePair<string, string> keyValuePair in DOfParameters)
            {
                switch (keyValuePair.Key)
                {
                    case "-p":
                        pathFlag = true;
                        if (keyValuePair.Value == string.Empty)
                        {
                            Console.WriteLine("Path parameter can not be empty!");
                            return 1;
                        }
                        break;

                    case "-e":
                        extensionFlag = true;
                        if (keyValuePair.Value == string.Empty)
                        {
                            Console.WriteLine("Extension parameter can not be empty!");
                            return 1;
                        }
                        break;

                    case "--NoLogging":
                        loggingFlag = false;
                        break;

                    default:
                        Console.WriteLine("Wrong parameters insertion.");
                        return 1;
                    // break;
                }
            }

            if ((pathFlag == true) && (extensionFlag == true))
            {
                IFileProcessor IFP = LogDeleterFactory.GetFileProcessorImplementation();

                string givenFolder = DOfParameters["-p"];
                if (IFP.GivendDirectoryExists(givenFolder) == false)
                {
                    Console.WriteLine("Given folder (" + givenFolder + ") doesn't exist.");
                    return 1;
                }

                IFP.DeleteFiles(givenFolder, IPP.SeparateRightSideValues(DOfParameters["-e"]), loggingFlag);
                
                return 0;
            }

            Console.WriteLine("Wrong parameter insertion! Use -p= with combination of -e=.");
            return 1; 
        }
    }
}
