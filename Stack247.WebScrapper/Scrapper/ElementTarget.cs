using Stack247.WebScrapper.Constants;
using Stack247.WebScrapper.Contracts;
namespace Stack247.WebScrapper.Scrapper
{
    public class ElementTarget : ITarget
    {
        public string Name { get; set; }
        public string Selector { get; set; }
        public GetValueMethods GetValueMethod { get; set; }
        public string Value { get; set; }
    }
}