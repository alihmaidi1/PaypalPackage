using Paypal.Enum;
using Paypal.Order;
using Paypal.Tests.Constant;
using PayPalIntegration;

namespace Paypal.Tests.Order;

public class PaypalCreateOrderTests
{

    [Fact]
    public async Task CreatePayment_With_Minimal_Attribute_Test()
    {
        // Given
        PaypalOrder order=new PaypalOrder(Credential.ClientId,Credential.ClientSecret,Enum.PaypalApplicationMode.Sandbox);
         var request = new CreateOrderRequest
                {
                    intent = PaypalIntent.CAPTURE,
                    purchase_units=new List<Transaction>(){
                        
                        new Transaction(){

                            amount=new Amount(){

                                currency_code="USD",
                                value=23
                            }
                        }
                        
                    },
                    payment_source=new PaymentSource(){

                        paypal=new PayPalIntegration.Paypal(){

                            experience_context=new ExperienceContext(){

                                cancel_url="https://example.com/cancelUrl",
                                return_url="https://example.com/returnUrl"
                            }

                        }
                    }
                    
                };
                    
        // When
        var response=await order.CreatePayment(request);

        // Then
        if(!response.Status){

            Console.WriteLine(response.Message);
        }
        Assert.True(response.Status);
        
    }
    
    
}
