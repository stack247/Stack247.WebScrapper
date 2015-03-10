using Stack247.WebScrapper.Contracts;
namespace Stack247.WebScrapper.Scrapper
{
    public class ElementTarget : ITarget
    {
        public string Name { get; set; }
        public string Selector { get; set; }
        public string GetValueMethod { get; set; }
        public string Value { get; set; }
    }
}