using RestSharp;
using Newtonsoft.Json;
using FilterApiApplication.Models;
using System.Collections.Generic;

namespace FilterApiApplication.Code
{
    /// <summary>
    /// This class contains methods for retrieving product data from API.
    /// </summary>
    public class Initializer
    {
        /// <summary>
        /// This method retrieve initial list of data from API.
        /// </summary>
        /// <returns>Returns a list of class Product</returns>
        public static List<Product> GetInitialDataFromApi()
        {
            var products = new List<Product>();
            var client = new RestClient("http://www.mocky.io/v2/5e307edf3200005d00858b49"); //initial API used for retrieving data
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<JsonApi>(response.Content);
            if (result != null) products = result.Products;

            return products;
        }
    }
}
