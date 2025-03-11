using HornsAndHoovesCrm.Core.Exceptions;
using HornsAndHoovesCrm.Core.Extensions.Collections;
using HornsAndHoovesCrm.Core.Extensions.DependencyInjection;
using HornsAndHoovesCrm.Core.Modularity.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace HornsAndHoovesCrm.Core.Modularity;

public class ModuleLoader : IModuleLoader
{
    public ICrmModuleDescriptor[] LoadModules(
        IServiceCollection services,
        Type startupModuleType)
    {

        var modules = GetDescriptors(services, startupModuleType);

        modules = SortByDependency(modules, startupModuleType);

        return modules.ToArray();
    }

    private List<ICrmModuleDescriptor> GetDescriptors(
        IServiceCollection services,
        Type startupModuleType)
    {
        var modules = new List<CrmModuleDescriptor>();

        FillModules(modules, services, startupModuleType);
        SetDependencies(modules);

        return modules.Cast<ICrmModuleDescriptor>().ToList();
    }

    protected virtual void FillModules(
        List<CrmModuleDescriptor> modules,
        IServiceCollection services,
        Type startupModuleType)
    {
        var logger = services.GetInitLogger<CrmApplicationBase>();

        //All modules starting from the startup module
        foreach (var moduleType in CrmModuleHelper.FindAllModuleTypes(startupModuleType, logger))
        {
            modules.Add(CreateModuleDescriptor(services, moduleType));
        }

    }

    protected virtual void SetDependencies(List<CrmModuleDescriptor> modules)
    {
        foreach (var module in modules)
        {
            SetDependencies(modules, module);
        }
    }

    protected virtual List<ICrmModuleDescriptor> SortByDependency(List<ICrmModuleDescriptor> modules, Type startupModuleType)
    {
        var sortedModules = modules.SortByDependencies(m => m.Dependencies);
        sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
        return sortedModules;
    }

    protected virtual CrmModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType)
    {
        return new CrmModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType));
    }

    protected virtual ICrmModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
    {
        var module = (ICrmModule)Activator.CreateInstance(moduleType)!;
        services.AddSingleton(moduleType, module);
        return module;
    }

    protected virtual void SetDependencies(List<CrmModuleDescriptor> modules, CrmModuleDescriptor module)
    {
        foreach (var dependedModuleType in CrmModuleHelper.FindDependedModuleTypes(module.Type))
        {
            var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
            if (dependedModule == null)
            {
                throw new CrmException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
            }

            module.AddDependency(dependedModule);
        }
    }
}