// <copyright file="GenericPropertyCollection.cs" company="IrcShark Team">
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
namespace NetSync.Core.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Description of PropertyPath.
    /// </summary>
    public class PropertyPathEnumerator : IEnumerator<string>
    {
        private string[] levels;
        private int index;
        
        public PropertyPathEnumerator(String path)
        {
            levels = path.Split('.');
            index = -1;
        }
        
        public string Current {
            get {
                if (index == -1 || index >= levels.Length)
                    return null;
                
                return levels[index];
            }
        }
        
        object System.Collections.IEnumerator.Current {
            get {
                if (index == -1 || index >= levels.Length)
                    return null;
                
                return levels[index];
            }
        }
        
        public void Dispose()
        {
        }
        
        public bool HasNext {
            get {
                return levels.Length > index+1;
            }
        }
        
        public bool MoveNext()
        {
            if (!HasNext)
                return false;
            
            index++;
            return true;
        }
        
        public void Reset()
        {
            index = 0;
        }
    }
}
