using StockApp.Application.Common.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _context;

    

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        private IRepositoryBase<Stock> _stockRepo;

        public IRepositoryBase<Stock> StockRepo => _stockRepo ?? new RepositoryBase<Stock>(_context);

        private IRepositoryBase<Order> _orderRepo;

        public IRepositoryBase<Order> OrderRepo => _orderRepo ?? new RepositoryBase<Order>(_context);


        public async Task<bool> SaveAsync()
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                    return true;

                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    //todo: Log the error
                    return false;

                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
