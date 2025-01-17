using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        string jsonRequest =JsonSerializer.Serialize(createOrderRequest,new JsonSerializerOptions{

            Converters={
                new JsonStringEnumConverter()
            }
        });
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(PaypalAuthenticationHelper.GetBaseUrl(mode)+PaypalEndPointConstant.Order, content);
        if(!response.IsSuccessStatusCode){

            return PaypalResponseResult.Failed<CreateOrderResponse?>(await response.Content.ReadAsStringAsync());
        }else{
            var jsonResponse = await response.Content.ReadAsStringAsync();
            CreateOrderResponse? data=JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse);
            return PaypalResponseResult.Success<CreateOrderResponse?>(data);

        }
    
    }


    public async Task<PaypalResponse<CreateOrderResponse?>> ShowOrder(string orderId){

        var tokenInfo=await new PaypalAuthentication(clientId,clientSecret,mode).GetAccessTokenInfo();
        if(!tokenInfo.Status){

            return PaypalResponseResult.Failed<CreateOrderResponse?>("Credential is not correct");
        }
        HttpRequestMessage request=new HttpRequestMessage{


            RequestUri=new Uri(PaypalAuthenticationHelper.GetBaseUrl(mode)+PaypalEndPointConstant.Order+orderId),
            Method=HttpMethod.Get,
            Headers={

                { "Authorization", $"Bearer {tokenInfo.Result?.access_token}" }
            },
            Content=new FormUrlEncodedContent(new List<KeyValuePair<string, string>>())


        };
        var httpResponse = await httpClient.SendAsync(request);
        if(httpResponse.IsSuccessStatusCode){

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();            
            
            CreateOrderResponse? response = JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse);            
            return PaypalResponseResult.Success(response);


        }else{

            var errorMessage = await httpResponse.Content.ReadAsStringAsync();
            return PaypalResponseResult.Failed<CreateOrderResponse?>(errorMessage);

        }



    }


    public async Task<PaypalResponse<Object>> UpdateOrder<T>(string orderId,List<UpdateOrderRequest<T>> updateRequest){

        
        var tokenInfo=await new PaypalAuthentication(clientId,clientSecret,mode).GetAccessTokenInfo();
        if(!tokenInfo.Status){

            return PaypalResponseResult.Failed<Object>("Credential is not correct");
        }
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenInfo.Result?.access_token}");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        string jsonRequest =JsonSerializer.Serialize(updateRequest,new JsonSerializerOptions{

            Converters={
                new JsonStringEnumConverter()
            }
        });

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PatchAsync(PaypalAuthenticationHelper.GetBaseUrl(mode)+PaypalEndPointConstant.Order+orderId, content);

        if(!response.IsSuccessStatusCode){

            return PaypalResponseResult.Failed<Object>(await response.Content.ReadAsStringAsync());
        }else{
            return PaypalResponseResult.Success<Object>();

        }


    }


    
}
