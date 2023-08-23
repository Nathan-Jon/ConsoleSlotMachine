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

        ISlotMachineLogic GridLogic;

        public GridLogicUnitTests()
        {
            Random random = new Random();
            IConfigReader configReader = new ConfigReader();
            ICellValueLogic cellValues = new CellValueLogic(random);

            GridLogic = new SlotMachineLogic(cellValues);
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

        [TestMethod]
        public void WheelRowIsWinner_Success()
        {
            WheelCell[] cells = new WheelCell[]
            {
              new WheelCell(CellValueEnum.Apple, (decimal)0.4, 45),
              new WheelCell(CellValueEnum.Apple, (decimal)0.4, 45),
              new WheelCell(CellValueEnum.Apple, (decimal)0.4, 45)
            };

            WheelRow row = new WheelRow(cells);

            Assert.IsTrue(row.IsWinningRow());
        }

        [TestMethod]
        public void WheelRowIsWinner_Fail()
        {
            WheelCell[] cells = new WheelCell[]
            {
              new WheelCell(CellValueEnum.Apple,(decimal) 0.4, 45),
              new WheelCell(CellValueEnum.Apple, (decimal)0.4, 45),
              new WheelCell(CellValueEnum.Pineapple, (decimal)0.8, 45)
            };

            WheelRow row = new WheelRow(cells);

            Assert.IsFalse(row.IsWinningRow());
        }

        [TestMethod]
        public void WheelRowWildcardIsWinner_Sucess()
        {
            WheelCell[] cells = new WheelCell[]
            {
              new WheelCell(CellValueEnum.Apple, (decimal)0.4, 45),
              new WheelCell(CellValueEnum.Wildcard, 0, 5),
              new WheelCell(CellValueEnum.Wildcard, 0, 5),
            };

            WheelRow row = new WheelRow(cells);

            Assert.IsTrue(row.IsWinningRow());
        }
        
        [TestMethod]
        public void WheelRowCorrectWinCoefficient_Success()
        {
            WheelCell[] cells = new WheelCell[]
            {
              new WheelCell(CellValueEnum.Apple, (decimal)0.4, 45),
              new WheelCell(CellValueEnum.Apple, (decimal) 0.4, 45),
              new WheelCell(CellValueEnum.Apple, (decimal) 0.4, 45)
            };

            WheelRow row = new WheelRow(cells);

            decimal rowCoefficient = row.GetTotalRowCoefficient();

            Assert.AreEqual(rowCoefficient, (decimal)1.2);
        }

        [TestMethod]
        public void WheelRowCorrectWinCoefficient_Fail()
        {
            WheelCell[] cells = new WheelCell[]
            {
              new WheelCell(CellValueEnum.Apple, (decimal) 0.4, 45),
              new WheelCell(CellValueEnum.Pineapple, (decimal) 0.8, 15),
              new WheelCell(CellValueEnum.Apple, (decimal) 0.4, 45)
            };

            WheelRow row = new WheelRow(cells);

            decimal rowCoefficient = row.GetTotalRowCoefficient();

            Assert.AreNotEqual(rowCoefficient, (decimal)1.2);
        }

    }
}
