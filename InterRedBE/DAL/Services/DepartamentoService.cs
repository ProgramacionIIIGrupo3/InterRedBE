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
