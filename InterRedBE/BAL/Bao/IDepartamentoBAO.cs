using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;

namespace InterRedBE.BAL.Bao
{
    public interface IDepartamentoBAO : ICatalogoBAO<Departamento>
    {
        Task<OperationResponse<long>> ObtenerPoblacionDepartamento(int departamentoId);
    }
}
