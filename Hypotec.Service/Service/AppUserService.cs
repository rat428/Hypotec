using AutoMapper;
using Hypotec.Data.Data;
using Hypotec.Data.Entity;
using Hypotec.Service.Dto;
using Hypotec.Service.IService;
using Microsoft.AspNetCore.Identity;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Hypotec.Service.Service
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public AppUserService(UserManager<ApplicationUser> userManager, IMapper mapper, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }
        /// <summary>
        /// save user into database
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public async Task<bool> SaveUser(AppUserDto applicationUser)
        {
            try
            {
                bool Issaved = false;
                if (applicationUser != null)
                {
                    var mapApplicationUser = _mapper.Map<ApplicationUser>(applicationUser);

                    if (applicationUser.Id == 0)
                    {
                        mapApplicationUser.IsActive = true;
                        var saveUser = await _userManager.CreateAsync(mapApplicationUser, mapApplicationUser.PasswordHash).ConfigureAwait(false);
                        if (saveUser.Succeeded)
                        {
                            var results = await _userManager.AddToRoleAsync(mapApplicationUser, "ClientUser").ConfigureAwait(false);
                            if (results.Succeeded)
                                Issaved = true;
                        }
                    }
                    else
                    {
                        applicationUser.SecurityStamp = applicationUser.SecurityStamp;
                        applicationUser.IsActive = true;
                        var entry = _applicationDbContext.Users.FirstOrDefault(e => e.Id == applicationUser.Id);
                        applicationUser.PasswordHash = entry.PasswordHash;
                        _applicationDbContext.Entry(entry).CurrentValues.SetValues(applicationUser);
                        var result = _applicationDbContext.SaveChanges();

                        if (result == 1)
                        {
                            Issaved = true;
                        }
                    }
                    return Issaved;
                }
                return Issaved;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
