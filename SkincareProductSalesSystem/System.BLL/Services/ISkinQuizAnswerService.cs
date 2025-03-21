using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface ISkinQuizAnswerService
    {
        Task<IEnumerable<SkinQuizAnswer>> GetAllAnswersAsync();
        Task<SkinQuizAnswer?> GetAnswerByIdAsync(int id);
        Task AddAnswerAsync(SkinQuizAnswer answer);
        Task UpdateAnswerAsync(SkinQuizAnswer answer);
        Task DeleteAnswerAsync(int id);
        Task<int> DetermineSkinType(IEnumerable<int> selectedAnswerIds);
    }
}
