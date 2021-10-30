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
        private Dictionary<string, double> variableDictionary = new Dictionary<string, double>();

        /// <summary>
        /// Operator dictionary, contains operator characters as keys, and the operator nodes they corespond to as values.
        /// </summary>
        private Dictionary<char, Type> operatorDictionary = new Dictionary<char, Type>();

        /// <summary>
        /// .
        /// </summary>
        private List<string> variables = new List<string>();

        /// <summary>
        /// The starting node in the expression tree.
        /// </summary>
        private Node root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> The string that will be parsed into nodes to create the expression tree. </param>
        /// <param name="predefinedVariableDictionary"></param>
        public ExpressionTree(string expression, Dictionary<string, double> predefinedVariableDictionary)
        {
            this.TraverseAvailableOperators((op, type) => this.operatorDictionary.Add(op, type)); // Adds operator characters and thier coresponding node type to operatorDictionary.
            this.variableDictionary = predefinedVariableDictionary;
            this.root = this.BuildTree(expression); // Sets root to node returned by BuildTree
        }

        /// <summary>
        /// Operator Delegate.
        /// </summary>
        /// <param name="op"> Operator character. </param>
        /// <param name="type"> Type: The kind of operator node indicated by the character. </param>
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Adds variables to a dictionary to be referenced by variable nodes when evaluated.
        /// </summary>
        /// <param name="variableName"> The name of the variable to add to the dictionary. </param>
        /// <param name="variableValue"> The value assaociated with the veriables name. </param>
        public void SetVariable(string variableName, double variableValue)
        {
            if (this.variableDictionary.ContainsKey(variableName))
            { // If the variable name has already been added to the dictionary, remove it so it can be replaced.
                this.variableDictionary.Remove(variableName);
            }

            this.variableDictionary.Add(variableName, variableValue); // Add variable with associated value to dictionary.
        }

        /// <summary>
        /// .
        /// </summary>
        /// <returns></returns>
        public List<string> GetVariables()
        {
            return this.variables;
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
        /// Builds a binary expression tree.
        /// </summary>
        /// <param name="expression"> Math expression to parse and then build a tree with. </param>
        /// <returns> The node the is the root of the expresssion tree built. </returns>
        private Node BuildTree(string expression)
        {
            NodeFactory nodeFactory = new NodeFactory(this.variableDictionary, this.operatorDictionary); // Initialize node factory, with operator dictionary and variable dictionary
            List<Node> nodeList = new List<Node>(); // Initalizes list of nodes that will be converted into an expression tree.
            List<char> operatorCharactersList = this.operatorDictionary.Keys.ToList(); // Creates list of operator characters using operator dictionary
            operatorCharactersList.Add('('); // Add Parentheses to list of operators to check for while iterating.
            operatorCharactersList.Add(')');
            char[] operatorCharacters = operatorCharactersList.ToArray(); // Create character array of operators to check while iterating through expression. Needs to be an array to be used with expression.IndexOfAny().
            int operatorIndex; // Index of the first operator found while iterating.
            int parenthesesCount = 0; // Count of parentheses, used to find parentheses pairs.

            expression = string.Concat(expression.Where(c => !char.IsWhiteSpace(c))); // Remove white space in expression before parsing.

            // While-check sets operatorIndex to the index of the first operator character found, if no operator is found, it breaks the loop
            while ((operatorIndex = expression.IndexOfAny(operatorCharacters)) >= 0)
            {
                if (expression[operatorIndex] == '(')
                { // If operator is an open parentheses, find its pair.
                    ++parenthesesCount; // Increment for first parentheses found
                    int i = operatorIndex; // Iterates through rest of list to find close parentheses pair.
                    while (parenthesesCount != 0 && i < expression.Length - 1)
                    { // While there is an uneven number of matches and the end of the string hasnt been reached.
                        ++i; // Check next element

                        if (expression[i] == '(')
                        {
                            ++parenthesesCount; // Increment for new open
                        }

                        if (expression[i] == ')')
                        {
                            --parenthesesCount; // Decrement for new close
                        }
                    }

                    if ((parenthesesCount != 0) || (i == operatorIndex + 1))
                    {
                        // Unimplimented, Should throw an error becuase parentheses are mis matched.
                    }
                    else
                    {
                        nodeList.Add(this.BuildTree(expression.Substring(operatorIndex + 1, i - operatorIndex - 1))); // Recursively call build tree using the substring between the parentheses as new expression parameter. Add root of tree returned to nodelist.
                        expression = expression.Remove(0, i + 1); // Once this portion of the string has been converted into a node, remove it from the expression before parsing for the next node.
                    }
                }
                else
                { // Create nodes for operator and charaters preceding it (if any)
                    string operandString = expression.Substring(0, operatorIndex); // Create a substring starting at the begining of the expression and going until the first operator

                    // If operand string is empty, dont create a node from the empty string.
                    if (operandString != string.Empty)
                    {
                        nodeList.Add(nodeFactory.CreateNode(operandString)); // Create a node using the substring preceding the operator
                        if (operandString.Any(x => char.IsLetter(x)))
                        {
                            this.variables.Add(operandString);
                        }
                    }

                    nodeList.Add(nodeFactory.CreateNode(expression[operatorIndex].ToString())); // Create node using the operator character
                    expression = expression.Remove(0, operatorIndex + 1); // Remove string that has now been converted to nodes.
                }
            }

            // If there are any characters left in string after all relevant operators have been turned into nodes
            if (expression.Length > 0)
            {
                nodeList.Add(nodeFactory.CreateNode(expression)); // Creates a node for the last operand in the exspression
                if (expression.Any(x => char.IsLetter(x)))
                {
                    this.variables.Add(expression);
                }
            }

            // converts nodeList into an expression tree.
            int weightValue = 1; // Starting weight value. Loop will handle all operator of a given weight before proceeding to the next weight, and will continue to do so untill all operators are handled (assigned children)
            while (nodeList.Count > 1)
            { // While there is more than one node left in the list, continue converting. When there is one left, that one will be the root of the tree.
                for (int i = 0; i < nodeList.Count; ++i)
                { // Iterate through nodeList.
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

            return nodeList.FirstOrDefault(); // Returns root node.
        }

        /// <summary>
        /// Traverses and places.
        /// </summary>
        /// <param name="onOperator">thing.</param>
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OperatorNode);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
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
