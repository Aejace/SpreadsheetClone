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
        /// Executes command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Creates a new command that can undo execute.
        /// </summary>
        /// <returns></returns>
        ICommand createRedo();
    }
}
