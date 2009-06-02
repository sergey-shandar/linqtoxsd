
-What is LINQ to XSD? 
	The LINQ to XSD technology provides .NET developers with support for typed XML programming. 
        LINQ to XSD contributes to the LINQ project (.NET Language Integrated Query); in particular, 
        LINQ to XSD enhances the existing LINQ to XML technology. 

-Features offered by this release
	•Comprehensive and canonical XML-schema-to object-model mapping. 
	•The generated C# classes model typed views on XML trees. 
	•Typed descendant and ancestor axes on XML object types. 
	•Discoverable object models including tool tips for XML schema constraints. 
	•Visual Studio integration through build tasks. 
	•Command-line interface for mapping tool. 

-Requirements
	• .Net 3.5 SDK
	• Visual Studio 2008 for integrated development experience
	• Operating Systems: Windows XP(SP2), Windows Vista, Windows Server 2003.

-Code
	•XOjects Solution
		o XObjects project : LINQ to XSD runtime API and code generation
		o XOTask project   : LINQ to XSD build task
		o CmdLine project  : LINQ to XSD command line utility

	•Sample projects
		o LinqToXsdCLI  : Sample for building LINQ to XSD apps without VS
		o LinqToXsdAPI  : Sample for using LINQ to XSD API
		o LinqToXsdVB   : Sample for using LINQ to XSD within VB project
		o LinqToXsdDemo : Sample scenarios using LINQ to XSD programming

- How to build samples
	• Set up the environment variable LinqToXsdBinDir (e.g. c:\LinqToXSDBin) before opening any Visual Studio instance.
	• Build XObjects solution, it will copy the following files to where you define as LinqToXsdBinDir
		o Xml.Schema.Linq.dll  //LinqToXSD runtime, and code generator
		o LinqToXsd.exe        //LinqToXSD command line utility
		o XOTask.dll           //Task for integrating XSD files to VisualStudio
		o LinqToXsd.targets    //Definition for LINQ to XSD projects
	
	• Open and build sample projects, they are designed to pick up LINQ to XSD files from %LinqToXsdBinDir%
	• You might see some access denied exception while running some tests that write out xml, make sure that output files have write access. 

- Command line integration
	• Look at LinqToXSDCLI sample to learn how to use LinqToXSD.exe utility to generate either C# code or a dll from XSD schemas

- Visual Studio integration
        • Set up the environment variable LinqToXsdBinDir (e.g. c:\LinqToXSDBin) before opening any Visual Studio instance.
	• Import LinqToXsd.targets template into your C# project by adding following lines into your project file (i.e. yourproject.csproj)
	        <PropertyGroup>
		    <LinqToXsdBinDir Condition="'$(LinqToXsdBinDir)' == ''">$(SolutionDir)</LinqToXsdBinDir>
		</PropertyGroup>
		<Import Project="$(LinqToXsdBinDir)\LinqToXsd.targets" />

		o You can use your favorite XML editor (e.g. notepad) to edit .csproj files, 
		o Make sure that your <Import Project="$(LinqToXsdBinDir)\LinqToXsd.targets" /> line is 
                   after any other Imports (e.g.   <Import Project="$(MSBuildBinPath)\Microsoft.CSharp.targets" />)
		o You can take a look at samples to see how it is done.
 
	• Open your project in Visual Studio
	• Add your XSD schemas to the project (i.e. right click on project->Add->Existing Item)
	• Change "Build Action" to LinqToXSDSchema on your XSD schemas
        • Build your project, after your first build XSD namespaces and types will be available in intellisense.
	• To see how you can integrate LINQ to XSD in your VB project please see LinqToXsdVB sample.
 
 
 