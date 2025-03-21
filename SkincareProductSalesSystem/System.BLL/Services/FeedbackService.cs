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
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Feedback> _repository;
        public FeedbackService(IUnitOfWork unitOfWork, IGenericRepository<Feedback> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddFeedbackAsync(Feedback feedback)
        {
            if (feedback == null)
                throw new ArgumentNullException(nameof(feedback));

            _repository.Create(feedback);
            await _unitOfWork.SaveChange();
        }

        public Task DeleteFeedbackAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Feedback>> GetFeedbackByUserIdAsync(int? id)
        {
            return await _repository.FindAll(o => o.UserId == id).ToListAsync();
        }


        public Task UpdateFeedbackAsync(Feedback feedback)
        {
            throw new NotImplementedException();
        }
    }
}
