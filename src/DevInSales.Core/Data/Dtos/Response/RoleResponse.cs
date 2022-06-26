using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Dtos
{
    public class RoleResponse
    {
        public RoleResponse(string roleName, int roleId)
        {
            RoleName = roleName;
            RoleId = roleId;
        }

        public string RoleName { get; set; }
        public int RoleId { get; set; }
    }
}
