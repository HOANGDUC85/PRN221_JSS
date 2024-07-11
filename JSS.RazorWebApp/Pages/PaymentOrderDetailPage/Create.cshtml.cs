using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JSS.Data.Models;
using JSS.Business;

namespace JSS.RazorWebApp.Pages.PaymentOrderDetailPage
{
    public class CreateModel : PageModel
    {
        private readonly IOrderDetail business;

        public CreateModel()
        {
            business = new OrderDetailBusiness();
        }

        public IActionResult OnGet()
        {
       
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await business.Save(OrderDetail);

            return RedirectToPage("./Index");
        }
    }
}
