using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAMS.Domain.Models.Dtos.Response
{
    public class ViewListResponse
    {
        public int TotalPage { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public object[] List {  get; set; } = new object[] { };
    }
}
