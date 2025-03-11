using System;
using System.Collections.Generic;
using BusinessObjects.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<User>> GetAllAccountsAsync();
        Task<User?> GetAccountByIdAsync(int id);
        Task AddAccountAsync(User user);
        Task UpdateAccountAsync(User user);
        Task DeleteAccountAsync(int id);
    }
}
