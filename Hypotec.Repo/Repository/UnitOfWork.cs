using Hypotec.Data.Data;
using Hypotec.Repo.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hypotec.Repo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }
        #region Enity(Tables)
        IAppUserRepository appUserRepository;
        public IAppUserRepository AppUserRepository => appUserRepository = appUserRepository ?? new AppUserRepository(context);

        #endregion
        #region StoreProcedure (View Model)

        #endregion
        public int SaveChanges() => context.SaveChanges();
        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync().ConfigureAwait(false);
    }
}
