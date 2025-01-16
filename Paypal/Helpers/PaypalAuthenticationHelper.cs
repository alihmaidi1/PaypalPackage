using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paypal.Enum;

namespace Paypal.Helpers;

public class PaypalAuthenticationHelper
{
    public static string GetBaseUrl(PaypalApplicationMode mode){

        return mode==PaypalApplicationMode.Live?"https://api-m.paypal.com":"https://api-m.sandbox.paypal.com";
    }

    public static string GetAuthenticationBase64(string ClientId,string ClientSecret){


        return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));


    }

}
