using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlloyTemplates.Models.ViewModels.Filters;
using EPiServer.Core;
using EPiServer.Find.Api;

namespace AlloyTemplates.Business.Search
{
    public class SearchResult
    {

        public List<SearchResultItem> Hits { get; set; }
        public List<FacetItem> FacetResults { get; set; }
        public string Query { get; set; }

        public SearchResult()
        {
            Hits = new List<SearchResultItem>();
            FacetResults = new List<FacetItem>();
        }
    }





}
