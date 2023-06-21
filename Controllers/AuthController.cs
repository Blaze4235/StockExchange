using StockExchange.Models;
using StockExchange.Models.DbModels;
using StockExchange.Helpers;
using StockExchange.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using StockExchange.Models.ResponceModels;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace StockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        ApplicationContext appCtx;

        [HttpGet]
        public string Get()
        {
            return "Sure";
        }

        public AuthController(ApplicationContext context)
        {
            appCtx = context;
        }

        public User AddUser(string username, string password, string email, string login, int role_id)
        {
            if (appCtx.User.FirstOrDefault(u => u.email == email) == null)
            {
                User user = new User
                {
                    email = email,
                    password = Hasher.HashPassword(password),
                    role_id = role_id,
                    username = username,
                    login = login,
                };

                appCtx.User.Add(user);
                appCtx.SaveChanges();

                return user;
            }
            return null;
        }

        private bool IsEmailValid(string email)
        {
            // Перевірка наявності символу '@' в email
            return email.Contains("@gmail.com") || email.Contains("@");
        }

        private bool IsLoginValid(string login)
        {
            // Перевірка, що логін має щонайменше 6 символів
            return login.Length >= 6;
        }

        private bool IsPasswordValid(string password)
        {
            // Перевірка, що пароль має щонайменше 8 символів
            return password.Length >= 8;
        }

        private bool IsUserModelValid(UserModel userModel)
        {
            // Виконати всі необхідні перевірки для UserModel
            return IsEmailValid(userModel.email) &&
                   IsLoginValid(userModel.login) &&
                   IsPasswordValid(userModel.password);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel us)
        {
            if (!IsUserModelValid(us))
            {
                List<string> errors = new List<string>();

                if (!IsEmailValid(us.email))
                {
                    errors.Add("Invalid email format");
                }

                if (!IsLoginValid(us.login))
                {
                    errors.Add("Invalid login: must be at least 6 characters long");
                }

                if (!IsPasswordValid(us.password))
                {
                    errors.Add("Invalid password: must be at least 8 characters long");
                }

                return BadRequest(new { errorMessage = "Registration failed", errors });
            }

            // Автоматично присвоїти role_id = 2
            us.role_id = 1;

            User u = AddUser(us.username, us.password, us.email, us.login, us.role_id);

            if (u != null)
            {
                return Json(Authenticate(new UserModel
                {
                    email = u.email,
                    password = u.password,
                    login = u.login,
                    username = u.username,
                    role_id = us.role_id
                }));
            }

            return BadRequest(new { errorMessage = "This email is already used" });
        }

        public AuthResponse Authenticate(UserModel userModel)
        {
            User user = appCtx.User.ToList().FirstOrDefault(x => x.login == userModel.login);

            if (user != null && Hasher.VerifyHashedPassword(user.password, userModel.password))
            {
                var identity = GetIdentity(user);

                if (identity == null)
                {
                    return null;
                }

                var now = DateTime.UtcNow;

                // Створення JWT-токена
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return new AuthResponse(user, encodedJwt);
            }

            return null;
        }


        [HttpPost("authenticate")]
        public IActionResult Authentication([FromBody] UserModel us)
        {
            var a = Authenticate(us);

            if (a == null)
            {
                return new UnauthorizedResult();
            }

            return Json(a);
        }

        private ClaimsIdentity GetIdentity(User u)
        {
            if (u != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, u.login),
                    new Claim("user_id", u.password.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }

            return null;
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var result = HttpContext.User.Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return Json(result);
        }
    }
}

