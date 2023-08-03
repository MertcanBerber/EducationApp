using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Binte.Services.Responses
{
    public class BaseResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public static BaseResponse GetSuccess()
        {
            return new BaseResponse { Status = true, };
        }
        public static BaseResponse GetError(string message)
        {
            return new BaseResponse { Status = false, Message=message};
        }
    }
}
