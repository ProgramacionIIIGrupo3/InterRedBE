using InterRedBE.DAL.Context;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InterRedBE.DAL.Services
{
    
    public class DepartamentoService : IDepartamentoDAO
    {
        // Contexto de la base de datos
        public readonly InterRedContext _context;

        // Constructor que recibe el contexto de la base de datos
        public DepartamentoService(InterRedContext context)
        {
            _context = context;
        }

        // Método para crear un nuevo departamento 
        public async Task<OperationResponse<Departamento>> CreateOne(Departamento obj)
        {
            try
            {
                // Agregar el nuevo departamento al contexto y guardar los cambios en la base de datos
                _context.Departamento.Add(obj);
                await _context.SaveChangesAsync();
                // Devolver una respuesta exitosa con el departamento creado
                return new OperationResponse<Departamento>(1, "Departamento creado exitosamente", obj);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la creación y devolver una respuesta de error
                return new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }

        // Método para eliminar un departamento 
        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                // Buscar el departamento por su ID
                var departamento = await _context.Departamento.FindAsync(id);

                // Verificar si el departamento existe
                if (departamento == null)
                {
                    // Si no existe, devolver un mensaje de error
                    return new OperationResponse<int>(0, "El departamento no existe", 0);
                }

                // Eliminar el departamento del contexto y guardar los cambios en la base de datos
                _context.Departamento.Remove(departamento);
                await _context.SaveChangesAsync();

                // Devolver una respuesta exitosa con el ID del departamento eliminado
                return new OperationResponse<int>(1, "Departamento eliminado correctamente", id);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la eliminación y devolver una respuesta de error
                return new OperationResponse<int>(0, ex.Message, 0);
            }
        }

        // Método para obtener todos los departamentos 
        // Método para obtener todos los departamentos 
        public OperationResponse<ListaEnlazadaDoble<Departamento>> GetAll()
        {
            try
            {
                // Obtener todos los departamentos de la base de datos incluyendo sus municipios
                var departamentos = _context.Departamento
                                            .Include(d => d.Municipios)
                                            .ToList();

                if (departamentos.Count == 0)
                {
                    return new OperationResponse<ListaEnlazadaDoble<Departamento>>(0, "No se encontraron los Departamentos", null);
                }

                var listaDepartamentos = new ListaEnlazadaDoble<Departamento>();
                foreach (var departamento in departamentos)
                {
                    listaDepartamentos.InsertarAlFinal(departamento);
                }

                return new OperationResponse<ListaEnlazadaDoble<Departamento>>(1, "Departamentos Encontrados Correctamente", listaDepartamentos);
            }
            catch (Exception ex)
            {
                return new OperationResponse<ListaEnlazadaDoble<Departamento>>(0, ex.Message, null);
            }
        }


        // Método para obtener un departamento 
        public async Task<OperationResponse<Departamento>> GetOne(int id)
        {
            try
            {
                // Buscar el departamento por su ID incluyendo sus municipios
                var departamento = _context.Departamento
                                           .Include(d => d.Municipios)
                                           .FirstOrDefault(ts => ts.Id == id);

                if (departamento != null)
                {
                    return new OperationResponse<Departamento>(1, "Departamento Encontrado Correctamente", departamento);
                }
                else
                {
                    return new OperationResponse<Departamento>(0, "Departamento no encontrado", null);
                }
            }
            catch (Exception ex)
            {
                return new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }


        // Método para actualizar un departamento 
        public async Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            try
            {
                // Buscar el departamento existente por su ID
                var departamentoExistente = _context.Departamento.FirstOrDefault(d => d.Id == obj.Id);

                // Verificar si el departamento existe
                if (departamentoExistente == null)
                {
                    // Si no existe, devolver un mensaje de error
                    return new OperationResponse<Departamento>(0, "El departamento no existe", null);
                }

                // Actualizar los datos del departamento existente con los datos del objeto proporcionado
                departamentoExistente.Nombre = obj.Nombre;
                departamentoExistente.Descripcion = obj.Descripcion;
                departamentoExistente.Imagen = obj.Imagen;
                departamentoExistente.IdCabecera = obj.IdCabecera;

                // Actualizar el departamento en el contexto y guardar los cambios en la base de datos
                _context.Departamento.Update(departamentoExistente);
                await _context.SaveChangesAsync();

                // Devolver una respuesta exitosa con el departamento actualizado
                return new OperationResponse<Departamento>(1, "Departamento actualizado exitosamente", departamentoExistente);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la actualización y devolver una respuesta de error
                return new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }
    }
}

