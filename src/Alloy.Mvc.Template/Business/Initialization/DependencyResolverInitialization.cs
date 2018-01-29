using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace AlloyTemplates.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(InitializationModule))]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {

            var container = context.StructureMap();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }

        public void Initialize(InitializationEngine context)
        {
           
        }

        public void Uninitialize(InitializationEngine context)
        {
        }


    }
}
