using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace GetUserInfoFromAD
{
    class ActiveDirectoryImplementation : IActiveDirectory
    {
        private Domain domain = null;
        private PrincipalContext oPrincipalContext = null;

        // Constructor
        public ActiveDirectoryImplementation(string connectionString = null)
        {
            string DNSDomainName, logonName, password;
            try
            {
                if (connectionString == null)
                {
                    domain = Domain.GetCurrentDomain();
                    oPrincipalContext = new PrincipalContext(ContextType.Domain, domain.Name);
                }
                else
                {
                    ParseConnectionString(connectionString, out DNSDomainName, out logonName, out password);
                    DirectoryContext directoryContext = new DirectoryContext(DirectoryContextType.Domain, DNSDomainName, logonName, password);
                    domain = Domain.GetDomain(directoryContext);
                    oPrincipalContext = new PrincipalContext(ContextType.Domain, domain.Name, logonName, password);
                }
                
            }
            catch (ActiveDirectoryObjectNotFoundException)
            {
                Console.WriteLine("Active Directory structure not found.");
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Active Directory error:\n" + e.Message);
                Environment.Exit(1);
            }

            try
            {
                if (domain.Name == string.Empty)
                {
                    Console.WriteLine("Domain Name is empty.");
                    Environment.Exit(1);
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Null reference exeption. Domain object was not created.");
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Other error:\n" + e.Message);
                Environment.Exit(1);
            }
        }

        public void DisplayDomain()
        {
            Console.WriteLine(ReturnDomainName());
        }

        public string ReturnDomainName()
        {
            return domain.Name;
        }

        public ArrayList EnumerateRootDirectory()
        {
            ArrayList alDcs = new ArrayList();

            foreach (DomainController dc in domain.DomainControllers)
            {
                alDcs.Add(dc.Name);
            }
            return alDcs;
        }

        public ArrayList GetUsersGroups(string user)
        {
            ArrayList allOfGroups = new ArrayList();

            UserPrincipal oUserPrincipal = new UserPrincipal(oPrincipalContext);
            oUserPrincipal = UserPrincipal.FindByIdentity(oPrincipalContext, user);
            if (oUserPrincipal == null)
            {
                return allOfGroups;
            }

            // This won't work if alOfGroups is empty. -> If oUserPrincipal is empty, it is checked few lines above this comment.
            PrincipalSearchResult<Principal> oPrincipalSearchResult = oUserPrincipal.GetGroups();
            oPrincipalSearchResult = oUserPrincipal.GetGroups();
            foreach (Principal oResult in oPrincipalSearchResult)
            {
                allOfGroups.Add(oResult.Name);
            }

            return allOfGroups;
        }

        public bool IsUserWithinGroup(string user, string group)
        {
            ArrayList allOfGroups = GetUsersGroups(user);
            
            foreach (Object o in allOfGroups)
            {
                if (o.ToString() == group)
                { return true; }
            }

            return false;
        }

        public bool IsUsersPassword(string user, string password)
        {
            return oPrincipalContext.ValidateCredentials(user, password);
        }

        private void ParseConnectionString(string connectionString, out string DNSDomainName, out string logonName, out string password)
        {
            string[] parts = connectionString.Split(',');
            DNSDomainName = parts[0];
            logonName = parts[1];
            password = parts[2];

            if (DNSDomainName == string.Empty)
            {
                Console.WriteLine("DNS name of the desired domain in parameter -d is empty.");
                Console.WriteLine("Remote domain parameter usage: -d=DNSDomainName,userName,password .");
                Environment.Exit(1);
            }
            if (logonName == string.Empty)
            {
                Console.WriteLine("User name part in parameter -d is empty.");
                Console.WriteLine("Remote domain parameter usage: -d=DNSDomainName,userName,password .");
                Environment.Exit(1);
            }
            if (password == string.Empty)
            {
                Console.WriteLine("Password part in parameter -d is empty.");
                Console.WriteLine("Remote domain parameter usage: -d=DNSDomainName,userName,password .");
                Environment.Exit(1);
            }
        }
    }
}
