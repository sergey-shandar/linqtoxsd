rem
rem Builds a console app while generating code for schema
rem Executable is called Total.exe
rem
rem Please add/change pathes as appropriate
rem Please run this from within a VS command prompt, preferably.
rem 
del Orders.cs
del *.dll
del *.exe
del *.pdb
rem set framework=c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5
set l2xsd=%LinqToXsdBinDir%
rem copy "%framework%\System.Core.dll" .
rem copy "%framework%\System.Xml.Linq.dll" .
copy "%l2xsd%\Xml.Schema.Linq.DLL" .
"%l2xsd%\LinqToXsd.exe" Orders.xsd
csc Total.cs Orders.cs /r:System.Core.dll /r:System.Xml.Linq.dll /r:Xml.Schema.Linq.dll
