﻿//
//  DeleteEntityResult.cs
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
using JetBrains.Annotations;

namespace Remora.Results
{
    /// <summary>
    /// Encapsulates the result of an attempt to delete an entity.
    /// </summary>
    [PublicAPI]
    public sealed class DeleteEntityResult : ResultBase<DeleteEntityResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEntityResult"/> class.
        /// </summary>
        private DeleteEntityResult()
        {
        }

        /// <inheritdoc cref="ResultBase{TResultType}(string,Exception)"/>
        [UsedImplicitly]
        private DeleteEntityResult
        (
            string? errorReason,
            Exception? exception = null
        )
            : base(errorReason, exception)
        {
        }

        /// <summary>
        /// Creates a new successful result.
        /// </summary>
        /// <returns>A successful result.</returns>
        [PublicAPI, Pure]
        public static DeleteEntityResult FromSuccess()
        {
            return new DeleteEntityResult();
        }
    }
}
