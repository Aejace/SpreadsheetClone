// <copyright file="FibonacciTextReader.cs" company="Arlo Jones 011778052">
// Copyright (c) Aejace. All rights reserved.
// </copyright>

namespace HW3
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// FibonacciText reader is a text reader that inherits from TextReader. It is able to generate fibonacci numbers and output them.
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        /// <summary>
        /// A list of integers. Specified Big integers so that it can contain integers at the scale needed for large fibonacci numbers.
        /// </summary>
        private readonly List<BigInteger> fibonacciNumbers = new List<BigInteger>();

        /// <summary>
        /// Maintains an index value in the fibonaccinumbers list.
        /// </summary>
        private int index = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// <param name="fibonacciTerm"> Indicates how many numbers of the fibonacci sequence should be generated. </param>
        public FibonacciTextReader(int fibonacciTerm)
        {
            // Local variables
            bool isNegative = false; // Flag indicating whether or not negative fibonacci terms are being used.
            BigInteger newestNumber = 1; // Next number to add to the fibonacciNumbers list.

            // Sets the isNegative flag if necessary
            if (fibonacciTerm < 0)
            {
                fibonacciTerm = fibonacciTerm * -1;
                isNegative = true;
            }

            // Handles edgecase where fibonacciTerm is zero
            if (fibonacciTerm >= 0)
            {
                this.fibonacciNumbers.Add(0);
            }

            // Handles edgecases where fibonacciTerm is 1 or negative 1
            if (fibonacciTerm >= 1)
            {
                if (isNegative == true)
                {
                    newestNumber = newestNumber * -1;
                }

                this.fibonacciNumbers.Add(newestNumber);
            }

            // Generates fibonacci numbers for the rest of the list, up till the value indicated by fibonacciTerm.
            if (fibonacciTerm > 1)
            {
                for (this.index = 2; this.index <= fibonacciTerm; ++this.index)
                {
                    this.fibonacciNumbers.Add(this.fibonacciNumbers.ElementAt(this.index - 1) + this.fibonacciNumbers.ElementAt(this.index - 2));
                }
            }

            this.index = 0;
        }

        /// <summary>
        /// Overides the ReadLine method so special formating can be applied when FibonacciTextReader is called
        /// by something that takes a TextReader as a parameter. ReadLine will get the value held at the current
        /// index value in the fibonacciNumbers list, and return it with appropriate formatting.
        /// </summary>
        /// <returns> A string containing the current fibonacci number, and  relevant formatting. </returns>
        public override string ReadLine()
        {
            // Checks that index isnt past the size of the list.
            if (this.index < this.fibonacciNumbers.Count())
            {
                string output;

                // Checks if the list will have positive of negative index values, then compiles the output string appropriately.
                if (this.fibonacciNumbers.ElementAt(this.index) >= 0)
                {
                    output = "Term: " + this.index.ToString() + " = " + this.fibonacciNumbers.ElementAt(this.index).ToString() + "\r\n";
                }
                else
                {
                    int negIndex = this.index * -1;
                    output = "Term: " + negIndex + " = " + this.fibonacciNumbers.ElementAt(this.index).ToString() + "\r\n";
                }

                this.index++;
                return output;
            }
            else
            {
                return null;
            }
        }
    }
}
