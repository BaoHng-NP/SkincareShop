using BusinessObjects.Models;
using System.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace System.DAL.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly IGenericRepository<Content> _repository;
        private readonly SkincareShopContext _context;

        public ContentRepository(IGenericRepository<Content> repository, SkincareShopContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<List<Content>> GetPublishedContents(int page = 1, int pageSize = 6)
        {
            return await _repository.FindAll(
                c => c.IsPublished == true,
                c => c.Author)
                .OrderByDescending(c => c.PublishedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Content> GetContentById(int id)
        {
            return await _repository.FindById(id, c => c.Author);
        }

        public async Task<int> GetTotalPublishedContents()
        {
            return await _repository
                .FindAll(c => c.IsPublished == true)
                .CountAsync();
        }

        public async Task<List<Content>> GetLatestContents(int count = 3)
        {
            return await _repository.FindAll(
                c => c.IsPublished == true,
                c => c.Author)
                .OrderByDescending(c => c.PublishedAt)
                .Take(count)
                .ToListAsync();
        }
    }

    public interface IContentRepository
    {
        Task<List<Content>> GetPublishedContents(int page = 1, int pageSize = 6);
        Task<Content> GetContentById(int id);
        Task<int> GetTotalPublishedContents();
        Task<List<Content>> GetLatestContents(int count = 3);
    }
}