//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producto
    {
        public int Sku { get; set; }
        public string Nombre { get; set; }
        public string NumMaterial { get; set; }
        public int Categoria { get; set; }
        public int Inventario { get; set; }
    
        public virtual SubCategoria SubCategoria { get; set; }
    }
}