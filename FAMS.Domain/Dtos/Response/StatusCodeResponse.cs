using FAMS.Domain.Models.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class StatusCodeResponse<T>
    {
        public T Data {  get; set; }
        public HttpStatusCode statusCode { get; set; }
        public string Errormessge { get; set; }
        public object? bonusData { get; set; }
    }
}
