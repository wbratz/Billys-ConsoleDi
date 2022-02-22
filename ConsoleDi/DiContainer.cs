// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleDI
{
    public class DiContainer
    {
        private ConcurrentDictionary<Type, ServiceDescriptior> _serviceDescriptiors = new ConcurrentDictionary<Type, ServiceDescriptior>();
        public DiContainer()
        {
        }

        public DiContainer(List<ServiceDescriptior> serviceDescriptiors)
        {
            foreach (ServiceDescriptior serviceDescriptior in serviceDescriptiors)
            {
                _serviceDescriptiors.TryAdd(serviceDescriptior.ServiceType, serviceDescriptior);
            }
        }

        public object GetService(Type serivceType)
        {
            var descriptor = _serviceDescriptiors[serivceType];

            if (descriptor == null)
                throw new Exception($"Unregistered service: {serivceType.Name}");

            if (descriptor.Implementation != null)
                return descriptor.Implementation;

            var actualType = descriptor.ImplementationType ?? descriptor.ServiceType;

            if (actualType.IsAbstract || actualType.IsInterface)
            {
                throw new Exception($"Cannot instantiate non-concrete classes");
            }

            //ctor where you have ways to provide the types it require

            var constructorInfo = actualType.GetConstructors().First();

            var parameters = constructorInfo.GetParameters().Select(x => GetService(x.ParameterType)).ToArray();

            var implementation = Activator.CreateInstance(actualType, parameters);

            if (descriptor.ServiceLifetime == ServiceLifetime.Singleton)
            {
                descriptor.Implementation = implementation;
            }

            return implementation;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }
    }
}
