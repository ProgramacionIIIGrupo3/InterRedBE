using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Services
{
    public class VisitaService : IVisistaDAO
    {
        private readonly InterRedContext _context;

        public VisitaService(InterRedContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Visita>> CreateOne(Visita obj)
        {
            try
            {
                _context.Visita.Add(obj);
                await _context.SaveChangesAsync();
                return new OperationResponse<Visita>(1, "Visita creada exitosamente", obj);
            }
            catch (System.Exception ex)
            {
                return new OperationResponse<Visita>(0, ex.Message, null);
            }
        }

        public async Task<OperationResponse<Visita>> GetOne(int id)
        {
            try
            {
                var visita = await _context.Visita.FindAsync(id);
                if (visita == null)
                {
                    return new OperationResponse<Visita>(0, "Visita no encontrada", null);
                }
                return new OperationResponse<Visita>(1, "Visita encontrada", visita);
            }
            catch (System.Exception ex)
            {
                return new OperationResponse<Visita>(0, ex.Message, null);
            }
        }

        public async Task<OperationResponse<Visita>> UpdateOne(Visita obj)
        {
            try
            {
                var visitaActualizar = await _context.Visita.FindAsync(obj.Id);
                if (visitaActualizar == null)
                {
                    return new OperationResponse<Visita>(0, "Visita no encontrada", null);
                }

                _context.Entry(visitaActualizar).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
                return new OperationResponse<Visita>(1, "Visita actualizada exitosamente", visitaActualizar);
            }
            catch (System.Exception ex)
            {
                return new OperationResponse<Visita>(0, ex.Message, null);
            }
        }
    }
}