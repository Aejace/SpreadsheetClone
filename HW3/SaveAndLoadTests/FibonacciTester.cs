// <copyright file="FibonacciTester.cs" company="Arlo Jones 011778052">
// Copyright (c) Aejace. All rights reserved.
// </copyright>

namespace FibonacciTests
{
    using HW3;
    using NUnit.Framework;

    /// <summary>
    /// A class for testing the FibonacciText reader.
    /// </summary>
    [TestFixture]
    public class FibonacciTester
    {
        /// <summary>
        /// Tests FibonacciTextReader to ensure that it returns the correct values.
        /// </summary>
        /// <param name="fibonacciTerm"> The number of fibonacci numbers generated. </param>
        /// <returns> A string containing the value of the term of the fibonacci sequence specified. </returns>
        [TestCase(0, ExpectedResult = "Term: 0 = 0\r\n")]
        [TestCase(1, ExpectedResult = "Term: 0 = 0\r\nTerm: 1 = 1\r\n")]
        [TestCase(3, ExpectedResult = "Term: 0 = 0\r\nTerm: 1 = 1\r\nTerm: 2 = 1\r\nTerm: 3 = 2\r\n")]
        [TestCase(-1, ExpectedResult = "Term: 0 = 0\r\nTerm: -1 = -1\r\n")]
        [TestCase(-3, ExpectedResult = "Term: 0 = 0\r\nTerm: -1 = -1\r\nTerm: -2 = -1\r\nTerm: -3 = -2\r\n")]
        public string FibonacciTextReaderTests(int fibonacciTerm)
        {
            FibonacciTextReader fibo = new FibonacciTextReader(fibonacciTerm);
            string result = string.Empty;
            string currentLine = string.Empty;
            while (currentLine != null)
            {
                currentLine = fibo.ReadLine();
                result += currentLine;
            }

            return result;
        }
    }
}