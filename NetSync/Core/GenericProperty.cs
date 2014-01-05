// <copyright file="LocalFileProperties.cs" company="IrcShark Team">
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
using System.Reflection;
using System.Runtime.Serialization;

namespace NetSync.Core
{
	/// <summary>
	/// Description of GenericProperty.
	/// </summary>
	[DefaultMember("Value"),Serializable]
	public class GenericProperty<T> : IProperty where T : IComparable<T>
	{
		private T val;
		private SynchronizableObject syncObject;
		private DateTime updated;
		
		public GenericProperty(SynchronizableObject obj, T val)
		{
			this.val = val;
			this.syncObject = obj;
			this.updated = DateTime.Now;
		}
		
		public GenericProperty(SynchronizableObject obj, T val, DateTime updated) {
			this.val = val;
			this.syncObject = obj;
			this.updated = updated;
		}
		
		public SynchronizableObject Object {
			get { return syncObject; }
		}
		
		public T Value {
			get { return val; }
		}
		
		public int CompareTo(IProperty other)
		{
			GenericProperty<T> oth2 = other as GenericProperty<T>;
			if (oth2 == null)
				return -1;
			
			return val.CompareTo(oth2.val);
		}
		
		public DateTime LastUpdated {
			get {
				return updated;
			}
		}
	}
}
