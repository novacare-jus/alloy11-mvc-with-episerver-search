using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Find.Api.Facets;

namespace AlloyTemplates.Business.Search
{
    public interface IFilterModel
    {
        string Title { get; set; }
        string Url { get; set; }
        List<Facet> Facets { get; set; }
       
    }
}
