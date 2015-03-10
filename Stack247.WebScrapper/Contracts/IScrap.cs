using System.Collections.Generic;

namespace Stack247.WebScrapper.Contracts
{
    // Strategy interface
    public interface IScrap<T> where T : ITarget
    {
        ICollection<Response<T>> Process();
    }
}
