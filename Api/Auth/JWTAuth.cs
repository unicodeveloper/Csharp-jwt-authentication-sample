using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Api.Models;
using JWT;
using Newtonsoft.Json.Linq;

namespace Api.Auth
{
    public class JWTAuth
    {
        // I know I know this shouldn't be visible
        // it's a sample cut me some slack :)
        private static string secret = "SomeRandomString";

        public static string GenerateToken(User user)
        {
            DateTime expires = DateTime.UtcNow.AddDays(1);
            var payload = new Dictionary<string, object>
            {
                {"user_id", user.Id},
                {"username", user.Username},
                {"expires", expires}
            };

            return JWT.JsonWebToken.Encode(payload, secret, JWT.JwtHashAlgorithm.HS256);
        }

        public static bool ValidateToken(string accessToken, int userId)
        {
            try
            {
                var payload = JWT.JsonWebToken.Decode(accessToken, secret);
                dynamic obj = JObject.Parse(payload);
                TimeSpan ts = DateTime.UtcNow - DateTime.Parse(obj.expires.ToString());

                if (obj.user_id == userId && ts.Days < 1)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
