using DataAccess;
using Modelo.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaDelModelo
{
    public class UserViewModel
    {
        private UserDb DbUser { get; }

        public UserViewModel()
        {
            this.DbUser = new UserDb();
        }

        public User GetAllByUserId(Guid uid)
        {
            return this.DbUser.GetUser(uid);
        }
    }
}
