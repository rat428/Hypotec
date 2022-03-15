using Hypotec.Data.Data;
using Hypotec.Data.Entity;
using Hypotec.Repo.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hypotec.Repo.Repository
{
    public class AppUserRepository : GenericRepository<ApplicationUser>, IAppUserRepository
    {
        private ApplicationDbContext context;

        public AppUserRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }



    }
}
