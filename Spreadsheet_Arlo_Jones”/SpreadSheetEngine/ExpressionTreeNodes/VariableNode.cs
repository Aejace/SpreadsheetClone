// <copyright file="VariableNode.cs" company="Arlo Jones">
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
    internal class VariableNode : Node
    {
        internal VariableNode()
        {

        }

        /// <summary>
        /// Evalute is overloaded to return the nodes value.
        /// </summary>
        /// <returns> A double containing the evaluated value of the constant node. </returns>
        internal override double Evaluate()
        {
            return count * 
        }
    }
}
