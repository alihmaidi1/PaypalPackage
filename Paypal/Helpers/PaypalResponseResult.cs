using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paypal.Dto;

namespace Paypal.Helpers;
public class  PaypalResponseResult
{

    public static PaypalResponse<T> Success<T>(T data)
            => new PaypalResponse<T>() { Result = data,Status=true };

        public static PaypalResponse<T> Failed<T>(string message)
           => new PaypalResponse<T>() { Message = message, Status=false };


    
}
