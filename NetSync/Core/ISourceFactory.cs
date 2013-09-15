using Mono.Addins;
// <copyright file="ISourceFactory.cs" company="IrcShark Team">
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

	/// <summary>
	/// An ISourceFactory is able to create a number of sources.
	/// </summary>
	[TypeExtensionPoint(ExtensionAttributeType=typeof(SourceFactoryAttribute))]
	public interface ISourceFactory
	{
		/// <summary>
		/// Gets the source described by the given source. 
		/// </summary>
		/// <remarks>
		/// This method should throw an InvalidArgumentException for any id, that causes CanHandle to return false.
		/// </remarks>
		/// <param name="sourceId">The factory specific id belonging to the source.</param>
		/// <param name="create">If the source under the given id doesn't exist, the factory tries to create it if this is true.</param>
		/// <returns>The source for the given id. Can be null, when create is false.</returns>
		ISource GetSource(Uri sourceId, bool create = false);
		
		/// <summary>
		/// Checks if the given id is a valid id for the factory.
		/// </summary>
		/// <param name="sourceId">The id to check.</param>
		/// <returns>Returns true, when the sourceId can be handled by the factory instance, false otherwise.</returns>
		bool CanHandle(Uri sourceId);
	}
}
