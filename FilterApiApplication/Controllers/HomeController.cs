using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilterApiApplication.Models;
using FilterApiApplication.Code;
using Newtonsoft.Json;

namespace FilterApiApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Filter(decimal maxPrice = 0, string size = "", string highlight = "")
        {
            var viewModel = new FilterObjectModel();

            //get initial list
            var filterProducts = Initializer.GetInitialDataFromApi();

            //filter data
            if (!FilterLogic.CheckIfFilterListIsEmpty(filterProducts))
            {
                //filter list by max price
                if (maxPrice > 0) filterProducts = FilterLogic.FilterProductsByMaxPrice(filterProducts, maxPrice);

                //filter list by size
                if (!string.IsNullOrEmpty(size)) filterProducts = FilterLogic.FilterProductsBySize(filterProducts, size);

                //find all sizes 
                var allSIzes = FilterLogic.FindAllSizesFromProductList(filterProducts);

                //find common words
                var commonWords = FilterLogic.FindCommonWordsFromProductList(filterProducts);

                //highlight words in description
                if (!string.IsNullOrEmpty(highlight)) filterProducts = FilterLogic.HighlightWordsInDescription(filterProducts, highlight);

                viewModel = new FilterObjectModel
                {
                    Success = "True",
                    ErrorMessage = !FilterLogic.CheckIfFilterListIsEmpty(filterProducts) ? "" : "There are no results with this parameters!",
                    Products = filterProducts,
                    MinPrice = FilterLogic.FindMinPriceFromProductList(filterProducts),
                    MaxPrice = FilterLogic.FindMaxPriceFromProductList(filterProducts),
                    AllSizes = allSIzes,
                    MostCommonWords = commonWords
                };
            }
            else
            {
                viewModel = new FilterObjectModel
                {
                    Success = "False",
                    ErrorMessage = "Error while retrieving products from database"
                };
            }

            Response.ContentType = "application/json";
            return Content(JsonConvert.SerializeObject(viewModel));
        }
    }
}
