//////////////////////////////////////////////////
//Author    : Mohammed Gaffer Aidaab
//For       : King Faisual University
//Under     : ISB integrated sulution business Company
//App       : PeopleOfDetermination Application (())
//Date      : July - 2024 
/////////////////////////////////////////////////////

using Core.Enums;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class StaticRepository
    {
        private readonly DbConn _StaticContext;

        public StaticRepository()
        {
            _StaticContext = new DbConn();
        }

        public string GetUserRole(string Username)
        {

            var user = _StaticContext.Users.FirstOrDefault(m => m.UserName == Username);

            Roles roleName = Roles.User;


            if (user != null)
            {
               roleName = _StaticContext.Users.FirstOrDefault(m => m.UserName == Username).Role;
            }


            if (roleName != null)
            {
                return Roles.GetName(roleName);
            }
            else
            {
                return null;
            }

        }

    }
}
