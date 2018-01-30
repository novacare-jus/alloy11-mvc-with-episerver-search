using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Framework.Web;
using EPiServer.Search;
using AlloyTemplates.Business;
using AlloyTemplates.Business.Search;
using AlloyTemplates.Models.Pages;
using AlloyTemplates.Models.ViewModels;
using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiServer.Web;
using EPiServer.Web.Hosting;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;

namespace AlloyTemplates.Controllers
{
    public class SearchPageController : PageControllerBase<SearchPage>
    {

        private readonly ISearchProvider _searchProvider;
        private const int HitsContentPrPage = 10;
        public SearchPageController(ISearchProvider searchProvider)
        {
            _searchProvider = searchProvider;
        }

        [ValidateInput(false)]
        public ViewResult Index(SearchPage currentPage, string query)
        {
            var model = new SearchPageViewModel(currentPage);
            if (string.IsNullOrEmpty(query))
            {
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
               
                var hitsPrPage = currentPage.ResultLimit != 0 ? currentPage.ResultLimit : HitsContentPrPage;
                var searchQuery = new SearchParameters { SearchString = query, HitsPrPage = hitsPrPage };
                var results = _searchProvider.ExecuteSearch(searchQuery);
                model.Result = results;
                model.Query = query;
                var filterModel = new FilterModel
                {
                    Facets = results.FacetResults,
                    Title = "Spesifiser søket:"
                };
                model.Filter = filterModel;
            }


            return View(model);
        }



    }
}
