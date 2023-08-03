using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Responses
{
    public class GetAllResponse<T>:BaseResponse
    {
        public List<T> DataList { get; set; }
    }
}
