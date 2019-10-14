using HOMM.Units;
using NUnit.Framework;

namespace HOMM.Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void CreateUnit()
        {
            UnitType type = UnitType.Hydra;
            const uint hitPoints = 10;
            const uint attack = 11;
            const uint defence = 12;
            (uint, uint) damage = (5, 4);
            const float initiative = 13.0f;
            
            Unit unit = new Unit(type, hitPoints, attack, defence, damage, initiative);

            Assert.AreEqual(type, unit.GetUnitType());
            Assert.AreEqual(hitPoints, unit.GetHitPoints());
            Assert.AreEqual(attack, unit.GetAttack());
            Assert.AreEqual(defence, unit.GetDefence());
            Assert.AreEqual(damage, unit.GetDamage());
            Assert.AreEqual(initiative, unit.GetInitiative());
        }

        [Test]
        public void CreateUnitAngel()
        {
            UnitType type = UnitType.Angel;
            const uint hitPoints = 180;
            const uint attack = 27;
            const uint defence = 27;
            (uint, uint) damage = (45, 45);
            const float initiative = 11.0f;
            
            Unit unit = new UnitAngel();

            Assert.AreEqual(type, unit.GetUnitType());
            Assert.AreEqual(hitPoints, unit.GetHitPoints());
            Assert.AreEqual(attack, unit.GetAttack());
            Assert.AreEqual(defence, unit.GetDefence());
            Assert.AreEqual(damage, unit.GetDamage());
            Assert.AreEqual(initiative, unit.GetInitiative());
        }
    }
}