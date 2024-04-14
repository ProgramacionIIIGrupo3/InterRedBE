using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Models;

namespace InterRedBE.BAL.Services
{
    IDepartamentoService departamentoService;

    public DepartamentoBAOService(IDepartamentoService departamentoService)
    {
        this.departamentoService = departamentoService;
    }
    public class DepartamentoBAOService : IDepartamentoBAOcs
    {
        public Task<List<Departamento>> ObtenerDepartamentos()
        {

        }
    }
}
