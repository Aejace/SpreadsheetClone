// <copyright file="SubtractionNode.cs" company="Arlo Jones">
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
    internal class SubtractionNode : OperatorNode
    {
        /// <summary>
        /// Evalute is overloaded to handle subtraction.
        /// </summary>
        /// <returns> A double containing the evaluated value of the subtration node. </returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
