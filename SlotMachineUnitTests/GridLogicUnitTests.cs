using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlotMachine;
using SlotMachine.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachineUnitTests
{
    [TestClass]
    public class GridLogicUnitTests
    {

        IGridLogic GridLogic;

        public GridLogicUnitTests()
        {
            Random random = new Random();
            IConfigReader configReader = new ConfigReader();
            ICellValues cellValues = new CellValues(random);

            GridLogic = new GridLogic(configReader, cellValues);
        }

        [TestMethod]
        public void GenerateWheelRows_SingleRow_SingleColumn()
        {
            IList<IWheelRow> rows = new List<IWheelRow>();
            rows = GridLogic.GenerateWheelRows(1, 1);

            Assert.IsTrue(rows.Count == 1 && rows.FirstOrDefault().WheelCells.Count == 1);
        }

        [TestMethod]
        public void GenerateWheelRows_TwoRow_SingleColumn()
        {
            IList<IWheelRow> rows = new List<IWheelRow>();
            rows = GridLogic.GenerateWheelRows(2, 1);

            int rowCount = rows.Count;

            //Cell count will be # of Rows * # of Cells 
            int cellCount = rows.Sum(x => x.WheelCells.Count);

            
            Assert.IsTrue(rowCount == 2 && cellCount== 2);
        }

        [TestMethod]
        public void GenerateWheelRows_SingleRow_TwoColumns()
        {
            IList<IWheelRow> rows = new List<IWheelRow>();
            rows = GridLogic.GenerateWheelRows(1, 2);

            int rowCount = rows.Count;
            int cellCount = rows.Sum(x => x.WheelCells.Count);

            //Cell count will be # of Rows * # of Cells 
            Assert.IsTrue(rowCount == 1 && cellCount == 2);
        }

        [TestMethod]
        public void GenerateWheelRows_CorrectCellCount()
        {
            int totalRows = 20;
            int totalCellsPerRow = 20;

            IList<IWheelRow> rows = new List<IWheelRow>();
            rows = GridLogic.GenerateWheelRows(totalRows, totalCellsPerRow);

            int rowCount = rows.Count;
            int cellCount = rows.Sum(x => x.WheelCells.Count);

            //Cell count will be # of Rows * # of Cells 
            Assert.IsTrue(rowCount == 20 && cellCount == 400);
        }
    }
}
