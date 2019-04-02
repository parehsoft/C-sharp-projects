using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogDeleterConsole
{
    class ParameterProcessorImplementation : IParameterProcessor
    {
        private char splitter = '=';
        public char Splitter
        {
            get { return splitter; }
        }

        private string LeftSide(string parameter)
        {
            return parameter.Split(this.splitter).First();
        }

        private string LeftSide(string parameter, char splitter)
        {
            return parameter.Split(splitter).First();
        }

        private string RightSide(string parameter)
        {
            bool isSplitterThere = false;
            foreach (char character in parameter)
            {
                if (character == this.splitter)
                {
                    isSplitterThere = true;
                    break;
                }
            }

            if (isSplitterThere == true)
            { return parameter.Split(this.splitter).Last(); }
            else
            { return string.Empty; }
        }

        private string RightSide(string parameter, char splitter)
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

        /* PUBLIC */
        public string[] SeparateRightSideValues(string values, char separator = ',')
        {
            return values.Split(separator);
        }

        public Dictionary<string, string> ParseParameters(string[] args, char splitter)
        {
            Dictionary<string, string> paramDictionary = new Dictionary<string, string>();

            foreach (string item in args)
            {
                paramDictionary.Add(/*key*/ LeftSide(item, splitter), /*value*/ RightSide(item, splitter));
            }

            return paramDictionary;
        }

        public Dictionary<string, string> ParseParameters(string[] args)
        {
            Dictionary<string, string> paramDictionary = new Dictionary<string, string>();

            foreach (string item in args)
            {
                paramDictionary.Add(/*key*/ LeftSide(item), /*value*/ RightSide(item));
            }

            return paramDictionary;
        }

        /* CONSTRUCTOR */
        //empty constructor:
        public ParameterProcessorImplementation() { }
        //set your private spliter when instantiating:
        public ParameterProcessorImplementation(char splitter)
        {
            if (splitter == '\0')
            { this.splitter = '='; }
            else
            { this.splitter = splitter; }
        }
    }
}
