using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace BL
{
    public class CargaMasiva
    {
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

        public static (bool, string, Exception, ML.DTO.ErrorDTOExcel) Insercion(string stringConnection)
        {
            try
            {
                using (OleDbConnection oleDB = new OleDbConnection(stringConnection))
                {
                    OleDbCommand cmd = oleDB.CreateCommand();
                    cmd.Connection = oleDB;
                    cmd.CommandText = "SELECT * FROM Sheet$1";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }
    }
}
