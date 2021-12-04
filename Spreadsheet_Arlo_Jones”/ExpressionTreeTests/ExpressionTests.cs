// <copyright file="ExpressionTests.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

using NUnit.Framework.Internal;

namespace ExpressionTreeTests
{
    using System.Collections.Generic;
    using Cpts321;
    using NUnit.Framework;

    /// <summary>
    /// Tests the functionality of ExpressionTree using the evaluation of different input expressions.
    /// </summary>
    public class ExpressionTests
    {
        /// <summary>
        /// Creates an expression tree using the given expression, then evaluates the expression using the expression tree.
        /// </summary>
        /// <param name="expression"> The math expression to be evaluated using the tree. </param>
        /// <returns> The evaluated value of the expression. </returns>
        [TestCase("4+7-2", ExpectedResult = 9.0)]
        [TestCase("5-4-3+10", ExpectedResult = 8.0)]
        [TestCase("5-4+10-3+7-6", ExpectedResult = 9.0)]
        [TestCase("10/(7-2)", ExpectedResult = 2.0)]
        [TestCase("(12-2)/2", ExpectedResult = 5.0)]
        [TestCase("(((((2+3)-(4+5)))))", ExpectedResult = -4.0)]
        [TestCase("2*3+5", ExpectedResult = 11.0)]
        [TestCase("2+3*5", ExpectedResult = 17.0)]
        [TestCase("2 + 3 * 5", ExpectedResult = 17.0)]
        [TestCase("5/0", ExpectedResult = double.PositiveInfinity)]
        [TestCase("Test-2", ExpectedResult = -2.0)]
        [TestCase("0/5", ExpectedResult = 0.0)]
        public double TestEvaluateCases(string expression)
        {
            var defaultDictionary = new Dictionary<string, double>
            {
                { "Test", 0 },
            };
            var expressionTree = new ExpressionTree(expression, defaultDictionary);
            return expressionTree.Evaluate();
        }
    }
}