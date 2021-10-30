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
    /// Represents a variable and it's accompanying quantity in an expression.
    /// </summary>
    internal class VariableNode : Node
    {
        /// <summary>
        /// String variable, is the key in a key value pair for variableDictionary Ex. 'A23' = 15.
        /// </summary>
        private string variableString;

        /// <summary>
        /// Dictionary passed in by reference to determine the value of any string variables.
        /// </summary>
        private Dictionary<string, double> variableDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="inputString"> The string used to determine the variable string and count of the variable node. </param>
        /// <param name="dictionary"> Dictionary used to determine the value of the string variable. </param>
        internal VariableNode(string inputString, Dictionary<string, double> dictionary)
        {
            this.variableDictionary = dictionary; // initalize variableDictionary using the dictionary that was passed in.
            this.variableString = inputString;
            this.Weight = 0; // Weight is zero. Variable nodes dont have a precedence they should be operated on, but all nodes will be checked for precedence, so it is neccessary to include it here.
        }

        /// <summary>
        /// Evalute is overloaded to return the nodes value.
        /// </summary>
        /// <returns> A double containing the evaluated value of the constant node. </returns>
        internal override double Evaluate()
        {
            return this.variableDictionary[this.variableString];
        }
    }
}
