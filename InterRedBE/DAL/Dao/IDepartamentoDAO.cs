using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;

namespace InterRedBE.DAL.Dao
{
    public interface IDepartamentoDAO : ICRUD<Departamento>
    {
        Task<OperationResponse<long>> ObtenerPoblacionDepartamento(int departamentoId);
    }
}

