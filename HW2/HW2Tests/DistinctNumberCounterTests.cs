// <copyright file="DistinctNumberCounterTests.cs" company="Arlo Jones 011778052">
// Copyright (c) Arlo Jones 011778052. All rights reserved.
// </copyright>

namespace HW2.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// A class for testing the number of distinct elements in a list useing three different methods.
    /// </summary>
    public class DistinctNumberCounterTests
    {
        /// <summary>
        /// Declare local variables. distinctNumberCounter is an object which contains the three methods tested below.
        /// </summary>
        private DistinctNumberCounter distinctNumberCounter = new DistinctNumberCounter();

        /// <summary>
        /// Creats a list and adds elements to it. The list is passed to the HashSetMethod
        /// which returns the number of unique elements in the list.
        /// </summary>
        /// <param name="listValue0"> First element to be added to list.</param>
        /// <param name="listValue1"> Second element to be added to list.</param>
        /// <param name="listValue2"> Third element to be added to list.</param>
        /// <returns> A string containing the number of unique values in the list.</returns>
        [TestCase(0, 1, 2, ExpectedResult = "3")] // Testing a list with three items in order.
        [TestCase(2, 1, 0, ExpectedResult = "3")] // Testing a list with items out of order.
        [TestCase(0, 0, 0, ExpectedResult = "1")] // Testing a list with repeating items.
        [TestCase(1, 0, 0, ExpectedResult = "2")] // Testing a list with repeating items out of order.
        public string HashSetTests(int listValue0, int listValue1, int listValue2)
        {
            System.Collections.Generic.List<int> listOfTestInts = new System.Collections.Generic.List<int> { listValue0, listValue1, listValue2 };
            return this.distinctNumberCounter.HashSetMethod(listOfTestInts);
        }

        /// <summary>
        /// Creats a list and adds elements to it. The list is passed to the ConstantStorage
        /// which returns the number of unique elements in the list.
        /// </summary>
        /// <param name="listValue0"> First element to be added to list.</param>
        /// <param name="listValue1"> Second element to be added to list.</param>
        /// <param name="listValue2"> Third element to be added to list.</param>
        /// <returns> A string containing the number of unique values in the list.</returns>
        [TestCase(0, 1, 2, ExpectedResult = "3")] // Testing a list with items in order.
        [TestCase(2, 1, 0, ExpectedResult = "3")] // Testing a list with items out of order.
        [TestCase(0, 0, 0, ExpectedResult = "1")] // Testing a list with repeating items.
        [TestCase(1, 0, 0, ExpectedResult = "2")] // Testing a list with repeating items out of order.
        public string ConstantStorageTests(int listValue0, int listValue1, int listValue2)
        {
            System.Collections.Generic.List<int> listOfTestInts = new System.Collections.Generic.List<int> { listValue0, listValue1, listValue2 };
            return this.distinctNumberCounter.ConstantStorageMethod(listOfTestInts);
        }

        /// <summary>
        /// Creats a list and adds elements to it. The list is passed to the SortedMethod
        /// which returns the number of unique elements in the list.
        /// </summary>
        /// <param name="listValue0"> First element to be added to list.</param>
        /// <param name="listValue1"> Second element to be added to list.</param>
        /// <param name="listValue2"> Third element to be added to list.</param>
        /// <returns> A string containing the number of unique values in the list.</returns>
        [TestCase(0, 1, 2, ExpectedResult = "3")] // Testing a list with three items in order.
        [TestCase(2, 1, 0, ExpectedResult = "3")] // Testing a list with items out of order.
        [TestCase(0, 0, 0, ExpectedResult = "1")] // Testing a list with repeating items.
        [TestCase(1, 0, 0, ExpectedResult = "2")] // Testing a list with repeating items out of order.
        public string SortedTests(int listValue0, int listValue1, int listValue2)
        {
            System.Collections.Generic.List<int> listOfTestInts = new System.Collections.Generic.List<int> { listValue0, listValue1, listValue2 };
            return this.distinctNumberCounter.SortedMethod(listOfTestInts);
        }
    }
}