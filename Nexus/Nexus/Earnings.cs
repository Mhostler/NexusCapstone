using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class Earnings
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public decimal Cash { get; set; }
        public decimal Credit { get; set; }
        public decimal Total
        {
            get
            {
                return Cash + Credit;
            }
        }
        public Earnings()
        {
            Id = 0;
            Cash = 0;
            Credit = 0;
        }

        public void Insert()
        {
            string query = "INSERT INTO DailyEarnings (Day, Cash, Credit) VALUES ('" + Day.ToString("yyyy-MM-dd") +
                "', " + Cash.ToString() + ", " + Credit.ToString() + ")";

            DBHandler.ExecuteNoReturn(query);
        }

        public void Update()
        {
            string query = "UPDATE DailyEarnings SET Day='" + Day.ToString("yyyy-MM-dd") + "', Cash=" + Cash.ToString() + ", Credit="
                + Credit.ToString() + " WHERE EarningsID=" + Id.ToString();

            DBHandler.ExecuteNoReturn(query);
        }
    }
}
