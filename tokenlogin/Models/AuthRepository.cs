/*using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace tokenlogin.Models
{
    public class AuthRepository
    {
        private AuthContext _ctx;
        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }
        public async Task<IdentityResult>  RigisterUser(User _user)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = _user.UserName
            };
            var result = await _userManager.CreateAsync(user, _user.UserPwd);
            return result;
        }
    }
}*/