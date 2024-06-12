using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace RepasoAJAX.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Productos")]
        public IHttpActionResult Get()
        {
            ML.DTO.ResultDTO resultDTO = new ML.DTO.ResultDTO();
            var result = BL.Producto.GetAll();
            if (result.Item1)
            {
                resultDTO.Success = true;
                foreach (var item in result.Item4.Productos)
                {
                    resultDTO.Objects.Add(item);
                }
                return Ok<ML.DTO.ResultDTO>(resultDTO);
            }
            else
            {
                resultDTO.Message = result.Item2;
                resultDTO.Error = result.Item3;
                return BadRequest();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("RevisarProducto")]
        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            var result = BL.Producto.GetById(id);
            ML.DTO.ResultDTO resultDTO = new ML.DTO.ResultDTO();
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Object = result.Item4;
                return Ok(resultDTO);
            }
            else
            {
                resultDTO.Error = result.Item3;
                resultDTO.Message = result.Item2;
                return BadRequest();
            }
        }

        // POST api/values
        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("AgregarNuevaMercancia")]
        public IHttpActionResult Post([FromBody] ML.Producto producto)
        {
            var result = BL.Producto.Add(producto);
            ML.DTO.ResultDTO resultDTO = new ML.DTO.ResultDTO();
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = result.Item2;
                return Ok(resultDTO);
            }
            else
            {
                resultDTO.Message = result.Item2;
                resultDTO.Error = result.Item3;
                return BadRequest();
            }
        }

        [System.Web.Mvc.HttpPut]
        [System.Web.Http.Route("ActualizarPorProducto")]
        // PUT api/values/5
        public IHttpActionResult Put([FromBody] ML.Producto producto)
        {
            ML.DTO.ResultDTO resultDTO = new ML.DTO.ResultDTO();
            var result = BL.Producto.Update(producto);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = result.Item2;
                return Ok(resultDTO);
            }
            else
            {
                resultDTO.Message = result.Item2;
                resultDTO.Error = result.Item3;
                return BadRequest();
            }
        }

        [System.Web.Mvc.HttpDelete]
        [System.Web.Http.Route("EliminarPorProducto")]
        // DELETE api/values/5
        public IHttpActionResult Delete(int idProducto)
        {
            ML.DTO.ResultDTO resultDTO = new ML.DTO.ResultDTO();
            var result = BL.Producto.Delete(idProducto);
            if (result.Item1)
            {
                resultDTO.Success = true;
                resultDTO.Message = result.Item2;
                return Ok(resultDTO);
            }
            else
            {
                resultDTO.Message = result.Item2;
                resultDTO.Error = result.Item3;
                return BadRequest();
            }
        }
    }
}
