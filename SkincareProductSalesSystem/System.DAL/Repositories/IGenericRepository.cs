﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity?> FindById(int id, params Expression<Func<TEntity, object>>[] includeProperties);

        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

}
