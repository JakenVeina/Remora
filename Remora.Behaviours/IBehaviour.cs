//
//  IBehaviour.cs
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

using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Remora.Behaviours
{
    /// <summary>
    /// Interface for a behaviour.
    /// </summary>
    [PublicAPI]
    public interface IBehaviour
    {
        /// <summary>
        /// Gets a value indicating whether the behaviour is currently running.
        /// </summary>
        [PublicAPI]
        bool IsRunning { get; }

        /// <summary>
        /// Starts the behaviour, allowing it to perform its tasks. Calling this method while the behaviour is running
        /// does nothing.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [PublicAPI]
        Task StartAsync();

        /// <summary>
        /// Stops the behaviour, ceasing its tasks. Calling this method when the behaviour is not running does nothing.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [PublicAPI]
        Task StopAsync();
    }
}
