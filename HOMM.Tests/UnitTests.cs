using HOMM.Objects;
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
            const int hitPoints = 10;
            const int attack = 11;
            const int defence = 12;
            (int, int) damage = (5, 4);
            const float initiative = 13.0f;
            
            Unit unit = new Unit(hitPoints, attack, defence, damage, initiative);

            Assert.AreEqual(hitPoints, unit.GetHitPoints());
            Assert.AreEqual(attack, unit.GetAttack());
            Assert.AreEqual(defence, unit.GetDefence());
            Assert.AreEqual(damage, unit.GetDamage());
            Assert.AreEqual(initiative, unit.GetInitiative());
        }

        [Test]
        public void CreateUnitAngel()
        {
            const int hitPoints = 180;
            const int attack = 27;
            const int defence = 27;
            (int, int) damage = (45, 45);
            const float initiative = 11.0f;
            
            Unit unit = new UnitAngel();

            Assert.AreEqual(hitPoints, unit.GetHitPoints());
            Assert.AreEqual(attack, unit.GetAttack());
            Assert.AreEqual(defence, unit.GetDefence());
            Assert.AreEqual(damage, unit.GetDamage());
            Assert.AreEqual(initiative, unit.GetInitiative());
        }
    }
}