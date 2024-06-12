using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int Sku { get; set; }
        public string Nombre { get; set; }
        public string NumMateria { get; set; }
        public SubCategoria Categoria { get; set; }
        public int Inventario { get; set; }
        public List<ML.Producto> Productos { get; set; }
    }
}
