// <copyright file="SpreadSheet.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

using System.CodeDom;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Creates and manages a 2d array of cells.
    /// </summary>
    public class SpreadSheet
    {
        /// <summary>
        /// Dictionary containing the values of cells, accessible by name.
        /// </summary>
        private readonly Dictionary<string, double> cellNameAndValueDictionary;

        /// <summary>
        /// A 2d array that contains cells.
        /// </summary>
        private readonly Cell[,] cellArray;

        /// <summary>
        /// Lists of cells that are dependent on cell indicated by array position.
        /// </summary>
        private readonly List<string>[,] listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition;

        /// <summary>
        /// The number of rows the spreadsheet has.
        /// </summary>
        private readonly int numberOfRows;

        /// <summary>
        /// The number of columns the spreadsheet has.
        /// </summary>
        private readonly int numberOfColumns;

        /// <summary>
        /// An instantiation of UndoAndRedo class, allows spreadsheet to store cell data so it can undo and redo changes to those cells.
        /// </summary>
        private readonly UndoAndRedo undoRedo = new UndoAndRedo();

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
            for (var i = 0; i < numRows; ++i)
            {
                for (var j = 0; j < numColumns; ++j)
                {
                    this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[i, j] = new List<string>();
                }
            }

            this.cellNameAndValueDictionary = new Dictionary<string, double>();

            for (var i = 0; i < this.numberOfRows; ++i)
            {
                for (var j = 0; j < this.numberOfColumns; ++j)
                {
                    this.cellArray[i, j] = new SpreadSheetCell(i, j);
                    this.cellArray[i, j].PropertyChanged += this.CellPropertyChangedEventHandler; // Subscribe to each SpreadSheetCell's property changed event
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
        /// Returns a cell name given it's row and column indexes.
        /// </summary>
        /// <param name="rowIndex"> The row index of the cell. </param>
        /// <param name="columnIndex"> The column index of the cell. </param>
        /// <returns> The cell name Ex. "A23". </returns>
        public string GetCellName(int rowIndex, int columnIndex)
        {
            var columnName = Convert.ToChar(columnIndex + 65).ToString();
            var rowName = rowIndex.ToString();
            return columnName + rowName;
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
        /// Sets the text of a specific cell in cell array.
        /// </summary>
        /// <param name="rowIndex"> The row index of a cell in cell array. </param>
        /// <param name="columnIndex"> The column index of a cell in cell array. </param>
        /// <param name="text"> New text value for the cell. </param>
        public void SetCellText(int rowIndex, int columnIndex, string text)
        {
            var command = new CellTextCommand(this.cellArray[rowIndex, columnIndex], this.cellArray[rowIndex, columnIndex].Text);
            var commandList = new List<ICommand> { command };
            this.AddUndo(commandList);
            this.cellArray[rowIndex, columnIndex].Text = text;
        }

        /// <summary>
        /// Sets the color of a specific cell in cell array.
        /// </summary>
        /// <param name="rowIndexes"> List of row indexes of cells in cell array. </param>
        /// <param name="columnIndexes"> List of column indexes of cells in cell array. </param>
        /// <param name="color"> New color value for the cell. </param>
        public void SetCellColor(List<int> rowIndexes, List<int> columnIndexes, uint color)
        {
            var commandList = new List<ICommand>();
            for (var i = 0; i < rowIndexes.Count; ++i)
            {
                var command = new CellColorCommand(this.cellArray[rowIndexes[i], columnIndexes[i]], this.cellArray[rowIndexes[i], columnIndexes[i]].BGColor);
                commandList.Add(command);
                this.cellArray[rowIndexes[i], columnIndexes[i]].BGColor = color;
            }

            this.AddUndo(commandList);
        }

        /// <summary>
        /// Adds a list of ICommands to undoRedo.
        /// </summary>
        /// <param name="commands"> List of ICommand objects to be added to undo stack. </param>
        public void AddUndo(List<ICommand> commands)
        {
            this.undoRedo.AddUndo(commands);
        }

        /// <summary>
        /// Undoes most recent change made to spreadsheet.
        /// </summary>
        public void Undo()
        {
            this.undoRedo.Undo();
        }

        /// <summary>
        /// Redoes actions undone by Undo.
        /// </summary>
        public void Redo()
        {
            this.undoRedo.Redo();
        }

        /// <summary>
        /// Gets count of items in undoStack in undoRedo.
        /// </summary>
        /// <returns> Count if items in undoStack in undoRedo. </returns>
        public int GetUndoCount()
        {
            return this.undoRedo.UndoCount();
        }

        /// <summary>
        /// Gets count of items in redoStack in undoRedo.
        /// </summary>
        /// <returns> Count if items in redoStack in undoRedo. </returns>
        public int GetRedoCount()
        {
            return this.undoRedo.RedoCount();
        }

        /// <summary>
        /// Gets a string indicating what undoing the top undo item will change.
        /// </summary>
        /// <returns> String indicating what undoing the top undo item will change. </returns>
        public string GetTopUndoString()
        {
            return this.undoRedo.GetUndoCommandUIString();
        }

        /// <summary>
        /// Gets a string indicating what redoing the top redo item will change.
        /// </summary>
        /// <returns> String indicating what redoing the top redo item will change. </returns>
        public string GetTopRedoString()
        {
            return this.undoRedo.GetRedoCommandUIString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        public void SaveSpreadSheet(Stream stream)
        {
            var writer = XmlWriter.Create(stream);
            writer.WriteStartDocument();
            writer.WriteStartElement("Spreadsheet");

            foreach (var cell in this.cellArray)
            {
                if (cell.Text == string.Empty && cell.BGColor == uint.MaxValue)
                {
                    continue;
                }

                writer.WriteStartElement("Cell");
                writer.WriteAttributeString("RowIndex", cell.RowIndexNumber.ToString());
                writer.WriteAttributeString("ColumnIndex", cell.ColumnIndexNumber.ToString());
                writer.WriteAttributeString("CellText", cell.Text);
                writer.WriteAttributeString("CellBackgroundColor", cell.BGColor.ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public void LoadSpreadSheet(Stream stream)
        {
            var spreadSheetDocument = new XmlDocument();
            spreadSheetDocument.Load(stream);

            foreach (XmlNode node in spreadSheetDocument.DocumentElement.ChildNodes)
            {
                var rowIndex = int.Parse(node.Attributes.GetNamedItem("RowIndex").Value);
                var columnIndex = int.Parse(node.Attributes.GetNamedItem("ColumnIndex").Value);
                var cellText = node.Attributes.GetNamedItem("CellText").Value;
                var cellColor = uint.Parse(node.Attributes.GetNamedItem("CellBackgroundColor").Value);

                var cell = this.cellArray[rowIndex, columnIndex];
                cell.Text = cellText;
                cell.BGColor = cellColor;
            }
        }

        /// <summary>
        /// Event handler sets the value of a cell when the text property of the cell has changed.
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event. </param>
        /// <param name="e"> Contains information about the triggering event. </param>
        private void CellPropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            var cellWhosePropertyChanged = sender as Cell; // the cell whose property changed is the sender of the event.
            switch (e.PropertyName)
            {
                case "Text":
                    {
                        // If the property that changed was the text property.
                        if (cellWhosePropertyChanged != null && cellWhosePropertyChanged.Text.StartsWith("="))
                        { // If the text is an expression
                          // Remove old subscriptions, requires iterating through the double array of cells to find any instance where the cell whose property changed is subscribed to the value change of another cell.
                            var cellWhosePropertyChangedName = this.GetCellName(cellWhosePropertyChanged.RowIndexNumber, cellWhosePropertyChanged.ColumnIndexNumber);
                            for (var i = 0; i < this.numberOfRows; ++i)
                            {
                                for (var j = 0; j < this.numberOfColumns; ++j)
                                {
                                    if (this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[i, j].Contains(cellWhosePropertyChangedName))
                                    { // If cell name is found in list of subscribers, remove it.
                                        this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[i, j].Remove(cellWhosePropertyChangedName);
                                    }
                                }
                            }

                            // Evaluate text
                            cellWhosePropertyChanged.Value = this.Evaluate(cellWhosePropertyChanged, this.cellNameAndValueDictionary);
                        }
                        else
                        {
                            // Text is not an expression, set value equal to text.
                            if (cellWhosePropertyChanged != null)
                            {
                                cellWhosePropertyChanged.Value = cellWhosePropertyChanged.Text;
                            }
                        }

                        this.NotifyPropertyChanged(cellWhosePropertyChanged, "Value"); // Notify subscribers (UI)
                        break;
                    }

                case "Value":
                    {
                        // If the property that changed was the cell's value.
                        if (cellWhosePropertyChanged != null)
                        {
                            var cellWhosePropertyChangedName = this.GetCellName(cellWhosePropertyChanged.RowIndexNumber, cellWhosePropertyChanged.ColumnIndexNumber);
                            if (double.TryParse(cellWhosePropertyChanged.Value, out var cellValue))
                            {
                                this.cellNameAndValueDictionary[cellWhosePropertyChangedName] = cellValue;
                            }
                            else
                            {
                                this.cellNameAndValueDictionary[cellWhosePropertyChangedName] = 0;
                            }
                        }

                        // Evaluate every cell that is subscribed to the cell that's value was changed.
                        if (cellWhosePropertyChanged != null)
                        {
                            var listCount = this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[cellWhosePropertyChanged.RowIndexNumber, cellWhosePropertyChanged.ColumnIndexNumber].Count;
                            for (var i = 0; i < listCount; ++i)
                            {
                                var cellName = this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[cellWhosePropertyChanged.RowIndexNumber, cellWhosePropertyChanged.ColumnIndexNumber][i];

                                // Convert name of cell into row and column values.
                                var columnIndex = cellName[0] - 65;
                                var rowIndex = string.Empty;
                                for (var j = 1; j < cellName.Count(); ++j)
                                {
                                    rowIndex += cellName[j];
                                }

                                // Evaluate the cells text and update it's value.
                                this.cellArray[int.Parse(rowIndex), columnIndex].Value = this.Evaluate(this.GetCellByRowAndColumn(int.Parse(rowIndex), columnIndex), this.cellNameAndValueDictionary);
                            }
                        }

                        this.NotifyPropertyChanged(cellWhosePropertyChanged, "Value"); // Notify subscribers (UI)
                        break;
                    }

                case "Color":
                    this.NotifyPropertyChanged(cellWhosePropertyChanged, "Color"); // Notify subscribers (UI)
                    break;
            }
        }

        /// <summary>
        /// Evaluates a cell to find it's value.
        /// </summary>
        /// <param name="cellThatIsBeingEvaluated"> The cell that's being evaluated. </param>
        /// <param name="cellValuesByName"> Dictionary of cell name keys and value value pairs. </param>
        /// <returns> Returns the value of a cell's text expression. </returns>
        private string Evaluate(Cell cellThatIsBeingEvaluated, Dictionary<string, double> cellValuesByName)
        {
            var cellName = this.GetCellName(cellThatIsBeingEvaluated.RowIndexNumber, cellThatIsBeingEvaluated.ColumnIndexNumber);
            var expressionTree = new ExpressionTree(cellThatIsBeingEvaluated.Text.Substring(1), cellValuesByName);
            var cellsReferencedInExpression = expressionTree.GetVariables();

            // Subscribe to property changes in all cells the cell that is being evaluated is dependent on.
            foreach (var variableCell in cellsReferencedInExpression)
            {
                // Convert name of cell into row and column values.
                var columnIndex = variableCell[0] - 65;
                var rowIndex = string.Empty;
                for (var j = 1; j < variableCell.Count(); ++j)
                {
                    rowIndex += variableCell[j];
                }

                this.listsOfCellsThatAreDependentOnCellIndicatedByArrayPosition[int.Parse(rowIndex), columnIndex].Add(cellName);
            }

            try
            {
                return expressionTree.Evaluate().ToString(CultureInfo.InvariantCulture);
            }
            catch (KeyNotFoundException)
            {
                return "Invalid Expression";
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
