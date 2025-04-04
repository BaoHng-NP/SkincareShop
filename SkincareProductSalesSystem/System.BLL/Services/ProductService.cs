﻿using BusinessObjects.Models;
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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Product> _repository;
        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> genericRepository)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _repository.Create(product);
            await _unitOfWork.SaveChange();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repository.FindById(id);
            if (product == null)
                throw new ArgumentException("product not found");

            _repository.Delete(product);
            await _unitOfWork.SaveChange();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.FindAll(null,u => u.Category, u => u.Brand).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProductsByCateIdAsync(int? cateId, int excludeProductId)
        {
            return await _repository.FindAll(
                u => u.CategoryId == cateId && u.Id != excludeProductId,
                u => u.Category,
                u => u.Brand
            ).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.FindById(id, u => u.Category, u => u.Brand, u => u.SkinTypes);
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _repository.Update(product);
            await _unitOfWork.SaveChange();
        }
        public async Task<IEnumerable<Product>> GetProductsByFilterAsync(int? categoryId, int? brandId)
        {
            var query = _repository.FindAll(p => p.Stock > 0); 

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (brandId.HasValue)
            {
                query = query.Where(p => p.BrandId == brandId.Value);
            }

            return await query.OrderBy(p => p.Name).ToListAsync(); 
        }
        public async Task<IEnumerable<Product>> GetProductsBySkinTypeAsync(int skinTypeId)
        {
            return await _repository
                .FindAll(p => p.SkinTypes.Any(st => st.Id == skinTypeId),
                         u => u.Category,
                         u => u.Brand,
                         u => u.SkinTypes)
                .ToListAsync();
        }


    }
}
