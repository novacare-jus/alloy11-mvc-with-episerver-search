using System.Collections.Generic;
using System.Web.Mvc;
using AlloyTemplates.Business.Search;
using AlloyTemplates.Models.Pages;
using EPiServer.Find.UnifiedSearch;

namespace AlloyTemplates.Models.ViewModels
{
    public class SearchPageViewModel : PageViewModel<SearchPage>
    {
        public SearchPageViewModel(SearchPage currentPage) : base(currentPage)
        {
            Result = new SearchResult{Hits = new List<SearchResultItem>()};
        }

        public string Query { get; set; }
        public SearchResult Result { get; set; }

        //public FilterViewModel FilterModel { get; set; }
    }
}
