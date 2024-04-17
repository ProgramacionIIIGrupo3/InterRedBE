using InterRedBE.BAL.Bao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly IUsuarioBAO _usuarioBAO;

        public UsuarioController(IUsuarioBAO usuarioBAO)
        {
            _usuarioBAO = usuarioBAO;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            try
            {
                return Ok(_usuarioBAO.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
