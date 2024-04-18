using InterRedBE.BAL.Bao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class LugarTuristicoController : ControllerBase
    {
        public readonly ILugarTuristicoBAO _lugarTuristicoBAO;
        public LugarTuristicoController(ILugarTuristicoBAO lugarTuristicoBAO)
        {
            _lugarTuristicoBAO = lugarTuristicoBAO;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_lugarTuristicoBAO.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
