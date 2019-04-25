using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// Stores the information for an earnings record
    /// </summary>
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

        /// <summary>
        /// default constructor sets values to zero
        /// </summary>
        public Earnings()
        {
            Id = 0;
            Cash = 0;
            Credit = 0;
        }

        /// <summary>
        /// inserts earnings into database
        /// </summary>
        public void Insert()
        {
            string query = "INSERT INTO DailyEarnings (Day, Cash, Credit) VALUES ('" + Day.ToString("yyyy-MM-dd") +
                "', " + Cash.ToString() + ", " + Credit.ToString() + ")";

            DBHandler.ExecuteNoReturn(query);
        }

        /// <summary>
        /// updates earnings in database to new values
        /// </summary>
        public void Update()
        {
            string query = "UPDATE DailyEarnings SET Day='" + Day.ToString("yyyy-MM-dd") + "', Cash=" + Cash.ToString() + ", Credit="
                + Credit.ToString() + " WHERE EarningsID=" + Id.ToString();

            DBHandler.ExecuteNoReturn(query);
        }
    }
}
