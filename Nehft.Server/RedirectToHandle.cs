using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nehft.Server
{
    public static class RedirectToHandle
    {
        public static void InvokeEvent<T>(T instance, object @event)
        {
            if (!Cache<T>.Dict.TryGetValue(@event.GetType(), out var info))
            {
                return;
            }

            try
            {
                info.Invoke(instance, new[] {@event});
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        private static class Cache<T>
        {
            public static readonly IDictionary<Type, MethodInfo> Dict = typeof(T)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "Handle")
                .Where(m => m.GetParameters().Length == 1)
                .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
        }
    }
}