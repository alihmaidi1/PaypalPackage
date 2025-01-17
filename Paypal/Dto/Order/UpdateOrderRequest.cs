using Paypal.Enum;

namespace Paypal.Dto.Order;

public class UpdateOrderRequest<T>
{



    public UpdateOrderOperation op{get;set;}

    public string path{get;set;}


    public T value{get;set;}

    
}
