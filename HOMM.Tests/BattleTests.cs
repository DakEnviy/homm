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

            Army army1 = new Army(stacks1);
            Army army2 = new Army(stacks2);
            
            Battle battle = new Battle(army1, army2);
            
            Assert.AreEqual(army1, battle.GetAttacker().GetBaseArmy());
            Assert.AreEqual(army2, battle.GetTarget().GetBaseArmy());
            Assert.AreEqual(BattleState.None, battle.GetBattleState());
            Assert.AreEqual(0, battle.GetRound());
            Assert.IsNull(battle.GetWinner());
            Assert.IsNull(battle.GetCurrentStacks());
            Assert.IsNull(battle.GetCurrentStack());
            Assert.IsNull(battle.GetCurrentArmy());
        }
    }
}