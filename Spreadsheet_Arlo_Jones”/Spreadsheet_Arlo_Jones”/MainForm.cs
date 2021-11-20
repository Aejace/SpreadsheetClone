// <copyright file="MainForm.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Spreadsheet_Arlo_Jones_
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
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
                    this.MainDataGridView.Rows[rowName].Cells[columnName].Style.BackColor =
                        Color.FromArgb((int)cellWhosePropertyChanged.BGColor);
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
                this.mainSpreadSheet.GetCellByRowAndColumn(i, 0).Text = "=B" + i;
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

            var rowIndexes = new List<int>();
            var columnIndexes = new List<int>();

            foreach (DataGridViewCell cell in this.MainDataGridView.SelectedCells)
            {
                rowIndexes.Add(cell.RowIndex);
                columnIndexes.Add(cell.ColumnIndex);
            }

            this.mainSpreadSheet.SetCellColor(rowIndexes, columnIndexes, (uint)colorDialog.Color.ToArgb());
        }

        /// <summary>
        /// Tells the spreadsheet to undo it's most recent action.
        /// </summary>
        /// <param name="sender"> The undo button that was clicked. </param>
        /// <param name="e"> Information about event. </param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mainSpreadSheet.Undo();
        }

        /// <summary>
        /// Tells the spreadsheet to redo it's most recently undone action.
        /// </summary>
        /// <param name="sender"> The redo button that was clicked. </param>
        /// <param name="e"> Information about event. </param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mainSpreadSheet.Redo();
        }

        /// <summary>
        /// Configures drop down buttons on click.
        /// </summary>
        /// <param name="sender"> The edit button that was clicked. </param>
        /// <param name="e"> Information about event. </param>
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.mainSpreadSheet.GetUndoCount() == 0)
            {
                this.undoToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.undoToolStripMenuItem.Enabled = true;
                this.undoToolStripMenuItem.Text = "Undo " + this.mainSpreadSheet.GetTopUndoString();
            }

            if (this.mainSpreadSheet.GetRedoCount() == 0)
            {
                this.redoToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = true;
                this.redoToolStripMenuItem.Text = "Redo " + this.mainSpreadSheet.GetTopRedoString();
            }
        }

        /// <summary>
        /// Saves the spreadsheet as an xml document.
        /// </summary>
        /// <param name="sender"> The save button that was clicked. </param>
        /// <param name="e"> Information about event. </param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML File | *.xml"; // Specifies the file type to be created.

            if (saveFileDialog.ShowDialog() != DialogResult.OK || saveFileDialog.FileName == string.Empty)
            {
                return;
            }

            var fileName = saveFileDialog.FileName;

            // Creates a stream writer to write text into the designated file.
            using (var sw = new FileStream(fileName, FileMode.Create))
            {
                this.mainSpreadSheet.SaveSpreadSheet(sw);
                sw.Close();
            }
        }

        /// <summary>
        /// Loads a saved spreadsheet from an xml document.
        /// </summary>
        /// <param name="sender"> The load button that was clicked. </param>
        /// <param name="e"> Information about event. </param>
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML File | *.xml";

            if (openFileDialog.ShowDialog() != DialogResult.OK || openFileDialog.FileName == string.Empty)
            {
                return;
            }

            this.ClearForm();

            var fileName = openFileDialog.FileName;
            this.mainSpreadSheet = new SpreadSheet(50, 26);
            this.mainSpreadSheet.CellPropertyChanged += this.SpreadSheetChangedEventHandler;

            // Creates a stream writer to write text into the designated file.
            using (var sr = new FileStream(fileName, FileMode.Open))
            {
                this.mainSpreadSheet.LoadSpreadSheet(sr);
                sr.Close();
            }
        }

        /// <summary>
        /// Clears the UI, used to make sure information from different spreadsheets don't overlap.
        /// </summary>
        private void ClearForm()
        {
            this.MainDataGridView.SelectAll();
            foreach (DataGridViewCell cell in this.MainDataGridView.SelectedCells)
            {
                cell.Value = string.Empty;
                cell.Style.BackColor = Color.White;
            }

            this.MainDataGridView.ClearSelection();
        }
    }
}
