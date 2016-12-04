using Autofac;
using Autofac.Integration.Mvc;
using System.Configuration;

namespace Cactus.Common
{
    public class IocHelper
    {
        public static string sqltype = ConfigurationManager.ConnectionStrings["sqltype"].ConnectionString;
        public static TService AutofacResolveNamed<TService>(string ServiceName)
        {
            string _ServiceName = sqltype + "." + ServiceName;
            return AutofacDependencyResolver.Current.ApplicationContainer.ResolveNamed<TService>(_ServiceName);
        }
    }
}
