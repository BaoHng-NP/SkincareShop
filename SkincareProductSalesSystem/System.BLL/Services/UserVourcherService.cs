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
    public class UserVourcherService : IUserVourcherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserVoucher> _repository;
        public UserVourcherService(IUnitOfWork unitOfWork, IGenericRepository<UserVoucher> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddUserVoucherAsync(UserVoucher userVoucher)
        {
               if (userVoucher == null)
                throw new ArgumentNullException(nameof(userVoucher));

            _repository.Create(userVoucher);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteUserVoucherAsync(int id)
        {

            var question = await _repository.FindById(id);
            if (question == null)
                throw new ArgumentException("question not found");

            _repository.Delete(question);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<UserVoucher>> GetAllUserVouchersAsync()
        {
            return await _repository.FindAll(null,u => u.Discount, u => u.User).ToListAsync();
        }

        public async Task<IEnumerable<UserVoucher>> GetUserVoucherByUserIdAsync(int id)
        {
            return await _repository.FindAll(q => q.UserId == id,q => q.User, q => q.Discount).ToListAsync();
        }

        public async Task UpdateUserVoucherAsync(UserVoucher userSkinTest)
        {
            throw new NotImplementedException();
        }
    }
}
