using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using Microsoft.Data.SqlClient;

namespace InterRedBE.DAL.Services
{
    public class DepartamentoService : IDepartamentoDAO
    {
        public readonly InterRedContext _context;

        public DepartamentoService(InterRedContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<Departamento>> CreateOne(Departamento obj)
        {
            try
            {
                _context.Departamento.Add(obj);
                await _context.SaveChangesAsync();
                return new OperationResponse<Departamento>(1, "Departamento creado exitosamente", obj);
            }
            catch (Exception ex)
            {
                // Registrar la excepción interna
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine($"Inner Exception Message: {innerException.Message}");
                    Console.WriteLine($"Inner Exception Type: {innerException.GetType().FullName}");
                    Console.WriteLine($"Inner Exception Stack Trace: {innerException.StackTrace}");

                    // Verificar si la excepción interna es una excepción de SQL
                    if (innerException is SqlException sqlException)
                    {
                        Console.WriteLine($"SQL Exception Number: {sqlException.Number}");
                        Console.WriteLine($"SQL Exception Message: {sqlException.Message}");
                        Console.WriteLine($"SQL Exception Line Number: {sqlException.LineNumber}");
                        Console.WriteLine($"SQL Exception Procedure: {sqlException.Procedure}");
                    }

                    innerException = innerException.InnerException;
                }

                return new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }

        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                var departamento = await _context.Departamento.FindAsync(id);

                if (departamento == null)
                {
                    return new OperationResponse<int>(0, "El departamento no existe", 0);
                }

                _context.Departamento.Remove(departamento);
                await _context.SaveChangesAsync();

                return new OperationResponse<int>(1, "Departamento eliminado correctamente", id);
            }
            catch (Exception ex)
            {
                return new OperationResponse<int>(0, ex.Message, 0);
            }
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

        public async Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            try
            {
                var departamentoExistente = _context.Departamento.FirstOrDefault(d => d.Id == obj.Id);

                if (departamentoExistente == null)
                {
                    return new OperationResponse<Departamento>(0, "El departamento no existe", null);
                }

                departamentoExistente.Nombre = obj.Nombre;
                departamentoExistente.Descripcion = obj.Descripcion;
                departamentoExistente.Imagen = obj.Imagen;
                departamentoExistente.Poblacion = obj.Poblacion;
                departamentoExistente.IdCabecera = obj.IdCabecera;

                _context.Departamento.Update(departamentoExistente);
                await _context.SaveChangesAsync();

                return new OperationResponse<Departamento>(1, "Departamento actualizado exitosamente", departamentoExistente);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }


    }
}
