using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        public static (bool, string, Exception) GetId(int idCategoria)
        {
            try
            {
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    var result = (from value in context.Categorias
                                  where value.IdCategoria == idCategoria
                                  select new ML.Categoria
                                  {
                                      IdCategoria = value.IdCategoria,
                                      Nombre = value.Nombre,
                                  }).SingleOrDefault();
                    if (result != null)
                    {
                        return (true, "", null);
                    }
                    else
                    {
                        return (false, "No se encuentra el área", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (true, ex.Message, ex);
            }
        }

        public static (bool, string, Exception, ML.Categoria) GetAll()
        {
            try
            {
                ML.Categoria model = new ML.Categoria { Categorias = new List<ML.Categoria>() };
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    var result = context.ListarLasCategorias().ToList();
                    if (result.Count > 0)
                    {
                        foreach (var categorias in result)
                        {
                            ML.Categoria objCategoria = new ML.Categoria()
                            {
                                IdCategoria = categorias.IdCategoria,
                                Nombre = categorias.Nombre,
                            };
                            model.Categorias.Add(objCategoria);
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
