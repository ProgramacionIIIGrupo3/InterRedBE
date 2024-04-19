using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    // Define la clase DepartamentoBAOService que implementa la interfaz IDepartamentoBAO.
    public class DepartamentoBAOService : IDepartamentoBAO
    {
        // Declaración de la interfaz del objeto de acceso a datos para departamento.
        public readonly IDepartamentoDAO _departamentoDAO;

        // Constructor que recibe una implementación de IDepartamentoDAO para manejar la persistencia.
        public DepartamentoBAOService(IDepartamentoDAO departamentoDAO)
        {
            _departamentoDAO = departamentoDAO;
        }

        // Método  para crear un departamento. Retorna una respuesta encapsulada en un objeto OperationResponse.
        public async Task<OperationResponse<Departamento>> CreateOne(Departamento obj)
        {
            return await _departamentoDAO.CreateOne(obj);
        }

        // Método  para eliminar un departamento por su ID. 
        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                return await _departamentoDAO.DeleteOne(id);
            }
            catch (Exception ex)
            {
                // En caso de error, retorna un objeto OperationResponse con código 0 y mensaje de error.
                return new OperationResponse<int>(0, ex.Message, 0);
            }
        }

        // Método para obtener todos los departamentos.
        public OperationResponse<ListaEnlazadaDoble<Departamento>> GetAll()
        {
            try
            {
                return _departamentoDAO.GetAll();
            }
            catch (Exception ex)
            {
                // En caso de error, retorna un objeto OperationResponse con datos nulos y mensaje de error.
                return new OperationResponse<ListaEnlazadaDoble<Departamento>>(0, ex.Message, null);
            }
        }

        // Método para obtener un departamento específico por ID. 
        public async Task<OperationResponse<Departamento>> GetOneInt(int id)
        {
            try
            {
                return await _departamentoDAO.GetOne(id);
            }
            catch (Exception ex)
            {
                // En caso de error, retorna un objeto OperationResponse con datos nulos y mensaje de error.
                return new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }

        // Método para actualizar un departamento. 
        public async Task<OperationResponse<Departamento>> UpdateOne(Departamento obj)
        {
            try
            {
                return await _departamentoDAO.UpdateOne(obj);
            }
            catch (Exception ex)
            {
                // En caso de error, retorna un objeto OperationResponse con datos nulos y mensaje de error.
                return new OperationResponse<Departamento>(0, ex.Message, null);
            }
        }
    }
}
