// <copyright file="MultiplicationNode.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321.ExpressionTreeNodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Performs the multiplication operation with it's two children node's value's as input when evaluated.
    /// </summary>
    internal class MultiplicationNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationNode"/> class.
        /// </summary>
        /// <param name="priority"> Represents the priority weight of the operator being constructed. </param>
        public MultiplicationNode(int priority)
            : base(priority)
        {
        }

        /// <summary>
        /// Evalute is overloaded to handle multiplication.
        /// </summary>
        /// <returns> A double containing the evaluated value of the multiplication node. </returns>
        internal override double Evaluate()
        {
            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}
