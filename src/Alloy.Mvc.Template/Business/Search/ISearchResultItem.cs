using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlloyTemplates.Business.Search
{
    public interface ISearchResultItem
    {
        string Title { get; set; }
        string Url { get; set; }
    }
}
