using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        public static (bool, string, Exception) GetId(int idCategoria)
        {
            try
            {
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    var result = (from value in context.SubCategorias
                                  where value.IdSubCategoria == idCategoria
                                  select new ML.SubCategoria
                                  {
                                      IdSubCategoria = value.IdSubCategoria,
                                      Nombre = value.Nombre,
                                  }).SingleOrDefault();
                    if (result != null)
                    {
                        return (true, "", null);
                    }
                    else
                    {
                        return (false, "No existe la sección", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }

        public static (bool, string, Exception, ML.SubCategoria) GetAllByCategoria(int idCategoria)
        {
            try
            {
                ML.SubCategoria model = new ML.SubCategoria { Categorias = new List<ML.SubCategoria>() };
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    var result = context.PorSubCategorias(idCategoria).ToList();
                    if (result.Count > 0)
                    {
                        foreach (var categorias in result)
                        {
                            ML.SubCategoria subCategoria = new ML.SubCategoria
                            {
                                IdSubCategoria = categorias.IdSubCategoria,
                                Nombre = categorias.Seccion,
                                Categoria = new ML.Categoria
                                {
                                    Nombre = categorias.Area
                                }
                            };
                            model.Categorias.Add(subCategoria);
                        }
                        return (true, "", null, model);
                    }
                    else
                    {
                        return (false, "", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }
    }
}
