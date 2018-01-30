using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlloyTemplates.Models.Media;
using AlloyTemplates.Models.Pages;
using AlloyTemplates.Models.ViewModels;
using AlloyTemplates.Models.ViewModels.Filters;
using EPiServer.Find;
using EPiServer.Find.UnifiedSearch;

namespace AlloyTemplates.Business.Search
{
    public class SearchProvider : ISearchProvider
    {
        private readonly IClient _client;

        public SearchProvider(IClient epiFindClient)
        {
            _client = epiFindClient;
          }

        public SearchResult ExecuteSearch(SearchParameters searchParams)
        {


            var mainFilter = _client.BuildFilter<ISearchContent>();
            mainFilter = mainFilter.Or(x => !x.MatchType(typeof(ImageFile))
                                            & !x.MatchType(typeof(VideoFile))
                                            & !x.MatchType(typeof(StartPage))
                                            & !x.MatchType(typeof(ContainerPage))
                                            & !x.MatchType(typeof(SearchPage))
                                            & !x.MatchType(typeof(SitePageData)));
          
            var searchResults = _client.UnifiedSearchFor(searchParams.SearchString)
                .FilterHits(mainFilter)
                .FilterFacet(FilterBy.All, mainFilter)
                .FilterFacet(FilterBy.ArticlePages, x => x.MatchType(typeof(ArticlePage)))
                .FilterFacet(FilterBy.NewsPages, x => x.MatchType(typeof(NewsPage)))
                .FilterFacet(FilterBy.ProductPages, x => x.MatchType(typeof(ProductPage)))
                .FilterFacet(FilterBy.StandardPages, x => x.MatchType(typeof(StandardPage)))
                .FilterFacet(FilterBy.LandingPages, x => x.MatchType(typeof(LandingPage)))
                .FilterFacet(FilterBy.ContactPages, x => x.MatchType(typeof(ContactPage)))
                .Take(searchParams.HitsPrPage)
                .GetResult();


            var result = new SearchResult();
            result.Hits = searchResults.Hits.Select(hit => new SearchResultItem()
            {
                Title = hit.Document.Title,
                Url = hit.Document.Url,
                PreviewText = hit.Document.Excerpt,
            }).ToList();

            result.FacetResults = GetFacetResult(searchResults);

            return result;
        }

        private List<FacetItem> GetFacetResult(UnifiedSearchResults searchResults)
        {
            var facetList = new List<FacetItem>();
            foreach (var result in searchResults.Facets)
            {
                var count = searchResults.FilterFacet(result.Name).Count;
                var item = new FacetItem
                {
                    Name = result.Name,
                    Count = count
                };
                facetList.Add(item);
            }

            return facetList;
        }
    }
}
