using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlloyTemplates.Models.ViewModels;

namespace AlloyTemplates.Business.Search
{
    public class ISearchService
    {
        IEnumerable<SearchContentModel.SearchHit> Search(string searchText, int maxResults);
    }
}
