// <copyright file="ICommand.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    /// <summary>
    /// Interface for command objects, requires command objects to implement and Execute function.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Returns a string for the user interface, identifies what the command will do.
        /// </summary>
        /// <returns> returns a string containing information about what the command will do. </returns>
        string GetUserInterfaceText();

        /// <summary>
        /// Executes command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Creates a new command that can undo execute.
        /// </summary>
        /// <returns> A command object that's execute can undo the execute of this object. </returns>
        ICommand CreateRedo();
    }
}
