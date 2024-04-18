using InterRedBE.BAL.Bao;
using InterRedBE.DAL.Dao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InterRedBE.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        public readonly IMunicipioBAO _municipioBAO;

        public MunicipioController(IMunicipioBAO municipioBAO)
        {
            _municipioBAO = municipioBAO;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_municipioBAO.GetAll());
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
        //        return Ok(_municipioBAO.CreateOne(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        public IActionResult DeleteOne(int id)
        {
            try
            {
                return Ok(_municipioBAO.DeleteOne(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //public IActionResult UpdateOne( Usuario)
        //{
        //    try
        //    {
        //        return Ok(_municipioBAO.UpdateOne());
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

    }
}
