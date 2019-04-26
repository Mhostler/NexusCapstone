using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    /// <summary>
    /// Vendoritem is a child of merchandise
    /// represents the vendors representation of our stock
    /// </summary>
    class VendorItem : Merchandise
    {
        public int Vmid { get; set; }
        public int VendorID { get; set; }
        public int UnitSize { get; set; }
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// default constructor zeroes values
        /// </summary>
        public VendorItem() : base()
        {
            Vmid = 0;
            VendorID = 0;
            UnitSize = 0;
            UnitPrice = 0;
        }

        /// <summary>
        /// constructor copies from another vendor item
        /// </summary>
        /// <param name="vi">vendoritem to copy</param>
        public VendorItem(VendorItem vi) : base(vi.Name, vi.Size, vi.Inventory, vi.Price, vi.ItemID)
        {
            Vmid = vi.Vmid;
            VendorID = vi.VendorID;
            UnitSize = vi.UnitSize;
            UnitPrice = vi.UnitPrice;
        }

        /// <summary>
        /// sets a vendor item given merchandise and values
        /// </summary>
        /// <param name="m">merchandise to copy</param>
        /// <param name="vendorid">ID of owning vendor</param>
        /// <param name="unitsize">amount of individual items per purchase</param>
        /// <param name="unitprice">cost of a single unit</param>
        /// <param name="vmid">id of the vendoritem</param>
        public VendorItem(Merchandise m, int vendorid, int unitsize, decimal unitprice, int vmid = 0) : base(m)
        {
            Vmid = vmid;
            VendorID = vendorid;
            UnitSize = unitsize;
            UnitPrice = unitprice;
        }

        /// <summary>
        /// copy a vendoritem given another
        /// </summary>
        /// <param name="v">vendoritem to copy</param>
        public void SetVendorItem(VendorItem v)
        {
            SetMerchandise(v);
            Vmid = v.Vmid;
            VendorID = v.VendorID;
            UnitSize = v.UnitSize;
            UnitPrice = v.UnitPrice;
        }

        /// <summary>
        /// inserts item into the database
        /// </summary>
        public void InsertVendorItem()
        {
            String query = "INSERT INTO VendorMerch (ItemID, VendorID, UnitSize, UnitPrice) VALUES(" +
                this.ItemID.ToString() + ", " + VendorID.ToString() + ", " + UnitSize.ToString() + ", " + UnitPrice.ToString()
                + ")";

            DBHandler.ExecuteNoReturn(query);
        }
    }
}
