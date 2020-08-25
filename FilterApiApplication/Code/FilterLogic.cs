using System.Collections.Generic;
using System.Linq;
using FilterApiApplication.Models;

/// <summary>
/// This namespace includes classes and methods for filtering product data.
/// </summary>
namespace FilterApiApplication.Code
{
    /// <summary>
    /// This class contains methods for filtering product data.
    /// </summary>
    public class FilterLogic
    {
        /// <summary>
        /// This method checks if list of products is null or empty.
        /// </summary>
        /// <param name="filterProducts">Parameter with list of products</param>
        /// <returns>Returns true if list of products is null or empty.</returns>
        public static bool CheckIfFilterListIsEmpty(List<Product> filterProducts)
        {
            return (filterProducts != null && filterProducts.Count() > 0) ? false : true;
        }

        /// <summary>
        /// This method filters list of products by max price.
        /// </summary>
        /// <param name="filterProducts">Parameter with list of products</param>
        /// <param name="maxPrice">Parameter max price for filter</param>
        /// <returns>Returns filtered list of products</returns>
        public static List<Product> FilterProductsByMaxPrice(List<Product> filterProducts, decimal maxPrice)
        {
            return filterProducts.Where(x => x.Price <= maxPrice).ToList();
        }

        /// <summary>
        /// This method filters list of products by size.
        /// </summary>
        /// <param name="filterProducts">Parameter with list of products</param>
        /// <param name="size">Parameter size for filter</param>
        /// <returns>Returns filtered list of products</returns>
        public static List<Product> FilterProductsBySize(List<Product> filterProducts, string size)
        {          
            return filterProducts.Where(x => x.Sizes.Contains(size.ToLower())).ToList();
        }

        /// <summary>
        /// This method finds all sizes from the list of products.
        /// </summary>
        /// <param name="filterProducts">Parameter with list of products</param>
        /// <returns>Returns list of string with all sizes</returns>
        public static List<string> FindAllSizesFromProductList(List<Product> filterProducts)
        {
            var allSIzes = new List<string>();

            foreach (var product in filterProducts)
            {
                if (product.Sizes != null && product.Sizes.Count() > 0)
                    allSIzes.AddRange(product.Sizes);               
            }

            if (allSIzes != null && allSIzes.Count() > 0)
                allSIzes = allSIzes.Distinct().ToList();

            return allSIzes;
        }

        /// <summary>
        /// This method finds most common words from description.
        /// </summary>
        /// <param name="filterProducts">Parameter with list of products</param>
        /// <returns>Returns list of string with most common words</returns>
        public static List<string> FindCommonWordsFromProductList(List<Product> filterProducts)
        {
            var commonWords = new List<string>();

            foreach (var product in filterProducts)
            {
                //find common words
                if (!string.IsNullOrEmpty(product.Description))
                    commonWords.AddRange(product.Description.Replace(".", "").Split(' ').ToList());
            }

            if (commonWords != null && commonWords.Count() > 0)
            {
                //group common words
                //commonWords = commonWords.Distinct().ToList();

                var groupCommonWOrds = commonWords.GroupBy(x => x)
                        .OrderByDescending(group => group.Count());

                //take 10 most common words excluding first five
                commonWords = groupCommonWOrds.Select(x => x.Key).Skip(5).Take(10).ToList();
            }

            return commonWords;
        }

        /// <summary>
        /// This method highlights words in description.
        /// </summary>
        /// <param name="filterProducts">Parameter with list of products</param>
        /// <param name="highlight">Parameter with list of words to highlight in description. Can contain multiple words separated by commas</param>
        /// <returns>Returns list of produts with highlighted description</returns>
        public static List<Product> HighlightWordsInDescription(List<Product> filterProducts,string highlight)
        {
            //split highlight into list
            var highlightList = new List<string>();
            highlightList = highlight.Split(',').ToList();

            if (highlightList != null && highlightList.Count() > 0)
            {
                foreach (var product in filterProducts)
                {
                    //highlight words in description
                    foreach (var word in highlightList)
                        product.Description = product.Description.Replace(word, "<em>" + word + "</em>");
                }
            }
               
            return filterProducts;
        }

        /// <summary>
        /// This method finds min price from the list of products.
        /// </summary>
        /// <param name="filterProducts">Parameter with list of products</param>
        /// <returns>Returns decimal with min price or 0 if list of products is empty</returns>
        public static decimal FindMinPriceFromProductList(List<Product> filterProducts)
        {
            if(!CheckIfFilterListIsEmpty(filterProducts))
            {
                return filterProducts.Select(x => x.Price).ToList().Min(y => y);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// This method finds max price from the list of products.
        /// </summary>
        /// <param name="filterProducts">Parameter with list of products</param>
        /// <returns>Returns decimal with max price or 0 if list of products is empty</returns>
        public static decimal FindMaxPriceFromProductList(List<Product> filterProducts)
        {
            if (!CheckIfFilterListIsEmpty(filterProducts))
            {
                return filterProducts.Select(x => x.Price).ToList().Max(y => y);
            }
            else
            {
                return 0;
            }
        }       
    }
}
