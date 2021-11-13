// <copyright file="MultiplicationNode.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321.ExpressionTreeNodes
{
    /// <summary>
    /// Performs the multiplication operation with it's two children node's value's as input when evaluated.
    /// </summary>
    internal class MultiplicationNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        public MultiplicationNode()
        {
            this.Weight = 3; // Weight is 3 using C precedence conventions
        }

        /// <summary>
        /// Gets the character associated with the type of operation the operator node performs.
        /// </summary>
        public static char OperatorChar => '*';

        /// <summary>
        /// Evaluate is overloaded to handle multiplication.
        /// </summary>
        /// <returns> A double containing the evaluated value of the multiplication node. </returns>
        internal override double Evaluate()
        {
            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
