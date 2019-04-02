using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetUserInfoFromAD
{
    interface IParameterParser
    {
        Dictionary<string, string> ParseParameters(string[] args, char splitter);
    }
}
