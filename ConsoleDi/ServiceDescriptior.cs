// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDI
{
    public class ServiceDescriptior
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public object Implementation { get; internal set; }

        public ServiceLifetime ServiceLifetime { get; }

        public ServiceDescriptior(object implementation, ServiceLifetime lifetime)
        {
            ServiceType = implementation.GetType();
            Implementation = implementation;
            ServiceLifetime = lifetime;
        }

        public ServiceDescriptior(Type serviceType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ServiceLifetime = lifetime;
        }

        public ServiceDescriptior(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            ServiceLifetime = lifetime;
        }
    }
}
