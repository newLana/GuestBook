using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace GuestBook.Models.DI
{
    public class GuestBookDependencyResolver : IDependencyResolver
    {
        private static Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public static void Bind<TContract, TImplement>()
        {
            _map.Add(typeof(TContract), typeof(TImplement));
        }

        public object GetService(Type serviceType)
        {
            if(_map.ContainsKey(serviceType))
            {
                return ContainsCtorWithBindedTypeParams(_map[serviceType]);
            }
            if (serviceType.IsClass && !serviceType.IsAbstract)
            {
                return ContainsCtorWithBindedTypeParams(serviceType);
            }
            return null;
        }

        private object ContainsCtorWithBindedTypeParams(Type serviceType)
        {
            ConstructorInfo constructor = serviceType.GetConstructors()[0];
            ParameterInfo[] ctorParams = constructor.GetParameters();
            if (ctorParams.Length != 0)
            {
                return constructor.Invoke(ctorParams.Select(p => GetService(p.ParameterType)).ToArray());
            }
            return Activator.CreateInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_map.ContainsKey(serviceType))
            {
                yield return Activator.CreateInstance(_map[serviceType]);
            }
        }
    }
}