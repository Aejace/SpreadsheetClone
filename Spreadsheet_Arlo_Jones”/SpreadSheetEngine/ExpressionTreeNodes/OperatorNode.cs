// <copyright file="OperatorNode.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321.ExpressionTreeNodes
{
    /// <summary>
    /// Operator nodes have two children nodes, a left and a right. They are evaluated by performing an
    /// operation using the value of each of their children nodes as input.
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        /// <summary>
        /// Gets or sets the right child node.
        /// </summary>
        internal Node Right { get; set; }

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        internal Node Left { get; set; }
    }
}
