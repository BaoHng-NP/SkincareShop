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
    public class SkinQuizAnswerService : ISkinQuizAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<SkinQuizAnswer> _repository;
        public SkinQuizAnswerService(IUnitOfWork unitOfWork, IGenericRepository<SkinQuizAnswer> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public Task AddAnswerAsync(SkinQuizAnswer answer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnswerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DetermineSkinType(IEnumerable<int> selectedAnswerIds)
        {
            if (!selectedAnswerIds.Any())
            {
                return 0; // Không tìm thấy loại da nào phù hợp
            }

            // Đếm số lần xuất hiện của từng SkinTypeId
            var answerCounts = selectedAnswerIds
                .GroupBy(id => id)
                .ToDictionary(g => g.Key, g => g.Count());

            // Debug: In ra số lần chọn từng loại da
            Console.WriteLine("=== 🛠 DEBUG: Số lần chọn mỗi loại da ===");
            foreach (var item in answerCounts)
            {
                Console.WriteLine($"SkinTypeId: {item.Key}, Count: {item.Value}");
            }
            Console.WriteLine("===============================================");

            // Tìm số lần xuất hiện cao nhất
            int maxCount = answerCounts.Values.Max();

            // Lấy danh sách các SkinType có số lần xuất hiện nhiều nhất
            var topSkinTypes = answerCounts
                .Where(x => x.Value == maxCount)
                .Select(x => x.Key)
                .ToList();

            // ✅ Nếu có nhiều loại da có số lần xuất hiện ngang nhau => Trả về Combination (4)
            if (topSkinTypes.Count > 1)
            {
                return 4;
            }

            // Nếu chỉ có 1 loại da có số câu trả lời cao nhất, trả về loại đó
            return topSkinTypes.First();
        }







        public Task<IEnumerable<SkinQuizAnswer>> GetAllAnswersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SkinQuizAnswer?> GetAnswerByIdAsync(int id)
        {
            return await _repository.FindById(id);
        }

        public Task UpdateAnswerAsync(SkinQuizAnswer answer)
        {
            throw new NotImplementedException();
        }
    }
}
