using System.Collections;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace GetUserInfoFromAD
{
    public interface IActiveDirectory
    {
        void DisplayDomain();
        string ReturnDomainName();
        ArrayList EnumerateRootDirectory();
        ArrayList GetUsersGroups(string user);
        bool IsUserWithinGroup(string user, string group);
        bool IsUsersPassword(string user, string sPassword);
    }
}
