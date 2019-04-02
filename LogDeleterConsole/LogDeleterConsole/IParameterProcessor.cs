using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogDeleterConsole
{
    interface IParameterProcessor
    {
        string[] SeparateRightSideValues(string values, char separator = ',');
        Dictionary<string, string> ParseParameters(string[] args);
        Dictionary<string, string> ParseParameters(string[] args, char splitter);
    }
}
