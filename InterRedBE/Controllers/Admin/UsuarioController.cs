using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Models;
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

        [HttpDelete("{id}")]
        public IActionResult DeleteOne(int id)
        {
            try
            {
                return Ok(_usuarioBAO.DeleteOne(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOne(int id, [FromBody] Usuario usuario)
        {
            try
            {
                return Ok(_usuarioBAO.UpdateOne(usuario));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }
}








