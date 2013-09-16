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
using System.IO;
using NetSync.Core;

namespace NetSync.Source.LocalFileSystem
{
	/// <summary>
	/// Description of LocalFileSource.
	/// </summary>
	[Serializable]
	public class LocalFileSource : ISource
	{
		private string baseDirectory;
		private String machineName;
		
		public LocalFileSource(DirectoryInfo baseDirectory)
		{
			this.baseDirectory = baseDirectory.FullName;
			this.machineName = System.Environment.MachineName.ToLower();
		}
		
		public System.Collections.Generic.IEnumerator<SynchronizableObject> GetEnumerator()
		{
			DirectoryInfo dir = new DirectoryInfo(baseDirectory);
			return new RecursivLocalFileEnumerator(this);
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return (System.Collections.IEnumerator)this.GetEnumerator();
		}
		
		public Uri Uri {
			get {
				UriBuilder build = new UriBuilder("file", machineName);
				build.Path = baseDirectory.Replace(Path.PathSeparator, '/');
				return build.Uri;
			}
		}
		
		public string Type {
			get {
				return "file";
			}
		}
		
		public string MachineName {
			get { return machineName; }
		}
		
		public DirectoryInfo GetDirectoryInfo() {
			return new DirectoryInfo(baseDirectory);
		}
	}
}