using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using Microsoft.Data.SqlClient;

namespace InterRedBE.DAL.Services
{
    public class LugarTuristicoService : ILugarTuristicoDAO
    {
        public readonly InterRedContext _context;
        public LugarTuristicoService(InterRedContext context)
        {
            _context = context;
        }
        public Task<OperationResponse<LugarTuristico>> CreateOne(LugarTuristico obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetAll()
        {
            var listaLugaresTuristicos = new ListaEnlazadaDoble<LugarTuristico>();
            var lugaresTuristicos = _context.LugarTuristico.ToList();


            if (lugaresTuristicos.Count == 0)
            {
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(0, "No se encontraron los Lugares Turisticos", null);
            }
            foreach (var lugarTuristico in lugaresTuristicos)
            {
                listaLugaresTuristicos.InsertarAlFinal(lugarTuristico);
            }

            return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(1, "Lugares Turisticos Encontrados Correctamente", listaLugaresTuristicos);
        }

        public Task<OperationResponse<LugarTuristico>> GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<LugarTuristico>> UpdateOne(LugarTuristico obj)
        {
            throw new NotImplementedException();
        }
    }


}
