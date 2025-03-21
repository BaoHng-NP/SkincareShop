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
    public class SkinQuizQuestionsService : ISkinQuizQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<SkinQuizQuestion> _repository;
        public SkinQuizQuestionsService(IUnitOfWork unitOfWork, IGenericRepository<SkinQuizQuestion> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddQuestionAsync(SkinQuizQuestion question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            _repository.Create(question);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _repository.FindById(id);
            if (question == null)
                throw new ArgumentException("question not found");

            _repository.Delete(question);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<SkinQuizQuestion>> GetAllQuestionsAsync()
        {
            return await _repository.FindAll(null, q => q.SkinQuizAnswers).ToListAsync();
        }

        public async Task<SkinQuizQuestion?> GetQuestionByIdAsync(int id)
        {
            return await _repository.FindById(id, q => q.SkinQuizAnswers);

        }

        public async Task UpdateQuestionAsync(SkinQuizQuestion question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            _repository.Update(question);
            await _unitOfWork.SaveChange();
        }
    }
}
