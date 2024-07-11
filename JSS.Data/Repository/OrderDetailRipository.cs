using JSS.Data.Base;
using JSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS.Data.Repository
{
    public class OrderDetailRipository: GenericRepository<OrderDetail>
    {
        public OrderDetailRipository()
        {

        }
        public OrderDetailRipository(Net1704_221_7_JSSContext context) => _context = context;
    }
}
