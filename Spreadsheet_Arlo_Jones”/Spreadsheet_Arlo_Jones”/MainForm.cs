// <copyright file="MainForm.cs" company="Arlo Jones">
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
    using Cpts321;

    /// <summary>
    /// Partial class definition of MainForm, allows new fields and methods to be added to the class.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// SpreadSheet.
        /// </summary>
        private SpreadSheet mainSpreadSheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.mainSpreadSheet = new SpreadSheet(50, 26);
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

            this.mainSpreadSheet.CellPropertyChanged += new PropertyChangedEventHandler(this.SpreadSheetChangedEventHandler);
        }

        /// <summary>
        /// Updates the UI when a cell value changes.
        /// </summary>
        /// <param name="cellSender"> The cell that changed in spreadsheet. </param>
        /// <param name="cellValue"> The value of the cell that changed. </param>
        private void SpreadSheetChangedEventHandler(object cellSender, PropertyChangedEventArgs cellValue)
        {
            Cell cellWhosePropertyChanged = cellSender as Cell;
            int rowName = cellWhosePropertyChanged.RowIndexNumber;
            int columnName = cellWhosePropertyChanged.ColumnIndexNumber;
            this.MainDataGridView.Rows[rowName].Cells[columnName].Value = cellWhosePropertyChanged.Value;
        }

        /// <summary>
        /// Updates the appropriate cell's text in the spreadsheet.
        /// </summary>
        /// <param name="sender"> A data grid view cell. </param>
        /// <param name="e"> Contains the row and column index of the sender call. </param>
        private void MainDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell cellThatsBeingChanged = this.MainDataGridView.CurrentCell;
            cellThatsBeingChanged.Value = this.mainSpreadSheet.GetCellByRowAndColumn(e.RowIndex, e.ColumnIndex).Text;
        }

        /// <summary>
        /// Updates the appropriate cell's text in the spreadsheet.
        /// </summary>
        /// <param name="sender"> A data grid view cell. </param>
        /// <param name="e"> Contains the row and column index of the sender call. </param>
        private void MainDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cellThatsTextChanged = this.MainDataGridView.CurrentCell;
            this.mainSpreadSheet.GetCellByRowAndColumn(e.RowIndex, e.ColumnIndex).Text = cellThatsTextChanged.Value.ToString();
        }

        /// <summary>
        /// Runs a demo that put values into the A and B columns.
        /// </summary>
        /// <param name="sender"> Button that raised event. </param>
        /// <param name="e"> Event that was raised. </param>
        private void Button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int i = 0; i < 50; ++i)
            {
                this.mainSpreadSheet.GetCellByRowAndColumn(rand.Next(50), rand.Next(26)).Text = "Howdy";
            }

            for (int i = 0; i < this.mainSpreadSheet.RowCount(); ++i)
            {
                this.mainSpreadSheet.GetCellByRowAndColumn(i, 1).Text = "This Cell is B" + (i + 1);
            }

            for (int i = 0; i < this.mainSpreadSheet.RowCount(); ++i)
            {
                this.mainSpreadSheet.GetCellByRowAndColumn(i, 0).Text = "=B" + i;
            }
        }
    }
}
