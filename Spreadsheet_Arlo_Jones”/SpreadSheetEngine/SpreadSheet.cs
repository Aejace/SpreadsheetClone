// <copyright file="SpreadSheet.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates and manages a 2d array of cells.
    /// </summary>
    public class SpreadSheet
    {
        /// <summary>
        /// A 2d array that contains cells.
        /// </summary>
        private Cell[,] cellArray;

        /// <summary>
        /// The number of rows the spreadsheet has.
        /// </summary>
        private int numberOfRows;

        /// <summary>
        /// The number of columns the spreadsheet has.
        /// </summary>
        private int numberOfColumns;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadSheet"/> class.
        /// </summary>
        /// <param name="numRows"> The number of rows to be generated in the spreadsheet. </param>
        /// <param name="numColumns"> The number of columns to be generated in the spreadsheet. </param>
        public SpreadSheet(int numRows, int numColumns)
        {
            this.numberOfRows = numRows;
            this.numberOfColumns = numColumns;
            this.cellArray = new Cell[numRows, numColumns]; // Creates a new instance of a cellArray

            for (int i = 0; i < this.numberOfRows; ++i)
            {
                for (int j = 0; j < this.numberOfColumns; ++j)
                {
                    this.cellArray[i, j] = new SpreadSheetCell(i, j);
                    this.cellArray[i, j].PropertyChanged += new PropertyChangedEventHandler(this.CellPropertyChangedEventHandler); // Subscribe to each SpreadSheetCell's property changed event
                }
            }
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged;

        /// <summary>
        /// Gets the cell indicated by the row and column index values.
        /// </summary>
        /// <param name="rowIndex"> The row index of the cell desired. </param>
        /// <param name="columnIndex"> The column index of the cell desired. </param>
        /// <returns> Returns the cell at the index values given. </returns>
        public Cell GetCell(int rowIndex, int columnIndex)
        {
            return this.cellArray[rowIndex, columnIndex];
        }

        /// <summary>
        /// Gets a count of the number of rows.
        /// </summary>
        /// <returns> Returns the total number of rows in the spreadsheet. </returns>
        public int RowCount()
        {
            return this.numberOfRows;
        }

        /// <summary>
        /// Gets a count of the number of columns.
        /// </summary>
        /// <returns> Returns the total number of columns in the spreadsheet. </returns>
        public int ColumnCount()
        {
            return this.numberOfColumns;
        }

        /// <summary>
        /// Event handler sets the value of a cell when the text property of the cell has changed.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event. </param>
        /// <param name="e"> Contains information about the triggering event. </param>
        private void CellPropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            Cell cellWhosePropertyChanged = sender as Cell;
            if (e.PropertyName == "Text")
            {
                if (cellWhosePropertyChanged.Text.StartsWith("="))
                {

                }
                else
                {
                    cellWhosePropertyChanged.Value = cellWhosePropertyChanged.Text;
                }

                this.NotifyPropertyChanged(cellWhosePropertyChanged, cellWhosePropertyChanged.Value); // Notify subscribers (UI)
            }
        }

        /// <summary>
        /// NotifyPropertyChanged method inherited from the INotifyPropertyChanged interface.
        /// Method is called when the CellPropertyChangedEvent is fired.
        /// </summary>
        /// <param name="sender"> The cell that had a property change. </param>
        /// <param name="propertyName"> The value of the cell after its property was changed. </param>
        private void NotifyPropertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}
