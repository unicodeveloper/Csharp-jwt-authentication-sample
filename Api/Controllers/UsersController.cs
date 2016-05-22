using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Api.Models;
using Api.Auth;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private ApiDbContext _db;
        public UsersController()
        {
            _db = new ApiDbContext();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string token = this.Request.Headers["Authorization"];
            if (token == null)
                return HttpBadRequest(new { error = "No authorization header" });

            bool isValid = JWTAuth.ValidateToken(token, id);
            if (!isValid)
            {
                this.Response.StatusCode = 403;
                return new ObjectResult(new { error = "Invalid access token" });
            }

            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            return new ObjectResult(new
            {
                user.Id,
                user.Username,
                user.Firstname,
                user.Middlename,
                user.Lastname,
                user.Age
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            User _user = _db.Users.FirstOrDefault(
                u => u.Username == user.Username &&
                u.Password == user.Password);

            if (_user == null)
                return new HttpUnauthorizedResult();

            string accessToken = JWTAuth.GenerateToken(_user);
            return new ObjectResult(new
            {
                _user.Id,
                _user.Username,
                access_token = accessToken
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var users = _db.Users;

            // Validate uniqueness of submitted username
            if (users.FirstOrDefault(u => u.Username == user.Username) != null)
                return HttpBadRequest(new { error = "Username already in use" });

            // Auto increment Id
            if (users.Count == 0)
                user.Id = 1;
            else
                user.Id = users.Last().Id + 1;

            _db.Users.Add(user);
            _db.SaveChanges();

            string accessToken = JWTAuth.GenerateToken(user);
            return new ObjectResult(new
            {
                user.Id,
                user.Username,
                access_token = accessToken
            });
        }
    }
}
