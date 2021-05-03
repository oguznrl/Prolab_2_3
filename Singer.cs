using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab2_3Spo
{
    public class Singer
    {
        public int singerId;
        public string singerName;
        public string country;

        public Singer()
        {

        }
        public Singer(int singerId, string singerName, string country)
        {
            this.singerId = singerId;
            this.singerName = singerName;
            this.country = country;
        }

    }
}
