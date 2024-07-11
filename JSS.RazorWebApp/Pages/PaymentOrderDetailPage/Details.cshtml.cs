using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JSS.Data.Models;
using JSS.Business;
using Models = JSS.Data.Models;


namespace JSS.RazorWebApp.Pages.PaymentOrderDetailPage
{
    public class DetailsModel : PageModel
    {
        private readonly IOrderDetail business;

        public DetailsModel()
        {
            business ??= new OrderDetailBusiness();
        }

        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await business.GetById(id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = (Models.OrderDetail)orderdetail.Data;
            }
            return Page();
        }
    }
}
