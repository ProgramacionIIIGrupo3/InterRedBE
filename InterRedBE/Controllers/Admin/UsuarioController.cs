using InterRedBE.AUTH.Aao;
using InterRedBE.AUTH.Service;
using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Context;
using InterRedBE.DAL.DTO;
using InterRedBE.DAL.Models;
using InterRedBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InterRedBE.UTILS;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioBAO _usuarioBAO;
        private readonly ILoginBAO _loginBAO;
        private readonly IConfiguration _config;
        private readonly InterRedContext _interRedContext;
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

            if (await _loginBAO.VerifyUser(login.NombreUsuario, login.Contrasena))
            {
                var user = await _interRedContext.Usuario.FirstOrDefaultAsync(u => u.NombreUsuario == login.NombreUsuario);
                var token = _jwtAAO.GenerateToken(user);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Credenciales inválidas.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult GetAll()
        {
            try
            {
                var result = _usuarioBAO.GetAll();
                if (result!=null)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateOne([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (usuario.Rol != "Invitado" && usuario.Rol != "Administrador")
            {
                return BadRequest("El rol debe ser 'Invitado' o 'Administrador'.");
            }

            var passwordHasher = new PasswordHasher();
            usuario.Contrasena = passwordHasher.HashPassword(usuario, usuario.Contrasena);

            var response = await _usuarioBAO.CreateOne(usuario);
            if (response.Code == 1)
            {
                return Ok(response.Data);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            var response = await _usuarioBAO.DeleteOne(id);
            if (response.Code == 1)
            {
                return Ok(response.Data);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateOne(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("No se encuentra el usuario.");
            }

            if (usuario.Rol != "Invitado" && usuario.Rol != "Administrador")
            {
                return BadRequest("El rol debe ser 'Invitado' o 'Administrador'.");
            }

            try
            {
                var result = await _usuarioBAO.UpdateOne(usuario);
                if (result.Code == 1)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
