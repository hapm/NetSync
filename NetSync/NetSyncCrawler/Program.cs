using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using Mono.Addins;
using NetSync.Core;

// <copyright file="CommonFileProperties.cs" company="IrcShark Team">
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
namespace NetSyncCrawler
{
	using System;

	class Program
	{
		private static List<ISourceFactory> sourceFactories;
		
		public static void Main(string[] args)
		{
			Console.WriteLine(String.Format("Loading NetSync {0} ...", typeof(SynchronizableObject).Assembly.GetName().Version));
			sourceFactories = new List<ISourceFactory>();
			AddinManager.Initialize(ConfigurationManager.AppSettings["ConfigPath"]);
			AddinManager.Registry.Update();
			sourceFactories.AddRange((ISourceFactory[])AddinManager.GetExtensionObjects(typeof(ISourceFactory)));
			if (sourceFactories.Count == 0) {
				Console.WriteLine("No source types available");
			}
			else {
				Console.WriteLine(String.Format("Loaded {0} source types", sourceFactories.Count));
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}