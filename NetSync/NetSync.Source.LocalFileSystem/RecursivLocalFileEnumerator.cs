// <copyright file="LocalFileSource.cs" company="IrcShark Team">
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
using System.Collections.Generic;
using System.IO;
using NetSync.Core;

namespace NetSync.Source.LocalFileSystem
{
	/// <summary>
	/// Description of RecursivLocalFileIterator.
	/// </summary>
	public class RecursivLocalFileEnumerator : IEnumerator<SynchronizableObject>
	{
		private IEnumerator<FileInfo> files;
		private SynchronizableObject current;
		private LocalFileSource source;
		private UriBuilder builder;
		
		public RecursivLocalFileEnumerator(LocalFileSource source)
		{
			this.files = source.GetDirectoryInfo().EnumerateFiles("*", SearchOption.AllDirectories).GetEnumerator();
			this.builder = new UriBuilder("file", source.MachineName);
			this.current = null;
			this.source = source;
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
			current = null;
		}
		
		public bool MoveNext()
		{
			if (!files.MoveNext())
				return false;
			
			builder.Path = files.Current.FullName.Replace(Path.PathSeparator, '/');
			current = new SynchronizableObject(source, builder.Uri);
			return true;
		}
		
		public void Reset()
		{
			files.Reset();
			current = null;
		}
	}
}
