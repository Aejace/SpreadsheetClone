// <copyright file="DivisionNode.cs" company="Arlo Jones">
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
    internal class DivisionNode : OperatorNode
    {
        /// <summary>
        /// Evalute is overloaded to handle division.
        /// </summary>
        /// <returns> A double containing the evaluated value of the division node. </returns>
        internal override double Evaluate()
        {
            return this.Left.Evaluate() / this.Right.Evaluate();
        }
    }
}
