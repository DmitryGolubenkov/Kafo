using Autofac;
using Kafo.Application.Interfaces;
using Kafo.Server.Persistence;

namespace Kafo.Server.API.AutofacModules;

/// <summary>
/// Модуль для регистрации компонентов из Persistence
/// </summary>
public class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();   
    }
}
