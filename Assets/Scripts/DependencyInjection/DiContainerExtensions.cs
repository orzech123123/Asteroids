using System;
using System.Linq;
using Zenject;

namespace Assets.Scripts.Di
{
    public static class DiContainerExtensions
    {
        public static void BindFromImplementedInterface<TInterface>(this DiContainer container) where TInterface : class
        {
            var type = typeof(TInterface);

            if (!type.IsInterface)
            {
                throw new NotSupportedException("Type " + type.Name + " needs to be an interface to use BindFromImplementedInterface.");
            }

            var implementedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => type != t)
                .Where(t => type.IsAssignableFrom(t))
                .ToList();

            foreach (var implementedType in implementedTypes)
            {
                container.BindInterfacesAndSelfTo(implementedType).AsSingle();
            }
        }
    }
}