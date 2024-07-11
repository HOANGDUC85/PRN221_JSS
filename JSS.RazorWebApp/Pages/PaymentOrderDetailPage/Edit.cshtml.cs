using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JSS.Data.Models;
using Models = JSS.Data.Models;

using JSS.Business;

namespace JSS.RazorWebApp.Pages.PaymentOrderDetailPage
{
    public class EditModel : PageModel
    {
        private readonly IOrderDetail business;

        public EditModel()
        {
            business ??= new OrderDetailBusiness();
        }

        [BindProperty]
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
            OrderDetail = (Models.OrderDetail)orderdetail.Data;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            OrderDetail.CreatedAt = OrderDetail.CreatedAt;
            OrderDetail.UpdatedAt = OrderDetail.UpdatedAt;
            business.Update(OrderDetail);
            

            return RedirectToPage("./Index");
        }

    }
}
