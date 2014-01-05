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
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A Syncronizable object can be synchronized over network.
    /// </summary>
    [Serializable]
    public class SynchronizableObject : GenericPropertyCollection
    {
        private ISource source;
        private Uri uri;
        private Dictionary<string, IProperty> dynamicProperties;
        
        public SynchronizableObject(ISource source, Uri uri) : base()
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (uri == null)
                throw new ArgumentNullException("objectId");
            
            this.dynamicProperties = new Dictionary<string, IProperty>();
            this.source = source;
            this.uri = uri;
            base.Object = this;
        }
        
        public ISource Source { get { return source; } }
        
        public Uri Uri { get { return uri; } }
        
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
        
        public override IProperty this[string id] {
            get {
                IProperty property = base[id];
                if (property != null)
                    return property;
                
                return dynamicProperties[id];
            }
        }
        
        public void SetProperty(string name, IProperty property) {
            if (name.Contains("."))
                throw new ArgumentException("Dots aren't allowed in a property name.", "name");
            if (base[name] != null)
                throw new ArgumentException("Given name is a fixed property of a SynchronizableObject", "name");
            dynamicProperties.Remove(name);
            dynamicProperties.Add(name, property);
        }
    }
}
