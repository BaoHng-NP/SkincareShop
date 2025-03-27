using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.BLL.UnitOfWorks;
using System.Collections.Generic;
using System.DAL.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<User> _accountRepository;
        public AccountService(IUnitOfWork unitOfWork, IGenericRepository<User> genericRepository)
        {
            _accountRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<User>> GetAllAccountsAsync()
        {
            return await _accountRepository.FindAll(null, u=>u.Role).ToListAsync();
        }
        public async Task<User?> GetAccountByIdAsync(int id)
        {
            return await _accountRepository.FindById(id, u => u.Role);
        }
        public async Task AddAccountAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _accountRepository.Create(user);
            await _unitOfWork.SaveChange();
        }
        public async Task DeleteAccountAsync(int id)
        {
            var account = await _accountRepository.FindById(id);
            if (account == null)
                throw new ArgumentException("Account not found");

            _accountRepository.Delete(account);
            await _unitOfWork.SaveChange(); // Save changes using UnitOfWork
        }
        public async Task UpdateAccountAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _accountRepository.Update(user);
            await _unitOfWork.SaveChange();
        }
    }
}
