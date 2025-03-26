using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface IUserVourcherService
    {
        Task<IEnumerable<UserVoucher>> GetAllUserVouchersAsync();
        Task<IEnumerable<UserVoucher>> GetUserVoucherByUserIdAsync(int id);
        Task AddUserVoucherAsync(UserVoucher userVoucher);
        Task UpdateUserVoucherAsync(UserVoucher userSkinTest);
        Task DeleteUserVoucherAsync(int id);
    }
}
