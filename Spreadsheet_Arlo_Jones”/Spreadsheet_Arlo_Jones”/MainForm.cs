// <copyright file="Form1.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Spreadsheet_Arlo_Jones_
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
    /// Partial class definition of MainForm, allows new fields and methods to be added to the class.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Executes when MainForm is loaded. MainForm contains a dataGrid, which is set to have rows 1 - 50 and columns A-Z on load.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that raised the event.</param>
        /// <param name="e"> Contains event data. </param>
        private void Form1_Load(object sender, EventArgs e)
        {
            for (char columnName = 'A'; columnName <= 'Z'; ++columnName)
            {
                this.MainDataGridView.Columns.Add(columnName.ToString(), columnName.ToString());
            }

            for (int rowNumber = 0; rowNumber < 50; ++rowNumber)
            {
                this.MainDataGridView.Rows.Add();
                this.MainDataGridView.Rows[rowNumber].HeaderCell.Value = rowNumber.ToString();
            }

            this.MainDataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }
    }
}
