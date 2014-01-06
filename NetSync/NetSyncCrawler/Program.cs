// <copyright file="Program.cs" company="IrcShark Team">
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
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq.Expressions;
    
    using Mono.Addins;
    
    using NetSync.Core;
    using NetSync.Core.Filter;
    using NetSync.Core.Properties;

    class Program
    {
        private static ExtensionNodeList<TypeExtensionNode<SourceFactoryAttribute>> sourceFactories; 
        private static IDatabase database;
        
        public static void Main(string[] args)
        {
            Console.WriteLine(String.Format("Loading NetSync {0} ...", typeof(SynchronizableObject).Assembly.GetName().Version));
            AddinManager.Initialize(ConfigurationManager.AppSettings["ConfigPath"]);
            AddinManager.Registry.Update();
            sourceFactories = AddinManager.GetExtensionNodes<TypeExtensionNode<SourceFactoryAttribute>>(typeof(ISourceFactory));
            IFilter defaultFilter = new PropertyCompareFilter("file.Extension", new UnboundConstantGenericProperty<String>(".mp3"));
            IPropertyFactory[] propertyFactories = (IPropertyFactory[])AddinManager.GetExtensionObjects(typeof(IPropertyFactory));
            if (sourceFactories.Count == 0) {
                Console.WriteLine("No source types available, exiting...");
            }
            else {
                Console.WriteLine(String.Format("Loaded {0} source types", sourceFactories.Count));
            }
            
            foreach (TypeExtensionNode ext in AddinManager.GetExtensionNodes("NetSync/MetaData/Database")) {
                database = (IDatabase)ext.GetInstance();
                break;
            }
            
            if (database == null) {
                Console.WriteLine("No database loaded, exiting...");
            }
            else if (database.Sources.Length == 0) {
                Console.WriteLine("No sources defined in database, exiting...");
            }
            else {
                Console.WriteLine(String.Format("Begin crawling on {0} sources", database.Sources.Length));
                foreach (Uri sourceUri in database.Sources) {
                    ISourceFactory factory = GetSourceFactory(sourceUri);
                    if (factory == null) {
                        Console.WriteLine("No source factory found for {0}", sourceUri);
                        continue;
                    }
                    
                    ISource src = factory.GetSource(sourceUri);
                    int i = 0;
                    int matching = 0;
                    foreach (SynchronizableObject obj in src) {
                        foreach (IPropertyFactory propFac in propertyFactories) {
                            if (propFac.CanHandle(obj)) {
                                propFac.CreateProperties(obj);
                                if (defaultFilter.Matches(obj)) {
                                    Console.WriteLine("{0}: ", obj.Uri);
                                    matching++;
                                }
                            }
                        }
                        i++;
                    }
                    
                    Console.WriteLine(String.Format("{0}: {1} objects, {2} matching", sourceUri, i, matching));
                }
            }
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
        
        private static ISourceFactory GetSourceFactory(Uri sourceId) {
            foreach (TypeExtensionNode<SourceFactoryAttribute> sf in sourceFactories) {
                if (sf.Data.Type.Equals(sourceId.Scheme)) {
                    ISourceFactory result = (ISourceFactory)sf.GetInstance(typeof(ISourceFactory));
                    if (result.CanHandle(sourceId))
                        return result;
                }
            }
            
            return null;
        }
    }
}