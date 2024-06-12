using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Runtime.CompilerServices;
using System.Data.Entity.Infrastructure;

namespace BL
{
    public class CargaMasiva
    {
        public static (bool, string, Exception, ML.DTO.ErrorDTOExcel) ComprobacionDatos(List<ML.Producto> productos)
        {
            try
            {
                ML.DTO.ErrorDTOExcel errorModel = new ML.DTO.ErrorDTOExcel { Errors = new List<ML.DTO.ErrorDTOExcel>() };
                foreach (ML.Producto item in productos)
                {
                    ML.DTO.ErrorDTOExcel errorDTOExcel = new ML.DTO.ErrorDTOExcel();
                    errorDTOExcel.Message += string.IsNullOrEmpty(item.Nombre) ? "El valor debe ser rellenado" : "";
                    errorDTOExcel.Message += string.IsNullOrEmpty(item.NumMateria) ? "El valor debe ser de un codigo" : "";
                    errorDTOExcel.Message += item.Categoria.IdSubCategoria > 0 ? "" : "El valor de lectura 0 o menor no puede ser implementada";
                    errorDTOExcel.Message += !BL.SubCategoria.GetId(item.Categoria.IdSubCategoria).Item1 ? BL.SubCategoria.GetId(item.Categoria.IdSubCategoria).Item2 : "";
                    errorDTOExcel.Message += item.Inventario > 0 ? "" : "El valor de lectura 0 o menor no puede ser implementada";
                    errorDTOExcel.Message += item.Categoria.Categoria.IdCategoria > 0 ? "" : "El valor de lectura 0 o menor no puede ser implementada";
                    errorDTOExcel.Message += !BL.Categoria.GetId(item.Categoria.Categoria.IdCategoria).Item1 ? BL.Categoria.GetId(item.Categoria.Categoria.IdCategoria).Item2 : "";
                    if (!string.IsNullOrEmpty(errorDTOExcel.Message))
                    {
                        errorModel.Errors.Add(errorDTOExcel);
                    }
                }
                if (errorModel.Errors.Count == 0)
                {
                    return (true, "", null, null);
                }
                else
                {
                    return (false, "", null, errorModel);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.DTO.ErrorDTOExcel) ComprobarExtension(string file)
        {
            try
            {
                ML.DTO.ErrorDTOExcel model = new ML.DTO.ErrorDTOExcel();
                if (!string.IsNullOrEmpty(file))
                {
                    if (Path.GetExtension(file) == ".xlsx")
                    {
                        return (true, "", null, model);
                    }
                    else
                    {
                        model.Errors.Add(new ML.DTO.ErrorDTOExcel { Message = "No es valida la extension" });
                        return (false, "", null, model);
                    }
                }
                else
                {
                    model.Errors.Add(new ML.DTO.ErrorDTOExcel { Message = "No existe el archivo" });
                    return (false, "No se puede leer el archivo masivo", null, model);
                }

            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.Producto) Insercion(string stringConnection)
        {
            try
            {
                ML.Producto model = new ML.Producto { Productos = new List<ML.Producto>() };
                using (OleDbConnection oleDB = new OleDbConnection(stringConnection))
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = oleDB;
                    cmd.CommandText = "SELECT * FROM [Sheet1$]";
                    cmd.CommandType = CommandType.Text;
                    oleDB.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataSet filter = new DataSet();
                    adapter.Fill(filter);
                    DataTable table = filter.Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow item in table.Rows)
                        {
                            ML.Producto objProducto = new ML.Producto();
                            objProducto.Nombre = item[0].ToString();
                            objProducto.NumMateria = item[1].ToString();
                            objProducto.Categoria = new ML.SubCategoria();
                            objProducto.Categoria.IdSubCategoria = Convert.ToInt32(item[2].ToString());
                            objProducto.Inventario = Convert.ToInt32(item[3].ToString());
                            objProducto.Categoria.Categoria = new ML.Categoria();
                            objProducto.Categoria.Categoria.IdCategoria = Convert.ToInt32(item[4].ToString());
                            model.Productos.Add(objProducto);
                        }
                        return (true, "", null, model);
                    }
                    else
                    {
                        return (false, "No existe el valor correspondiente", null, null);
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
