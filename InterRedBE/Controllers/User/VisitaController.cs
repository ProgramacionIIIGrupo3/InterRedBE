using InterRedBE.BAL.Bao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.User
{
    [Route("api/user/[controller]")]
    [ApiController]
    public class VisitaController : ControllerBase
    {

        public readonly IVisitaBAO _visitaBAO;

        public VisitaController(IVisitaBAO VisitaBAO)
        {
            _visitaBAO = VisitaBAO;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_visitaBAO.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] VisitaDTO VisitaDto)
        {
            try
            {
                var visita = new Visita
                {
                    Id = VisitaDto.Id,
                    LugarTuristicoId = VisitaDto.LugarTuristicoId,
                };

                var result = await _visitaBAO.CreateOne(visita);
                if (result.Data != null)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Data);
                }
                else
                {
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
