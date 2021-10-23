// <copyright file="ExpressionTests.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace ExpressionTreeTests
{
    using Cpts321;
    using NUnit.Framework;

    public class Tests
    {
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
        public double TestEvaluateCases(string expresssion)
        {
            ExpressionTree expressionTree = new ExpressionTree(expresssion);
            return expressionTree.Evaluate();
        }
    }
}