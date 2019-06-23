using BP.Resource.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Resource
{
    public class ResourceDocument
    {
        public List<BookItem> bookItems = new List<BookItem>()
        {
            new BookItem(){Path="ebook.pdf",Title="Start Up Success"},
            new BookItem(){Path="ebook.pdf",Title="Accounting Basics"},
            new BookItem(){Path="ebook.pdf",Title="Marketing Analysis"},
            new BookItem(){Path="ebook.pdf",Title="Sales Strategies"},
            new BookItem(){Path="ebook.pdf",Title="Sales Targets"},
            new BookItem(){Path="ebook.pdf",Title="Increase Your Sales"},
            new BookItem(){Path="ebook.pdf",Title="Expand Your Business"},
            new BookItem(){Path="ebook.pdf",Title="Marketing Goals"},
            new BookItem(){Path="ebook.pdf",Title="Financial Statements"},
            new BookItem(){Path="ebook.pdf",Title="Success Stories"},
        };
    }
}
