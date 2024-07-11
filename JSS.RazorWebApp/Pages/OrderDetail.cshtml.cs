using JSS.Business;
using JSS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JSS.RazorWebApp.Pages
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderDetail _orderDetailBusiness = new OrderDetailBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default;
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public void OnGet()
        {
            OrderDetails = this.GetCurrencies();
        }

        public void OnPost()
        {
            this.SaveOrderDetail();
        }

        public void OnDelete()
        {
        }


        private List<OrderDetail> GetCurrencies()
        {
            var orderDetailResult = _orderDetailBusiness.GetAll();

            if (orderDetailResult.Status > 0 && orderDetailResult.Result.Data != null)
            {
                var currencies = (List<OrderDetail>)orderDetailResult.Result.Data;
                return currencies;
            }
            return new List<OrderDetail>();
        }

        private void SaveOrderDetail()
        {
            var orderDetailResult = _orderDetailBusiness.Save(this.OrderDetail);

            if (orderDetailResult != null)
            {
                this.Message = orderDetailResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }
    }
}
