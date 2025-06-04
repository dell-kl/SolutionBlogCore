namespace BlogCore.Models.Dto
{
    public class OrdersDTO
    {
        
        public string intent { get; set; }
        public Payment_Source payment_source { get; set; }
        public Purchase_Units[] purchase_units { get; set; }
        

        public class Payment_Source
        {
            public Paypal paypal { get; set; }
        }

        public class Paypal
        {
            public Experience_Context experience_context { get; set; }
        }

        public class Experience_Context
        {
            public string payment_method_preference { get; set; }
            public string landing_page { get; set; }
            public string shipping_preference { get; set; }
            public string user_action { get; set; }
            public string return_url { get; set; }
            public string cancel_url { get; set; }
        }

        public class Purchase_Units
        {
            public string invoice_id { get; set; }
            public Amount amount { get; set; }
        }

        public class Amount
        {
            public string currency_code { get; set; }
            public string value { get; set; }
        }

    }
}
