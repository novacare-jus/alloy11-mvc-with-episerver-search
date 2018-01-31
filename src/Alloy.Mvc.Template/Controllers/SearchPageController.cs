
using System;
using System.Web;
using System.Web.Mvc;
using AlloyTemplates.Business.Search;
using AlloyTemplates.Models.Pages;
using AlloyTemplates.Models.ViewModels;
using EPiServer;
using EPiServer.Web;
using Microsoft.Owin;


namespace AlloyTemplates.Controllers
{
    public class SearchPageController : PageControllerBase<SearchPage>
    {
        private readonly IContentLoader _contentLoader;
        private readonly ISearchService _searchProvider;
        private const int HitsContentPrPage = 40;
        public SearchPageController(ISearchService searchProvider, IContentLoader contentLoader)
        {
            _searchProvider = searchProvider;
            _contentLoader = contentLoader;
        }

        [ValidateInput(false)]
        public ViewResult Index(SearchPage currentPage)
        {
            var model = new SearchPageViewModel(currentPage);
            var query = Request.Params[QueryStringModel.Query];
            var filter = Request.Params[QueryStringModel.Filter];
            query = Server.HtmlEncode(query);
            filter = Server.HtmlEncode(filter);

            if (string.IsNullOrEmpty(query))
            {
                return View(model);
            }
            ViewBag.Query = query;
            ViewBag.Filter = filter ?? string.Empty;



            var hitsPrPage = currentPage.ResultLimit != 0 ? currentPage.ResultLimit : HitsContentPrPage;
                var searchQuery = new SearchParameters { SearchString = query, HitsPrPage = hitsPrPage, FilterParam = filter};
                var results = _searchProvider.ExecuteSearch(searchQuery);
                model.Result = results;
                model.Query = query;
                var filterModel = new FilterModel
                {
                    PageTypeFacets = results.FacetResults,
                    Title = "Spesifiser søket:",

                };
                model.Filter = filterModel;
          


            return View(model);
        }



    }
}
