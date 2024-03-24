using System.Text.Json.Serialization;

namespace Api.Model
{
    public class ProductViewModel
    { 
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal QuantityOnHand { get; set; }
        public decimal Value { get; set; }
     
    }
}
