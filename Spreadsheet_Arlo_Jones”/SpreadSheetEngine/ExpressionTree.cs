// <copyright file="ExpressionTree.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using Cpts321.ExpressionTreeNodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// .
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// .
        /// </summary>
        internal static Dictionary<string, double> treeDictionary = new Dictionary<string, double>();

        /// <summary>
        /// .
        /// </summary>
        private Node root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionTree(string expression)
        {
            NodeFactory nodeFactory = new NodeFactory(treeDictionary);
            List<Node> nodeList = new List<Node>();
            var operatorCharacters = new char[] { '*', '/', '+', '-' };
            int operatorIndex;

            while ((operatorIndex = expression.IndexOfAny(operatorCharacters)) > 0)
            {
                string operandString = expression.Substring(0, operatorIndex);
                nodeList.Add(nodeFactory.CreateNode(operandString));
                nodeList.Add(nodeFactory.CreateNode(expression[operatorIndex].ToString()));
                expression = expression.Remove(0, operatorIndex + 1);
            }

            nodeList.Add(nodeFactory.CreateNode(expression));

            int weightValue = 1;
            while (nodeList.Count > 1)
            {
                for (int i = 0; i < nodeList.Count; ++i)
                {
                    if (nodeList.ElementAt(i).Weight == weightValue && ((OperatorNode)nodeList.ElementAt(i)).Left == null)
                    {
                        var operatorNode = (OperatorNode)nodeList[i];
                        operatorNode.Left = nodeList[i - 1];
                        operatorNode.Right = nodeList[i + 1];
                        nodeList.Remove(operatorNode.Left);
                        nodeList.Remove(operatorNode.Right);
                        i = 0;
                    }
                }

                ++weightValue;
            }

            this.root = nodeList[0];
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="variableValue"></param>
        public void SetVariable(string variableName, double variableValue)
        {
            treeDictionary.Add(variableName, variableValue);
        }

        /// <summary>
        /// .
        /// </summary>
        /// <returns> . </returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }
    }
}
