﻿#region Using directives

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Mono.Addins;

#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("NetSync.Source.LocalFileSystem")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("NetSync.Source.LocalFileSystem")]
[assembly: AssemblyCopyright("Copyright 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]
[assembly: Addin("NetSync.Source.LocalFileSystem", "1.0")]
[assembly: AddinDescription("Allows crawling of files from the local file system.")]
[assembly: AddinDependency("NetSync", "1.0")]
[assembly: AddinDependency("NetSync.MetaData.FileSystem", "1.0")]
