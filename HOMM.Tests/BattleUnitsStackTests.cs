using HOMM.BattleObjects;
using HOMM.Objects;
using HOMM.Units;
using NUnit.Framework;

namespace HOMM.Tests
{
    [TestFixture]
    public class BattleUnitsStackTests
    {
        [Test]
        public void CreateBattleUnitsStack()
        {
            Unit unit = new UnitAngel();
            const int amount = 128;
            
            UnitsStack baseStack = new UnitsStack(unit, amount);
            
            BattleUnitsStack stack = new BattleUnitsStack(baseStack, null);
            
            Assert.AreEqual(baseStack, stack.GetBaseStack());
            Assert.AreEqual(unit, stack.GetBaseUnit());
            Assert.IsNull(stack.GetArmy());
            Assert.AreEqual(amount, stack.GetAmount());
            Assert.AreEqual(unit.GetHitPoints(), stack.GetTopHitPoints());
            Assert.AreEqual(unit.GetAttack(), stack.GetAttack());
            Assert.AreEqual(unit.GetDefence(), stack.GetDefence());
            Assert.AreEqual(unit.GetDamage(), stack.GetDamage());
            Assert.AreEqual(unit.GetInitiative(), stack.GetInitiative());
        }

        [Test]
        public void BattleUnitsStack_Setters()
        {
            Unit unit = new UnitAngel();
            const int amount = 128;
            const int newAmount = 100;
            const int newTopHitPoints = 50;
            const int newAttack = 10;
            const int newDefence = 12;
            (int, int) newDamage = (15, 30);
            float newInitiative = 1.1F;
            
            UnitsStack baseStack = new UnitsStack(unit, amount);
            
            BattleUnitsStack stack = new BattleUnitsStack(baseStack, null);
            
            Assert.AreEqual(amount, stack.GetAmount());
            stack.SetAmount(newAmount);
            Assert.AreEqual(newAmount, stack.GetAmount());
            
            Assert.AreEqual(unit.GetHitPoints(), stack.GetTopHitPoints());
            stack.SetTopHitPoints(newTopHitPoints);
            Assert.AreEqual(newTopHitPoints, stack.GetTopHitPoints());
            
            Assert.AreEqual(unit.GetAttack(), stack.GetAttack());
            stack.SetAttack(newAttack);
            Assert.AreEqual(newAttack, stack.GetAttack());
            
            Assert.AreEqual(unit.GetDefence(), stack.GetDefence());
            stack.SetDefence(newDefence);
            Assert.AreEqual(newDefence, stack.GetDefence());
            
            Assert.AreEqual(unit.GetDamage(), stack.GetDamage());
            stack.SetDamage(newDamage);
            Assert.AreEqual(newDamage, stack.GetDamage());
            
            Assert.AreEqual(unit.GetInitiative(), stack.GetInitiative());
            stack.SetInitiative(newInitiative);
            Assert.AreEqual(newInitiative, stack.GetInitiative());
        }

        [Test]
        public void BattleUnitsStack_Heal()
        {
            Unit unit = new UnitAngel();
            const int amount = 128;
            const int newTopHitPoints = 50;
            const int healHitPoints = 100;
            
            UnitsStack baseStack = new UnitsStack(unit, amount);
            
            BattleUnitsStack stack = new BattleUnitsStack(baseStack, null);
            
            Assert.AreEqual(unit.GetHitPoints(), stack.GetTopHitPoints());
            stack.SetTopHitPoints(newTopHitPoints);
            Assert.AreEqual(newTopHitPoints, stack.GetTopHitPoints());
            stack.Heal(healHitPoints);
            Assert.AreEqual(newTopHitPoints + healHitPoints, stack.GetTopHitPoints());
        }

        [Test]
        public void BattleUnitsStack_GetHitPoints()
        {
            Unit unit = new UnitAngel();
            const int amount = 128;
            const int newTopHitPoints = 80;
            const int newAmount = 120;

            int stackHitPoints = amount * unit.GetHitPoints();
            int newStackHitPoints = stackHitPoints - 100;
            int newStackHitPoints1 = newStackHitPoints - (amount - newAmount) * unit.GetHitPoints();
            
            UnitsStack baseStack = new UnitsStack(unit, amount);
            
            BattleUnitsStack stack = new BattleUnitsStack(baseStack, null);
            
            Assert.AreEqual(stackHitPoints, stack.GetHitPoints());
            stack.SetTopHitPoints(newTopHitPoints);
            Assert.AreEqual(newStackHitPoints, stack.GetHitPoints());
            stack.SetAmount(newAmount);
            Assert.AreEqual(newStackHitPoints1, stack.GetHitPoints());
        }

        [Test]
        public void BattleUnitsStack_SetHitPoints()
        {
            Unit unit = new UnitAngel();
            const int amount = 128;
            const int newStackHitPoints = 368;
            const int newStackHitPoints1 = -100;
            const int newAmount = 3;
            const int newTopHitPoints = 8;
            
            int stackHitPoints = amount * unit.GetHitPoints();
            int newStackHitPoints2 = stackHitPoints + 100;
            
            UnitsStack baseStack = new UnitsStack(unit, amount);
            
            BattleUnitsStack stack = new BattleUnitsStack(baseStack, null);
            
            Assert.AreEqual(stackHitPoints, stack.GetHitPoints());
            Assert.AreEqual(amount, stack.GetAmount());
            Assert.AreEqual(unit.GetHitPoints(), stack.GetTopHitPoints());
            stack.SetHitPoints(newStackHitPoints);
            Assert.AreEqual(newStackHitPoints, stack.GetHitPoints());
            Assert.AreEqual(newAmount, stack.GetAmount());
            Assert.AreEqual(newTopHitPoints, stack.GetTopHitPoints());
            stack.SetHitPoints(newStackHitPoints1);
            Assert.AreEqual(0, stack.GetHitPoints());
            Assert.AreEqual(0, stack.GetAmount());
            Assert.AreEqual(0, stack.GetTopHitPoints());
            stack.SetHitPoints(newStackHitPoints2);
            Assert.AreEqual(stackHitPoints, stack.GetHitPoints());
            Assert.AreEqual(amount, stack.GetAmount());
            Assert.AreEqual(unit.GetHitPoints(), stack.GetTopHitPoints());
        }

        [Test]
        public void BattleUnitsStack_Resurrect()
        {
            Unit unit = new UnitAngel();
            const int amount = 128;
            const int resurrectHitPoints = 188;
            
            UnitsStack baseStack = new UnitsStack(unit, amount);
            
            BattleUnitsStack stack = new BattleUnitsStack(baseStack, null);
            
            stack.SetHitPoints(0);
            stack.Resurrect(resurrectHitPoints);
            Assert.AreEqual(resurrectHitPoints, stack.GetHitPoints());
            stack.Resurrect(resurrectHitPoints);
            Assert.AreEqual(resurrectHitPoints * 2, stack.GetHitPoints());
        }
        
        [Test]
        public void BattleUnitsStack_Damage()
        {
            Unit unit = new UnitAngel();
            const int amount = 128;
            const int damageHitPoints = 55;
            
            int stackHitPoints = amount * unit.GetHitPoints();
            
            UnitsStack baseStack = new UnitsStack(unit, amount);
            
            BattleUnitsStack stack = new BattleUnitsStack(baseStack, null);
            
            stack.Damage(damageHitPoints);
            Assert.AreEqual(stackHitPoints - damageHitPoints, stack.GetHitPoints());
            stack.Damage(damageHitPoints);
            Assert.AreEqual(stackHitPoints - damageHitPoints * 2, stack.GetHitPoints());
        }
    }
}