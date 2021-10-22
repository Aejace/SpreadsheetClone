using Cpts321;
using System;

namespace ExpressionTreeDemo
{
    class Program
    {
        static void DisplayMenu(string currentExpression)
        {
            Console.WriteLine("Menu: (Current expression: " + currentExpression + ")");
            Console.WriteLine("1 = Enter a new expression: ");
            Console.WriteLine("2 = Set a variable value: ");
            Console.WriteLine("3 = Evaluate Tree: ");
            Console.WriteLine("4 = Quit: ");
        }

        static void SetVariableValue(ExpressionTree tree)
        {
            char variable;
            double value;
            Console.WriteLine("Enter variable name: ");
            variable = Console.ReadLine()[0];
            Console.WriteLine("Enter variable value: ");
            value = Convert.ToDouble(Console.ReadLine());
            tree.SetVariable(variable.ToString(), value);
        }

        static void Main(string[] args)
        {
            string expression = "0";
            ExpressionTree tree = new ExpressionTree(expression);


            DisplayMenu(expression);
            string userInput = Console.ReadLine();

            while (userInput != "4")
            {
                switch(userInput)
                {
                    case "1":
                        expression = Console.ReadLine();
                        tree = new ExpressionTree(expression);
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
