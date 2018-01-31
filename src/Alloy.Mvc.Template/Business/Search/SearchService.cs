using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using AlloyTemplates.Models.Media;
using AlloyTemplates.Models.Pages;
using AlloyTemplates.Models.ViewModels;
using AlloyTemplates.Models.ViewModels.Filters;
using EPiServer.Find;
using EPiServer.Find.Api.Querying;
using EPiServer.Find.UnifiedSearch;

namespace AlloyTemplates.Business.Search
{
    public class SearchService : ISearchService
    {
        private readonly IClient _client;

        public SearchService(IClient epiFindClient)
        {
            _client = epiFindClient;
          }

        public SearchResult ExecuteSearch(SearchParameters searchParams)
        {


            var mainFilter = GetMainFilter();
            var searchResults = GetUnifiedSearchResults(searchParams, mainFilter);
            var result = new SearchResult();
            result.Hits = searchResults.Hits.Select(hit => new SearchResultItem()
            {
                Title = hit.Document.Title,
                Url = hit.Document.Url,
                PreviewText = hit.Document.Excerpt,
            }).ToList();

            result.Query = searchParams.SearchString;
            result.FacetResults = GetFacetResult(searchResults);

            return result;
        }

        private UnifiedSearchResults GetUnifiedSearchResults(SearchParameters searchParams, FilterBuilder<ISearchContent> mainFilter)
        {
            var hitSpec = new HitSpecification
            {
                HighlightTitle = true,
                HighlightExcerpt = true,
                ExcerptLength = 400,
            };
            var query = _client.UnifiedSearchFor(searchParams.SearchString)
                .FilterHits(mainFilter)
                .FilterFacet(QueryStringModel.FilterBy.All, mainFilter)
                .FilterFacet(QueryStringModel.FilterBy.ArticlePages, x => x.MatchType(typeof(ArticlePage)))
                .FilterFacet(QueryStringModel.FilterBy.NewsPages, x => x.MatchType(typeof(NewsPage)))
                .FilterFacet(QueryStringModel.FilterBy.ProductPages, x => x.MatchType(typeof(ProductPage)))
                .FilterFacet(QueryStringModel.FilterBy.StandardPages, x => x.MatchType(typeof(StandardPage)))
                .FilterFacet(QueryStringModel.FilterBy.LandingPages, x => x.MatchType(typeof(LandingPage)))
                .FilterFacet(QueryStringModel.FilterBy.ContactPages, x => x.MatchType(typeof(ContactPage)));

            if (!string.IsNullOrEmpty(searchParams.FilterParam))
            {
                switch (searchParams.FilterParam)
                {
                    case QueryStringModel.FilterBy.ArticlePages:
                        query = query.Filter(x => x.MatchType(typeof(ArticlePage)));
                        break;
                    case QueryStringModel.FilterBy.NewsPages:
                        query = query.FilterHits(x => x.MatchType(typeof(NewsPage)));
                        break;
                    case QueryStringModel.FilterBy.ProductPages:
                        query = query.FilterHits(x => x.MatchType(typeof(ProductPage)));
                        break;
                    case QueryStringModel.FilterBy.StandardPages:
                        query = query.FilterHits(x => x.MatchType(typeof(StandardPage)));
                        break;
                    case QueryStringModel.FilterBy.LandingPages:
                        query = query.FilterHits(x => x.MatchType(typeof(LandingPage)));
                        break;
                    case QueryStringModel.FilterBy.ContactPages:
                        query = query.FilterHits(x => x.MatchType(typeof(ContactPage)));
                        break;

                    default:
                        break;
                }
            }

            query = query.ApplyBestBets();
            query = query.Skip((searchParams.page - 1) * searchParams.HitsPrPage).Take(searchParams.HitsPrPage);
           var searchResults = query.GetResult(hitSpec);
            return searchResults;
        }

        private FilterBuilder<ISearchContent> GetMainFilter()
        {
            var mainFilter = _client.BuildFilter<ISearchContent>();
            mainFilter = mainFilter.Or(x => !x.MatchType(typeof(ImageFile))
                                            & !x.MatchType(typeof(VideoFile))
                                            & !x.MatchType(typeof(StartPage))
                                            & !x.MatchType(typeof(ContainerPage))
                                            & !x.MatchType(typeof(SearchPage))
                                            & !x.MatchType(typeof(SitePageData)));
            return mainFilter;
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
