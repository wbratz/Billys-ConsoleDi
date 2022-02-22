// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace ConsoleDI
{
    public class DiServiceCollection
    {
        private List<ServiceDescriptior> _serviceDescriptiors = new List<ServiceDescriptior>();

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            _serviceDescriptiors.Add(new ServiceDescriptior(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton));
        }

        public void RegisterSingleton<TService>()
        {
            _serviceDescriptiors.Add(new ServiceDescriptior(typeof(TService), ServiceLifetime.Singleton));
        }

        public void RegisterSingleton<TService>(TService implementation)
        {
            _serviceDescriptiors.Add(new ServiceDescriptior(implementation, ServiceLifetime.Singleton));
        }

        public void RegisterTransient<TService>()
        {
            _serviceDescriptiors.Add(new ServiceDescriptior(typeof(TService), ServiceLifetime.Transient));
        }

        public void RegisterTransient<TService, TImplementation>() where TImplementation : TService
        {
            _serviceDescriptiors.Add(new ServiceDescriptior(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient));
        }

        public DiContainer BuildContainer()
        {
            return new DiContainer(_serviceDescriptiors);
        }
    }
}
