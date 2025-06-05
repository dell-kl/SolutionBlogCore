namespace BlogCore.Models.Dto
{
    public class ResponseOrdesDTO
    {
        public string id { get; set; }
        public string status { get; set; }
        public Payment_Source payment_source { get; set; }
        public Link[] links { get; set; }


        public class Payment_Source
        {
            public Paypal paypal { get; set; }
        }
        
        public class Paypal
        {
        }

        public class Link
        {
            public string href { get; set; }
            public string rel { get; set; }
            public string method { get; set; }
        }

    }
}
