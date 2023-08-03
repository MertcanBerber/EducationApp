using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Responses
{
    public class GetResponse<T>:BaseResponse
    {
        public T Data { get; set; }
    }
}
