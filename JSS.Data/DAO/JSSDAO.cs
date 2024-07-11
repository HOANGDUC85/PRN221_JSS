using JSS.Data.Base;
using JSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS.Data.DAO
{
    public class JSSDAO : BaseDAO<JSSDAO>
    {
        public JSSDAO() { }

        public async Task<int> CreateAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }
    }
}
