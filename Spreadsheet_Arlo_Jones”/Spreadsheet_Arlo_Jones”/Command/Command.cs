// <copyright file="Command.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Spreadsheet_Arlo_Jones_
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// .
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="type"></param>
        /// <param name="oldValue"></param>
        public Command(int row, int column, string type, object oldValue)
        {
            this.RowIndex = row;
            this.ColumnIndex = column;
            this.Type = type;
            this.Value = oldValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public int RowIndex { get; set; }
        
        public int ColumnIndex { get; set; }

        public string Type { get; set; }

        public object Value { get; set; }
    }
}
