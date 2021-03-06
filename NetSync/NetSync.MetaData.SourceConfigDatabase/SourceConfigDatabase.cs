﻿// <copyright file="LocalFileProperties.cs" company="IrcShark Team">
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
using Mono.Addins;
using NetSync.Core;

namespace NetSync.MetaData.SourceConfigDatabase
{
    /// <summary>
    /// Description of MyClass.
    /// </summary>
    [Extension("NetSync/MetaData/Database")]
    public class SourceConfigDatabase : IDatabase
    {
        public Uri[] Sources {
            get {
                UriBuilder builder = new UriBuilder();
                builder.Scheme = "file";
                builder.Host = System.Environment.MachineName;
                builder.Path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyMusic).Replace(Path.PathSeparator, '/');
                return new Uri[] { builder.Uri };
            }
        }
        
        public SynchronizableObject[] Objects {
            get {
                return new SynchronizableObject[0];
            }
        }
    }
}