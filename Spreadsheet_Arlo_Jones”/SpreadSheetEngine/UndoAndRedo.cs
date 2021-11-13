// <copyright file="UndoAndRedo.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Allows the spreadsheet to undo and redo changes made to it.
    /// </summary>
    public class UndoAndRedo
    {
        /// <summary>
        /// Stack of lists of command objects. Embodies a history of changes made to spreadsheet.
        /// </summary>
        private readonly Stack<List<ICommand>> undoStack = new Stack<List<ICommand>>();

        /// <summary>
        /// Stack of lists of command objects, Embodies a history of recently undone items.
        /// </summary>
        private Stack<List<ICommand>> redoStack = new Stack<List<ICommand>>();

        /// <summary>
        /// Gets count of items in undoStack.
        /// </summary>
        /// <returns> A count of items in undoStack. </returns>
        public int UndoCount()
        {
            return this.undoStack.Count();
        }

        /// <summary>
        /// Gets count of items in redoStack.
        /// </summary>
        /// <returns> A count of items in redoStack. </returns>
        public int RedoCount()
        {
            return this.redoStack.Count();
        }

        /// <summary>
        /// Returns the UserInterfaceText string of the top most command object on the undo stack.
        /// </summary>
        /// <returns> the UserInterfaceText string of the top most command object on the undo stack. </returns>
        public string GetUndoCommandUIString()
        {
            return this.undoStack.Peek().ElementAt(0).GetUserInterfaceText();
        }

        /// <summary>
        /// Returns the UserInterfaceText string of the top most command object on the redo stack.
        /// </summary>
        /// <returns> the UserInterfaceText string of the top most command object on the redo stack. </returns>
        public string GetRedoCommandUIString()
        {
            return this.redoStack.Peek().ElementAt(0).GetUserInterfaceText();
        }

        /// <summary>
        /// Adds a list of commands to the undo stack.
        /// </summary>
        /// <param name="commands"> List of commands to add to undo stack. </param>
        public void AddUndo(List<ICommand> commands)
        {
            this.undoStack.Push(commands);
            this.ClearRedoStack();
        }

        /// <summary>
        /// Undoes most recent change.
        /// </summary>
        public void Undo()
        {
            var undoCommands = this.undoStack.Pop();
            var redoCommands = undoCommands.Select(command => command.CreateRedo()).ToList();
            this.redoStack.Push(redoCommands);
            foreach (var command in undoCommands)
            {
                command.Execute();
            }
        }

        /// <summary>
        /// Redoes most recently undone change.
        /// </summary>
        public void Redo()
        {
            var redoCommands = this.redoStack.Pop();
            var undoCommands = redoCommands.Select(command => command.CreateRedo()).ToList();
            this.undoStack.Push(undoCommands);
            foreach (var command in redoCommands)
            {
                command.Execute();
            }
        }

        /// <summary>
        /// Clears the redo stack. Necessary after new changes have been made.
        /// </summary>
        private void ClearRedoStack()
        {
            this.redoStack = new Stack<List<ICommand>>();
        }
    }
}
