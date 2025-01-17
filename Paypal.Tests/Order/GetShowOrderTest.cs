using System;
using System.Collections.Generic;
using System.Linq;
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
        string orderId="5O190127TN364715T";
        PaypalOrder order=new PaypalOrder(Credential.ClientId,Credential.ClientSecret,Enum.PaypalApplicationMode.Sandbox);
        
        // When
        var response=await order.ShowOrder(orderId);
        
        // Then
        Assert.True(response.Status);




    }
    
    
}
