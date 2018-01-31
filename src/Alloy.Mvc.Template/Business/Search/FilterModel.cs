using System;
using System.Collections.Generic;
using AlloyTemplates.Models.ViewModels.Filters;
using EPiServer.Find.Api.Facets;

namespace AlloyTemplates.Business.Search
{
    public class FilterModel : IFilterModel
    {
        public string Title { get; set; }
        public List<FacetItem> PageTypeFacets { get; set; }
    }
}
