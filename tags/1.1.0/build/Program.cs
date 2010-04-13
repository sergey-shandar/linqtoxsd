namespace build
{
	using S = global::System;
	using IO = global::System.IO;
	using D = global::System.CodeDom;
	using C = global::Microsoft.CSharp;
	using E = global::Microsoft.Build.BuildEngine;
	using SD = global::System.Diagnostics;

	static class Program
	{
		const string Company = "Microsoft";
		const string Version3 = "1.1.0";

		const string XObjects = "XObjects";
		const string XOTask = "XOTask";
		const string Cmdline = "Cmdline";

		// const string binDir = "c://bin";

		static string Revision()
		{
			using (var reader = new IO.StreamReader("../../../.svn/entries"))
			{
				reader.ReadLine();
				reader.ReadLine();
				reader.ReadLine();
				return reader.ReadLine();
			}
		}

		static void AssemblyAdd(
			this D.CodeCompileUnit unit, string name, string value)
		{
			var type = "global::System.Reflection." + name;
			var declaration = new D.CodeAttributeDeclaration(type);
			var expression = new D.CodePrimitiveExpression(value);
			declaration.Arguments.Add(new D.CodeAttributeArgument(expression));
			unit.AssemblyCustomAttributes.Add(declaration);
		}

		static void Create(string dir, string name, string version)
		{
			var unit = new D.CodeCompileUnit();
			unit.AssemblyAdd("AssemblyCompany", Company);
			unit.AssemblyAdd("AssemblyCopyright", "Copyright © " + Company);
			unit.AssemblyAdd("AssemblyVersion", version);
			unit.AssemblyAdd("AssemblyFileVersion", version);
			unit.AssemblyAdd("AssemblyInformationalVersion", version);
			unit.AssemblyAdd("AssemblyProduct", name);
			unit.AssemblyAdd("AssemblyTitle", name);
			var fileName = IO.Path.Combine(dir, "_info.cs");
			using (var writer = new IO.StreamWriter(fileName))
			{
				var provider = new C.CSharpCodeProvider();
				var options = new D.Compiler.CodeGeneratorOptions();
				provider.GenerateCodeFromCompileUnit(unit, writer, options);
			}
		}

		static void Build(E.Engine engine, string dir, string name, string version, string outDir)
		{
			dir = IO.Path.Combine(dir, name);
			Create(dir, name, version);
			if (!engine.BuildProjectFile(IO.Path.Combine(dir, name + ".csproj")))
			{
				throw new S.Exception("build failed");
			}
			var path = IO.Path.Combine(dir, "bin\\Debug");
			var files = IO.Directory.GetFiles(path);
			// copy all files.
			foreach (var file in files)
			{
				var outFile = IO.Path.Combine(outDir, IO.Path.GetFileName(file));
				IO.File.Copy(file, outFile, true);
			}
		}

		[S.STAThread]
		static void Main(string[] args)
		{
			// binary folder.
			var version = Version3 + "." + Revision();
			var fileName = "linqtoxsd." + version + "-bin";
			var dirName = fileName;
			try
			{
				IO.Directory.Delete(dirName, true);
			}
			catch (IO.DirectoryNotFoundException)
			{
			}
			IO.Directory.CreateDirectory(dirName);
			{
				var engine = new E.Engine();
				{
					// Instantiate a new FileLogger to generate build log
					var logger = new E.FileLogger();
					// Set the logfile parameter to indicate the log destination
					logger.Parameters = @"logfile=build.log";
					engine.RegisterLogger(logger);
				}
				Build(engine, "..\\..\\..", XObjects, version, dirName);
				Build(engine, "..\\..\\..\\XObjects", XOTask, version, dirName);
				Build(engine, "..\\..\\..\\XObjects", Cmdline, version, dirName);
				engine.UnregisterAllLoggers();
			}
			// zipping:
			var zipFile = fileName + ".zip";
			IO.File.Delete(zipFile);
			{
				var zipProcess = new SD.ProcessStartInfo(
					"\"c:\\program files\\7-Zip\\7z.exe\"",
					"a " +
					zipFile + " " +
					dirName);
				zipProcess.UseShellExecute = false;
				var process = SD.Process.Start(zipProcess);
				process.WaitForExit();
			}
		}
	}
}
