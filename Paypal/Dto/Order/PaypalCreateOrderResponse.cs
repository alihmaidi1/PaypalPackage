namespace Paypal.Dto.Order;
public class CreateOrderResponse
    {
        

        
        // Required: The unique identifier for the order.
        public string id { get; set; }

        // Required: The status of the order, which can be "CREATED", "APPROVED", etc.
        public string status { get; set; }

        // Required: The intent of the payment, such as "sale", "authorize", or "order".
        public string intent { get; set; }

        // Required: Contains details about the payer (e.g., payment method).
        public Payer payer { get; set; }

        // Required: The list of links available for the created order (approve, cancel, etc.).
        public List<Link> links { get; set; }

        // Optional: The application context with additional settings for customizing the PayPal checkout page.
        public ApplicationContext? application_context { get; set; }

        // Optional: Information about the transactions for the order.
        public List<Transaction>? transactions { get; set; }
    }

    public class Payer
    {
        // Required: The method used for payment (e.g., "paypal", "credit_card").
        public string payment_method { get; set; }

        // Optional: Contains payer information if the payment method is PayPal.
        public PayerInfo? payer_info { get; set; }
    }

    public class PayerInfo
    {
        // Optional: The payer's email address.
        public string? email { get; set; }

        // Optional: The payer's first name.
        public string? first_name { get; set; }

        // Optional: The payer's last name.
        public string? last_name { get; set; }

        // Optional: The payer's phone number.
        public string? phone { get; set; }
    }

    public class Link
    {
        // Required: The URL for the action, like "approve".
        public string href { get; set; }

        // Required: The relationship of the link (e.g., "approve", "self").
        public string rel { get; set; }

        // Required: The HTTP method for the link (e.g., "GET").
        public string method { get; set; }
    }

    public class ApplicationContext
    {
        // Optional: The brand name displayed on the PayPal checkout page.
        public string? brand_name { get; set; }

        // Optional: The landing page type for PayPal checkout (e.g., "Login", "Billing").
        public string? landing_page { get; set; }

        // Optional: The shipping preference during checkout.
        public string? shipping_preference { get; set; }

        // Optional: The user action (e.g., "PAY_NOW", "CONTINUE").
        public string? user_action { get; set; }

        // Optional: The country code for shipping (e.g., "US").
        public string? country_code { get; set; }
    }

    public class Transaction
    {
        // Required: A description of the transaction.
        public string description { get; set; }

        // Required: A unique invoice number for the transaction.
        public string invoice_number { get; set; }

        // Required: The amount of money for the transaction.
        public Amount amount { get; set; }

        // Optional: A custom field for additional data.
        public string? custom { get; set; }

        // Optional: A soft descriptor that appears on the credit card statement.
        public string? soft_descriptor { get; set; }

        // Optional: The list of items in the transaction.
        public ItemList? item_list { get; set; }
    }

    public class Amount
    {
        // Required: The currency used in the transaction (e.g., "USD").
        public string currency { get; set; }

        // Required: The total amount for the transaction.
        public string total { get; set; }

        // Optional: The shipping cost for the transaction.
        public string? shipping { get; set; }

        // Optional: The tax amount for the transaction.
        public string? tax { get; set; }
    }

    public class ItemList
    {
        // Optional: A list of items in the transaction.
        public List<Item>? items { get; set; }
    }

    public class Item
    {
        // Required: The name of the item.
        public string name { get; set; }

        // Required: The quantity of the item.
        public int quantity { get; set; }

        // Required: The price of the item per unit.
        public string price { get; set; }

        // Optional: The SKU of the item.
        public string? sku { get; set; }

        // Optional: The category of the item (e.g., "physical", "digital").
        public string? category { get; set; }
    }
