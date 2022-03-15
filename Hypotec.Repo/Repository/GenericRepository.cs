using Hypotec.Data.Data;
using Hypotec.Repo.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hypotec.Repo.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext dataContext;

        public GenericRepository(ApplicationDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Add(TEntity entity)
        {
            dataContext.Set<TEntity>().Add(entity);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            dataContext.Set<TEntity>().Add(entity);
            await dataContext.SaveChangesAsync().ConfigureAwait(false);
            dataContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public void AddMany(IEnumerable<TEntity> entity)
        {
            dataContext.Set<TEntity>().AddRange(entity);
        }

        public void Delete(TEntity entity)
        {
            dataContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            dataContext.Set<TEntity>().RemoveRange(entities);
        }

        public void Edit(TEntity entity)
        {
            var entry = dataContext.Entry(entity);
            dataContext.Entry(entity).State = EntityState.Detached;
            dataContext.Set<TEntity>().Update(entity);
            entry = dataContext.Entry(entity);
            entry.State = EntityState.Modified;
        }
        public async Task<TEntity> EditAsync(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Detached;
            var entry = dataContext.Entry(entity);
            dataContext.Set<TEntity>().Attach(entity);
            entry = dataContext.Entry(entity);
            entry.State = EntityState.Modified;
            await dataContext.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        #region Find By
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return dataContext.Set<TEntity>().Where(predicate);
        }

        public Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public TEntity FindById(int id)
        {
            return dataContext.Set<TEntity>().Find(id);
        }

        public TEntity FindByMailId(object MailId)
        {
            return dataContext.Set<TEntity>().Find(MailId);
        }

        //public Task<TEntity> FindByIdAsync(object id)
        //{
        //    return dataContext.Set<TEntity>().FindAsync(id);
        //}

        //public Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object id)
        //{
        //    return dataContext.Set<TEntity>().FindAsync(cancellationToken, id);
        //}

        public TEntity FirstFindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return dataContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public Task<TEntity> FirstFindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstFindByAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(()=> dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken)).ConfigureAwait(false);
        }
        #endregion

        #region Get
        public bool Any()
        {
            return dataContext.Set<TEntity>().Any();
        }
        public IQueryable<TEntity> GetAll()
        {
            return dataContext.Set<TEntity>();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return dataContext.Set<TEntity>().ToListAsync<TEntity>();
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return dataContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public void Repository(ApplicationDbContext myCtx)
        {
            dataContext = myCtx;
            dataContext.Set<TEntity>();
        }

        #endregion

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = null;
            query = dataContext.Set<TEntity>().Include(includes[0]);

            for (int i = 1; i < includes.Length; i++)
            {
                query.Include(includes[1]);
            }
            return query;
        }

        public IQueryable<TEntity> PageAll(int skip, int take)
        {
            return dataContext.Set<TEntity>().Skip(skip).Take(take);
        }

        public Task<List<TEntity>> PageAllAsync(int skip, int take)
        {
            return dataContext.Set<TEntity>().Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<TEntity>> PageAllAsync(CancellationToken cancellationToken, int skip, int take)
        {
            return dataContext.Set<TEntity>().Skip(skip).Take(take).ToListAsync(cancellationToken);
        }


    }
}
