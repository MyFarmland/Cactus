using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

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
