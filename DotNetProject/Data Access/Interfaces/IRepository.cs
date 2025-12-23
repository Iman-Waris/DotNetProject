using System.Linq.Expressions;

namespace DotNetProject.Data_Access.Interfaces
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class


    {


        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        void Update(TEntity entity);

        void Delete(TEntity entity);
        Task<List<TEntity>> ToListAsync();

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();

        //IQueryable<TEntity> QueryableAsync(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Query();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector);
        IQueryable<IGrouping<TKey, TEntity>> GroupBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IQueryable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);
        IQueryable<TEntity> Take(int count);



    }
}
