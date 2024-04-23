using FluentValidation;
using InterRedBE.BAL.Bao;
//using InterRedBE.BAL.Validators;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using InterRedBE.UTILS.Services;

namespace InterRedBE.BAL.Services
{
    public class CalificacionBAOService : ICalificacionBAO<Calificacion>
    {
        private readonly ICalificacionDAO<Calificacion> _calificacionDAO;
        private readonly IValidator<Calificacion> _validator;

        public CalificacionBAOService(ICalificacionDAO<Calificacion> calificacionDAO, IValidator<Calificacion> validator)
        {
            _calificacionDAO = calificacionDAO;
            _validator = validator;
        }

        public async Task<OperationResponse<Calificacion>> CreateOne(Calificacion obj)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                return await _calificacionDAO.CreateOne(obj);
            }
            catch (ValidationException ex)
            {
                return new OperationResponse<Calificacion>(0, ex.Message, null);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Calificacion>(0, ex.Message, null);
            }
        }

        public Task<OperationResponse<int>> DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public OperationResponse<ListaEnlazadaDoble<Calificacion>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse<Calificacion>> GetOneInt(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResponse<Calificacion>> UpdateOne(Calificacion obj)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(obj);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                return await _calificacionDAO.UpdateOne(obj);
            }
            catch (ValidationException ex)
            {
                return new OperationResponse<Calificacion>(0, ex.Message, null);
            }
            catch (Exception ex)
            {
                return new OperationResponse<Calificacion>(0, ex.Message, null);
            }
        }
    }
}