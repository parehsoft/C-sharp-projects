using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetUserInfoFromAD
{
    class ParameterParserImplementation : IParameterParser
    {
        private string LeftSideOfParam(string parameter, char splitter)
        {
            return parameter.Split(splitter).First();
        }

        private string RightSideOfParam(string parameter, char splitter)
        {
            bool isSplitterThere = false;
            foreach (char character in parameter)
            {
                if (character == splitter)
                {
                    isSplitterThere = true;
                    break;
                }
            }

            if (isSplitterThere == true)
            { return parameter.Split(splitter).Last(); }
            else
            { return string.Empty; }
        }
        
        public Dictionary<string, string> ParseParameters(string[] args, char splitter)
        {
            Dictionary<string, string> paramDictionary = new Dictionary<string, string>();

            foreach (string item in args)
            {
                try
                {
                    paramDictionary.Add(/*key*/ LeftSideOfParam(item, splitter), /*value*/ RightSideOfParam(item, splitter));
                }
                catch (System.ArgumentException)
                {
                    Console.WriteLine("An item with the same key has already been added.\n -> Same parameter was used more than once!");
                    Environment.Exit(1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(1);
                }
            }

            return paramDictionary;
        }
    }
}
