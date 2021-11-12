// <copyright file="Cell.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Abstract Cell Class, contains the index values for the row and
    /// column of a cell, as well as its value, and a "Text" property
    /// representing what is typed into the cell.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// The text that is typed into a cell.
        /// </summary>
        private string text = string.Empty;

        /// <summary>
        /// The evaluated value of a cell. Will be displayed in the UI.
        /// </summary>
        private string value = string.Empty;

        /// <summary>
        /// The background color of the cell, initialized to white (0xFFFFFFFF).
        /// </summary>
        private uint bgColor = uint.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowNum"> The row number of a cell. </param>
        /// <param name="columnNum"> The column number of a cell. </param>
        protected Cell(int rowNum, int columnNum)
        {
            // Row and Column are set in the constructor, and then never changed.
            this.RowIndexNumber = rowNum;
            this.ColumnIndexNumber = columnNum;
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
            get => this.text;

            set
            {
                this.text = value;
                this.NotifyPropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets value, can be set only by things in this assembly.
        /// </summary>
        public string Value
        {
            get => this.value;

            internal set
            {
                if (this.value == value)
                {
                    return;
                }

                this.value = value;
                this.NotifyPropertyChanged("Value");
            }
        }

        /// <summary>
        /// Gets or sets the background color of a cell.
        /// </summary>
        public uint BGColor
        {
            get => this.bgColor;

            set
            {
                if (this.bgColor == value)
                {
                    return;
                }

                this.bgColor = value;
                this.NotifyPropertyChanged("Color");
            }
        }

        /// <summary>
        /// Gets columnIndexNumber.
        /// </summary>
        public int ColumnIndexNumber { get; }

        /// <summary>
        /// Gets rowIndexNumber.
        /// </summary>
        public int RowIndexNumber { get; }

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
