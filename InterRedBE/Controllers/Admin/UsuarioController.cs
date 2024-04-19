using InterRedBE.BAL.Bao;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //public IActionResult CreateOne(int id) 
        //{
        //    try
        //    {
        //        return Ok(_usuarioBAO.CreateOne(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteOne(int id)
        //{
        //    try
        //    {
        //        return Ok(_usuarioBAO.DeleteOne(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("No se encuentra el usuario.");
            }

            try
            {
                var result = await _usuarioBAO.UpdateOne(usuario);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}








