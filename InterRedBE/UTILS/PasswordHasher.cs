namespace InterRedBE.UTILS
{
    using InterRedBE.DAL.Models;
    using Microsoft.AspNetCore.Identity;

    public class PasswordHasher
    {
        private readonly IPasswordHasher<Usuario> _passwordHasher = new PasswordHasher<Usuario>();

        public string HashPassword(Usuario usuario, string password)
        {
            return _passwordHasher.HashPassword(usuario, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(Usuario usuario, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(usuario, hashedPassword, providedPassword);
        }
    }

}
