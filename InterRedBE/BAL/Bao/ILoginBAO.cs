namespace InterRedBE.BAL.Bao
{
    public interface ILoginBAO
    {
        public Task<bool> VerifyUser(string email, string password);
    }
}
