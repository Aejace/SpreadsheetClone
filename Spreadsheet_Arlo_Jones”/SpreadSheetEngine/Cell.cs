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
        /// The row number of a cell.
        /// </summary>
        private readonly int rowIndexNumber;

        /// <summary>
        /// The column number of a cell.
        /// </summary>
        private readonly int columnIndexNumber;

        /// <summary>
        /// The text that is typed into a cell.
        /// </summary>
        private string text;

        /// <summary>
        /// The evaluated value of a cell. Will be displayed in the UI.
        /// </summary>
        private string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowNum"> The row number of a cell. </param>
        /// <param name="columnNum"> The column number of a cell. </param>
        public Cell(int rowNum, int columnNum)
        {
            // Row and Column are set in the constructor, and then never changed.
            this.rowIndexNumber = rowNum;
            this.columnIndexNumber = columnNum;
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets text.
        /// </summary>
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
                    this.NotifyPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets value, can be set only by things in this assembly.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            internal set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets columnIndexNumber.
        /// </summary>
        public int ColumnIndexNumber
        {
            get
            {
                return this.columnIndexNumber;
            }
        }

        /// <summary>
        /// Gets rowIndexNumber.
        /// </summary>
        public int GetRowIndexNumber
        {
            get
            {
                return this.rowIndexNumber;
            }
        }

        /// <summary>
        /// NotifyPropertyChanged method inherited from the INotifyPropertyChanged interface.
        /// Method is called when the property changed event is fired.
        /// </summary>
        /// <param name="propertyName"> . </param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
