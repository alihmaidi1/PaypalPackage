using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paypal.Authentication;
using Paypal.Tests.Constant;

namespace Paypal.Tests.Authentication;

public class PaypalAuthenticationTests
{

    [Fact]
    public async Task Try_Get_Success_TokenInfo()
    {
        // Given
        PaypalAuthentication authentication=new PaypalAuthentication(Credential.ClientId,Credential.ClientSecret,Enum.PaypalApplicationMode.Sandbox);

        // When
        var response=await authentication.GetAccessTokenInfo();
        
        // Then
        Assert.True(response.Status);
    }
    

    [Fact]
    public async Task Try_Get_Fails_TokenInfo()
    {
        // Given
        PaypalAuthentication authentication=new PaypalAuthentication(Credential.ClientId+"s",Credential.ClientSecret,Enum.PaypalApplicationMode.Sandbox);

        // When
        var response=await authentication.GetAccessTokenInfo();
        
        // Then
        Assert.False(response.Status);
    }
}
