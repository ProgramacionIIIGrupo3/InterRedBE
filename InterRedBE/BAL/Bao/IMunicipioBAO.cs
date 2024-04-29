using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Services;
using InterRedBE.UTILS;

namespace InterRedBE.BAL.Bao
{
    public interface IMunicipioBAO : ICatalogoBAO<Municipio>    
    {
        Task<OperationResponse<ListaEnlazadaDoble<MunicipioDTO>>> GetByDepartamentoId(int idDepartamento);
    }
}
