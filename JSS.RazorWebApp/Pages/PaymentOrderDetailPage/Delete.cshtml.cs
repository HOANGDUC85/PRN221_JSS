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
    public class DeleteModel : PageModel
    {
        private readonly IOrderDetail business;
        public DeleteModel()
        {
           business??= new OrderDetailBusiness();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var orderdetail = await business.GetById(id);

            if (orderdetail.Data != null)
            {
                OrderDetail=(Models.OrderDetail)orderdetail.Data;
                return Page();
            }
            else
            {
                return NotFound();
            }
            
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail = await business.DeleteById(id);
            if (orderdetail != null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
            
        }
    }
}
