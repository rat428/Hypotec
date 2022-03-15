using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hypotec.Repo.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        bool Any();
        IQueryable<TEntity> GetAll();
        void Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void AddMany(IEnumerable<TEntity> entity);
        void Edit(TEntity entity);
        Task<TEntity> EditAsync(TEntity entity);
        void Delete(TEntity entity);
        void DeleteMany(IEnumerable<TEntity> entities);

        #region GetAll
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        #endregion

        #region Page ALL
        IQueryable<TEntity> PageAll(int skip, int take);
        Task<List<TEntity>> PageAllAsync(int skip, int take);
        Task<List<TEntity>> PageAllAsync(CancellationToken cancellationToken, int skip, int take);
        #endregion

        #region Find By
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        TEntity FindById(int id);
        //Task<TEntity> FindByIdAsync(object id);
        //Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object id);
        //TEntity FirstFindBy(Expression<Func<TEntity, bool>> predicate);
        //Task<TEntity> FirstFindByAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<TEntity> FirstFindByAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate);
        #endregion

    }
}
