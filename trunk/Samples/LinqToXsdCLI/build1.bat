rem
rem Builds a console app while generating a DLL for schema
rem Executable is called Total.exe
rem 
rem Please add/change pathes as appropriate.
rem Please run this from within a VS command prompt, preferably.
rem 
del *.dll
del *.exe
del *.pdb
set framework=c:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5
set l2xsd=%LinqToXsdBinDir%
copy "%framework%\System.Core.dll" .
copy "%framework%\System.Xml.Linq.dll" .
copy "%l2xsd%\Xml.Schema.Linq.DLL" .
"%l2xsd%\LinqToXsd.exe" Orders.xsd /lib:Orders.dll
csc Total.cs /r:System.Core.dll /r:System.Xml.Linq.dll /r:Xml.Schema.Linq.dll /r:Orders.dll
