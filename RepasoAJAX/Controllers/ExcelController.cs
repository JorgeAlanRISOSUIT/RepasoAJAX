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
            if (Request.Files["Excel"] != null)
            {
                HttpPostedFileBase request = Request.Files["Excel"];
                if (request.ContentLength > 0)
                {
                    var result = BL.CargaMasiva.ComprobarExtension(request.FileName);
                    if (result.Item1)
                    {
                        string server = Server.MapPath("~/") + "Posted/";
                        if (!Directory.Exists(server))
                        {
                            Directory.CreateDirectory(server);
                        }
                        server += Path.GetFileNameWithoutExtension(request.FileName) + "-" + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(request.FileName);
                        if (!System.IO.File.Exists(server))
                        {
                            request.SaveAs(server);
                            var resultInsercion = BL.CargaMasiva.Insercion($"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {server}; Extended Properties = Excel 12.0 Xml;HDR=YES");
                        }
                    }
                }
                return View();
            }
            return View();
        }
    }
}