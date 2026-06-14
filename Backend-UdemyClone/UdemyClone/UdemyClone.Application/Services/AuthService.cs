using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UdemyClone.Application.Interfaces;
using UdemyClone.Application.Models.DTOs;
using UdemyClone.Application.Models.Requests.Estudiante;
using UdemyClone.Domain.Entities;
using UdemyClone.Domain.Interfaces;

namespace UdemyClone.Application.Services
{
    public class AuthService(
         IUsuarioRepository repo,
         IConfiguration config,
         IEmailService emailService) : IAuthService
    {
        public async Task<AuthResponseDto> Register(RegisterRequest model)
        {
            if (await repo.ExisteEmail(model.Email))
                throw new InvalidOperationException("Ya existe un usuario con ese email");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var usuario = new Usuario
            {
                Email = model.Email,
                PasswordHash = passwordHash,
                Rol = model.Rol
            };

            await repo.Create(usuario);

            // ── Envia el correo de bienvenida 
            var nombre = model.Email.Split('@')[0];
            await emailService.SendWelcomeEmailAsync(model.Email, nombre);

            return GenerarToken(usuario);
        }

        public async Task<AuthResponseDto> Login(LoginRequest model)
        {
            var usuario = await repo.GetByEmail(model.Email)
                ?? throw new KeyNotFoundException("Email o contraseña incorrectos");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, usuario.PasswordHash))
                throw new KeyNotFoundException("Email o contraseña incorrectos");

            // ── Enviar notificación de login ────────────────────
            await emailService.SendLoginNotificationAsync(model.Email);

            return GenerarToken(usuario);
        }

        private AuthResponseDto GenerarToken(Usuario usuario)
        {
            var expiracion = DateTime.UtcNow.AddHours(8);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,  usuario.Email),
                new Claim(ClaimTypes.Role,   usuario.Rol),
                new Claim("UsuarioId",       usuario.UsuarioId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

            var credenciales = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: expiracion,
                signingCredentials: credenciales
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = usuario.Email,
                Rol = usuario.Rol,
                Expiracion = expiracion
            };
        }
    }
}
