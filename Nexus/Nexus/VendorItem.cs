using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus
{
    class VendorItem : Merchandise
    {
        public int Vmid { get; set; }
        public int VendorID { get; set; }
        public int UnitSize { get; set; }
        public decimal UnitPrice { get; set; }

        public VendorItem() : base()
        {
            Vmid = 0;
            VendorID = 0;
            UnitSize = 0;
            UnitPrice = 0;
        }

        public VendorItem(VendorItem vi) : base(vi.Name, vi.Size, vi.Inventory, vi.Price, vi.ItemID)
        {
            Vmid = vi.Vmid;
            VendorID = vi.VendorID;
            UnitSize = vi.UnitSize;
            UnitPrice = vi.UnitPrice;
        }

        public VendorItem(Merchandise m, int vendorid, int unitsize, decimal unitprice, int vmid = 0) : base(m)
        {
            Vmid = vmid;
            VendorID = vendorid;
            UnitSize = unitsize;
            UnitPrice = unitprice;
        }

        public void SetVendorItem(VendorItem v)
        {
            SetMerchandise(v);
            Vmid = v.Vmid;
            VendorID = v.VendorID;
            UnitSize = v.UnitSize;
            UnitPrice = v.UnitPrice;
        }

        public void InsertVendorItem()
        {
            String query = "INSERT INTO VendorMerch (ItemID, VendorID, UnitSize, UnitPrice) VALUES(" +
                this.ItemID.ToString() + ", " + VendorID.ToString() + ", " + UnitSize.ToString() + ", " + UnitPrice.ToString()
                + ")";

            DBHandler.ExecuteNoReturn(query);
        }
    }
}
