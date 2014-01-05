// <copyright file="IPropertyCollection.cs" company="IrcShark Team">
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

namespace NetSync.Core
{
	/// <summary>
	/// Description of GenericPropertyCollection.
	/// </summary>
	public abstract class GenericPropertyCollection : IPropertyCollection
	{
		private SynchronizableObject syncObject;
		
		protected GenericPropertyCollection() {
			
		}
		
		public GenericPropertyCollection(SynchronizableObject obj)
		{
			this.syncObject = obj;
		}
		
		public virtual IProperty this[string id] {
			get {
				PropertyInfo prop = GetType().GetProperty(id);
				if (prop == null)
					return null;
				
				if (prop.PropertyType.IsSubclassOf(typeof(IProperty)))
				    return (IProperty)prop.GetValue(this, null);
				
				return (IProperty)Activator.CreateInstance(typeof(GenericProperty<>).MakeGenericType(prop.PropertyType), syncObject, prop.GetValue(this, null));
			}
		}
		
		public SynchronizableObject Object {
			get {
				return syncObject;
			}
			
			protected set {
				syncObject = value;
			}
		}
		
		public int CompareTo(IProperty other)
		{
			throw new NotImplementedException();
		}
		
		public System.Collections.Generic.IEnumerator<IProperty> GetEnumerator()
		{
			throw new NotImplementedException();
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
		
		public IProperty Follow(PropertyPathEnumerator path)
		{
			String key;
			IProperty result;
			if (!path.MoveNext()) {
				return this;
			}
			
			key = path.Current;
			result = this[key];
			var next = result as IPropertyCollection;
			if (next != null)
				result = next.Follow(path);
			else if (path.HasNext)
				result = null;
			
			return result;
		}
		
		public IProperty Follow(string path)
		{
			return Follow(new PropertyPathEnumerator(path));
		}
		
		public abstract DateTime LastUpdated { get;	}
	}
}
