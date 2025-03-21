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
    public class SkinTypeService : ISkinTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<SkinType> _repository;
        public SkinTypeService(IUnitOfWork unitOfWork, IGenericRepository<SkinType> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddSkinTypeAsync(SkinType skinType)
        {
            if (skinType == null)
                throw new ArgumentNullException(nameof(skinType));

            _repository.Create(skinType);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteSkinTypeAsync(int id)
        {
            var skinType = await _repository.FindById(id);
            if (skinType == null)
                throw new ArgumentException("skinType not found");

            _repository.Delete(skinType);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<SkinType>> GetAllSkinTypesAsync()
        {
            return await _repository.FindAll(null).ToListAsync();
        }

        public async Task<SkinType?> GetSkinTypeByIdAsync(int id)
        {
            return await _repository.FindById(id);

        }

        public async Task UpdateSkinTypeAsync(SkinType skinType)
        {
            if (skinType == null)
                throw new ArgumentNullException(nameof(skinType));

            _repository.Update(skinType);
            await _unitOfWork.SaveChange();
        }
    }
}
