using System;
using HOMM.Objects;
using HOMM.Units;
using NUnit.Framework;

namespace HOMM.Tests
{
    [TestFixture]
    public class UnitsStackTests
    {
        [Test]
        public void CreateUnitsStack()
        {
            Unit unit = new UnitAngel();
            const int amount = 128;
            
            UnitsStack stack = new UnitsStack(unit, amount);
            
            Assert.AreEqual(unit, stack.GetUnit());
            Assert.AreEqual(amount, stack.GetAmount());
        }

        [Test]
        public void CreateUnitsStack_OutOfRange()
        {
            Unit unit = new UnitAngel();
            const int amount = 1_000_000;

            Assert.Throws<ArgumentOutOfRangeException>(() => new UnitsStack(unit, amount));
        }
    }
}