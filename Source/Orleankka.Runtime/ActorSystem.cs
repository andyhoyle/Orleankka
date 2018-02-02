﻿using System;

using Orleankka.Utility;

namespace Orleankka
{
    /// <summary>
    /// The actor system extensions.
    /// </summary>
    public static class ActorSystemExtensions
    {
        /// <summary>
        /// Acquires the actor reference for the given id and type of the actor.
        /// </summary>
        /// <typeparam name="TActor">The type of the actor</typeparam>
        /// <param name="system">The reference to actor system</param>
        /// <param name="id">The id</param>
        /// <returns>An actor reference</returns>
        public static ActorRef ActorOf<TActor>(this IActorSystem system, string id) where TActor : IActorGrain
        {
            Type tempQualifier = typeof(TActor);
            Requires.NotNull(tempQualifier, nameof(tempQualifier));
            return system.ActorOf(ActorPath.For(tempQualifier, id));
        }

        /// <summary>
        /// Acquires the actor reference for the given worker type.
        /// </summary>
        /// <typeparam name="TActor">The type of the actor</typeparam>
        /// <param name="system">The reference to actor system</param>
        /// <returns>An actor reference</returns>
        public static ActorRef WorkerOf<TActor>(this IActorSystem system) where TActor : IActorGrain
        {
            Type tempQualifier = typeof(TActor);
            Requires.NotNull(tempQualifier, nameof(tempQualifier));
            return system.ActorOf(ActorPath.For(tempQualifier, "#"));
        }
    }
}