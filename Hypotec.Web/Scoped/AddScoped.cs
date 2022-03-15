using Hypotec.Repo.IRepository;
using Hypotec.Repo.Repository;
using Hypotec.Service.IService;
using Hypotec.Service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Hypotec.Web.Scoped
{
    public static class AddScoped
    {
        /// <summary>
        /// Registration services
        /// </summary>
        /// <param name="service"></param>
        public static void RegisterService(IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IAppUserService, AppUserService>();
            service.AddScoped<IAppUserRepository, AppUserRepository>();
        }
    }
}
