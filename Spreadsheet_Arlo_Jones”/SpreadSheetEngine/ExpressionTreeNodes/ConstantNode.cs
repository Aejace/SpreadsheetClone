// <copyright file="ConstantNode.cs" company="Arlo Jones">
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
    internal class ConstantNode : Node
    {
        /// <summary>
        /// .
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="valueString"> The string of numbers used to create the double that is the value of the node. </param>
        internal ConstantNode(string valueString)
        {
            this.value = Convert.ToDouble(valueString);
        }

        /// <summary>
        /// Evaluate is overloaded to .
        /// </summary>
        /// <returns> A double containing the evaluated value of the variable node. </returns>
        internal override double Evaluate()
        {
            return this.value;
        }
    }
}
