using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Stack247.WebScrapper.Contracts;

namespace Stack247.WebScrapper.Scrapper
{
    // Strategy concrete class
    public class JsonScrap<T> : BaseScrap<JsonTarget<T>>
    {
        public override ICollection<Response<JsonTarget<T>>> Process(string htmlDom)
        {
            var _return = new List<Response<JsonTarget<T>>>();

            foreach (var _request in Requests)
                _return.Add(Process(_request, htmlDom));

            return _return;
        }

        // TODO: 7.29.2014 - Remove dependency on Log class.
        // TODO: 4.10.2015 - Remove dependency on Http helper class.
        private Response<JsonTarget<T>> Process(Request<JsonTarget<T>> request, string htmlDom)
        {
            var _return = new Response<JsonTarget<T>>();

            if (Verbose)
            {
                Console.WriteLine("Start Scrapping Process");
                Console.WriteLine("Settings:");
                Console.WriteLine(string.Empty.PadRight(4) + "Type: Json");
                Console.WriteLine(string.Empty.PadRight(4) + "Verbose: " + Verbose);
                Console.WriteLine(string.Empty.PadRight(4) + "Requests Count: " + Requests.Count);
            }

            //Log.Debug("Start Scrapping Process");
            //Log.Debug("Settings:");
            //Log.Debug(string.Empty.PadRight(4) + "Type: Json");
            //Log.Debug(string.Empty.PadRight(4) + "Verbose: " + Verbose);
            //Log.Debug(string.Empty.PadRight(4) + "Requests Count: " + Requests.Count);

            // Check url exist
            if (!string.IsNullOrWhiteSpace(request.Url))
            {
                if (Verbose)
                    Console.WriteLine("Processing (RequestId:" + request.Id + " Url:" + request.Url + ")");
                //Log.Debug("Processing (RequestId:" + request.Id + " Url:" + request.Url + ")");

                try
                {
                    #region Process Response

                    foreach (var _target in request.Targets)
                    {
                        // Loop through all requested scrap in a target URL. One Url can have multiple Targets.
                        _target.Result = new JavaScriptSerializer().Deserialize<List<T>>(htmlDom);
                    }

                    _return.Targets = request.Targets;

                    #endregion
                }
                catch (Exception exception)
                {
                    if (Verbose)
                        Console.WriteLine("Error - " + exception.Message);
                    //Log.Error("Error - " + exception.Message);
                }
            }

            if (Verbose)
            {
                Console.WriteLine();
                Console.WriteLine("Completed Scrapping All Requests");
            }
            //Log.Debug("Completed Scrapping All Requests");

            return _return;
        }
    }
}