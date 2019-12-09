using System;
using System.Collections.Generic;
using HOMM.BattleObjects;
using HOMM.Mod;
using HOMM.Objects;
using NUnit.Framework;

namespace HOMM.Tests
{
    [TestFixture]
    public class ModTests
    {
        [SetUp]
        public void BeforeEach()
        {
            Environment.SetEnvironmentVariable("is-testing", "true");
            
            HommMod mod = new TestMod();
            
            ModLoader.RegisterMod(mod);
            ModLoader.EnableMods();
        }
            
        [Test]
        public void Mod_Battle_TestBattle()
        {
            Skill resurrect = Skills.Skills.GetSkill("resurrect");
            
            Unit angel = Units.Units.GetUnit("angel");
            Unit skeleton = Units.Units.GetUnit("skeleton");
            Unit lich = Units.Units.GetUnit("lich");

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);
            UnitsStack stack3 = new UnitsStack(lich, 5);
            UnitsStack stack4 = new UnitsStack(lich, 20);
            
            List<UnitsStack> stacks1 = new List<UnitsStack> {stack1, stack2};
            List<UnitsStack> stacks2 = new List<UnitsStack> {stack3, stack4};
            
            Army army1 = new Army(stacks1);
            Army army2 = new Army(stacks2);
            
            Battle battle = new Battle(army1, army2);

            IList<BattleUnitsStack> battleStacks = battle.GetStacks();

            BattleUnitsStack battleStack1 = battleStacks[0];
            BattleUnitsStack battleStack2 = battleStacks[1];
            BattleUnitsStack battleStack3 = battleStacks[2];
            BattleUnitsStack battleStack4 = battleStacks[3];
            
            battle.Start();
            Assert.AreEqual(1, battle.GetRound());
            
            Assert.AreEqual(battleStack1, battle.GetCurrentStack());
            Assert.Throws<ArgumentException>(() => battle.UseSkill(resurrect, battleStack2));
            battle.Attack(battleStack4);
            Assert.AreEqual(1800, battleStack1.GetHitPoints());
            Assert.AreEqual(280, battleStack4.GetHitPoints());
            
            Assert.AreEqual(battleStack2, battle.GetCurrentStack());
            battle.Wait();
            Assert.IsTrue(battleStack2.IsWaiting());
            
            Assert.AreEqual(battleStack3, battle.GetCurrentStack());
            battle.UseSkill(resurrect, battleStack3);
            Assert.AreEqual(battleStack3, battle.GetCurrentStack());
//            battle.UseSkill(resurrect, battleStack1);
//            Assert.AreEqual(battleStack3, battle.GetCurrentStack());
            battle.UseSkill(resurrect, battleStack4);
            Assert.AreEqual(530, battleStack4.GetHitPoints());
            
            Assert.AreEqual(battleStack4,battle.GetCurrentStack());
            battle.Attack(battleStack2);
            Assert.AreEqual(0, battleStack2.GetHitPoints());
            Assert.AreEqual(530, battleStack4.GetHitPoints());
            
            Assert.AreEqual(2, battle.GetRound());
            
            Assert.AreEqual(battleStack1, battle.GetCurrentStack());
            battle.Attack(battleStack4);
            Assert.AreEqual(0, battleStack4.GetHitPoints());
            Assert.AreEqual(1800, battleStack1.GetHitPoints());
            
            Assert.AreEqual(battleStack3, battle.GetCurrentStack());
            battle.Attack(battleStack1);
            Assert.AreEqual(1763, battleStack1.GetHitPoints());
            Assert.AreEqual(250, battleStack3.GetHitPoints());
            
            Assert.AreEqual(3, battle.GetRound());
            
            Assert.AreEqual(battleStack1, battle.GetCurrentStack());
            battle.Attack(battleStack3);
            Assert.AreEqual(0, battleStack3.GetHitPoints());
            Assert.AreEqual(1763, battleStack1.GetHitPoints());
            
            Assert.AreEqual(BattleState.Ended, battle.GetBattleState());
            Assert.AreEqual(army1, battle.GetWinner().GetBaseArmy());
        }
    }
}