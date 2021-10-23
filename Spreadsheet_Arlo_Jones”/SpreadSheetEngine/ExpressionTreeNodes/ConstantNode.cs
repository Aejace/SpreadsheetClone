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
    /// Constant node contains a double the represents a constant value in the exspression to be evaluated.
    /// </summary>
    internal class ConstantNode : Node
    {
        /// <summary>
        /// The value of the node.
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="valueString"> The string of numbers used to create the double that is the value of the node. </param>
        internal ConstantNode(string valueString)
        {
            this.value = Convert.ToDouble(valueString); // Set value
            this.Weight = 0; // Weight is zero. Constant nodes dont have a precedence they should be operated on, but all nodes will be checked for precedence, so it is neccessary to include it here.
        }

        /// <summary>
        /// Evaluate is overloaded to return the value of the node.
        /// </summary>
        /// <returns> A double containing the evaluated value of the constant node. </returns>
        internal override double Evaluate()
        {
            return this.value;
        }
    }
}
