using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace ML.DTO
{
    public class ErrorDTOExcel
    {
        public string Message { get; set; }
        public List<ErrorDTOExcel> Errors { get; set; } = new List<ErrorDTOExcel>();
    }
}
