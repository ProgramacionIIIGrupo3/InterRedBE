using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.DAL.Services
{
    public class DepartamentoService : IDepartamentoDAO
    {
        public readonly InterRedContext _context;

        public DepartamentoService(InterRedContext context)
        {
            _context = context;
        }

        public Task<OperationResponse<Departamento>> CreateOne(Departamento obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<Departamento>> GetAll()
        {
            var listaDepartamentos = new ListaEnlazadaDoble<Departamento>();
            var departamentos = _context.Departamento.ToList();

            if (departamentos.Count == 0)
            {
                return new OperationResponse<ListaEnlazadaDoble<Departamento>>(0, "No se encontraron los Departamentos",null);
            }
            foreach (var departamento in departamentos)
            {
                listaDepartamentos.InsertarAlFinal(departamento);
            }

            return new OperationResponse<ListaEnlazadaDoble<Departamento>>(1, "Departamentos Encontrados Correctamente", listaDepartamentos);
        }

        public Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            throw new NotImplementedException();
        }
    }
}
