using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tokenlogin.Models
{
    public class TokenManager
    {
        public string key = "adb";
        private object payload;

        public Token Create(User _user)
        {
            var exp = 3600;
            var payload = new Payload
            {
               Info = _user,
               exp = Convert.ToInt32((DateTime.Now.AddSeconds(exp) - new DateTime(1970,1,1)).TotalSeconds)
            };

            var Json = JsonConvert.SerializeObject(payload);
            var iv = Guid.NewGuid().ToString();
            return new Token
            {
                Access_token = iv,
                Refresh_token = Guid.NewGuid().ToString(),
               
            };
        }

       public User GetUser(User _user,Payload _payload)
        {
            var payload = _payload;

            if (payload.exp < Convert.ToInt32(
            (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds))
            {
                _user.Coins += 1;
                return null;
            }
            return payload.Info;
        }
       
    }
}