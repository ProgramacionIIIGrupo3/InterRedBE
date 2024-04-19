using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.UTILS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InterRedBE.DAL.Services
{
    public class LoginService : ILoginDAO
    {
        public readonly InterRedContext _context;

        public LoginService(InterRedContext context)
        {
            _context = context;
        }

        public async Task<bool> VerifyUser(string email, string password)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == email);
            if (usuario == null)
            {
                return false; // Usuario no encontrado
            }

            var passwordHasher = new PasswordHasher();
            var result = passwordHasher.VerifyHashedPassword(usuario, usuario.Contrasena, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
