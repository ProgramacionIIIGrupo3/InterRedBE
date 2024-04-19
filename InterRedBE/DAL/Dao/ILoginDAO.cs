namespace InterRedBE.DAL.Dao
{
    public interface ILoginDAO
    {
        public Task<bool> VerifyUser (string email, string password);
    }
}
