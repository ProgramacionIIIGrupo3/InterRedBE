using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Services;
using InterRedBE.UTILS;
namespace InterRedBE.DAL.Dao
{
    public interface IMunicipioDAO : ICRUD<Municipio>
    {
        Task<OperationResponse<ListaEnlazadaDoble<MunicipioDTO>>> GetByDepartamentoId(int idDepartamento);
    }
}
