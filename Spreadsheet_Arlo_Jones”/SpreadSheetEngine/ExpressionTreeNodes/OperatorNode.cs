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
    /// .
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        /// <summary>
        /// .
        /// </summary>
        private Node right;

        /// <summary>
        /// .
        /// </summary>
        private Node left;

        /// <summary>
        /// .
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
        /// .
        /// </summary>
        public Node Left
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
