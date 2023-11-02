using BussnesLogic.DTO_s;
using BussnesLogic.Entity;
using BussnesLogic.Settings;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ParadoxPlayHub_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly PPHDbContext _context;

        public RegisterController(IConfiguration configuration,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              PPHDbContext context,
                              IWebHostEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            User regUser;
           
            regUser = new User()
            {
                FirstName = userDTO.FirstName,
                Email = userDTO.Email,
                NormalizedEmail = userManager.NormalizeEmail(userDTO.Email),
                PasswordHash = await ComputeSHA256Hash(userDTO.Password),
                RoleId = "Client"
            };

            regUser.Token = await GenerateJwtTokenAsync(userDTO.Email, regUser);

            _context.Users.Add(regUser);
            _context.SaveChanges(true);
            return Ok(regUser);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpDTO signUp)
        {
            User user;
            string normEm = userManager.NormalizeEmail(signUp.Email);
            user = await userManager.FindByEmailAsync(normEm);

            if (user != null)
            {
                try
                {
                    // Пошук моделі за ідентифікатором
                    var existingModel = await _context.Users.FindAsync(user.Id);

                    if (existingModel == null)
                        return NotFound();

                    return Ok(existingModel);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Помилка при вході: {ex.Message}");
                }
            }
            else
            {
                return NotFound(); // Користувача з такою адресою не знайдено.
            }
        }

        [HttpPost("reg-dev")]
        public async Task<IActionResult> RegisterToDeveloper(UserDTO user)
        {
            User foundUser;
            string normEmail = userManager.NormalizeEmail(user.Email);

            foundUser = await userManager.FindByEmailAsync(normEmail);
            if (foundUser != null)
            {
                try
                {
                    // Пошук моделі за ідентифікатором
                    var existingModel = await _context.Users.FindAsync(foundUser.Id);

                    if (existingModel == null)
                        return NotFound();

                    // Оновлення полів моделі
                    existingModel.RoleId = "Developer";

                    // Зберегти зміни
                    await _context.SaveChangesAsync();

                    return Ok(existingModel);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Помилка при оновленні даних: {ex.Message}");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("reg-admin")]
        public async Task<IActionResult> RegisterToAdmin(UserDTO user)
        {
            User foundUser;
            string normEmail = userManager.NormalizeEmail(user.Email);

            foundUser = await userManager.FindByEmailAsync(normEmail);
            if (foundUser != null)
            {
                try
                {
                    // Пошук моделі за ідентифікатором
                    var existingModel = await _context.Users.FindAsync(foundUser.Id);

                    if (existingModel == null)
                        return NotFound();

                    // Оновлення полів моделі
                    existingModel.RoleId = "Full";

                    // Зберегти зміни
                    await _context.SaveChangesAsync();

                    return Ok(existingModel);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Помилка при оновленні даних: {ex.Message}");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        private async Task<string> ComputeSHA256Hash(string password)
        {

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
                return hashedPassword;
            }
        }

        private async Task<string> GenerateJwtTokenAsync(string phoneNumber, User user)
        {
            // Отримати параметри JWT-токена з конфігурації
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Задати клейми для JWT-токена (якщо потрібно)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, phoneNumber)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Створити JWT-токен
            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationMinutes),
                signingCredentials: signingCredentials
            );

            // Згенерувати строкове представлення JWT-токена
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
