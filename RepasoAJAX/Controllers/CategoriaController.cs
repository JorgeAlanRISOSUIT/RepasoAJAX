using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RepasoAJAX.Controllers
{
    public class CategoriaController : ApiController
    {
        [HttpGet]
        [Route("Categorias")]
        public IHttpActionResult GetAll()
        {
            ML.DTO.ResultDTO resultDTO = new ML.DTO.ResultDTO();
            var result = BL.Categoria.GetAll();
            if (result.Item1)
            {
                resultDTO.Success = true;
                foreach (var item in result.Item4.Categorias)
                {
                    resultDTO.Objects.Add(item);
                }
                return Ok(resultDTO);
            }
            else
            {
                resultDTO.Message = result.Item2;
                resultDTO.Error = result.Item3;
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetCategoriasBySeccion")]
        public IHttpActionResult GetAllByIdCategoria(int idCategoria)
        {
            ML.DTO.ResultDTO resultDTO = new ML.DTO.ResultDTO();
            var result = BL.SubCategoria.GetAllByCategoria(idCategoria);
            if (result.Item1)
            {
                resultDTO.Success = true;
                foreach (var item in result.Item4.Categorias)
                {
                    resultDTO.Objects.Add(item);
                }
                return Ok(resultDTO);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
