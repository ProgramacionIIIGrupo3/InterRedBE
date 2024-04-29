using InterRedBE.AUTH.Aao;
using InterRedBE.AUTH.Service;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly IUsuarioBAO _usuarioBAO;
        public readonly ILoginBAO _loginBAO;
        public readonly IConfiguration _config;
        public readonly InterRedContext _interRedContext;
        private readonly IJwtAAO _jwtAAO;

        public UsuarioController(IUsuarioBAO usuarioBAO, ILoginBAO loginBAO, IConfiguration config, InterRedContext interRedContext, IJwtAAO jwtAAO)
        {
            _usuarioBAO = usuarioBAO;
            _loginBAO = loginBAO;
            _config = config;
            _interRedContext = interRedContext;
            _jwtAAO = jwtAAO;
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
                var user = await _interRedContext.Usuario.FirstOrDefaultAsync(u => u.Correo == login.Correo);
                var token = _jwtAAO.GenerateToken(user);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Credenciales inválidas.");
            }
        }


        [HttpGet]
        [Authorize]
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
        [Authorize]
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








