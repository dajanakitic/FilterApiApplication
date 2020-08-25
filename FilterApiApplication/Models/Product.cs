using Newtonsoft.Json;
using System.Collections.Generic;

namespace FilterApiApplication.Models
{
    /// <summary>
    /// Class with parameters for product data. 
    /// </summary>
    public class Product
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("sizes")]
        public List<string> Sizes { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        public Product(string _title,decimal _price,List<string> _sizes,string _description)
        {
            Title = _title;
            Price = _price;
            Sizes = _sizes;
            Description = _description;

        }
    }

    /// <summary>
    /// Class with parameters for api keys from the API.  
    /// </summary>
    public class ApiKeys
    {
        [JsonProperty("primary")]
        public string Primary { get; set; }
        [JsonProperty("secondary")]
        public string Secondary { get; set; }
    }

    /// <summary>
    /// Class with complete inital data from API. 
    /// </summary>
    public class JsonApi
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; }
        [JsonProperty("apiKeys")]
        public ApiKeys ApiKeys { get; set; }
    }
}