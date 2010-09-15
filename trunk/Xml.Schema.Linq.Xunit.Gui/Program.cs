using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Xml.Schema.Linq.Xunit.Gui
{
    using G = global::Xunit.Gui;
    using A = global::System.Reflection.Assembly;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new G.RunnerForm(new string[] { "Xml.Schema.Linq.Xunit.dll" }));
        }
    }
}
