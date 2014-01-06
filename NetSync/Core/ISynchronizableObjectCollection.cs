// <copyright file="ISynchronizableObjectCollection.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
namespace NetSync.Core
{
    using System;
    using System.Collections.Generic;
    using NetSync.Core.Filter;

    /// <summary>
    /// Description of ISynchronizableObjectCollection.
    /// </summary>
    public interface ISynchronizableObjectCollection : ICollection<SynchronizableObject>
    {
        /// <summary>
        /// Applys the given filter to all elements in the ISynchronizableObjectCollection and returns the subset 
        /// of SynchronizableObjects as a new ISynchronizableObjectCollection using its default IFilterProcessor.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>The resulting collection of SynchronizableObjects.</returns>
        ISynchronizableObjectCollection ApplyFilter(IFilter filter);
    }
}
