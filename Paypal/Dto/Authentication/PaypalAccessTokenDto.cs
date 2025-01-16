namespace Paypal.Dto.Authentication;
public class PaypalAccessTokenDto
{

    public string scope {get;}

    public string accessToken{get;}

    public string tokenType{get;}

    public string appId{get;}
    
    public int expiresIn{get;}

    public string nonce{get;}


}
