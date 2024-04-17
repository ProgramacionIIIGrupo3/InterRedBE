using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.DAL.Services
{
    public class MunicipioService : IMunicipioBAO
    {
        public readonly InterRedContext _context;

        public MunicipioService(InterRedContext context)
        {
            _context = context;
        }

        public Task<OperationResponse<Municipio>> CreateOne(Municipio obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<Municipio>> GetAll()
        {
            var listaMunicipios = new ListaEnlazadaDoble<Municipio>();

            var municipios = _context.Municipio.ToList();

            if(municipios.Count == 0)
            {
                return new OperationResponse<ListaEnlazadaDoble<Municipio>>(0, "Municipios no encontrados", null);
            }

            foreach(var municipio in municipios)
            {
                listaMunicipios.InsertarAlFinal(municipio);
            }

            return new OperationResponse<ListaEnlazadaDoble<Municipio>>(1, "Municipios encontrados", listaMunicipios);
        }

        public Task<OperationResponse<Municipio>> UpdateOne(Municipio obj)
        {
            throw new NotImplementedException();
        }
    }
}
