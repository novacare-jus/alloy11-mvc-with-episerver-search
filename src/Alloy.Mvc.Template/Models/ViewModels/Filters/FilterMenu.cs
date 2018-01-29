using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlloyTemplates.Models.ViewModels.Filters
{
    public class FilterMenu
    {
        public List<FilterMenuItem> Items { get; set; }
    }

    public class FilterMenuItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

    }
}
