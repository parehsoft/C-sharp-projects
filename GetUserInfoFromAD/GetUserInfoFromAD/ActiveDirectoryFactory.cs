using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetUserInfoFromAD
{
    class ActiveDirectoryFactory
    {
        static ActiveDirectoryImplementation ADImplementation = null;
        static HelperImplementation HImplementation = null;
        static ParameterParserImplementation PPImplementation = null;
        
        public static IActiveDirectory GetActiveDirectoryInterface()
        {
            if (ADImplementation == null)
            {
                ADImplementation = new ActiveDirectoryImplementation();
            }

            return ADImplementation;
        }      
        
        public static IHelper GetHelperDirectoryInterface()
        {
            if (HImplementation == null)
            {
                HImplementation = new HelperImplementation();
            }

            return HImplementation;
        }

        // For external domain.
        public static IActiveDirectory GetActiveDirectoryInterface(string connectionString)
        {
            if (ADImplementation == null)
            {
                ADImplementation = new ActiveDirectoryImplementation(connectionString);
            }

            return ADImplementation;
        }

        public static IParameterParser GetParameterParserInterface()
        {
            if (PPImplementation == null)
            {
                PPImplementation = new ParameterParserImplementation();
            }

            return PPImplementation;
        }
    }
}
