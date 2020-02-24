using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using AmazonService;
using System.Data;
using TempoServico;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            AmazonService.AWSECommerceServicePortTypeClient aws = new AWSECommerceServicePortTypeClient(AWSECommerceServicePortTypeClient.EndpointConfiguration.AWSECommerceServicePort);  


        }

        public IEnumerable<Item> GetProducts(string sona, int leht)
        {
            AWSECommerceServicePortTypeClient ecs = new AWSECommerceServicePortTypeClient();
            ItemSearchRequest paring = new ItemSearchRequest();
            paring.ResponseGroup = new string[] { "ItemAttributes,OfferSummary" };
            paring.SearchIndex = "All";
            paring.Keywords = sona;
            paring.ItemPage = String.Format("{0}", leht);
            paring.MinimumPrice = "0";

            ItemSearch otsi = new ItemSearch();
            otsi.Request = new ItemSearchRequest[] { paring };
            otsi.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            otsi.AssociateTag = ConfigurationManager.AppSettings["associateTag"];
            ItemSearchResponse vastus = ecs.ItemSearch(otsi);

            if (vastus == null)
            {
                throw new Exception("Server Error - didn't get any reponse from server!");
            }
            else if (vastus.OperationRequest.Errors != null)
            {
                throw new Exception(vastus.OperationRequest.Errors[0].Message);
            }
            else if (vastus.Items[0].Item == null)
            {
                throw new Exception("Didn't get any items!Try agen with different keyword.");
            }
            else
            {
                return vastus.Items[0].Item;
            }
        }

        public ItemSearchResponse ItemSearch(string SearchIndex, string[] Group, string Keywords)
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.MaxBufferSize = int.MaxValue;

            AWSECommerceServicePortTypeClient amazonClient = new AWSECommerceServicePortTypeClient(
                        binding,
                        new EndpointAddress("https://webservices.amazon.com/onca/soap?Service=AWSECommerceService"));
            // add authentication to the ECS client
            amazonClient.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(ConfigurationManager.AppSettings["accessKeyId"], ConfigurationManager.AppSettings["secretKey"]));

            ItemSearchRequest search = new ItemSearchRequest();
            search.SearchIndex = SearchIndex;
            search.ResponseGroup = Group;
            search.Keywords = Keywords;

            ItemSearch itemSearch = new ItemSearch();
            itemSearch.Request = new ItemSearchRequest[] { search };
            itemSearch.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            itemSearch.AssociateTag = ConfigurationManager.AppSettings["associateTag"];

            ItemSearchResponse response = amazonClient.ItemSearch(itemSearch);
            return response;
        }

        public string ReviewsFrame()
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.MaxBufferSize = int.MaxValue;

            AWSECommerceServicePortTypeClient amazonClient = new AWSECommerceServicePortTypeClient(
                        binding,
                        new EndpointAddress("https://webservices.amazon.com/onca/soap?Service=AWSECommerceService"));
            // add authentication to the ECS client
            amazonClient.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(ConfigurationManager.AppSettings["accessKeyId"], ConfigurationManager.AppSettings["secretKey"]));

            ItemLookupRequest request = new ItemLookupRequest();
            request.ItemId = this.ItemIds;
            request.IdType = ItemLookupRequestIdType.ASIN;
            request.ResponseGroup = new string[] { "Reviews" };

            ItemLookup itemLookup = new ItemLookup();
            itemLookup.Request = new ItemLookupRequest[] { request };
            itemLookup.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
            itemLookup.AssociateTag = ConfigurationManager.AppSettings["associateTag"];

            ItemLookupResponse response = amazonClient.ItemLookup(itemLookup);
            string frameUrl = response.Items[0].Item[0].CustomerReviews.IFrameURL;
            return frameUrl;
        }

        public string getCurrentPrice(string itemID)
        {
            const string accessKeyId = "AKIAINHOZEYXDKHXMYUQ";
            const string secretKey = "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";

            // create a WCF Amazon ECS client
            AWSECommerceServicePortTypeClient client = new AWSECommerceServicePortTypeClient(
                new BasicHttpBinding(BasicHttpSecurityMode.Transport),
                new EndpointAddress("https://webservices.amazon.com/onca/soap?Service=AWSECommerceService"));

            // add authentication to the ECS client
            client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(accessKeyId, secretKey));

            // prepare an ItemSearch request
            ItemLookupRequest request = new ItemLookupRequest();
            request.ItemId = new string[] { itemID };
            request.ResponseGroup = new string[] { "OfferSummary" };
            request.MerchantId = "Amazon";

            ItemLookup itemSearch = new ItemLookup();
            itemSearch.Request = new ItemLookupRequest[] { request };
            itemSearch.AWSAccessKeyId = accessKeyId;
            itemSearch.AssociateTag = "rgreuel-20";

            // issue the ItemSearch request
            ItemLookupResponse response = client.ItemLookup(itemSearch);

            // write out the results
            Item returnedItem = response.Items[0].Item.First();

            return (returnedItem.OfferSummary.LowestNewPrice.FormattedPrice).Substring(1, returnedItem.OfferSummary.LowestNewPrice.FormattedPrice.Length - 1);
        }

        public static void AmazonWCF()
        {
            // create a WCF Amazon ECS client
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;
            AWSECommerceServicePortTypeClient client = new AWSECommerceServicePortTypeClient(
                binding,
                new EndpointAddress("https://webservices.amazon.com/onca/soap?Service=AWSECommerceService"));

            // add authentication to the ECS client
            client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(accessKeyId, secretKey));

            // prepare an ItemSearch request
            ItemSearchRequest request = new ItemSearchRequest();
            request.SearchIndex = "Electronics";
            request.Title = "Monitor";
            request.ResponseGroup = new string[] { "Small" };

            ItemSearch itemSearch = new ItemSearch();
            itemSearch.Request = new ItemSearchRequest[] { request };
            itemSearch.AWSAccessKeyId = accessKeyId;
            //itemSearch.AssociateTag = tab
            // issue the ItemSearch request
            ItemSearchResponse response = client.ItemSearch(itemSearch);

            // write out the results
            foreach (var item in response.Items[0].Item)
            {
                Console.WriteLine(item.ItemAttributes.Title);
            }
        }
        public AmazonService()
        {
          var  binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;

          var  client = new AWSECommerceServicePortTypeClient(binding,
                                new EndpointAddress("https://webservices.amazon.com/onca/soap?Service=AWSECommerceService"));

            client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(Settings.AMAZON_ACCESS_KEY_ID, Settings.AMAZON_SECRET_KEY));
        }

        public DataTable GetProducts(string productName)
        {
            DataTable products = new DataTable();
            products.Columns.Add("Product", typeof(string));
            products.Columns.Add("Price", typeof(string));
            products.Columns.Add("Image", typeof(string));
            products.Columns.Add("url", typeof(string));

            // Instantiate Amazon ProductAdvertisingAPI client
            AWSECommerceServicePortTypeClient amazonClient = new AWSECommerceServicePortTypeClient();
            for (int i = 1; i <= 5; i++)
            {

                // prepare an ItemSearch request
                ItemSearchRequest request = new ItemSearchRequest();
                request.SearchIndex = "All";
                request.Keywords = productName + " -kindle";
                request.ResponseGroup = new string[] { "ItemAttributes", "Images" };
                request.ItemPage = i.ToString();
                request.Condition = Condition.New;
                request.Availability = ItemSearchRequestAvailability.Available;

                ItemSearch itemSearch = new ItemSearch();
                itemSearch.Request = new ItemSearchRequest[] { request };
                itemSearch.AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"];
                itemSearch.AssociateTag = "testfo-20";

                // send the ItemSearch request
                ItemSearchResponse response = amazonClient.ItemSearch(itemSearch);

                foreach (var item in response.Items[0].Item)
                {
                    try
                    {

                        products.Rows.Add(item.ItemAttributes.Title, item.ItemAttributes.ListPrice.FormattedPrice.Replace("$", ""), item.SmallImage.URL, item.DetailPageURL);

                    }

                    catch (NullReferenceException ex)
                    {
                        Debug.WriteLine("Caught Exception: " + ex.Message);
                        continue;
                    }

                }

            }
            return products;
        }

        public string getCurrentPrice(string itemID)
        {
            const string accessKeyId = "AKIAINHOZEYXDKHXMYUQ";
            const string secretKey = "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";

            // create a WCF Amazon ECS client
            AWSECommerceServicePortTypeClient client = new AWSECommerceServicePortTypeClient(
                new BasicHttpBinding(BasicHttpSecurityMode.Transport),
                new EndpointAddress("https://webservices.amazon.com/onca/soap?Service=AWSECommerceService"));

            // add authentication to the ECS client
            client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(accessKeyId, secretKey));

            // prepare an ItemSearch request
            ItemLookupRequest request = new ItemLookupRequest();
            request.ItemId = new string[] { itemID };
            request.ResponseGroup = new string[] { "OfferSummary" };
            request.MerchantId = "Amazon";

            ItemLookup itemSearch = new ItemLookup();
            itemSearch.Request = new ItemLookupRequest[] { request };
            itemSearch.AWSAccessKeyId = accessKeyId;
            itemSearch.AssociateTag = "rgreuel-20";

            // issue the ItemSearch request
            ItemLookupResponse response = client.ItemLookup(itemSearch);

            // write out the results
            Item returnedItem = response.Items[0].Item.First();

            return (returnedItem.OfferSummary.LowestNewPrice.FormattedPrice).Substring(1, returnedItem.OfferSummary.LowestNewPrice.FormattedPrice.Length - 1);
        }

        void teste()
        {
            TempConverterEndpointClient cliente = new TempConverterEndpointClient();

            var username = cliente.ClientCredentials.UserName;

            TempoServico.celsiusToFahrenheitRequest  req = new TempoServico.celsiusToFahrenheitRequest ();
            CelsiusToFahrenheit ctof = new CelsiusToFahrenheit();
            req.TemperatureInCelsius = 25;
            celsiusToFahrenheitResponse response = new celsiusToFahrenheitResponse();
            //response = cliente.CelsiusToFahrenheitAsync(ctof);

        }
    }
}
