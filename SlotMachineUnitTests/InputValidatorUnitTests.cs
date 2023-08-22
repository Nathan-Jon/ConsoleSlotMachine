using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlotMachine;
using System;

namespace SlotMachineUnitTests
{
    [TestClass]
    public class InputValidatorUnitTests
    {

        IInputValidator Validator;

        public InputValidatorUnitTests()
        {
            IConfigReader configReader = new ConfigReader();
            Validator = new InputValidator(configReader);
        }

        [TestMethod]
        public void IsZeroOrBelow_Fail()
        {
            Assert.IsFalse(Validator.ValidateIsZeroOrBelow(1));
        }

        [TestMethod]
        public void IsZeroOrBelow_Success()
        {
            Assert.IsTrue(Validator.ValidateIsZeroOrBelow(0));
        }

        [TestMethod]
        public void IsZeroOrBelow_Success2()
        {
            Assert.IsTrue(Validator.ValidateIsZeroOrBelow(-1));
        }

        [TestMethod]
        public void ValidateTextIsDouble_Fail()
        {
            Assert.IsFalse(Validator.ValidateTextIsDouble("test"));
        }

        [TestMethod]
        public void ValidateTextIsDouble_Success()
        {
            Assert.IsTrue(Validator.ValidateTextIsDouble("123"));
        }

        [TestMethod]
        public void ValidateValueIsHigher_Fail()
        {
            Assert.IsFalse(Validator.ValidateValueIsHigher(1, 100));

        }

        [TestMethod]
        public void ValidateValueIsHigher_Success()
        {
            Assert.IsTrue(Validator.ValidateValueIsHigher(100, 1));

        }

    }
}
