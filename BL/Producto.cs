using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;

namespace BL
{
    public class Producto
    {
        public static (bool, string, Exception, ML.Producto) GetAll()
        {
            try
            {
                ML.Producto model = new ML.Producto { Productos = new List<ML.Producto>() };
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    var productos = context.MostrarProductos().ToList();
                    if (productos.Count > 0)
                    {
                        foreach (var producto in productos)
                        {
                            ML.Producto objProducto = new ML.Producto
                            {
                                Sku = producto.Sku,
                                Nombre = producto.Producto,
                                NumMateria = producto.NumMaterial,
                                Categoria = new ML.SubCategoria
                                {
                                    IdSubCategoria = producto.IdSubCategoria,
                                    Nombre = producto.Area,
                                    Categoria = new ML.Categoria
                                    {
                                        IdCategoria = producto.IdCategoria,
                                        Nombre = producto.Seccion
                                    }
                                },
                                Inventario = producto.Inventario,
                            };
                            model.Productos.Add(objProducto);
                        }
                        return (true, "", null, model);
                    }
                    else
                    {
                        return (false, "No se encontraron los productos", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }
        public static (bool, string, Exception, ML.Producto) GetById(int idProducto) 
        {
            try
            {
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    var producto = context.EscogerProducto(idProducto).SingleOrDefault();
                    if(producto != null)
                    {
                        ML.Producto objProducto = new ML.Producto
                        {
                            Sku = producto.Sku,
                            Nombre = producto.Producto,
                            NumMateria = producto.Producto,
                            Categoria = new ML.SubCategoria
                            {
                                IdSubCategoria = producto.IdSubCategoria,
                                Nombre = producto.Area,
                                Categoria = new ML.Categoria
                                {
                                    IdCategoria = producto.IdCategoria,
                                    Nombre = producto.Seccion
                                }
                            },
                            Inventario = producto.Inventario,
                        };
                        return (true, "", null, objProducto);
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
        public static (bool, string, Exception) Update(ML.Producto producto) 
        {
            try
            {
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    int row = context.CambiarProducto(producto.Sku, producto.Nombre, producto.NumMateria, producto.Categoria.IdSubCategoria, producto.Inventario);
                    if(row > 0)
                    {
                        return (true, "Se ha modificato el producto", null);
                    }
                    else
                    {
                        return (false, "No se ha modificado el producto", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
        public static (bool, string, Exception) Add(ML.Producto producto) 
        {
            try
            {
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    int row = context.VenderProducto(producto.Nombre, producto.NumMateria, producto.Categoria.IdSubCategoria, producto.Inventario);
                    if(row > 0)
                    {
                        return (true, $"se ha llenado el producto con el {producto.Nombre}", null);
                    }
                    else
                    {
                        return (false, "La inserción no cumple las validaciones", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
        public static (bool, string, Exception) Delete(int idProducto) 
        {
            try
            {
                using (DL.JAEscobarRepasoAJAXEntities context = new DL.JAEscobarRepasoAJAXEntities())
                {
                    int row = context.ElmiminarProducto(idProducto);
                    if(row > 0)
                    {
                        return (true, "", null);
                    }
                    else
                    {
                        return (false, "", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }

    }
}
