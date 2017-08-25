namespace StarsReloaded.Client.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using GalaSoft.MvvmLight;

    using StarsReloaded.Client.ViewModel.Attributes;

    public class BaseViewModel : ViewModelBase
    {
        private static readonly Dictionary<Type, Dictionary<string, List<string>>> PropertyDependencies =
            new Dictionary<Type, Dictionary<string, List<string>>>();

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(propertyName);

            foreach (var property in GetDependentProperties(this.GetType(), propertyName))
            {
#if DEBUG
                Console.WriteLine($"{propertyName} -> {property}");
#endif
                this.RaisePropertyChanged(property);
            }
        }

        private static IEnumerable<string> GetDependentProperties(Type type, string propName)
        {
            lock (PropertyDependencies)
            {
                if (!PropertyDependencies.ContainsKey(type))
                {
                    PropertyDependencies[type] = ResolvePropertyDependencies(type);
                }

                var typeMap = PropertyDependencies[type];
                if (!typeMap.ContainsKey(propName))
                {
                    return new string[0];
                }

                return PropertyDependencies[type][propName];
            }
        }

        private static Dictionary<string, List<string>> ResolvePropertyDependencies(Type type)
        {
#if DEBUG
            Console.WriteLine($"Resolving property dependencies for {type.FullName}");
#endif

            var map = new Dictionary<string, List<string>>();

            foreach (var property in type.GetProperties())
            {
                foreach (var dependency in property.GetCustomAttributes(typeof(DependsUponAttribute)))
                {
                    var dependsUpon = (dependency as DependsUponAttribute)?.PropertyName;
                    if (dependsUpon != null && dependsUpon != property.Name)
                    {
                        RegisterDependency(map, dependsUpon, property.Name);
                    }
                }
            }

            return map;
        }

        private static void RegisterDependency(IDictionary<string, List<string>> map, string fromProp, string toProp)
        {
            if (!map.ContainsKey(fromProp))
            {
                map[fromProp] = new List<string>();
            }

            if (!map[fromProp].Contains(toProp))
            {
                map[fromProp].Add(toProp);
            }
        }
    }
}