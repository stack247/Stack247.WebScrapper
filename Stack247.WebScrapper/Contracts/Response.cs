using System.Collections.Generic;

namespace Stack247.WebScrapper.Contracts
{
    public class Response<T> where T : ITarget
    {
        public ICollection<T> Targets { get; set; }
    }
}