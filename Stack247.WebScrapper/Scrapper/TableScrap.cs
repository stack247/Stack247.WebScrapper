using System;
using System.Collections.Generic;
using System.Linq;
using Stack247.WebScrapper.Helpers;
using Stack247.WebScrapper.Contracts;
using Stack247.WebScrapper.Constants;

namespace Stack247.WebScrapper.Scrapper
{
    // Strategy concrete class
    public class TableScrap : BaseScrap<TableTarget>
    {
        public override ICollection<Response<TableTarget>> Process(string htmlDom)
        {
            var _return = new List<Response<TableTarget>>();

            foreach (var _request in Requests)
                _return.Add(Process(_request, htmlDom));

            return _return;
        }

        // TODO: 7.29.2014 - Remove dependency on Log class.
        // TODO: 4.10.2015 - Remove dependency on Http helper class.
        private Response<TableTarget> Process(Request<TableTarget> request, string htmlDom)
        {
            var _return = new Response<TableTarget>();

            if (Verbose)
            {
                Console.WriteLine("Start Scrapping Process");
                Console.WriteLine("Settings:");
                Console.WriteLine(string.Empty.PadRight(4) + "Type: Table");
                Console.WriteLine(string.Empty.PadRight(4) + "Verbose: " + Verbose);
                Console.WriteLine(string.Empty.PadRight(4) + "Requests Count: " + Requests.Count);
            }

            //Log.Debug("Start Scrapping Process");
            //Log.Debug("Settings:");
            //Log.Debug(string.Empty.PadRight(4) + "Type: Table");
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

                    // Loop through all requested scrap in a target URL. One Url can have multiple Targets.
                    foreach (var _target in request.Targets)
                    {
                        // Get all rows of data in a table inside the target
                        var _rowsDom = Http.GetValuesFromDom(htmlDom, _target.RowsSelector, GetValueMethods.Html);

                        // Loop through all rows in a table
                        foreach (var _rowDom in _rowsDom)
                        {
                            var _row = new TableTarget.Row();

                            foreach (var _schema in _target.Schema)
                            {
                                // Parse out DOM to get value based on given selector
                                var _columnValue = Http.GetValueFromDom(_rowDom, _schema.Selector, _schema.GetValueMethod);

                                _row.Columns.Add(new TableTarget.Column
                                {
                                    Name = _schema.Name,
                                    GetValueMethod = _schema.GetValueMethod,
                                    Selector = _schema.Selector,
                                    Value = _columnValue
                                });
                            }

                            _target.Rows.Add(_row);
                        }
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