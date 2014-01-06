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

using System;
using System.IO;
using Mono.Addins;
using NetSync.Core;

namespace NetSync.Source.LocalFileSystem
{
    /// <summary>
    /// Description of LocalFileSourceFactory.
    /// </summary>
    [SourceFactory("file")]
    public class LocalFileSourceFactory : ISourceFactory
    {
        public LocalFileSourceFactory()
        {
        }
        
        public bool CanHandle(Uri sourceId)
        {
            if (!sourceId.Scheme.Equals("file"))
                return false;
            if (!sourceId.Host.Equals(System.Environment.MachineName.ToLower()))
                return false;
            
            return true;
        }
        
        public ISource GetSource(Uri sourceId, bool create)
        {
            if (sourceId == null)
                throw new ArgumentNullException("sourceId");
            
            if (!CanHandle(sourceId))
                throw new ArgumentException(String.Format("The given source address \"{0}\" can't be handled by the LocalFileSystem source factory", sourceId), "sourceId");
            
            DirectoryInfo baseDirectory = new DirectoryInfo(sourceId.AbsolutePath.Substring(1));
            if (!baseDirectory.Exists) {
                if (create) {
                    throw new NotImplementedException();
                }
                else 
                    throw new ArgumentException("Source coudln't be found on the system", "sourceId");
            }
            
            LocalFileSource result = new LocalFileSource(baseDirectory);
            return result;
        }
    }
}
