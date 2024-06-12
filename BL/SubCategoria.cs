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
					if(result != null)
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
    }
}
