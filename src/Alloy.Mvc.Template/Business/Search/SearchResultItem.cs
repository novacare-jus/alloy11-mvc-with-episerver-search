using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlloyTemplates.Business.Search;

namespace AlloyTemplates.Business.Search
{
    public class SearchResultItem : ISearchResultItem
    {
        public string Title { get; set; }
        public string PreviewText { get; set; }
        public string Url { get; set; }



    }
}
