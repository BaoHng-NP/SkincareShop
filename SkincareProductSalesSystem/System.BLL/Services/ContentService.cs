using BusinessObjects.Models;
using System.Collections.Generic;
using System.DAL.Repositories;
using System.Threading.Tasks;

namespace System.BLL.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;

        public ContentService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<List<Content>> GetPublishedContents(int page = 1, int pageSize = 6)
        {
            return await _contentRepository.GetPublishedContents(page, pageSize);
        }

        public async Task<Content> GetContentById(int id)
        {
            return await _contentRepository.GetContentById(id);
        }

        public async Task<int> GetTotalPublishedContents()
        {
            return await _contentRepository.GetTotalPublishedContents();
        }

        public async Task<List<Content>> GetLatestContents(int count = 3)
        {
            return await _contentRepository.GetLatestContents(count);
        }
    }

    public interface IContentService
    {
        Task<List<Content>> GetPublishedContents(int page = 1, int pageSize = 6);
        Task<Content> GetContentById(int id);
        Task<int> GetTotalPublishedContents();
        Task<List<Content>> GetLatestContents(int count = 3);
    }
}