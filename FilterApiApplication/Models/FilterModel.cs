using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

/// <summary>
/// This namespace includes models for filtering data in application.
/// </summary>
namespace FilterApiApplication.Models
{
    /// <summary>
    /// The main filter class.  
    /// </summary>
    [Produces("application/json")]
    public class FilterObjectModel
    {
        public string Success { get; set; }
        public string ErrorMessage { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<string> AllSizes { get; set; }
        public List<string> MostCommonWords { get; set; }
        public List<Product> Products { get; set; }
    }
}