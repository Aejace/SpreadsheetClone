// <copyright file="Cell.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// .
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// The text that is typed into a cell.
        /// </summary>
        protected private string text;

        /// <summary>
        /// The evaluated value of a cell. Will be displayed in the UI.
        /// </summary>
        private protected string value;

        /// <summary>
        /// The row number of a cell.
        /// </summary>
        private readonly int rowIndexNumber;

        /// <summary>
        /// The column number of a cell.
        /// </summary>
        private readonly int columnIndexNumber;

        

       

        public Cell(int rowNum, int columnNum)
        {
            this.rowIndexNumber = rowNum;
            this.columnIndexNumber = columnNum;
        }

        /// <summary>
        /// .
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.NotifyPropertyChanged(this.text);
                }
            }
        }

        public string Value
        {
            get
            {
                return this.Value;
            }

            internal set
            {
                this.Value = value;
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int ColumnIndexNumber
        {
            get
            {
                return this.columnIndexNumber;
            }
        }

        public int GetRowIndexNumber
        {
            get
            {
                return this.rowIndexNumber;
            }
        }
    }
}
