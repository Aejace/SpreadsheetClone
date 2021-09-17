// <copyright file="Form1.cs" company="Arlo Jones 011778052">
// Copyright (c) Arlo Jones 011778052. All rights reserved.
// </copyright>

namespace HW2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Partial class definition of Form1, allows new fields and methods to be added to the class.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Executes when Form1 is loaded. On load, 10000 random integers ranging in value from 0 to 20000
        /// are generated and added to a list. The number of unique values in the list are determined using
        /// several different methods, and the results of those methods are displayed in textBox1 in Form1.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Declare local variables.
            var rand = new Random();
            System.Collections.Generic.List<int> tenKRandomInts = new List<int>();
            string outputStringForTextBox1;
            DistinctNumberCounter distinctNumberCounter = new DistinctNumberCounter();

            // Create 10000 random numbers between 0 and 20000 and put them in list "tenKRandomInts".
            for (int i = 0; i < 10000; ++i)
            {
                tenKRandomInts.Add(rand.Next(0, 20000));
            }

            // Build string
            outputStringForTextBox1 = "HashSet Method: ";
            outputStringForTextBox1 += distinctNumberCounter.HashSetMethod(tenKRandomInts);
            outputStringForTextBox1 += " unique numbers \r\n";
            outputStringForTextBox1 += "Explination about time and space complexity \r\n \r\n";
            outputStringForTextBox1 += "O(1) Storage Method: ";
            outputStringForTextBox1 += distinctNumberCounter.ConstantStorageMethod(tenKRandomInts);
            outputStringForTextBox1 += " unique numbers \r\n";
            outputStringForTextBox1 += "Explination about time and space complexity \r\n \r\n";
            outputStringForTextBox1 += "Sorted Method: ";
            outputStringForTextBox1 += distinctNumberCounter.SortedMethod(tenKRandomInts);
            outputStringForTextBox1 += " unique numbers \r\n";
            outputStringForTextBox1 += "Explination about time and space complexity \r\n \r\n";

            // output string to textBox1
            this.textBox1.Text = outputStringForTextBox1;
        }
    }
}
