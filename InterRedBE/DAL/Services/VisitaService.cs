using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InterRedBE.DAL.Services
{
    public class VisitaService : IVisistaDAO<Visita>
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
                // Obtener el valor numérico del puntaje
                var puntaje = (int?)typeof(Visita).GetProperty("Puntaje")?.GetValue(obj, null);

                // Obtener el valor del comentario
                var comentario = (string)typeof(Visita).GetProperty("Comentario")?.GetValue(obj, null);

                // Validar campos obligatorios
                if (puntaje <= 0 || string.IsNullOrEmpty(comentario))
                {
                    return new OperationResponse<Visita>(0, "El puntaje y el comentario son obligatorios", null);
                }

                // Validar longitud del comentario
                if (comentario.Length > 500)
                {
                    return new OperationResponse<Visita>(0, "El comentario no puede tener más de 500 caracteres", null);
                }

                // Validar rango del puntaje
                if (puntaje < 1 || puntaje > 5)
                {
                    return new OperationResponse<Visita>(0, "El puntaje de la visita debe estar entre 1 y 5", null);
                }

                _context.Visita.Add(obj);
                await _context.SaveChangesAsync();
                return new OperationResponse<Visita>(1, "Visita creada exitosamente", obj);
            }
            catch (Exception ex)
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
            catch (Exception ex)
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

                // Obtener el valor numérico del puntaje
                var puntaje = (int?)typeof(Visita).GetProperty("Puntaje")?.GetValue(obj, null);

                // Obtener el valor del comentario
                var comentario = (string)typeof(Visita).GetProperty("Comentario")?.GetValue(obj, null);

                // Validar campos obligatorios
                if (puntaje <= 0 || string.IsNullOrEmpty(comentario))
                {
                    return new OperationResponse<Visita>(0, "El puntaje y el comentario son obligatorios", null);
                }

                // Validar longitud del comentario
                if (comentario.Length > 500)
                {
                    return new OperationResponse<Visita>(0, "El comentario no puede tener más de 500 caracteres", null);
                }

                // Validar rango del puntaje
                if (puntaje < 1 || puntaje > 5)
                {
                    return new OperationResponse<Visita>(0, "El puntaje de la visita debe estar entre 1 y 5", null);
                }

                _context.Entry(visitaActualizar).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
                return new OperationResponse<Visita>(1, "Visita actualizada exitosamente", visitaActualizar);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Visita>(0, ex.Message, null);
            }
        }
    }
}