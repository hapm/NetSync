// <copyright file="LocalFileSourceFactory.cs" company="IrcShark Team">
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

using System;

namespace NetSync.Core
{
	/// <summary>
	/// Description of LocalFileSource.
	/// </summary>
	[Serializable]
	public class LocalFileSource : ISource
	{
		public LocalFileSource()
		{
		}
		
		public System.Collections.Generic.IEnumerator<SynchronizableObject> GetEnumerator()
		{
			throw new NotImplementedException();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
		
		public string Id {
			get {
				throw new NotImplementedException();
			}
		}
		
		public string Type {
			get {
				throw new NotImplementedException();
			}
		}
	}
}