using Stack247.WebScrapper.Constants;
using Stack247.WebScrapper.Contracts;
using System.Collections.Generic;

namespace Stack247.WebScrapper.Contracts
{
    public class Request<T> where T : ITarget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public RequestMethod Method { get; set; }
        public string Body { get; set; }
        public RequestType Type { get; set; }
        public IList<T> Targets { get; set; }
    }
}
