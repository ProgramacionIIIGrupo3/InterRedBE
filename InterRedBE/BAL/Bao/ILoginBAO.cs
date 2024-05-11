namespace InterRedBE.BAL.Bao
{
    public interface ILoginBAO
    {
        public Task<bool> VerifyUser(string nombreUsuario, string password);
    }
}
