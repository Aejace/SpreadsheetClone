// <copyright file="Cell.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class Cell : INotifyPropertyChanged
    {
        private readonly int rowIndexNumber;
        private readonly int columnIndexNumber;
        protected string text;
        protected string value;

        public Cell(int rowNum, int columnNum)
        {
            this.rowIndexNumber = rowNum;
            this.columnIndexNumber = columnNum;
        }

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
