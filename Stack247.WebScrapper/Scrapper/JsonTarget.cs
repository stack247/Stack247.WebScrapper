using Stack247.WebScrapper.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Stack247.WebScrapper.Scrapper
{
    public class JsonTarget<T> : ITarget
    {
        // Scrap request with JsonTarget will always have one target.
        // This is because the response will return the final result and we don't need selector to get the values.

        public T Result { get; set; }
    }
}