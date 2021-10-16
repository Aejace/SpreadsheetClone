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
    /// .
    /// </summary>
    internal class AdditionNode : OperatorNode
    {
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
