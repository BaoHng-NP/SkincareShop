using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface ISkinQuizQuestionService
    {
        Task<IEnumerable<SkinQuizQuestion>> GetAllQuestionsAsync();
        Task<SkinQuizQuestion?> GetQuestionByIdAsync(int id);
        Task AddQuestionAsync(SkinQuizQuestion question);
        Task UpdateQuestionAsync(SkinQuizQuestion question);
        Task DeleteQuestionAsync(int id);
    }
}
