using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogDeleterConsole
{
    class LogDeleterFactory
    {
        static HelperImplementation HI = null;
        public static IHelper GetHelperImplementation()
        {
            if (HI == null)
            {
                HI = new HelperImplementation();
            }
            
            return HI;
        }

        static ParameterProcessorImplementation PPI = null;
        public static IParameterProcessor GetParameterProcessorImplementation(char splitter)
        {
            if (PPI == null)
            {
                PPI = new ParameterProcessorImplementation(splitter);
            }

            return PPI;
        }

        static FileProcessorImplementation FPI = null;
        public static IFileProcessor GetFileProcessorImplementation()
        {
            if (FPI == null)
            {
                FPI = new FileProcessorImplementation();
            }

            return FPI;
        }
    }
}
