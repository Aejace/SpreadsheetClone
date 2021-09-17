// <copyright file="Program.cs" company="Arlo Jones 011778052">
// Copyright (c) Arlo Jones 011778052. All rights reserved.
// </copyright>

namespace HW2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Creation of class "Program" so that method "Main" has a class it can be called from.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
