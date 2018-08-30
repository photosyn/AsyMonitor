using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyMonitor
{
    class Role
    {
        private bool ischeck;
        private string roleId;
        private string roleName;
        private bool isDefault;

        public bool Ischeck { get => ischeck; set => ischeck = value; }
        public string RoleId { get => roleId; set => roleId = value; }
        public string RoleName { get => roleName; set => roleName = value; }
        public bool IsDefault { get => isDefault; set => isDefault = value; }
    }
}
