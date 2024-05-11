using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {

        public readonly ICalificacionBAO _calificacionBAO;

        public CalificacionController(ICalificacionBAO calificacionBAO)
        {
            _calificacionBAO = calificacionBAO;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_calificacionBAO.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] CalificacionDTO calificacionDto)
        {
            try
            {
                var calificacion = new Calificacion
                {
                    LugarTuristicoId = calificacionDto.LugarTuristicoId,
                    Puntuacion = calificacionDto.Puntuacion,
                    Comentario = calificacionDto.Comentario,
          

                };
                
                var result = await _calificacionBAO.CreateOne(calificacion);
                if(result.Data!=null)
                {
                    return StatusCode (StatusCodes.Status201Created, result.Data);
                }
                else {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





    }
}
