using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    // Define la clase LugarTuristicoBAOService que implementa la interfaz ILugarTuristicoBAO.
    public class LugarTuristicoBAOService : ILugarTuristicoBAO
    {
        // Declaración de una variable de tipo ILugarTuristicoDAO para interactuar con la base de datos.
        public readonly ILugarTuristicoDAO _lugarTuristicoDAO;

        // Constructor que inicializa el DAO necesario para las operaciones de la base de datos.
        public LugarTuristicoBAOService(ILugarTuristicoDAO lugarTuristicoDAO)
        {
            _lugarTuristicoDAO = lugarTuristicoDAO;
        }

        // Método para crear un nuevo lugar turístico. Retorna una respuesta de operación.
        public async Task<OperationResponse<LugarTuristico>> CreateOne(LugarTuristico obj)
        {
            return await _lugarTuristicoDAO.CreateOne(obj);
        }

        // Método  para eliminar un lugar turístico por ID. Maneja excepciones internas.
        public async Task<OperationResponse<int>> DeleteOne(int id)
        {
            try
            {
                return await _lugarTuristicoDAO.DeleteOne(id);
            }
            catch (Exception ex)
            {
                // En caso de excepción, devuelve una respuesta de operación con código 0 y mensaje de error.
                return new OperationResponse<int>(0, ex.Message, 0);
            }
        }

        // Método para obtener todos los lugares turísticos. Maneja excepciones internas.
        public OperationResponse<ListaEnlazadaDoble<LugarTuristico>> GetAll()
        {
            try
            {
                return _lugarTuristicoDAO.GetAll();
            }
            catch (Exception ex)
            {
                // En caso de excepción, devuelve una respuesta de operación con datos nulos y mensaje de error.
                return new OperationResponse<ListaEnlazadaDoble<LugarTuristico>>(0, ex.Message, null);
            }
        }

        // Método para obtener un lugar turístico por ID. Maneja excepciones internas.
        public async Task<OperationResponse<LugarTuristico>> GetOneInt(int id)
        {
            try
            {
                return await _lugarTuristicoDAO.GetOne(id);
            }
            catch (Exception ex)
            {
                // En caso de excepción, devuelve una respuesta de operación con datos nulos y mensaje de error.
                return new OperationResponse<LugarTuristico>(0, ex.Message, null);
            }
        }

        // Método  actualizar un lugar turístico. Maneja excepciones internas.
        public async Task<OperationResponse<LugarTuristico>> UpdateOne(LugarTuristico obj)
        {
            try
            {
                return await _lugarTuristicoDAO.UpdateOne(obj);
            }
            catch (Exception ex)
            {
                // En caso de excepción, devuelve una respuesta de operación con datos nulos y mensaje de error.
                return new OperationResponse<LugarTuristico>(0, ex.Message, null);
            }
        }
    }
}
