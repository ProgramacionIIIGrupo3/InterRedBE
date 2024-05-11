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
    public class CalificacionService : ICalificacionDAO
    {
        private readonly InterRedContext _context;

        public CalificacionService(InterRedContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Calificacion>> CreateOne(Calificacion obj)
        {
            try
            {
                _context.Calificacion.Add(obj);
                await _context.SaveChangesAsync();
                return new OperationResponse<Calificacion>(1, "Calificación creada exitosamente", obj);
            }
            catch (Exception ex)
            {
                // Aquí capturamos y mostramos la excepción interna
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                return new OperationResponse<Calificacion>(0, $"Error al crear calificación: {innerExceptionMessage}", null);
            }
        }


        public async Task<OperationResponse<Calificacion>> GetOne(int id)
        {
            try
            {
                var calificacion = await _context.Calificacion.FindAsync(id);
                if (calificacion == null)
                {
                    return new OperationResponse<Calificacion>(0, "Calificación no encontrada", null);
                }
                return new OperationResponse<Calificacion>(1, "Calificación encontrada", calificacion);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Calificacion>(0, ex.Message, null);
            }
        }

        public async Task<OperationResponse<Calificacion>> UpdateOne(Calificacion obj)
        {
            try
            {
                var calificacionActualizar = await _context.Calificacion.FindAsync(obj.Id);
                if (calificacionActualizar == null)
                {
                    return new OperationResponse<Calificacion>(0, "Calificación no encontrada", null);
                }

                // Obtener el valor numérico de la calificación
                var puntaje = (int?)typeof(Calificacion).GetProperty("Puntaje")?.GetValue(obj, null);

                // Validar campos obligatorios
                if (puntaje <= 0 || string.IsNullOrEmpty(obj.Comentario))
                {
                    return new OperationResponse<Calificacion>(0, "El puntaje y el comentario son obligatorios", null);
                }

                // Validar longitud del comentario
                if (obj.Comentario.Length > 500)
                {
                    return new OperationResponse<Calificacion>(0, "El comentario no puede tener más de 500 caracteres", null);
                }

                // Validar rango del puntaje
                if (puntaje < 1 || puntaje > 5)
                {
                    return new OperationResponse<Calificacion>(0, "El puntaje de la calificación debe estar entre 1 y 5", null);
                }

                _context.Entry(calificacionActualizar).CurrentValues.SetValues(obj);
                await _context.SaveChangesAsync();
                return new OperationResponse<Calificacion>(1, "Calificación actualizada exitosamente", calificacionActualizar);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Calificacion>(0, ex.Message, null);
            }
        }
    }
}