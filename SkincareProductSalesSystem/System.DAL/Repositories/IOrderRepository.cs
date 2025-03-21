using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.DAL.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> FindById(int id, params Expression<Func<Order, object>>[] includes);

    }
}
