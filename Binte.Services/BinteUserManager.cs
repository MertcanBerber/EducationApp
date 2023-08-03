using Binte.Data.Entities.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services
{
    public class BinteUserManager:UserManager<BinteUser>
    {
        IHttpContextAccessor _httpContextAccessor;

        public BinteUserManager(IUserStore<BinteUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<BinteUser> passwordHasher,
            IHttpContextAccessor httpContextAccessor,
            IEnumerable<IUserValidator<BinteUser>> userValidators,
            IEnumerable<IPasswordValidator<BinteUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<BinteUser>> logger):
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,services, logger)
        {
            _httpContextAccessor= httpContextAccessor;

        }
        public BinteUser ActiveUser { get { return GetUserAsync(_httpContextAccessor.HttpContext.User).Result; } }

        public string GetUserName(int UserId)
        {
            var user=Users.FirstOrDefault(p => p.Id == UserId);
            return user == null ? "" : user.Name + " " + user.Surname;
        }
        public List<BinteUser> GetUsersWithRole(string roleName)
        {
            var ul=new List<BinteUser>();
            foreach (var user in Users.ToList())
            {
               if (IsInRoleAsync(user, roleName).Result)
                {
                    ul.Add(user);
                }
            }
            return ul;
        }
    }
}
