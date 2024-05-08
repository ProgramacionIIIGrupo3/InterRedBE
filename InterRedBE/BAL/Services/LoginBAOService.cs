using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;

namespace InterRedBE.BAL.Services
{
    public class LoginBAOService : ILoginBAO
    {
        public readonly ILoginDAO _loginDAO;

        public LoginBAOService(ILoginDAO loginDAO)
        {
            _loginDAO = loginDAO;
        }

        public async Task<bool> VerifyUser(string nombreUsuario, string password)
        {
            return await _loginDAO.VerifyUser(nombreUsuario, password);
        }
    }
}
