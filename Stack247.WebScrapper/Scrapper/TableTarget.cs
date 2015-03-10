using Stack247.WebScrapper.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Stack247.WebScrapper.Scrapper
{
    public class TableTarget : ITarget
    {
        public string TableSelector { get; set; }
        public string RowsSelector { get; set; }
        public ICollection<Row> Rows { get; set; }
        public ICollection<Column> Schema { get; set; }

        public TableTarget()
        {
            Rows = new Collection<Row>();
        }

        public class Row
        {
            public string Value { get; set; }
            public ICollection<Column> Columns { get; set; }

            public Row()
            {
                Columns = new Collection<Column>();
            }
        }
        
        public class Column
        {
            public string Name { get; set; }
            public string Selector { get; set; }
            public string GetValueMethod { get; set; }
            public string Value { get; set; }
        }
    }
}