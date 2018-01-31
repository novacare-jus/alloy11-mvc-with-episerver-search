using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlloyTemplates.Models.ViewModels.Filters;
using EPiServer.Find.Api.Facets;

namespace AlloyTemplates.Business.Search
{
    public interface IFilterModel
    {
        string Title { get; set; }
        List<FacetItem> PageTypeFacets { get; set; }
       
    }
}
