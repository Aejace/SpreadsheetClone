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
        /// The priority node will be evaluated with.
        /// </summary>
        private int weight;

        /// <summary>
        /// Gets or sets the value of weight.
        /// </summary>
        internal int Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                this.weight = value;
            }
        }

        /// <summary>
        /// Will be overloaded to handle different elvaluation methods.
        /// </summary>
        /// <returns> A double representing the evaluted value of the node. </returns>
        internal abstract double Evaluate();
    }
}
