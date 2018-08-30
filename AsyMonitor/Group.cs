using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyMonitor
{
    class Group
    {
        private string authId;
        private string groupName;

        public string AuthId { get => authId; set => authId = value; }
        public string GroupName { get => groupName; set => groupName = value; }
    }
}
