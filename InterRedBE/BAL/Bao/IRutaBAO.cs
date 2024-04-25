using InterRedBE.DAL.Models;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Bao
{
    public interface IRutaBAO
    {
        Task<(ListaEnlazadaDoble<Departamento>, double)> EncontrarRutaAsync(int idDepartamentoInicio, int idDepartamentoFin);


    }
}
