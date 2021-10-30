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
        /// .
        /// </summary>
        private Dictionary<string, double> cellNameandValueDictionary; // TODO: Might need to be internal

        /// <summary>
        /// A 2d array that contains cells.
        /// </summary>
        private Cell[,] cellArray;

        /// <summary>
        /// Lists of cells that are dependent on cell indicated by array position.
        /// </summary>
        private List<string>[,] listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition;

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
            this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition = new List<string>[numRows, numColumns]; // Creates a new instance of listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition
            for (int i = 0; i < numRows; ++i)
            {
                for (int j = 0; j < numColumns; ++j)
                {
                    this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[i, j] = new List<string>();
                }
            }

            this.cellNameandValueDictionary = new Dictionary<string, double>();

            for (int i = 0; i < this.numberOfRows; ++i)
            {
                for (int j = 0; j < this.numberOfColumns; ++j)
                {
                    this.cellArray[i, j] = new SpreadSheetCell(i, j);
                    this.cellArray[i, j].PropertyChanged += new PropertyChangedEventHandler(this.CellPropertyChangedEventHandler); // Subscribe to each SpreadSheetCell's property changed event
                    string columnLetter = Convert.ToChar(i + 65).ToString();
                    // this.cellNameandValueDictionary.Add(columnLetter += (j + 1).ToString(), Convert.ToDouble(this.cellArray[i, j].Value));
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
        public Cell GetCellByRowAndColumn(int rowIndex, int columnIndex)
        {
            return this.cellArray[rowIndex, columnIndex];
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public string GetCellName(int rowIndex, int columnIndex)
        {
            string columnName = Convert.ToChar(columnIndex + 65).ToString();
            string rowName = rowIndex.ToString();
            return columnName += rowName;
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
            Cell cellWhosePropertyChanged = sender as Cell; // the cell whose property changed is the sender of the event.
            if (e.PropertyName == "Text")
            { // If the property that changed was the text property.
                if (cellWhosePropertyChanged.Text.StartsWith("="))
                { // If the text is an expression
                    // Remove old subscriptions, requires iterating through the double array of cells to find any instance where the cell whose property changed is subscribed to the value change of another cell.
                    string cellWhosePropertyChangedName = this.GetCellName(cellWhosePropertyChanged.RowIndexNumber, cellWhosePropertyChanged.ColumnIndexNumber);
                    for (int i = 0; i < this.numberOfRows; ++i)
                    {
                        for (int j = 0; j < this.numberOfColumns; ++j)
                        {
                            if (this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[i, j].Contains(cellWhosePropertyChangedName))
                            { // If cell name is found in list of subscribers, remove it.
                                this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[i, j].Remove(cellWhosePropertyChangedName);
                            }
                        }
                    }

                    // Evaluate text
                    cellWhosePropertyChanged.Value = this.Evaluate(cellWhosePropertyChanged, this.cellNameandValueDictionary);
                }
                else
                { // Text is not an expression, set value equal to text.
                    cellWhosePropertyChanged.Value = cellWhosePropertyChanged.Text;
                }
            }
            else if (e.PropertyName == "Value")
            { // If the property that changed was the cell's value.
                
                string cellWhosePropertyChangedName = this.GetCellName(cellWhosePropertyChanged.RowIndexNumber, cellWhosePropertyChanged.ColumnIndexNumber);
                if (double.TryParse(cellWhosePropertyChanged.Value, out double cellValue))
                {
                    this.cellNameandValueDictionary[cellWhosePropertyChangedName] = cellValue;
                }
                else
                {
                    this.cellNameandValueDictionary[cellWhosePropertyChangedName] = 0;
                }

                // Evaluate every cell that is subscribed to the cell that's value was changed.
                int listCount = this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[cellWhosePropertyChanged.RowIndexNumber, cellWhosePropertyChanged.ColumnIndexNumber].Count;
                for (int i = 0; i < listCount; ++i)
                {
                    string cellName = this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[cellWhosePropertyChanged.RowIndexNumber, cellWhosePropertyChanged.ColumnIndexNumber][i];
                    // Convert name of cell into row and column values.
                    int columnIndex = cellName[0] - 65;
                    string rowIndex = string.Empty;
                    for (int j = 1; j < cellName.Count(); ++j)
                    {
                        rowIndex += cellName[j];
                    }

                    // Evaluate the cells text and update it's value.
                    this.cellArray[int.Parse(rowIndex), columnIndex].Value = this.Evaluate(this.GetCellByRowAndColumn(int.Parse(rowIndex), columnIndex), this.cellNameandValueDictionary);
                }
            }

            this.NotifyPropertyChanged(cellWhosePropertyChanged, cellWhosePropertyChanged.Value); // Notify subscribers (UI)
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="cellThatsBeingEvaluated"></param>
        /// <param name="cellValuesByName"></param>
        /// <returns></returns>
        private string Evaluate(Cell cellThatsBeingEvaluated, Dictionary<string, double> cellValuesByName)
        {
            string cellName = this.GetCellName(cellThatsBeingEvaluated.RowIndexNumber, cellThatsBeingEvaluated.ColumnIndexNumber);
            ExpressionTree expressionTree = new ExpressionTree(cellThatsBeingEvaluated.Text.Substring(1), cellValuesByName);
            List<string> cellsReferencedInExpression = expressionTree.GetVariables();

            // Subscribe to property changes in all cells the cell that is being evaluated is dependent on.
            foreach (string variableCell in cellsReferencedInExpression)
            {
                // Convert name of cell into row and column values.
                int columnIndex = variableCell[0] - 65;
                string rowIndex = string.Empty;
                for (int j = 1; j < variableCell.Count(); ++j)
                {
                    rowIndex += variableCell[j];
                }

                this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[int.Parse(rowIndex), columnIndex].Add(cellName);
            }

            return expressionTree.Evaluate().ToString();
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
