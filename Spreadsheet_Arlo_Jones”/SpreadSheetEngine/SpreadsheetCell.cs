// <copyright file="SpreadSheetCell.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// SpreadSheetCell inherits from the Cell class. It doesnt extend Cell class in anyway but creates
    /// an unabstract version of the cell class that can be instantiated. It is an internal class meaning
    /// that only things in Spreadsheet engine can create a instantiated SpreadSheetCell or set its
    /// it value, which is a property inherited from Cell that is also internal. However, things from outside
    /// of the library can still access the public properties and methods that SpreadSheetCell inherited from cell.
    /// </summary>
    internal class SpreadSheetCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadSheetCell"/> class.
        /// </summary>
        /// <param name="rowIndex"> The row number of a cell. </param>
        /// <param name="columnIndex"> The column number of a cell. </param>
        public SpreadSheetCell(int rowIndex, int columnIndex)
            : base(rowIndex, columnIndex)
        {
        }
    }
}
