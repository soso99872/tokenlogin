using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using tokenlogin.Models;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace tokenlogin.Controllers
{
    public class AccountController : ApiController
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AuthContext"].ConnectionString);
        private UserDBEntities db = new UserDBEntities();
        private static Dictionary<string, User> refreshTokens =
            new Dictionary<string, User>();

        private TokenManager _tokenManager;
        public AccountController()
        {
            _tokenManager = new TokenManager();
        }

        [HttpPost]
        [Route("register")]
        public void Register(string _UserName, string _UserPwd, string _UserRePwd)
        {
            var a = from users in db.User select users;
           foreach (var i in a)
            {
                if(_UserName != i.UserName && _UserPwd == _UserRePwd)
                {
                    
                        User _user = new User()
                        {
                            UserName = _UserName,
                            UserPwd = _UserPwd,
                            Coins = 0
                        };
                        db.User.Add(_user);
                }
            }

            db.SaveChanges();
        }

        [HttpPost]
        [Route("signIn")]
        public Token SignIn(string _UserName, string _UserPwd)
        {
            
            var a = from users in db.User select users;
            foreach (var i in a)
            {
                if (i.UserName == _UserName && i.UserPwd == _UserPwd)
                {
                    var token = _tokenManager.Create(i);
                    i.token = token.Access_token;
                    i.Expires_in = token.expires_in;
                    refreshTokens.Add(token.Refresh_token, i);
                    return token;
                }
            }
            return null;
        }

        [HttpPost]
        [Route("refresh")]
        public Token Refrssh(string refreshToken)
        {
            var user = refreshTokens[refreshToken];
            var token = _tokenManager.Create(user);
            refreshTokens.Remove(refreshToken);
            refreshTokens.Add(token.Refresh_token, user);
           
            return token;
        }
    }
}

