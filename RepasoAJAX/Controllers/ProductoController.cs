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
        public ActionResult ObtenerProducto(int idProducto)
        {
            if (idProducto > 0)
            {
                var result = BL.Producto.GetById(idProducto);
                if (result.Item1)
                {
                    var model = result.Item4;
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ML.Producto model = new ML.Producto();
                model.Productos = new List<ML.Producto>();
                return View(model);
            }
        }
    }
}