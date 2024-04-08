using Autofac.Core;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extentions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDependencyResolvers(
            this IServiceCollection servicesCollection, ICoreModule[] modules) {

            foreach (var modul in modules)
            {
                modul.Load(servicesCollection);
            }
            return ServiceTool.Create(servicesCollection);
        }
    }
}
