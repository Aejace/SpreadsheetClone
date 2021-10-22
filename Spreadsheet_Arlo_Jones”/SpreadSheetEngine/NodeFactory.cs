// <copyright file="NodeFactory.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Cpts321.ExpressionTreeNodes;

    /// <summary>
    /// .
    /// </summary>
    public class NodeFactory
    {
        /// <summary>
        /// .
        /// </summary>
        private Dictionary<string, double> expressionTreeDictionary;

        /// <summary>
        /// .
        /// </summary>
        /// <param name="dictionary"></param>
        public NodeFactory(Dictionary<string, double> dictionary)
        {
            this.expressionTreeDictionary = dictionary;
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="nodeString"></param>
        /// <returns></returns>
        public Node CreateNode(string nodeString)
        {
            if (string.IsNullOrEmpty(nodeString))
            {
                return null;
            }

            if (char.IsLetter(nodeString[0]))
            {
                return new VariableNode(nodeString, this.expressionTreeDictionary);
            }
            else if (char.IsNumber(nodeString[0]))
            {
                return new ConstantNode(nodeString);
            }
            else
            {
                if (nodeString[0] == '*')
                {
                    return new MultiplicationNode();
                }

                if (nodeString[0] == '/')
                {
                    return new DivisionNode();
                }

                if (nodeString[0] == '+')
                {
                    return new AdditionNode();
                }

                if (nodeString[0] == '-')
                {
                    return new SubtractionNode();
                }
            }

            return null;
        }
    }
}
