using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab2_3Spo
{
    public class Admin
    {
        public int adminId;
        public string adminName;
        public string adminPass;

        public Admin()
        {
        }

        public Admin(int adminId, string adminName, string adminPass)
        {
            this.adminId = adminId;
            this.adminName = adminName;
            this.adminPass = adminPass;
        }
    }


}
