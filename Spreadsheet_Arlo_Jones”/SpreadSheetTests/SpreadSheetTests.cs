// <copyright file="SpreadSheetTests.cs" company="Arlo Jones">
// Copyright (c) { Aejace studios }. All rights reserved.
// </copyright>

namespace SpreadSheetTests
{
    using Cpts321;
    using NUnit.Framework;

    /// <summary>
    /// A class for testing spreadsheet and cell functionality.
    /// </summary>
    public class SpreadSheetTests
    {
        /// <summary>
        /// This tests the catches for references.
        /// </summary>
        internal class TestReference
        {
            /// <summary>
            /// Tests for out of range or bad references.
            /// </summary>
            [Test]
            public void TestBadReference()
            {
                var spreadsheet = new SpreadSheet(3, 3);
                spreadsheet.SetCellText(0, 0, "=Ab");
                spreadsheet.SetCellText(0, 1, "=C50");
                spreadsheet.SetCellText(1, 2, "=AA3");

                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(0, 0).Value, "Bad reference");
                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(0, 1).Value, "Bad reference");
                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(1, 2).Value, "Bad reference");
            }

            /// <summary>
            /// Tests for self reference.
            /// </summary>
            [Test]
            public void TestSelfRef()
            {
                var spreadsheet = new SpreadSheet(3, 3);
                spreadsheet.SetCellText(0, 0, "=A0");
                spreadsheet.SetCellText(1, 1, "=B1");
                spreadsheet.SetCellText(2, 2, "=C2");

                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(0, 0).Value, "Self reference");
                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(1, 1).Value, "Self reference");
                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(2, 2).Value, "Self reference");
            }

            /// <summary>
            /// Tests for Circular reference.
            /// </summary>
            [Test]
            public void TestCircRef()
            {
                var spreadsheet = new SpreadSheet(3, 3);
                spreadsheet.SetCellText(0, 0, "=B1");
                spreadsheet.SetCellText(1, 1, "=C2");
                spreadsheet.SetCellText(2, 2, "=A0");

                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(0, 0).Value, "0");
                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(1, 1).Value, "0");
                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(2, 2).Value, "Circular reference");

                spreadsheet.SetCellText(2, 2, "8");

                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(0, 0).Value, "8");
                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(1, 1).Value, "8");
                Assert.AreEqual(spreadsheet.GetCellByRowAndColumn(2, 2).Value, "8");
            }
        }
    }
}