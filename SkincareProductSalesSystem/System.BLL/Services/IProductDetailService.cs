using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface IProductDetailService
    {

        Task<IList<ProductDetail>> GetPoductDetailServiceByIdAsync(int id);
        Task AddPoductDetailServiceAsync(ProductDetail userVoucher);
    }
}
