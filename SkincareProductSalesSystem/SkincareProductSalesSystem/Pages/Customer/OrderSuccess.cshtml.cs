using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.DAL;
using System.BLL.Services;

namespace SkincareProductSalesSystem.Pages.Customer
{
    public class OrderSuccessModel : PageModel
    {

        public int OrderId { get; set; }
        public string UserName { get; set; }

        public void OnGet(int orderId, string userName)
        {
            OrderId = orderId;
            UserName = userName;

        }
    }
}
