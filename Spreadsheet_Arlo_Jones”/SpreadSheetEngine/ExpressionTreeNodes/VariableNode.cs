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
        private double count;

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
        /// <param name="valueString"> The string of numbers used to create the double that is the value of the node. </param>
        /// <param name="dictionary"> Dictionary used to determine the value of the string variable. </param>
        internal VariableNode(string valueString, Dictionary<string, double> dictionary)
        {
            // Local variable used to collect character that are a part of the variables count.
            string countString = string.Empty;

            this.variableString = valueString[0].ToString(); // Sets the variable string to the first character in the value string
            this.dictionaryPassedInByReference = dictionary;

            for (int i = 1; i < valueString.Length; ++i)
            {
                countString += valueString[i].ToString();
            }

            this.count = Convert.ToDouble(countString);

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
