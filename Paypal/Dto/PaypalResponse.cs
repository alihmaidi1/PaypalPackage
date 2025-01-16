using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.Dto;

public class PaypalResponse<T>
{


    public T Result{get;set;}

    public string Message{get;set;}

    public bool Status{get;set;}  


}
