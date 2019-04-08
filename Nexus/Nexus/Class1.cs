using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class Earnings
    {
        int Id { get; set; }
        decimal Cash { get; set; }
        decimal Credit { get; set; }

        public Earnings()
        {
            Id = 0;
            Cash = 0;
            Credit = 0;
        }
    }
}
