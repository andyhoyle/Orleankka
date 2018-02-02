﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Orleans;
using Orleans.Streams;

namespace Orleankka.Cluster
{
    using Utility;
    
    class ClusterActorSystem : ActorSystem
    {
        readonly Dictionary<Type, ActorGrainImplementation> implementations = 
             new Dictionary<Type, ActorGrainImplementation>();

        internal ClusterActorSystem(
            Assembly[] assemblies,
            IStreamProviderManager streamProviderManager,
            IGrainFactory grainFactory,
            ActorMiddlewarePipeline pipeline,
            IActorRefMiddleware middleware = null)
            : base(assemblies, streamProviderManager, grainFactory, middleware)
        {
            Register(pipeline, assemblies);
        }

        void Register(ActorMiddlewarePipeline pipeline, IEnumerable<Assembly> assemblies)
        {
            foreach (var each in assemblies.SelectMany(x => x.GetTypes().Where(IsActorGrain)))
            {
                var implementation = new ActorGrainImplementation(each, pipeline.Middleware(each));
                implementations.Add(each, implementation);
            }

            bool IsActorGrain(Type type) => !type.IsAbstract && typeof(ActorGrain).IsAssignableFrom(type);
        }

        internal ActorGrainImplementation ImplementationOf(Type grain) => implementations.Find(grain);        
    }
}