// <copyright file="ExpressionTree.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace Cpts321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Cpts321.ExpressionTreeNodes;

    /// <summary>
    /// Binary tree data structure used to evaluate math expressions.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Dictionary contaning variable strings as keys and their assigned values as values.
        /// </summary>
        private static Dictionary<string, double> variableDictionary = new Dictionary<string, double>();

        /// <summary>
        /// Operator dictionary, contains operator characters as keys, and the operator nodes they corespond to as values.
        /// </summary>
        private Dictionary<char, Type> operatorDictionary = new Dictionary<char, Type>();

        /// <summary>
        /// The starting node in the expression tree.
        /// </summary>
        private Node root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> The string that will be parsed into nodes to create the expression tree. </param>
        public ExpressionTree(string expression)
        {
            this.TraverseAvailableOperators((op, type) => this.operatorDictionary.Add(op, type));
            this.root = this.BuildTree(expression);
        }

        private Node BuildTree(string expression)
        {
            NodeFactory nodeFactory = new NodeFactory(variableDictionary);
            List<char> operatorCharactersList = operatorDictionary.Keys.ToList();
            operatorCharactersList.Add('(');
            operatorCharactersList.Add(')');
            char[] operatorCharacters = operatorCharactersList.ToArray();
            List<Node> nodeList = new List<Node>();
            int operatorIndex;

            // While check sets operatorIndex to the index of the first operator character found, if no operator is found, it breaks the loop
            while ((operatorIndex = expression.IndexOfAny(operatorCharacters)) > 0)
            {
                string operandString = expression.Substring(0, operatorIndex); // Create a substring starting at the begining of the string and going until the first operator
                nodeList.Add(nodeFactory.CreateNode(operandString)); // Create a node using the substring preceding the operator
                nodeList.Add(nodeFactory.CreateNode(expression[operatorIndex].ToString())); // Create node using the operator character
                expression = expression.Remove(0, operatorIndex + 1); // Remove string that has now been converted to nodes.
            }

            nodeList.Add(nodeFactory.CreateNode(expression)); // Creates a node for the last operand in the exspression

            int weightValue = 1; // Starting weight value
            while (nodeList.Count > 1)
            {
                for (int i = 0; i < nodeList.Count; ++i)
                {
                    // If a given node is the correct weight and does not already have children
                    if (nodeList.ElementAt(i).Weight == weightValue && ((OperatorNode)nodeList.ElementAt(i)).Left == null)
                    {
                        var operatorNode = (OperatorNode)nodeList[i]; // Downcast as operator node. Safe because only operator nodes have a weight above 0
                        operatorNode.Left = nodeList[i - 1]; // Set preceding node to be left child.
                        operatorNode.Right = nodeList[i + 1]; // set next node to be right child.
                        nodeList.Remove(operatorNode.Left); // Remove nodes just set from the list.
                        nodeList.Remove(operatorNode.Right);
                        i = 0; // reset iterator to find the next operator node.
                    }
                }

                ++weightValue; // Increment weight value to begin pass for next lower precedence operator nodes
            }

            return nodeList[0];
        }

        /// <summary>
        /// Operator Delegate.
        /// </summary>
        /// <param name="op"> Operator. </param>
        /// <param name="type"> Type. </param>
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Adds variables to a dictionary to be referenced by variable nodes when evaluated.
        /// </summary>
        /// <param name="variableName"> The name of the variable to add to the dictionary. </param>
        /// <param name="variableValue"> The value assaociated with the veriables name. </param>
        public void SetVariable(string variableName, double variableValue)
        {
            variableDictionary.Add(variableName, variableValue);
        }

        /// <summary>
        /// Evaluates the expression tree.
        /// </summary>
        /// <returns> The value of the expression in the expression tree. </returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }

        /// <summary>
        /// Traverses and places.
        /// </summary>
        /// <param name="onOperator">thing.</param>
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OperatorNode);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes =
                    assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                foreach (var type in operatorTypes)
                {
                    PropertyInfo operatorField = type.GetProperty("OperatorChar");
                    if (operatorField != null)
                    {
                        object operatorValue = operatorField.GetValue(type);

                        if (operatorValue is char)
                        {
                            char operatorSymbol = (char)operatorValue;
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
