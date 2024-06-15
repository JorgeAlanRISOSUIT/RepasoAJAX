using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepasoAJAX.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult ObtenerProducto(int? idProducto)
        {
            if (idProducto.GetValueOrDefault(0) > 0)
            {
                var result = BL.Producto.GetById(idProducto.Value);
                if (result.Item1)
                {
                    ML.Producto model = result.Item4;
                    var resultCategoria = BL.Categoria.GetAll();
                    var resultSubCategoria = BL.SubCategoria.GetAllByCategoria(model.Categoria.Categoria.IdCategoria);
                    model.Categoria.Categoria.Categorias = resultCategoria.Item1 ? resultCategoria.Item4.Categorias : new List<ML.Categoria>();
                    model.Categoria.Categorias = resultSubCategoria.Item1 ? resultSubCategoria.Item4.Categorias: new List<ML.SubCategoria>();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ML.Producto producto = new ML.Producto();
                var resultCategoria = BL.Categoria.GetAll();
                producto.Categoria = new ML.SubCategoria();
                producto.Categoria.Categorias = new List<ML.SubCategoria>();
                producto.Categoria.Categoria = new ML.Categoria();
                producto.Categoria.Categoria.Categorias = resultCategoria.Item1 ? resultCategoria.Item4.Categorias : new List<ML.Categoria>();
                return View(producto);
            }
        }
    }
}