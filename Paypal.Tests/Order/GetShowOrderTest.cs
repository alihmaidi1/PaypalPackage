using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using Paypal.Order;
using Paypal.Tests.Constant;

namespace Paypal.Tests.Order;

public class GetShowOrderTest
{



    [Fact]
    public async Task Try_Get_Order_Test()
    {

        // Given
        string orderId="862315322K150770A";
        PaypalOrder order=new PaypalOrder(Credential.ClientId,Credential.ClientSecret,Enum.PaypalApplicationMode.Sandbox);
        
        // When
        var response=await order.ShowOrder(orderId);
        
        if(!response.Status){

            Console.WriteLine(response.Message);

        }
        // Then
        Assert.True(response.Status);




    }
    
    
}
