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
        /// The quantity multiplier of the variable.
        /// </summary>
        private double count = 1;

        /// <summary>
        /// String variable, is the key in a key value pair for dictionaryPassedInByReference Ex. 'X' = 23.
        /// </summary>
        private string variableString;

        /// <summary>
        /// Dictionary passed in by reference to determine the value of any string variables.
        /// </summary>
        private Dictionary<string, double> dictionaryPassedInByReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="inputString"> The string used to determine the variable string and count of the variable node. </param>
        /// <param name="dictionary"> Dictionary used to determine the value of the string variable. </param>
        internal VariableNode(string inputString, Dictionary<string, double> dictionary)
        {
            this.dictionaryPassedInByReference = dictionary;
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int firstIntegerIndex = inputString.IndexOfAny(digits);
            if (firstIntegerIndex != -1)
            {
                this.variableString = inputString.Substring(0, firstIntegerIndex); // Sets the variable string to the first character in the value string
                this.count = Convert.ToDouble(inputString.Substring(firstIntegerIndex));
            }
            else
            {
                this.variableString = inputString;
            }

            this.Weight = 0;
        }

        /// <summary>
        /// Evalute is overloaded to return the nodes value.
        /// </summary>
        /// <returns> A double containing the evaluated value of the constant node. </returns>
        internal override double Evaluate()
        {
            return this.count * this.dictionaryPassedInByReference[this.variableString];
        }
    }
}
