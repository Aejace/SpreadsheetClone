// <copyright file="OperatorNode.cs" company="Arlo Jones">
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
    /// Operator nodes have two children nodes, a left and a right. They are evaluated by performing an
    /// operation using the value of each of thier children nodes as input.
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        /// <summary>
        /// The right child node of the operator node.
        /// </summary>
        private Node right;

        /// <summary>
        /// The left child node of the operator node.
        /// </summary>
        private Node left;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="priority"> Represents the priority weight of the operator being constructed. </param>
        internal OperatorNode(int priority)
        {
            this.Weight = priority;
        }

        /// <summary>
        /// Gets or sets the right child node.
        /// </summary>
        internal Node Right
        {
            get
            {
                return this.right;
            }

            set
            {
                this.right = value;
            }
        }

        /// <summary>
        /// Gets or sets the left child node.
        /// </summary>
        internal Node Left
        {
            get
            {
                return this.left;
            }

            set
            {
                this.left = value;
            }
        }
    }
}
