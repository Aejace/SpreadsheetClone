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
    /// Performs the subtraction operation with it's two children node's value's as input when evaluated.
    /// </summary>
    internal class SubtractionNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionNode"/> class.
        /// </summary>
        public SubtractionNode()
        {
            this.Weight = 4;
        }

        /// <summary>
        /// Gets the character associated with the type of operation the operator node performs.
        /// </summary>
        public static char OperatorChar => '-';

        /// <summary>
        /// Evalute is overloaded to handle subtraction.
        /// </summary>
        /// <returns> A double containing the evaluated value of the subtration node. </returns>
        internal override double Evaluate()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
