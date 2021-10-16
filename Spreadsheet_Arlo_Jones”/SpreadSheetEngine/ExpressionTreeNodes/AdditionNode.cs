// <copyright file="AdditionNode.cs" company="Arlo Jones">
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
    /// Performs the addition operation with it's two children node's value's as input when evaluated.
    /// </summary>
    internal class AdditionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionNode"/> class.
        /// </summary>
        /// <param name="priority"> Represents the priority weight of the operator being constructed. </param>
        public AdditionNode(int priority)
            : base(priority)
        {
        }

        /// <summary>
        /// Evalute is overloaded to handle addition.
        /// </summary>
        /// <returns> A double containing the evaluated value of the addition node. </returns>
        internal override double Evaluate()
        {
            return this.Left.Evaluate() + this.Right.Evaluate();
        }
    }
}
