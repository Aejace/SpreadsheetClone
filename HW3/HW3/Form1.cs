// <copyright file="Form1.cs" company="Arlo Jones 011778052">
// Copyright (c) Aejace. All rights reserved.
// </copyright>

namespace HW3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
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
        /// Executes when Form1 is loaded. This program relies on user input. There are no tasks that need to happen on load.
        /// However, this method is still required to have the form load at all.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Executes when the text in textBox1 is changed. This method doesnt need to do anything in particular when the user changes the text,
        /// except to update the text feild which happens automatically.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Loads text from a stream into textBox1.
        /// </summary>
        /// <param name="sr"> sr is a TextReader, that can handle streams of data. </param>
        private void LoadText(TextReader sr)
        {
            this.textBox1.Text = sr.ReadLine();
        }

        /// <summary>
        /// Executes when "From File" File menu option is selected by user.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void FromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != string.Empty)
            {
                string input = openFileDialog1.FileName;
                using (StreamReader sr = new StreamReader(input))
                {
                    this.LoadText(sr);
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// Executes when "Fibonacci50" File menu option is selected by user.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void Fibonacci50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fiftyNumbers = new FibonacciTextReader(50);
            LoadText();
        }

        /// <summary>
        /// Executes when "Fibonacci100" File menu option is selected by user.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void Fibonacci100ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Executes when "ToFile" File menu option is selected by user.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void ToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text Files | *.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
            {
                string input = saveFileDialog1.FileName;
                using (StreamWriter sw = new StreamWriter(input))
                {
                    sw.WriteLine(this.textBox1.Text);
                    sw.Close();
                }
            }
        }
    }
}
