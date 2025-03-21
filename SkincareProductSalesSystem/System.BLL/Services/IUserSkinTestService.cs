using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface IUserSkinTestService
    {
        Task<IEnumerable<UserSkinTest>> GetAllUserSkinTestsAsync();
        Task<UserSkinTest?> GetUserSkinTestByIdAsync(int id);
        Task AddUserSkinTestAsync(UserSkinTest userSkinTest);
        Task UpdateUserSkinTestAsync(UserSkinTest userSkinTest);
        Task DeleteUserSkinTestAsync(int id);
    }
}
