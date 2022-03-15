using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hypotec.Repo.IRepository
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
