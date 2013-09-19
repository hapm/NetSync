// <copyright file="RecursiveFileEnumerator.cs" company="IrcShark Team">
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

namespace NetSync.Source.LocalFileSystem
{
	/// <summary>
	/// Description of RecursiveFileEnumerator.
	/// </summary>
	public class RecursiveFileEnumerator : IEnumerator<FileInfo>
	{
		private IEnumerator<FileInfo> files;
		private IEnumerator<DirectoryInfo> dirs;
		private DirectoryInfo baseDir;
		private RecursiveFileEnumerator currentSubDir;
		
		public delegate void ExceptionThrownEventHandler(Exception ex);
		
		public event ExceptionThrownEventHandler ExceptionThrown;
		
		public RecursiveFileEnumerator(DirectoryInfo baseDirectory)
		{
			baseDir = baseDirectory;
			files = baseDir.EnumerateFiles().GetEnumerator();
			dirs = baseDir.EnumerateDirectories().GetEnumerator();
		}
		
		public FileInfo Current {
			get {
				if (files != null)
					return files.Current;
				
				if (currentSubDir != null)
					return currentSubDir.Current;
				
				return null;
			}
		}
		
		public string CurrentRelativePath {
			get {
				if (files != null) {
					if (files.Current != null)
						return files.Current.Name;
					else
						return null;
				}
				
				if (currentSubDir != null && currentSubDir.Current != null) 
					return dirs.Current.Name + Path.PathSeparator + currentSubDir.CurrentRelativePath;
				else 
					return null;
			}
		}
		
		object System.Collections.IEnumerator.Current {
			get {
				return Current;
			}
		}
		
		public void Dispose()
		{
			AllToNull();
		}

		private void AllToNull()
		{
			if (files != null) {
				files.Dispose();
				files = null;
			}

			if (dirs != null) {
				dirs.Dispose();
				dirs = null;
			}

			if (currentSubDir != null) {
				currentSubDir.Dispose();
				currentSubDir = null;
			}
		}
		
		public bool MoveNext()
		{
			if (files != null) {
				if (!files.MoveNext()) {
					files.Dispose();
					files = null;
				}
				else
					return true;
			}
			
			if (dirs == null)
				return false;
			
			while (currentSubDir == null || !currentSubDir.MoveNext()) {
				if (currentSubDir != null) {
					currentSubDir.Dispose();
					currentSubDir = null;
				}
				
				while (currentSubDir == null) {
					if (dirs.MoveNext()) {
						try {
							currentSubDir = new RecursiveFileEnumerator(dirs.Current);
							currentSubDir.ExceptionThrown += new ExceptionThrownEventHandler(subDir_ExceptionThrown);
						}
						catch (Exception ex) {
							OnExceptionThrown(ex);
						}
					}
					else {
						dirs.Dispose();
						dirs = null;
						return false;
					}
				}
			}
			
			return true;
		}

		private void subDir_ExceptionThrown(Exception ex)
		{
			OnExceptionThrown(ex);
		}

		private void OnExceptionThrown(Exception ex)
		{
			if (ExceptionThrown != null)
				ExceptionThrown(ex);
		}
		
		public void Reset()
		{
			AllToNull();
			files = baseDir.EnumerateFiles().GetEnumerator();
			dirs = baseDir.EnumerateDirectories().GetEnumerator();
		}
	}
}
