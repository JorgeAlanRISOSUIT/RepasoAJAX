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
                    if(result != null)
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
    }
}
