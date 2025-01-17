using Paypal.Dto.Order;
using Paypal.Enum;
using Paypal.Order;
using Paypal.Tests.Constant;

namespace Paypal.Tests.Order;

public class UpdateOrderTest
{



    [Fact]
    public async Task Try_Update_Order_Test()
    {

        // Given
        string orderId="862315322K150770A";
        PaypalOrder order=new PaypalOrder(Credential.ClientId,Credential.ClientSecret,Enum.PaypalApplicationMode.Sandbox);
        List<UpdateOrderRequest<PaypalIntent>> updateOrderRequests=new List<UpdateOrderRequest<PaypalIntent>>{

            new UpdateOrderRequest<PaypalIntent>{

                op=UpdateOrderOperation.replace,
                path="/intent",
                value=PaypalIntent.CAPTURE
                
            }

        } ;

        // When
        var response=await order.UpdateOrder(orderId,updateOrderRequests);        
        // Then
        Assert.True(response.Status);




    }





}
