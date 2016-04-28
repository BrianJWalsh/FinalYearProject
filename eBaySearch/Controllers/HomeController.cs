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

//Amazon service reference
using eBaySearch.AmazonItemSearch;
//using eBaySearch.AmazonItemSearch.ItemSearch;



namespace eBaySearch.Controllers
{
    public class HomeController : Controller
    {

        // eBay ID's
        public static string appID = "BrianWal-f6d8-43f7-80f0-854c2a33aded";
        public static string findingServerAddress = "http://svcs.ebay.com/services/search/FindingService/v1?&sortOrder=PriceShippingLowest";
        public ClientConfig config = new ClientConfig(appID, findingServerAddress);

        // Amazon ID's

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
                //request.categoryId = new string[] {"#11450"};


                PaginationInput PaginationInput = new PaginationInput();
                //PaginationInput.totalNumberOfPages = 5;
                PaginationInput.entriesPerPageSpecified = true;
                PaginationInput.entriesPerPage = 100;
                PaginationInput.pageNumberSpecified = true;
                PaginationInput.pageNumber = 10;
                request.paginationInput = PaginationInput;

                // Call the api
                FindItemsAdvancedResponse response = client.findItemsAdvanced(request);

                using (SqlConnection oConn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    string insertSearch = "INSERT into searches (SearchTest, NoOfTimesSearched) VALUES (@SearchTest,@NoOfTimesSearched)";

                    using (SqlCommand queryinsertSearch = new SqlCommand(insertSearch))
                    {
                        queryinsertSearch.Connection = oConn;
                        queryinsertSearch.Parameters.Add("@SearchTest", SqlDbType.VarChar, 30).Value = Id;
                        queryinsertSearch.Parameters.Add("@NoOfTimesSearched", SqlDbType.Int, 30).Value = 1;
                        oConn.Open();
                        oConn.Close();
                    }
                }

                // Show output
                if (response.searchResult != null && response.searchResult.item != null)
                {
                    items = response.searchResult.item;
                    ViewBag.noResultsMsg = "";
                    return View(items);
                }
                else {
                    ViewBag.noResultsMsg = "No Results. Please try another search";
                }

                //
                // noResult.inn

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
            ViewBag.Message = "TShirt";
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
