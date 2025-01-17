﻿using JSS.Common;
using JSS.Business.Base;
using JSS.Data.DAO;
using JSS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSS.Data;

namespace JSS.Business
{
    public interface IOrderDetail
    {
        Task<IBusinessResult> GetAll();
        Task<IBusinessResult> GetById(int code);
        Task<IBusinessResult> Save(OrderDetail orderDetail);
        Task<IBusinessResult> Update(OrderDetail orderDetail);
        Task<IBusinessResult> DeleteById(int code);
    }

    public class OrderDetailBusiness : IOrderDetail
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderDetailBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Business rule
                #endregion

                var orderDetail = await _unitOfWork.OrderDetailRipository.GetAllAsync();

                if (orderDetail == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetById(int code)
        {
            try
            {
                #region Business rule
                #endregion

                var orderDetail = await _unitOfWork.OrderDetailRipository.GetByIdAsync(code);

                if (orderDetail == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> Save(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.OrderDetailRipository.CreateAsync(orderDetail);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Update(OrderDetail orderDetail)
        {
            try
            {
                int result = await _unitOfWork.OrderDetailRipository.UpdateAsync(orderDetail);
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }

        public async Task<IBusinessResult> DeleteById(int code)
        {
            try
            {
                var orderDetail = await _unitOfWork.OrderDetailRipository.GetByIdAsync(code);
                if (orderDetail != null)
                {
                    var result = await _unitOfWork.OrderDetailRipository.RemoveAsync(orderDetail);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
                    }
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.ToString());
            }
        }
    }
}
