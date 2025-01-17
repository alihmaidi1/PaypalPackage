using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using Paypal.Abstraction;
using Paypal.Constant;
using Paypal.Dto;
using Paypal.Dto.Authentication;
using Paypal.Enum;
using Paypal.Helpers;

namespace Paypal.Authentication;


public class PaypalAuthentication: HttpClientDisposeAbstraction
{

    private  string mode{get;set;}
    private  string clientId{get;set;}

    private string clientSecret{get;set;}

    // private  HttpClient httpClient{get;set;}


    public PaypalAuthentication(string clientId,string clientSecret,PaypalApplicationMode mode){

        this.clientId=clientId;
        this.httpClient=new HttpClient();
        this.clientSecret=clientSecret;
        this.mode=PaypalAuthenticationHelper.GetBaseUrl(mode);


    }

    public async Task<PaypalResponse<PaypalAccessTokenDto?>> GetAccessTokenInfo(){

        string HashedCredentials=PaypalAuthenticationHelper.GetAuthenticationBase64(this.clientId,this.clientSecret);
        var content = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "client_credentials")
        };
        HttpRequestMessage request=new HttpRequestMessage{


            RequestUri=new Uri(mode+PaypalEndPointConstant.GetTokenInfo),
            Method=HttpMethod.Post,
            Headers={

                { "Authorization", $"Basic {HashedCredentials}" }
            },
            Content=new FormUrlEncodedContent(content)

        };
        var httpResponse = await httpClient.SendAsync(request);
        if(httpResponse.IsSuccessStatusCode){

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();            
            
            PaypalAccessTokenDto? response = JsonSerializer.Deserialize<PaypalAccessTokenDto>(jsonResponse);            
            return PaypalResponseResult.Success(response);


        }else{

            var errorMessage = await httpResponse.Content.ReadAsStringAsync();
            return PaypalResponseResult.Failed<PaypalAccessTokenDto?>(errorMessage);

        }

        
    }
}
