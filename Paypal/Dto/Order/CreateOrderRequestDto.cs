using System;
using System.Collections.Generic;

namespace PayPalIntegration;

public class CreateOrderRequest
{
    /// <summary>
    /// Required: The intent of the payment (AUTHORIZE,CAPTURE) 
    /// </summary> 
    public required string  intent { get; set; }

    /// <summary>
    /// Required: An array of purchase units. Each purchase unit establishes a contract between a payer and the payee. Each purchase unit represents either a full or partial order that the payer intends to purchase from the payee.
    /// </summary>
    /// <value></value>
    public required List<Transaction> purchase_units { get; set; }

    /// <summary>
    /// The payment source definition.
    /// </summary>
    /// <value></value>
    public required PaymentSource payment_source{get;set;}

}

public class PaymentSource{

    /// <summary>
    /// Indicates that PayPal Wallet is the payment source. Main use of this selection is to provide additional instructions associated with this choice like vaulting.
    /// </summary>
    /// <value></value>
    public required Paypal  paypal{get;set;}

}

public class Paypal{

    /// <summary>
    /// Customizes the payer experience during the approval process for payment with PayPal.
    /// </summary>
    /// <value></value>
    public required ExperienceContext experience_context{get;set;}

    
}

public class ExperienceContext{


    /// <summary>
    /// The URL where the customer will be redirected upon cancelling the payment approval.
    /// </summary>
    /// <value></value>
    public required string cancel_url{get;set;}


    /// <summary>
    /// The URL where the customer will be redirected upon approving a payment.
    /// </summary>
    /// <value></value>
    public required string return_url{get;set;}

    /// <summary>
    /// Configures a Continue or Pay Now checkout flow.
    /// </summary>
    /// <remarks>
    /// The possible values are:
    /// <list type="bullet">
    /// <item>CONTINUE</item>
    /// <item>PAY_NOW</item>
    /// </list>
    /// </remarks>
    public string? user_action{get;set;}

    /// <summary>
    /// The type of landing page to show on the PayPal site for customer checkout.
    /// </summary>
    /// <value></value>
    public string? landing_page{get;set;}

    /// <summary>
    /// The merchant-preferred payment methods.
    /// </summary>
    /// <remarks>
    /// The possible values are:
    /// <list type="bullet">
    /// <item>IMMEDIATE_PAYMENT_REQUIRED</item>
    /// <item>UNRESTRICTED</item>
    /// </list>
    /// </remarks>
    public string? payment_method_preference{get;set;}    
    /// <summary>
    /// The location from which the shipping address is derived.
    /// </summary>
    /// <value></value>
    public string? shipping_preference{get;set;}

}





public class Transaction
{


    /// <summary>
    /// Optional: The API caller-provided external ID for the purchase unit. Required for multiple purchase units when you must update the order through PATCH. If you omit this value and the order contains only one purchase unit, PayPal sets this value to default. 
    /// </summary>
    /// <value></value>

    public string? reference_id{get;set;}

    /// <summary>
    /// The purchase description. The maximum length of the character is dependent on the type of characters used. The character length is specified assuming a US ASCII character. Depending on type of character; (e.g. accented character, Japanese characters) the number of characters that that can be specified as input might not equal the permissible max length.
    /// </summary>
    /// <value></value>

    public string? description { get; set; }

    /// <summary>
    /// The API caller-provided external ID. Used to reconcile client transactions with PayPal transactions. Appears in transaction and settlement reports but is not visible to the payer.
    /// </summary>
    /// <value></value>

    public string? custom_id { get; set; }

    /// <summary>
    /// The API caller-provided external invoice number for this order. Appears in both the payer's transaction history and the emails that the payer receives.
    /// </summary>
    /// <value></value>    

    public string? invoice_id { get; set; }

    /// <summary>
    /// The soft descriptor is the dynamic text used to construct the statement descriptor that appears on a payer's card statement.
    /// If an Order is paid using the "PayPal Wallet", the statement descriptor will appear in following format on the payer's card statement: PAYPAL_prefix+(space)+merchant_descriptor+(space)+ soft_descriptor
    /// Note: The merchant descriptor is the descriptor of the merchant’s payment receiving preferences which can be seen by logging into the merchant account https://www.sandbox.paypal.com/businessprofile/settings/info/edit
    /// The PAYPAL prefix uses 8 characters. Only the first 22 characters will be displayed in the statement.
    /// For example, if:
    /// The PayPal prefix toggle is PAYPAL *.
    /// The merchant descriptor in the profile is Janes Gift.
    /// The soft descriptor is 800-123-1234.
    /// Then, the statement descriptor on the card is PAYPAL * Janes Gift 80.    
    /// </summary>
    /// <value></value>
    public string? soft_descriptor { get; set; }

    /// <summary>
    /// An array of items that the customer purchases from the merchant.
    /// </summary>
    /// <value></value>

    public List<Item>? items {get;set;}

    /// <summary>
    /// The total order amount with an optional breakdown that provides details, such as the total item amount, total tax amount, shipping, handling, insurance, and discounts, if any.
    ///If you specify amount.breakdown, the amount equals item_total plus tax_total plus shipping plus handling plus insurance minus shipping_discount minus discount.
    ///The amount must be a positive number. The amount.value field supports up to 15 digits preceding the decimal. For a list of supported currencies, decimal precision, and maximum charge amount, see the PayPal REST APIs Currency Codes.
    /// </summary>
    /// <value></value>

    public required Amount amount {get;set;}

    /// <summary>
    /// The merchant who receives payment for this transaction.
    /// </summary>
    /// <value></value>

    public Payee? payee{get;set;}


    /// <summary>
    /// The name and address of the person to whom to ship the items.
    /// </summary>
    /// <value></value>
    public  Shipping shipping{get;set;}

}


public class Shipping{


    /// <summary>
    /// A classification for the method of purchase fulfillment (e.g shipping, in-store pickup, etc). Either type or options may be present, but not both.
    /// </summary>
    /// <remarks>
    /// The possible values are:
    /// <list type="bullet">
    /// <item>SHIPPING</item>
    /// <item>PICKUP_IN_PERSON</item>
    /// <item>PICKUP_IN_STORE</item>
    /// <item>PICKUP_FROM_PERSON</item>    
    /// </list>
    /// </remarks>
    public string? type{get;set;}

    /// <summary>
    /// The email address of the recipient of the shipped items, which may belong to either the payer, or an alternate contact, for delivery.
    /// </summary>
    /// <value></value>
    public string? email_address{get;set;}
    

    /// <summary>
    /// The phone number of the recipient of the shipped items, which may belong to either the payer, or an alternate contact, for delivery. [Format - canonical international E.164 numbering plan]
    /// </summary>
    /// <value></value>
    public FullPhone? phone_number{get;set;}

    /// <summary>
    /// The address of the person to whom to ship the items. Supports only the address_line_1, address_line_2, admin_area_1, admin_area_2, postal_code, and country_code properties.
    /// </summary>
    /// <value></value>
    public Address? address{get;set;}

    /// <summary>
    /// The name of the person to whom to ship the items. Supports only the full_name property.
    /// </summary>
    /// <value></value>
    public Name? name{get;set;}

}

public class Name{

    /// <summary>
    /// When the party is a person, the party's full name.
    /// </summary>
    /// <value></value>
    public string full_name{get;set;}
}

public class Address{


    /// <summary>
    /// The first line of the address, such as number and street, for example, 173 Drury Lane. Needed for data entry, and Compliance and Risk checks. This field needs to pass the full address.
    /// </summary>
    /// <value></value>
    public string? address_line_1{get;set;}


    /// <summary>
    /// The second line of the address, for example, a suite or apartment number.
    /// </summary>
    /// <value></value>
    public string? address_line_2{get;set;}

    /// <summary>
    /// A city, town, or village. Smaller than admin_area_level_1.
    /// </summary>
    /// <value></value>
    public string? admin_area_2{get;set;}

    /// <summary>
    /// The highest-level sub-division in a country, which is usually a province, state, or ISO-3166-2 subdivision. This data is formatted for postal delivery, for example, CA and not California. Value, by country, is:
    /// UK. A county.
    /// US. A state.
    /// Canada. A province.
    /// Japan. A prefecture.
    /// Switzerland. A kanton.
    /// </summary>

    public string? admin_area_1{get;set;}


    /// <summary>
    /// The postal code, which is the ZIP code or equivalent. Typically required for countries with a postal code or an equivalent. See postal code.
    /// </summary>
    /// <value></value>

    public string? postal_code {get;set;}


    /// <summary>
    /// The 2-character ISO 3166-1 code that identifies the country or region.
    /// </summary>
    /// <value></value>

    public required string country_code{get;set;}

}

public class FullPhone{

    /// <summary>
    /// The country calling code (CC), in its canonical international E.164 numbering plan format. The combined length of the CC and the national number must not be greater than 15 digits. The national number consists of a national destination code (NDC) and subscriber number (SN).
    /// </summary>
    /// <value></value>
    public required string country_code{get;set;}

    /// <summary>
    /// The national number, in its canonical international E.164 numbering plan format. The combined length of the country calling code (CC) and the national number must not be greater than 15 digits. The national number consists of a national destination code (NDC) and subscriber number (SN).
    /// </summary>
    /// <value></value>

    public required string national_number{get;set;}


}

public class Amount
{


    /// <summary>
    /// The currency code for the transaction (e.g., "USD")
    /// </summary>
    /// <value></value>
    public required string currency_code { get; set; }

    /// <summary>
    /// The total amount of the transaction (e.g., "10.00").
    /// </summary>
    /// <value></value>

    public required double value {get;set;}


    public Breakdown? breakdown{get;set;}

    
    
}

public class Breakdown{


    public Money? item_total{get;set;}

    public Money? shipping{get;set;}

    public Money? handling{get;set;}

    public Money? tax_total{get;set;}

    public Money? insurance{get;set;}

    public Money? shipping_discount{get;set;}

    public Money? discount {get;set;}    


}

public class Money{


    public required double  value {get;set;}

    public required string currency_code{get;set;}

}

public class Item
{

    /// <summary>
    /// Required: The item name or title.
    /// </summary>
    /// <value></value>

    public required string name { get; set; }

    /// <summary>
    /// The item quantity. Must be a whole number.
    /// </summary>
    /// <value></value>
    public required int quantity { get; set; }


    /// <summary>
    /// Optional: The detailed item description.
    /// </summary>
    /// <value></value>
    public string? description { get; set; }

    /// <summary>
    /// The stock keeping unit (SKU) for the item.
    /// </summary>
    /// <value></value>

    public string? sku { get; set; }

    /// <summary>
    /// The URL to the item being purchased. Visible to buyer and used in buyer experiences.
    /// </summary>
    /// <value></value>

    public string? url{get;set;}

    /// <summary>
    /// Optional: The category of the item. 
    /// </summary>
    /// <remarks>
    /// The possible values are:
    /// <list type="bullet">
    /// <item>DIGITAL_GOODS</item>
    /// <item>PHYSICAL_GOODS</item>
    /// <item>DONATION</item>
    /// </list>
    /// </remarks>
    
    public string? category { get; set; }

    /// <summary>
    /// The URL of the item's image. File type and size restrictions apply. An image that violates these restrictions will not be honored.
    /// </summary>
    
    public string? image_url{get;set;}

    /// <summary>
    /// The item price or rate per unit. If you specify unit_amount, purchase_units[].amount.breakdown.item_total is required. Must equal unit_amount * quantity for all items. unit_amount.value can not be a negative number.
    /// </summary>
    /// <value></value>
    public required Money unit_amount{get;set;}

    /// <summary>
    /// The item tax for each unit. If tax is specified, purchase_units[].amount.breakdown.tax_total is required. Must equal tax * quantity for all items. tax.value can not be a negative number.
    /// </summary>
    /// <value></value>
    public Money tax{get;set;}
    /// <summary>
    /// The Universal Product Code of the item.
    /// </summary>
    /// <value></value>

    public UPC? upc{get;set;}
}




public class UPC{


    /// <summary>
    /// The Universal Product Code type.
    /// </summary>
    /// <value></value>
    public string type{get;set;}

    /// <summary>
    /// The UPC product code of the item.

    /// </summary>
    /// <value></value>
    public string code{get;set;}

}


public class Payee{

    /// <summary>
    /// The email address of merchant.
    /// </summary>
    /// <value></value>
    public string? email_address{get;set;}


    /// <summary>
    /// The encrypted PayPal account ID of the merchant.
    /// </summary>
    /// <value></value>
    public string? merchant_id{get;set;}

}
// public class RedirectUrls
// {
//     // Required: The URL to redirect to after the payment is approved.
//     public string return_url { get; set; }

//     // Required: The URL to redirect to if the payment is canceled.
//     public string cancel_url { get; set; }
// }


