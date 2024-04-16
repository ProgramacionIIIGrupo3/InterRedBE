using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.DAL.Services
{
    public class DepartamentoService : IDepartamentoDAO
    {
        public readonly InterRedContext context;

        public DepartamentoService(InterRedContext context)
        {
            this.context = context;
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
            var listadepartamento = new ListaEnlazadaDoble<Departamento>();
            var departamentos = this.context.Departamento.ToList();

            if (departamentos.Count == 0)
            {
                return new OperationResponse<ListaEnlazadaDoble<Departamento>>(0, "No se encontraron los Departamentos", null);
            }
            foreach (var departamento in departamentos)
            {
                listadepartamento.InsertarAlFinal(departamento);
            }

            return new OperationResponse<ListaEnlazadaDoble<Departamento>>(1, "Departamentos Encontrados Correctamente", listadepartamento);
        }

        public Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            throw new NotImplementedException();
        }
    }
}
