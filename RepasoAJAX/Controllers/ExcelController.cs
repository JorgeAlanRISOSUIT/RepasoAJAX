using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepasoAJAX.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Excel
        public ActionResult Configuration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Configuration(int? conf)
        {
            ML.DTO.ErrorDTOExcel errorExcel = new ML.DTO.ErrorDTOExcel();
            if (Request.Files["Excel"] != null)
            {
                HttpPostedFileBase request = Request.Files["Excel"];
                if (request.ContentLength > 0)
                {
                    var result = BL.CargaMasiva.ComprobarExtension(request.FileName);
                    if (result.Item1)
                    {
                        string server = $"{Server.MapPath("~/")}Posted\\";
                        if (!Directory.Exists(server))
                        {
                            Directory.CreateDirectory(server);
                        }
                        server += $"{Path.GetFileNameWithoutExtension(request.FileName)}-{DateTime.Now.ToString("yyyyMMddhhmmss")}{Path.GetExtension(request.FileName)}";
                        if (!System.IO.File.Exists(server))
                        {
                            request.SaveAs(server);
                            var resultInsercion = BL.CargaMasiva.Insercion($"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {server}; Extended Properties = Excel 12.0 Xml;");
                            if (resultInsercion.Item1)
                            {
                                var resultValidaciones = BL.CargaMasiva.ComprobacionDatos(resultInsercion.Item4.Productos);
                                if (resultValidaciones.Item1)
                                {
                                    var iteracion = BL.Producto.AddList(resultInsercion.Item4.Productos);
                                    if (iteracion.Item1)
                                    {
                                        return View(new ML.DTO.ErrorDTOExcel());
                                    }
                                    else
                                    {
                                        errorExcel.Errors = iteracion.Item4.Errors;
                                        errorExcel.Error = iteracion.Item3;
                                        errorExcel.Message = iteracion.Item2;
                                        return View(errorExcel);
                                    }
                                }
                                else
                                {
                                    errorExcel.Message = resultValidaciones.Item2;
                                    errorExcel.Error = resultValidaciones.Item3;
                                    return View(errorExcel);
                                }
                            }
                            else
                            {
                                errorExcel.Message = resultInsercion.Item2;
                                errorExcel.Error = resultInsercion.Item3;
                                return View(errorExcel);
                            }
                        }
                        else
                        {
                            errorExcel.Message = "Este archivo ha sido validado";
                            return View(errorExcel);
                        }
                    }
                    else
                    {
                        errorExcel.Message = "La Extension no es valida";
                        return View(errorExcel);
                    }
                }
                errorExcel.Message = "No es valido el archivo";
                return View(errorExcel);
            }
            return View();
        }
    }
}