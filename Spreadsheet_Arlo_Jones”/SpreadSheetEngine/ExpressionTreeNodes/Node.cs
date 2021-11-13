// <copyright file="Node.cs" company="Arlo Jones">
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
    /// Abstract node, Expression Tree will be built using nodes that inherit from this class.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Gets or sets the value of weight.
        /// </summary>
        internal int Weight { get; set; }

        /// <summary>
        /// Will be overloaded to handle different evaluation methods.
        /// </summary>
        /// <returns> A double representing the evaluated value of the node. </returns>
        internal abstract double Evaluate();
    }
}
