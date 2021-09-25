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
        /// except to update the text field which happens automatically.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void MainTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Loads text from a stream into mainTextBox.
        /// </summary>
        /// <param name="sr"> sr is a TextReader, that can handle streams of data. </param>
        private void LoadText(TextReader sr)
        {
            this.mainTextBox.Text = string.Empty; // Clears the text box for new input.
            string currentLine = string.Empty;

            // Iterates until the text reader hits a null value, signaling the end of the file.
            while (currentLine != null)
            {
                currentLine = sr.ReadLine();
                this.mainTextBox.Text += currentLine; // updates mainTextBox with text from the text reader
            }
        }

        /// <summary>
        /// Executes when "From File" File menu option is selected by user. It opens a file dialog
        /// wherein the user can select a text file they would like to load. This information is passed to
        /// LoadText, which then loads the text from the file into the mainTextBox.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void FromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files | *.txt"; // Specifies that the dialog should only allow for loading in text files.
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != string.Empty)
            {
                string fileName = openFileDialog1.FileName;

                // Creates a stream reader to read text from the designated file.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    this.LoadText(sr);
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// Executes when "Fibonacci50" File menu option is selected by user. This creates a FibonacciTextReader
        /// object, with 50 fibonacci numbers, then loads it into the mainTextBox via the LoadText method.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void Fibonacci50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader fiftyNumbers = new FibonacciTextReader(50);
            this.LoadText(fiftyNumbers);
        }

        /// <summary>
        /// Executes when "Fibonacci100" File menu option is selected by user. This creates a FibonacciTextReader
        /// object, with 100 fibonacci numbers, then loads it into the mainTextBox via the LoadText method.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void Fibonacci100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader oneHundredNumbers = new FibonacciTextReader(100);
            this.LoadText(oneHundredNumbers);
        }

        /// <summary>
        /// Executes when "To File" File menu option is selected by user. It opens a file dialog
        /// wherein the user can designate where they would like to save a text file and what to name the file.
        /// Then, the text in mainTextBox is written into the file.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void ToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text Files | *.txt"; // Specifies the file type to be created.

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
            {
                string fileName = saveFileDialog1.FileName;

                // Creates a stream writer to write text into the designated file.
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(this.mainTextBox.Text);
                    sw.Close();
                }
            }
        }
    }
}
