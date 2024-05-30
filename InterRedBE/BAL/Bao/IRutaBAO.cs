using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Interfaces;
using InterRedBE.UTILS.Services;
using System.Threading.Tasks;

namespace InterRedBE.BAL.Bao
{
    public interface IRutaBAO
    {
        Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>> EncontrarTodasLasRutasAsync(int idInicio, TipoEntidad tipoInicio, int idFin, TipoEntidad tipoFin, int numeroDeRutas = 5);
        Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarKRutasMasCortasAsync(int idDepartamentoInicio, int idDepartamentoFin, int k);
        Task<ListaEnlazadaDoble<(ListaEnlazadaDoble<IIdentificable>, double)>> EncontrarTodasLasRutasNuevoAsync(string idXInicio, string idXFin, int numeroDeRutas = 5);
    }
}
