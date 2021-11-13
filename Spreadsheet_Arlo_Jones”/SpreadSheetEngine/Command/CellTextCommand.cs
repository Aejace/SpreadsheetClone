// <copyright file="CellTextCommand.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    /// <summary>
    /// Stores a previous text value for a cell. Sets that cell's text to it's previous value when command is executed.
    /// </summary>
    public class CellTextCommand : ICommand
    {
        /// <summary>
        /// Reference to cell execute will act upon.
        /// </summary>
        private readonly Cell cell;

        /// <summary>
        /// A previous text of the cell.
        /// </summary>
        private readonly string text;

        /// <summary>
        /// A summary text about what is changed when command is executed, sent to the UI.
        /// </summary>
        private readonly string userInterfaceText = "cell text change";

        /// <summary>
        /// Initializes a new instance of the <see cref="CellTextCommand"/> class.
        /// </summary>
        /// <param name="cell"> Reference to a cell. </param>
        /// <param name="text"> Text being stored in the command.</param>
        public CellTextCommand(Cell cell, string text)
        {
            this.cell = cell;
            this.text = text;
        }

        /// <summary>
        /// Sets cell text property to the text stored in this command.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.text;
        }

        /// <summary>
        /// Creates a command object that can undo execute.
        /// </summary>
        /// <returns> A CellTextCommand, used to redo. </returns>
        public ICommand createRedo()
        {
            var redoCommand = new CellTextCommand(this.cell, this.cell.Text);
            return redoCommand;
        }
    }
}
