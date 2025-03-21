using BusinessObjects.Models;
using System;
using System.BLL.UnitOfWorks;
using System.Collections.Generic;
using System.DAL.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public class UserSkinTestService : IUserSkinTestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserSkinTest> _repository;
        public UserSkinTestService(IUnitOfWork unitOfWork, IGenericRepository<UserSkinTest> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddUserSkinTestAsync(UserSkinTest userSkinTest)
        {
            if (userSkinTest == null)
                throw new ArgumentNullException(nameof(userSkinTest));

            _repository.Create(userSkinTest);
            await _unitOfWork.SaveChange();
        }

        public Task DeleteUserSkinTestAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserSkinTest>> GetAllUserSkinTestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserSkinTest?> GetUserSkinTestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserSkinTestAsync(UserSkinTest userSkinTest)
        {
            throw new NotImplementedException();
        }
    }
}
