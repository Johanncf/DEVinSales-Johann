using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
        }

        public List<UserRole> UserRoles { get; set; }
    }
}
