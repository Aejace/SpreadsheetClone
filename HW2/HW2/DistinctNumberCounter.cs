// <copyright file="DistinctNumberCounter.cs" company="Arlo Jones 011778052">
// Copyright (c) Arlo Jones 011778052. All rights reserved.
// </copyright>

namespace HW2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class contains three different methods for obtaining the number of unique values in a given list of integers.
    /// </summary>
    public class DistinctNumberCounter
    {
        /// <summary>
        /// Determines the number of unique numbers in tenKRandomInts using a HashSet, which cannot contain duplicate values.
        /// Returns a string with value of the size of the hash set.
        /// </summary>
        /// <param name="randomInts"> A list of integers, presumed to be randomly generated. </param>
        /// <returns> Count of distinct numbers returned as a string. </returns>
        public string HashSetMethod(List<int> randomInts)
        {
            HashSet<int> distinctNumberHashSet = new HashSet<int>();
            return distinctNumberHashSet.Count.ToString();
        }

        /// <summary>
        /// Determines the number of unique numbers in tenKRandomInts with a constant storage limitation.
        /// This is accomplished by checking each element in the list against every element that came before it.
        /// If a duplicate is not found, the element is unique and a counter is incremented to keep track of the
        /// total number of unique values. The value of the counter is returned as a string.
        /// </summary>
        /// <param name="randomInts"> A list of integers, presumed to be randomly generated. </param>
        /// <returns> Count of distinct numbers returned as a string. </returns>
        public string ConstantStorageMethod(List<int> randomInts)
        {
            int count = default(int);
            return count.ToString();
        }

        /// <summary>
        /// Determines the number of unique numbers in tenKRandomInts with a constant storage limitation but makes
        /// use of a sorting function. With a sorted list, the number of unique values in the list can be determined by
        /// checking each element in the list against the element that came before it. If it is not a duplicate, the
        /// element is unique and a counter is incremented to keep track of the total number of unique values.
        /// The value of the counter is returned as a string.
        /// </summary>
        /// <param name="randomInts"> A list of integers, presumed to be randomly generated. </param>
        /// <returns> Count of distinct numbers returned as a string. </returns>
        public string SortedMethod(List<int> randomInts)
        {
            int count = default(int);
            return count.ToString();
        }
    }
}
