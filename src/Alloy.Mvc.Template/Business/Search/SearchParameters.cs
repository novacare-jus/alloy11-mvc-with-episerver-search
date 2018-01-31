using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlloyTemplates.Business.Search
{
    public class SearchParameters
    {
        public SearchParameters()
        {
            page = 1;
          

        }

        public string SearchString { get; set; }
        public int HitsPrPage { get; set; }
        public string FilterParam { get; set; }

        public int page;

    }
}
