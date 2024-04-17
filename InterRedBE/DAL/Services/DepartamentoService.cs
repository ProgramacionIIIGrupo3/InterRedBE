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

        public async Task<OperationResponse<Departamento>> GetOne(int id)
        {
            var departamentos = _context.Departamento.FirstOrDefault(ts=>ts.Id==id);
            if (departamentos != null )
            {
                return new OperationResponse<Departamento>(1, "Departamento Encontrado Correctamente",departamentos);
            }
            else
            {
                return  new OperationResponse<Departamento>(0, "Departamento no encontrado",null);
            }
        }

        public Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            throw new NotImplementedException();
        }
    }
}
