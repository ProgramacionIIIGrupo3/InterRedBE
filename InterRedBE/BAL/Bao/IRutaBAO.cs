using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Bao
{
    public interface IRutaBAO
    {
        Task<List<(ListaEnlazadaDoble<Departamento>, double)>> EncontrarTodasLasRutasAsync(int idDepartamentoInicio, int idDepartamentoFin, int numeroDeRutas = 5);


    }
}
