using AutoDependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DataLayer.Attributes;

namespace DataLayer.Injector
{
    public static class DependencyInjector
    {

        public static void InjectServices(IServiceCollection services)
        {
            var assemblies = GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                InjectServices(services, types);
            }
        }

        private static void InjectServices(IServiceCollection services, Type[] types)
        {
            foreach (var type in types)
            {
                if (IsInjectable(type)) continue;

                var derivedClasses = GetDerivedTypes(types, type);

                foreach (var derivedClass in derivedClasses)
                {
                    var derivedClassInjectType =
                        derivedClass.GetCustomAttribute<InjectionTypeAttribute>(false);

                    if (derivedClassInjectType != null)
                    {
                        switch (derivedClassInjectType)
                        {
                            case ScopedAttribute:
                                services.AddScoped(type, derivedClass);
                                break;

                            case SingletonAttribute:
                                services.AddSingleton(type, derivedClass);
                                break;

                            case TransientAttribute:
                                services.AddTransient(type, derivedClass);
                                break;

                            default:
                                throw new ArgumentOutOfRangeException(nameof(derivedClassInjectType));
                        }
                    }

                }
            }
        }

        private static Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        private static List<Type> GetDerivedTypes(Type[] types, Type type)
        {
            return types.Where(p => type.IsAssignableFrom(p) && p.IsClass).ToList();
        }

        private static bool IsInjectable(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(InjectableAttribute));

            return !attributes.Any();
        }
    }
}
