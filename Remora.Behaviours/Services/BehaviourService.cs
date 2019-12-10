//
//  BehaviourService.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Remora.Behaviours.Services
{
    /// <summary>
    /// This class manages the access to and lifetime of registered behaviours.
    /// </summary>
    public class BehaviourService
    {
        private readonly ICollection<IBehaviour> _registeredBehaviours = new List<IBehaviour>();

        /// <summary>
        /// Discovers and adds behaviours defined in the given assembly.
        /// </summary>
        /// <param name="containingAssembly">The assembly where behaviours are defined.</param>
        /// <param name="services">The services available to the application.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddBehavioursAsync([NotNull] Assembly containingAssembly, IServiceProvider services)
        {
            var definedTypes = containingAssembly.DefinedTypes;
            var behaviourTypes = definedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IBehaviour)));

            foreach (var behaviourType in behaviourTypes)
            {
                await AddBehaviourAsync(services, behaviourType);
            }
        }

        /// <summary>
        /// Adds the given behaviour to the service.
        /// </summary>
        /// <param name="services">The available services.</param>
        /// <typeparam name="TBehaviour">The type of the behaviour.</typeparam>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task AddBehaviourAsync<TBehaviour>(IServiceProvider services)
            where TBehaviour : IBehaviour
        {
            return AddBehaviourAsync(services, typeof(TBehaviour));
        }

        /// <summary>
        /// Adds the given behaviour to the service.
        /// </summary>
        /// <param name="services">The available services.</param>
        /// <param name="behaviourType">The type of the behaviour.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddBehaviourAsync(IServiceProvider services, Type behaviourType)
        {
            if (behaviourType.IsAbstract)
            {
                return;
            }

            // Since the behaviours run in their own threads, we'll do scoped contexts for them. The behaviours run
            // until they're disposed, so they're responsible for clearing up their own scopes.
            var scope = services.CreateScope();
            var behaviour = (IBehaviour)ActivatorUtilities.CreateInstance
            (
                scope.ServiceProvider,
                behaviourType
            );

            behaviour.WithScope(scope);

            // Behaviours are implicitly singletons; there's only ever one instance of a behaviour at any given
            // time.
            var existingBehaviour = _registeredBehaviours.FirstOrDefault(b => b.GetType() == behaviourType);
            if (!(existingBehaviour is null))
            {
                _registeredBehaviours.Remove(existingBehaviour);

                await existingBehaviour.StopAsync();
                existingBehaviour.Dispose();
            }

            _registeredBehaviours.Add(behaviour);
        }

        /// <summary>
        /// Starts all registered behaviours.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task StartBehavioursAsync()
        {
            foreach (var behaviour in _registeredBehaviours)
            {
                await behaviour.StartAsync();
            }
        }

        /// <summary>
        /// Stops all registered behaviours.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task StopBehavioursAsync()
        {
            foreach (var behaviour in _registeredBehaviours)
            {
                await behaviour.StopAsync();
            }
        }
    }
}
