// <copyright file="MainForm.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Spreadsheet_Arlo_Jones_
{
    using Cpts321;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Partial class definition of MainForm, allows new fields and methods to be added to the class.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// SpreadSheet.
        /// </summary>
        private readonly SpreadSheet mainSpreadSheet;

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
            for (var columnName = 'A'; columnName <= 'Z'; ++columnName)
            {
                this.MainDataGridView.Columns.Add(columnName.ToString(), columnName.ToString());
            }

            for (var rowNumber = 0; rowNumber < 50; ++rowNumber)
            {
                this.MainDataGridView.Rows.Add();
                this.MainDataGridView.Rows[rowNumber].HeaderCell.Value = rowNumber.ToString();
            }

            this.MainDataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            this.mainSpreadSheet.CellPropertyChanged += this.SpreadSheetChangedEventHandler;
        }

        /// <summary>
        /// Updates the UI when a cell value changes.
        /// </summary>
        /// <param name="cellSender"> The cell that changed in spreadsheet. </param>
        /// <param name="cellProperty"> The property of the cell that changed. </param>
        private void SpreadSheetChangedEventHandler(object cellSender, PropertyChangedEventArgs cellProperty)
        {
            if (!(cellSender is Cell cellWhosePropertyChanged))
            {
                return;
            }

            var rowName = cellWhosePropertyChanged.RowIndexNumber;
            var columnName = cellWhosePropertyChanged.ColumnIndexNumber;

            switch (cellProperty.PropertyName)
            {
                case "Value":
                    this.MainDataGridView.Rows[rowName].Cells[columnName].Value = cellWhosePropertyChanged.Value;
                    break;
                case "Color":
                    this.MainDataGridView.Rows[rowName].Cells[columnName].Style.BackColor = Color.FromArgb((int)cellWhosePropertyChanged.BGColor);
                    break;
            }
        }

        /// <summary>
        /// Updates the appropriate cell's text in the spreadsheet.
        /// </summary>
        /// <param name="sender"> A data grid view cell. </param>
        /// <param name="e"> Contains the row and column index of the sender call. </param>
        private void MainDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var cellThatIsBeingChanged = this.MainDataGridView.CurrentCell;
            cellThatIsBeingChanged.Value = this.mainSpreadSheet.GetCellByRowAndColumn(e.RowIndex, e.ColumnIndex).Text;
        }

        /// <summary>
        /// Updates the appropriate cell's text in the spreadsheet.
        /// </summary>
        /// <param name="sender"> A data grid view cell. </param>
        /// <param name="e"> Contains the row and column index of the sender call. </param>
        private void MainDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cellWhoseTextChanged = this.MainDataGridView.CurrentCell;
            this.mainSpreadSheet.SetCellText(e.RowIndex, e.ColumnIndex, cellWhoseTextChanged.Value.ToString());
        }

        /// <summary>
        /// Runs a demo that put values into the A and B columns.
        /// </summary>
        /// <param name="sender"> Button that raised event. </param>
        /// <param name="e"> Event that was raised. </param>
        private void Button1_Click(object sender, EventArgs e)
        {
            var rand = new Random();
            for (var i = 0; i < 50; ++i)
            {
                this.mainSpreadSheet.GetCellByRowAndColumn(rand.Next(50), rand.Next(26)).Text = "Howdy";
            }

            for (var i = 0; i < this.mainSpreadSheet.RowCount(); ++i)
            {
                this.mainSpreadSheet.GetCellByRowAndColumn(i, 1).Text = "This Cell is B" + (i + 1);
            }

            for (var i = 0; i < this.mainSpreadSheet.RowCount(); ++i)
            {
                this.mainSpreadSheet.GetCellByRowAndColumn(i, 0).Text = "=B" + i.ToString();
            }
        }

        /// <summary>
        /// Allows user to change the background color of a cell or set of selected cells.
        /// </summary>
        /// <param name="sender"> Change Cell Color button. </param>
        /// <param name="e"> Information about event. </param>
        private void ChangeCellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (DataGridViewCell cell in this.MainDataGridView.SelectedCells)
            {
                var cellThatIsBeingChanged = this.mainSpreadSheet.GetCellByRowAndColumn(cell.RowIndex, cell.ColumnIndex);
                this.mainSpreadSheet.SetCellColor(cell.RowIndex, cell.ColumnIndex, (uint)colorDialog.Color.ToArgb());
            }
        }

        /// <summary>
        /// Tells the spreadsheet to undo it's most recent action.
        /// </summary>
        /// <param name="sender"> The undo button that was clicked. </param>
        /// <param name="e"> Information about event. </param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainSpreadSheet.Undo();
        }

        /// <summary>
        /// Tells the spreadsheet to redo it's most recently undone action.
        /// </summary>
        /// <param name="sender"> The redo button that was clicked. </param>
        /// <param name="e"> Information about event. </param>
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mainSpreadSheet.Redo();
        }
    }
}
