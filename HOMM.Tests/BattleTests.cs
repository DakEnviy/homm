using System.Collections.Generic;
using HOMM.BattleObjects;
using HOMM.Objects;
using HOMM.Units;
using NUnit.Framework;

namespace HOMM.Tests
{
    [TestFixture]
    public class BattleTests
    {
        [Test]
        public void CreateBattle()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);
            UnitsStack stack3 = new UnitsStack(angel, 2);

            List<UnitsStack> stacks1 = new List<UnitsStack> {stack1, stack2};
            List<UnitsStack> stacks2 = new List<UnitsStack> {stack3};

            Army baseArmy1 = new Army(stacks1);
            Army baseArmy2 = new Army(stacks2);
            
            BattleArmy army1 = new BattleArmy(baseArmy1);
            BattleArmy army2 = new BattleArmy(baseArmy2);
            
            List<BattleArmy> armies = new List<BattleArmy> {army1, army2};
            
            Battle battle = new Battle(armies);
            
            Assert.AreEqual(armies, battle.GetArmies());
            Assert.AreEqual(BattleState.None, battle.GetBattleState());
            Assert.AreEqual(0, battle.GetRound());
            Assert.IsNull(battle.GetWinner());
            Assert.AreEqual(-1, battle.GetCurrentArmyIndex());
            Assert.IsNull(battle.GetCurrentStack());
        }
    }
}