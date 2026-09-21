using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShop.Api.Data;
using MobileShop.Api.Dtos;
using MobileShop.Api.Models;
using MobileShop.Api.Services;

namespace MobileShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
                return BadRequest("Tài khoản và mật khẩu không được để trống.");

            if (await _db.Users.AnyAsync(u => u.Username == req.Username))
                return Conflict("Tài khoản đã tồn tại.");

            var user = new User
            {
                FullName = req.FullName,
                PhoneNumber = req.PhoneNumber,
                Address = req.Address,
                Username = req.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                Role = UserRole.Customer
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var token = _jwt.GenerateToken(user);
            return Ok(new AuthResponse(token, user.Username, user.FullName, user.Role.ToString()));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest req)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized("Sai tài khoản hoặc mật khẩu.");

            var token = _jwt.GenerateToken(user);
            return Ok(new AuthResponse(token, user.Username, user.FullName, user.Role.ToString()));
        }
    }
}
