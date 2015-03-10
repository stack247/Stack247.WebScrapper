using Stack247.WebScrapper.Contracts;
using System;
using System.Collections.Generic;
using System.Net;

namespace Stack247.WebScrapper.Scrapper
{
    public abstract class BaseScrap<T> : IScrap<T> where T : ITarget
    {
        public ICollection<Request<T>> Requests;
        public Dictionary<HttpRequestHeader, string> Headers { get; set; }
        public bool Verbose { get; set; }

        public virtual ICollection<Response<T>> Process()
        {
            throw new NotImplementedException();
        }
    }
}