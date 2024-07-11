using JSS.Data.Models;
using JSS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS.Data
{
    public class UnitOfWork
    {
        private Net1704_221_7_JSSContext _unitOfWorkContext;
        private OrderDetailRipository _orderDetail;
        public UnitOfWork() { 
        _unitOfWorkContext ??=new Net1704_221_7_JSSContext();

        }
        public UnitOfWork(Net1704_221_7_JSSContext context) { }
        public OrderDetailRipository OrderDetailRipository
        {
            get
            {
                return _orderDetail ??=new Repository.OrderDetailRipository();
            }
        }
        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels


        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = _unitOfWorkContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _unitOfWorkContext.Database.BeginTransaction())
            {
                try
                {
                    result = await _unitOfWorkContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion

    }
}
