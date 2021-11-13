// <copyright file="NodeFactory.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cpts321.ExpressionTreeNodes;

    /// <summary>
    /// Handles the creation of nodes.
    /// </summary>
    public class NodeFactory
    {
        /// <summary>
        /// Dictionary containing key value pairs for variables names and what value they represent.
        /// </summary>
        private readonly Dictionary<string, double> expressionTreeVariableDictionary;

        /// <summary>
        /// Dictionary containing key value pairs for variables names and what value they represent.
        /// </summary>
        private readonly Dictionary<char, Type> expressionTreeOperatorDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeFactory"/> class.
        /// </summary>
        /// <param name="varDictionary"> Dictionary passed in from expression tree, will be passed into variable nodes for evaluation. </param>
        /// <param name="opDictionary"> Dictionary passed in from expression tree, contains operators expression tree supports. </param>
        public NodeFactory(Dictionary<string, double> varDictionary, Dictionary<char, Type> opDictionary)
        {
            // Initialize dictionaries.
            this.expressionTreeVariableDictionary = varDictionary;
            this.expressionTreeOperatorDictionary = opDictionary;
        }

        /// <summary>
        /// Factory method that creates nodes, returns the nodes to expression tree.
        /// </summary>
        /// <param name="nodeString"> String used to determine what kind of node to create. </param>
        /// <returns> Returns instantiated nodes of various types. </returns>
        public Node CreateNode(string nodeString)
        {
            // Null check
            if (string.IsNullOrEmpty(nodeString))
            {
                return null;
            }

            var result = nodeString.Any(char.IsLetter); // Result will be set to true if nodeString contains any letters.
            if (result)
            {
                return new VariableNode(nodeString, this.expressionTreeVariableDictionary); // Initialize a new variable node.
            }

            if (char.IsNumber(nodeString[0]))
            {
                return new ConstantNode(nodeString); // Initialize a new constant Node
            }

            if (!this.expressionTreeOperatorDictionary
                .ContainsKey(nodeString[0]))
            {
                return null; // If operator is recognized, create a node of the appropriate type.
            }

            var operatorNodeObject = Activator.CreateInstance(this.expressionTreeOperatorDictionary[nodeString[0]]);
            return (Node)operatorNodeObject;
        }
    }
}
