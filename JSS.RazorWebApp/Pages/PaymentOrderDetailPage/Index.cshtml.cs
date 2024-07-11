using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JSS.Data.Models;
using JSS.Business;

namespace JSS.RazorWebApp.Pages.PaymentOrderDetailPage
{
    public class IndexModel : PageModel
    {
        private readonly IOrderDetail business;

        public IndexModel()
        {
            business ??= new OrderDetailBusiness();
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 2;
        public async Task OnGetAsync(int? pageIndex)
        {
            CurrentPage = pageIndex ?? 1;
            var result = await business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                OrderDetail = result.Data as List<OrderDetail>;
                int totalItems = OrderDetail.Count;
                TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

                OrderDetail = OrderDetail
                           .Skip((CurrentPage - 1) * PageSize)
                           .Take(PageSize)
                           .ToList();
            }
        }
    }
}
