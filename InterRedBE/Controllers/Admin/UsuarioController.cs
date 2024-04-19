using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly IUsuarioBAO _usuarioBAO;
        public readonly ILoginBAO _loginBAO;

        public UsuarioController(IUsuarioBAO usuarioBAO, ILoginBAO loginBAO)
        {
            _usuarioBAO = usuarioBAO;
            _loginBAO = loginBAO;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario login)
        {
            if (await _loginBAO.VerifyUser(login.Correo, login.Contrasena))
            {
                // Autenticación exitosa, puede devolver un token JWT u otro indicador de sesión
                return Ok("Login exitoso.");
            }
            else
            {
                return Unauthorized("Credenciales inválidas.");
            }
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_usuarioBAO.GetAll());
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
