
using System.Text;
using System.Text.Json;
using Paypal.Abstraction;
using Paypal.Authentication;
using Paypal.Constant;
using Paypal.Dto;
using Paypal.Dto.Order;
using Paypal.Enum;
using Paypal.Helpers;
using PayPalIntegration;

namespace Paypal.Order;

public  class PaypalOrder:HttpClientDisposeAbstraction
{

    private  PaypalApplicationMode mode{get;set;}
    private  string clientId{get;set;}

    private string clientSecret{get;set;}

    public PaypalOrder(string clientId,string clientSecret,PaypalApplicationMode mode){

        this.clientId=clientId;
        this.httpClient=new HttpClient();
        this.clientSecret=clientSecret;        
        this.mode=mode;


    }

    public async Task<PaypalResponse<CreateOrderResponse?>> CreatePayment(CreateOrderRequest createOrderRequest){

        var tokenInfo=await new PaypalAuthentication(clientId,clientSecret,mode).GetAccessTokenInfo();
        if(!tokenInfo.Status){

            return PaypalResponseResult.Failed<CreateOrderResponse?>("Credential is not correct");
        }

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenInfo.Result?.access_token}");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        string jsonRequest =JsonSerializer.Serialize(createOrderRequest);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(PaypalAuthenticationHelper.GetBaseUrl(mode)+PaypalEndPointConstant.CreateOrder, content);
        if(!response.IsSuccessStatusCode){

            return PaypalResponseResult.Failed<CreateOrderResponse?>(await response.Content.ReadAsStringAsync());
        }else{
            var jsonResponse = await response.Content.ReadAsStringAsync();
            CreateOrderResponse? data=JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse);
            Console.WriteLine(jsonResponse);
            return PaypalResponseResult.Success<CreateOrderResponse?>(data);

        }
    
    }

    
}
