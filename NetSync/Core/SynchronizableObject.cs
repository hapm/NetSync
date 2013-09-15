// <copyright file="ISynchronizable.cs" company="IrcShark Team">
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
	using System.Runtime.Serialization;

	/// <summary>
	/// A Syncronizable object can be synchronized over network.
	/// </summary>
	[Serializable]
	public class SynchronizableObject : GenericPropertyCollection
	{
		private ISource source;
		private String objectId;
		
		public SynchronizableObject(ISource source, string objectId) : base()
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (objectId == null)
				throw new ArgumentNullException("objectId");
			
			this.source = source;
			this.objectId = objectId;
			base.Object = this;
		}
		
		public ISource Source { get { return source; } }
		
		public string Id { get { return objectId; } }
		
		public override DateTime LastUpdated {
			get {
				DateTime lastUpdate = DateTime.Now;
				foreach (IProperty p in this) {
					if (p.LastUpdated < lastUpdate)
						lastUpdate = p.LastUpdated;
				}
				
				return lastUpdate;
			}
		}
	}
}
