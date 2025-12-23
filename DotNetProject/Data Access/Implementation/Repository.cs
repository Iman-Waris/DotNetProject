using System.Linq.Expressions;
using DotNetProject.Data_Access.Interfaces;
using DotNetProject.DataFolder;
using Microsoft.EntityFrameworkCore;

namespace DotNetProject.Data_Access.Implementation
{
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class
        //public class Repository : IRepository
    {



        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(AppDbContext context)
        {

            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public async Task AddAsync(TEntity entity)
        {

            await _context.Set<TEntity>().AddAsync(entity);

        }


        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {

            return await _context.Set<TEntity>().FindAsync(id);
        }


        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }


        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }


        public async Task<List<TEntity>> ToListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }


        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        //todo
        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }


        public IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbSet.Select(selector);
        }



        public IQueryable<IGrouping<TKey, TEntity>> GroupBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return _dbSet.GroupBy(keySelector);
        }

        public IQueryable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector)
        {
            return _dbSet.OrderByDescending(keySelector);
        }



        public IQueryable<TEntity> Take(int count)
        {
            return _dbSet.Take(count);
        }


    }
}
