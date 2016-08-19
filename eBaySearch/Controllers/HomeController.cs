using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ServiceModel;

//eBay service reference
using eBaySearch.Models;
using eBaySearch.Finding;
using eBaySearch.Services.Common;
using eBaySearch.Services;




namespace eBaySearch.Controllers
{
    public class HomeController : Controller
    {

        // eBay ID's
        public static string appID = "";
        public static string serverAddress = "";
        public ClientConfig config = new ClientConfig(appID, serverAddress);


        [HttpPost]
        public ActionResult Index(string Id)
        {
            // create a eBay client
            config.GlobalId = "EBAY-IE"; //use Irish eBay website (www.eBay.ie)
            FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);

            ViewBag.Message = Id;

            SearchItem[] items = null;
            try
            {
                // Create request object
                FindItemsAdvancedRequest request = new FindItemsAdvancedRequest();

                //filter items so just "buy it now" items appear
                ItemFilter itemFilter1 = new ItemFilter();
                itemFilter1.name = ItemFilterType.ListingType;
                itemFilter1.value = new string[] { "FixedPrice" };

                ItemFilter[] itemFilterArray = new ItemFilter[1];
                itemFilterArray[0] = itemFilter1;

                // Set request parameters
                request.keywords = Id;
                request.itemFilter = itemFilterArray;
                request.sortOrder = SortOrderType.PricePlusShippingLowest;
                //request.sortOrderSpecified = true;
                OutputSelectorType[] outputs = { OutputSelectorType.SellerInfo };
                request.outputSelector = outputs;


                PaginationInput PaginationInput = new PaginationInput();
                //PaginationInput.totalNumberOfPages = 5;
                PaginationInput.entriesPerPageSpecified = true;
                PaginationInput.entriesPerPage = 100;
                PaginationInput.pageNumberSpecified = true;
                PaginationInput.pageNumber = 10;
                request.paginationInput = PaginationInput;

                // Call the api
                FindItemsAdvancedResponse response = client.findItemsAdvanced(request);

                // Show output
                if (response.searchResult != null && response.searchResult.item != null)
                {
                    items = response.searchResult.item;
                    ViewBag.noResultsMsg = "";
                    return View(items);
                }
                else if (Id.Length == 0)
                {
                    ViewBag.noResultsMsg = "No item entered. Please enter an item to search and try again";
                }
                else if (response.searchResult == null || response.searchResult.item == null)
                {
                    ViewBag.noResultsMsg = "No Results. Please try another search";
                }


            }
            catch (Exception ex)
            {
                var errorText = ex.Message;
            }

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users(string Id)
        {
            FindItemsByKeywordsRequest req = new FindItemsByKeywordsRequest();
            FindItemsByKeywordsResponse resp = new FindItemsByKeywordsResponse();

            return View();
        }


    }
}
