using System.Reflection;
using Autofac;
using Kafo.Server.Application.Authorization;
using Module = Autofac.Module;

namespace Kafo.Server.API.AutofacModules;

/// <summary>
/// Модуль для регистрации компонентов из Application
/// </summary>
public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Application
        builder.RegisterAssemblyTypes(typeof(AccessControl).Assembly)
            .Where(t => t.Name.EndsWith("Query"))
            .AsSelf();
        builder.RegisterAssemblyTypes(typeof(AccessControl).Assembly)
            .Where(t => t.Name.EndsWith("Command"))
            .AsSelf();

    }
}
