using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class Earnings
    {
        DateTime Date { get; set; }
        decimal Cash { get; set; }
        decimal Credit { get; set; }
        decimal Total
        {
            get { return Cash + Credit; }
        }

        public Earnings()
        {
            Date = DateTime.Today;
            Cash = 0.0M;
            Credit = 0.0M;
        }

        public Earnings(DateTime d, decimal cash, decimal credit)
        {
            Date = d;
            Cash = cash;
            Credit = credit;
        }

        public Earnings(Earnings e)
        {
            Date = e.Date;
            Cash = e.Cash;
            Credit = e.Credit;
        }
    }
}
