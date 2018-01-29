using System;
using System.Collections.Generic;
using EPiServer.Find.Api.Facets;

namespace AlloyTemplates.Business.Search
{
    public class FilterModel : IFilterModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Facet> Facets { get; set; }
    }
}
