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
        /// String variable, is the key in a key value pair for variableDictionary Ex. 'X' = 23.
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
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; // Character array of all digits, used to check if and where any numbers occur in the input string
            int firstIntegerIndex = inputString.IndexOfAny(digits); // Returns the first index where a digit was encountered, or -1 if none were.

            // If numeric digit was found
            if (firstIntegerIndex != -1)
            {
                this.variableString = inputString.Substring(0, firstIntegerIndex); // Sets the variable string to the string preceding the begining of the digits.
                this.count = Convert.ToDouble(inputString.Substring(firstIntegerIndex)); // Sets the count using any remainng characters in the string that arent a part of the variable name.
            }
            else
            {
                this.variableString = inputString; // Input string contains no numbers and is just a variable name.
            }

            dictionary.Add(this.variableString, 0); // Adds the variable to the dictionary with a default value of zero.

            this.Weight = 0; // Weight is zero. Variable nodes dont have a precedence they should be operated on, but all nodes will be checked for precedence, so it is neccessary to include it here.
        }

        /// <summary>
        /// Evalute is overloaded to return the nodes value.
        /// </summary>
        /// <returns> A double containing the evaluated value of the constant node. </returns>
        internal override double Evaluate()
        {
            return this.count * this.variableDictionary[this.variableString]; // Multiply variable value by count and return.
        }
    }
}
