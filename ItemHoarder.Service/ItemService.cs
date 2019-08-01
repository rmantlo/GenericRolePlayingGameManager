using ItemHoarder.Data;
using ItemHoarder.Data.ItemStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Service
{
    public class ItemService
    {
        //private readonly Guid _userId;
        public ItemService()
        {
            //_userId = userId;
        }

        public IEnumerable<Item> GetAllItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Items.Select(e => e);
            }
        }
        //get item by id

    }
}
