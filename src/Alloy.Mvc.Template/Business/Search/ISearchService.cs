using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlloyTemplates.Business.Search
{
    public interface ISearchService
    {
        SearchResult ExecuteSearch(SearchParameters searchParams);
    }
}
