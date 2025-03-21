using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public interface ISkinTypeService 
    {
        Task<IEnumerable<SkinType>> GetAllSkinTypesAsync();
        Task<SkinType?> GetSkinTypeByIdAsync(int id);
        Task AddSkinTypeAsync(SkinType skinType);
        Task UpdateSkinTypeAsync(SkinType skinType);
        Task DeleteSkinTypeAsync(int id);
    }
}
