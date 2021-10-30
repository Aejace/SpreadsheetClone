// <copyright file="Program.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace ExpressionTreeDemo
{
    using System;
    using System.Collections.Generic;
    using Cpts321;

    /// <summary>
    /// Class that contains main and other relevant functions.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Displays menu options to console.
        /// </summary>
        /// <param name="currentExpression"> Program holds one math expression at a given time, which the menu displays. </param>
        private static void DisplayMenu(string currentExpression)
        {
            Console.WriteLine("Menu: (Current expression: " + currentExpression + ")");
            Console.WriteLine("1 = Enter a new expression: ");
            Console.WriteLine("2 = Set a variable value: ");
            Console.WriteLine("3 = Evaluate Tree: ");
            Console.WriteLine("4 = Quit: ");
        }

        /// <summary>
        /// Set variable values in a given expression tree's dictionary of variables.
        /// </summary>
        /// <param name="tree"> tree contains a dictionary it uses to evaluate variable nodes with. </param>
        private static void SetVariableValue(ExpressionTree tree)
        {
            string variable;
            double value;
            Console.WriteLine("Enter variable name: ");
            variable = Console.ReadLine();
            Console.WriteLine("Enter variable value: ");
            value = Convert.ToDouble(Console.ReadLine());
            tree.SetVariable(variable.ToString(), value);
        }

        /// <summary>
        /// Entry point for console app.
        /// </summary>
        private static void Main()
        {
            string expression = string.Empty; // Default expression
            Dictionary<string, double> defaultDictionary = new Dictionary<string, double>();
            ExpressionTree tree = new ExpressionTree(expression, defaultDictionary);

            DisplayMenu(expression);
            string userInput = Console.ReadLine(); // Get intial user input.

            // Menu options
            while (userInput != "4")
            {
                switch (userInput)
                {
                    case "1":
                        expression = Console.ReadLine();
                        tree = new ExpressionTree(expression, defaultDictionary);
                        break;

                    case "2":
                        SetVariableValue(tree);
                        break;

                    case "3":
                        Console.WriteLine(tree.Evaluate());
                        break;
                }

                DisplayMenu(expression);
                userInput = Console.ReadLine();
            }
        }
    }
}
