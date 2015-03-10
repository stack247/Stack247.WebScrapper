using Stack247.WebScrapper.Contracts;
using System.Collections.Generic;
using System.Net;

namespace Stack247.WebScrapper.Scrapper
{
    // Strategy context class
    public class Scrapper<S, T>
        where S : BaseScrap<T>, new()
        where T : ITarget
    {
        // TODO: 7.29.2014 - Complete the following:
        // - Activate cache
        // - Add IsNearLimit check option (configurable by invoker to whether to check near-limit from response or somewhere else).

        public S Scrap { get; set; }
        //public ICacheRepository Cache { get; set; }
        //public bool ClearCacheWhenFinish { get; set; }

        public Scrapper(ICollection<Request<T>> requests = null,
            Dictionary<HttpRequestHeader, string> headers = null,
            bool verbose = false)
        {
            Scrap = new S
            {
                Requests = requests,
                Headers = headers,
                Verbose = verbose
            };
            //Cache = cache;
            //ClearCacheWhenFinish = clearCacheWhenFinish;
        }

        public ICollection<Response<T>> Process()
        {
            return Scrap.Requests != null ? Scrap.Process() : null;
        }
    }

}
