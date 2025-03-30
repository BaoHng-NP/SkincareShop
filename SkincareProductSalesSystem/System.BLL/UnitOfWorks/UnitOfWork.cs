using System.DAL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.BLL.UnitOfWorks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.BLL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SkincareShopContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(SkincareShopContext context)
        {
            _context = context;
        }

        // ✅ Bắt đầu Transaction
        public async Task BeginTransactionAsync()
        {
            _transaction ??= await _context.Database.BeginTransactionAsync();
        }

        // ✅ Commit Transaction
        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        // ✅ Rollback Transaction nếu có lỗi
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        // ✅ Lưu thay đổi vào database
        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        // ✅ Đảm bảo Dispose Transaction đúng cách
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }

}
