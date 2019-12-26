﻿//
//  CreateEntityResult.cs
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
    /// Represents an attempt to create an entity.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to encapsulate.</typeparam>
    [PublicAPI]
    public sealed class CreateEntityResult<TEntity> : ResultBase<CreateEntityResult<TEntity>> where TEntity : class
    {
        /// <summary>
        /// Holds the actual entity value.
        /// </summary>
        private readonly TEntity? _entity;

        /// <summary>
        /// Gets the entity that was retrieved.
        /// </summary>
        [PublicAPI, NotNull]
        public TEntity Entity
        {
            get
            {
                if (!this.IsSuccess || _entity is null)
                {
                    throw new InvalidOperationException("The result does not contain a valid value.");
                }

                return _entity;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEntityResult{T}"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        private CreateEntityResult(TEntity? entity)
        {
            _entity = entity;
        }

        /// <inheritdoc cref="ResultBase{TResultType}(string,Exception)"/>
        [UsedImplicitly]
        private CreateEntityResult
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
        /// <param name="entity">The entity that was retrieved.</param>
        /// <returns>A successful result.</returns>
        [PublicAPI, Pure, NotNull]
        public static CreateEntityResult<TEntity> FromSuccess([NotNull] TEntity entity)
        {
            return new CreateEntityResult<TEntity>(entity);
        }

        /// <summary>
        /// Implicitly converts a compatible value to a successful result.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The successful result.</returns>
        [PublicAPI, Pure, NotNull]
        public static implicit operator CreateEntityResult<TEntity>([NotNull] TEntity entity)
        {
            return FromSuccess(entity);
        }
    }
}