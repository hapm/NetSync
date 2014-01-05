// <copyright file="LocalFileSource.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the RecursivLocalFileEnumerator class.</summary>

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
namespace NetSync.Source.LocalFileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using NetSync.Core;

    /// <summary>
    /// Description of RecursivLocalFileIterator.
    /// </summary>
    public class RecursivLocalFileEnumerator : IEnumerator<SynchronizableObject>
    {
        private RecursiveFileEnumerator files;
        private SynchronizableObject current;
        private LocalFileSource source;
        private UriBuilder builder;
        
        public delegate void ExceptionThrownEventHandler(Exception ex);
        
        public event ExceptionThrownEventHandler ExceptionThrown;
        
        public RecursivLocalFileEnumerator(LocalFileSource source)
        {
            this.files = new RecursiveFileEnumerator(source.GetDirectoryInfo());
            this.files.ExceptionThrown += new RecursiveFileEnumerator.ExceptionThrownEventHandler(files_ExceptionThrown);
            this.builder = new UriBuilder("file", source.MachineName);
            this.current = null;
            this.source = source;
        }

        private void files_ExceptionThrown(Exception ex)
        {
            if (ExceptionThrown != null)
                ExceptionThrown(ex);
        }
        
        public SynchronizableObject Current {
            get {
                return current;
            }
        }
        
        object System.Collections.IEnumerator.Current {
            get {
                return current;
            }
        }
        
        public void Dispose()
        {
            files.Dispose();
            files = null;
            current = null;
        }
        
        public bool MoveNext()
        {
            if (files.MoveNext()) {
                builder.Path = (source.GetDirectoryInfo().FullName + Path.PathSeparator + files.CurrentRelativePath).Replace(Path.PathSeparator, '/');
                current = new SynchronizableObject(source, builder.Uri);
                return true;
            }
            else {
                return false;
            }
        }
        
        public void Reset()
        {
            files.Reset();
            current = null;
        }
    }
}
