using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBaySearch.Finding;
using System.Net;

namespace eBaySearch.Models
{
    public class CustomFindingService
    {
        //protected override WebRequest GetWebRequest(Uri uri)
        //{
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)base.(uri);
        //        request.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME", "BrianWal-f6d8-43f7-80f0-854c2a33aded");
        //        request.Headers.Add("X-EBAY-SOA-OPERATION-NAME", "findItemsByKeywords");
        //        request.Headers.Add("X-EBAY-SOA-SERVICE-NAME", "FindingService");
        //        request.Headers.Add("X-EBAY-SOA-MESSAGE-PROTOCOL", "SOAP11");
        //        request.Headers.Add("X-EBAY-SOA-SERVICE-VERSION", "1.0.0");
        //        request.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-IE");
        //        return request;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}