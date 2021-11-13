// <copyright file="CellColorCommand.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    /// <summary>
    /// Stores a previous background color value for a cell. Sets that cell's background to it's previous value when command is executed.
    /// </summary>
    public class CellColorCommand : ICommand
    {
        /// <summary>
        /// A summary text about what is changed when command is executed, sent to the UI.
        /// </summary>
        private const string UserInterfaceText = "cell color change";

        /// <summary>
        /// Reference to cell execute will act upon.
        /// </summary>
        private readonly Cell cell;

        /// <summary>
        /// A previous color of the cell.
        /// </summary>
        private readonly uint color;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellColorCommand"/> class.
        /// </summary>
        /// <param name="cell"> Reference to a cell. </param>
        /// <param name="color"> color being stored in the command.</param>
        public CellColorCommand(Cell cell, uint color)
        {
            this.cell = cell;
            this.color = color;
        }

        /// <summary>
        /// Returns a summary text about what is changed when command is executed.
        /// </summary>
        /// <returns> A summary text about what is changed when command is executed. </returns>
        public string GetUserInterfaceText()
        {
            return UserInterfaceText;
        }

        /// <summary>
        /// Sets cell color property to the color stored in this command.
        /// </summary>
        public void Execute()
        {
            this.cell.BGColor = this.color;
        }

        /// <summary>
        /// Creates a command object that can undo execute.
        /// </summary>
        /// <returns> A CellColorCommand, used to redo. </returns>
        public ICommand CreateRedo()
        {
            var redoCommand = new CellColorCommand(this.cell, this.cell.BGColor);
            return redoCommand;
        }
    }
}
