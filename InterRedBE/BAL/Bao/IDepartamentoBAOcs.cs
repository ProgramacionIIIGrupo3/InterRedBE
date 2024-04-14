using InterRedBE.DAL.Models;
namespace InterRedBE.BAL.Bao
{
    public interface IDepartamentoBAOcs
    {
        public Task<List<Departamento>> ObtenerDepartamentos();
    }
}
