using Microsoft.EntityFrameworkCore;
using Rookies_EF.Common.GenericModel;
using Rookies_EFCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_EF.Common.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly RookiesEFDBContext _context;
        private DbSet<T> _dbset;
        public GenericRepository(RookiesEFDBContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }
        public async Task<int> AddAsync(T entity)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                entity.CreatedAt = DateTime.Now;
                await _dbset.AddAsync(entity);
                int status = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return status;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return ConstantsStatus.Failed;
            }

        }

        public async Task<int> DeleteAsync(Guid id)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var entity = await _dbset.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null) return ConstantsStatus.Failed;
                entity.DeletedAt = DateTime.Now;
                entity.IsDeleted = true;
                int status = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return status;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return ConstantsStatus.Failed;
            }

        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.Where(x => !x.IsDeleted).ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbset.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                entity.UpdatedAt = DateTime.Now;
                _dbset.Update(entity);
                int status = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return status;
            }
            catch (Exception)
            {

                await transaction.RollbackAsync();
                return ConstantsStatus.Failed;
            }

        }
    }
}
