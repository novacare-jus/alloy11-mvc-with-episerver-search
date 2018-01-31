using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlloyTemplates.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace AlloyTemplates.Helpers
{
    
    public static class ContentHelper
    {
        private static readonly IContentLoader _contentLoader;
        static ContentHelper()
        {
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

        }
        public static SearchPage GetSearchPage()
        {
            var startPage = _contentLoader.Get<StartPage>(ContentReference.StartPage);
            return _contentLoader.Get<SearchPage>(startPage.SearchPageLink);
        }

    }
}
