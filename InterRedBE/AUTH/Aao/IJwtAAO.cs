using InterRedBE.DAL.Models;

namespace InterRedBE.AUTH.Aao
{
    public interface IJwtAAO
    {
        string GenerateToken(Usuario user);
    }
}
