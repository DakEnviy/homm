using System;
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

        [Test]
        public void Battle_Start()
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

            Assert.AreEqual(BattleState.None, battle.GetBattleState());
            battle.Start();
            Assert.AreEqual(BattleState.InGame, battle.GetBattleState());
            Assert.AreEqual(1, battle.GetRound());
        }

        [Test]
        public void Battle_Stop()
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

            Assert.AreEqual(BattleState.None, battle.GetBattleState());
            battle.Stop();
            Assert.AreEqual(BattleState.Ended, battle.GetBattleState());
        }

        [Test]
        public void Battle_TryToStop()
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
            BattleUnitsStack battleStack1 = battle.GetStacks()[0];
            BattleUnitsStack battleStack2 = battle.GetStacks()[1];
            BattleUnitsStack battleStack3 = battle.GetStacks()[2];
            
            battle.Start();
            battleStack3.SetHitPoints(0);
            battle.TryToStop();
            Assert.AreEqual(BattleState.Ended, battle.GetBattleState());
            Assert.AreEqual(army1, battle.GetWinner().GetBaseArmy());
            
            battle.Start();
            battleStack1.SetHitPoints(0);
            battleStack2.SetHitPoints(0);
            battleStack3.RestoreFromBaseStack();
            battle.TryToStop();
            Assert.AreEqual(BattleState.Ended, battle.GetBattleState());
            Assert.AreEqual(army2, battle.GetWinner().GetBaseArmy());
            
            battle.Start();
            battleStack1.SetHitPoints(0);
            battleStack2.SetHitPoints(0);
            battleStack3.SetHitPoints(0);
            battle.TryToStop();
            Assert.AreEqual(BattleState.Ended, battle.GetBattleState());
            Assert.AreEqual(army2, battle.GetWinner().GetBaseArmy());
        }

        [Test]
        public void Battle_NextRound()
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
            battle.Start();

            BattleUnitsStack battleStack1 = battle.GetStacks()[0];
            BattleUnitsStack battleStack3 = battle.GetStacks()[2];
            
            battleStack1.SetWaiting(true);
            battleStack3.SetDefended(true);

            Assert.AreEqual(1, battle.GetRound());
            Assert.IsTrue(battleStack1.IsWaiting());
            Assert.IsTrue(battleStack3.IsDefended());
            battle.NextRound();
            Assert.AreEqual(2, battle.GetRound());
            Assert.IsFalse(battleStack1.IsWaiting());
            Assert.IsFalse(battleStack3.IsDefended());
        }

        [Test]
        public void Battle_NextTurn()
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
            battle.Start();
            
            Assert.AreEqual(1, battle.GetRound());
            Assert.AreEqual(3, battle.GetCurrentStacks().Count);
            battle.NextTurn();
            Assert.AreEqual(2, battle.GetCurrentStacks().Count);
            battle.NextTurn();
            Assert.AreEqual(1, battle.GetCurrentStacks().Count);
            battle.NextTurn();
            Assert.AreEqual(2, battle.GetRound());
            Assert.AreEqual(3, battle.GetCurrentStacks().Count);
        }

        [Test]
        public void Battle_GetStacks()
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
            battle.Start();

            IList<BattleUnitsStack> stacks = battle.GetStacks();
            Assert.AreEqual(stack1, stacks[0].GetBaseStack());
            Assert.AreEqual(stack2, stacks[1].GetBaseStack());
            Assert.AreEqual(stack3, stacks[2].GetBaseStack());

            stacks[0].SetHitPoints(0);
            
            IList<BattleUnitsStack> aliveStacks = battle.GetAliveStacks();
            IList<BattleUnitsStack> deadStacks = battle.GetDeadStacks();
            Assert.AreEqual(2, aliveStacks.Count);
            Assert.AreEqual(stacks[1], aliveStacks[0]);
            Assert.AreEqual(stacks[2], aliveStacks[1]);
            Assert.AreEqual(1, deadStacks.Count);
            Assert.AreEqual(stacks[0], deadStacks[0]);
        }

        [Test]
        public void Battle_Attack()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);
            UnitsStack stack3 = new UnitsStack(angel, 3);

            List<UnitsStack> stacks1 = new List<UnitsStack> {stack1, stack2};
            List<UnitsStack> stacks2 = new List<UnitsStack> {stack3};

            Army army1 = new Army(stacks1);
            Army army2 = new Army(stacks2);

            Battle battle = new Battle(army1, army2);
            battle.Start();

            BattleUnitsStack target1 = battle.GetCurrentStacks()[1];
            BattleUnitsStack target2 = battle.GetCurrentStacks()[2];

            Assert.AreEqual(3,battle.GetCurrentStacks().Count);
            Assert.Throws<InvalidOperationException>(() => battle.Attack(target2));
            battle.Attack(target1);
            Assert.AreEqual(1, target1.GetAmount());
            Assert.AreEqual(90, target1.GetTopHitPoints());
            Assert.AreEqual(2,battle.GetCurrentStacks().Count);
        }

        [Test]
        public void Battle_Wait()
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
            battle.Start();

            BattleUnitsStack mainStack = battle.GetStacks()[0];
            
            IList<BattleUnitsStack> currentStacks = battle.GetCurrentStacks();
            Assert.AreEqual(stack1, currentStacks[0].GetBaseStack());
            Assert.AreEqual(stack3, currentStacks[1].GetBaseStack());
            Assert.AreEqual(stack2, currentStacks[2].GetBaseStack());

            battle.Wait();
            Assert.IsTrue(mainStack.IsWaiting());
            
            IList<BattleUnitsStack> currentStacks1 = battle.GetCurrentStacks();
            Assert.AreEqual(stack3, currentStacks1[0].GetBaseStack());
            Assert.AreEqual(stack2, currentStacks1[1].GetBaseStack());
            Assert.AreEqual(stack1, currentStacks1[2].GetBaseStack());
            
            battle.NextRound();
            Assert.IsFalse(mainStack.IsWaiting());
        }

        [Test]
        public void Battle_Defend()
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
            battle.Start();

            BattleUnitsStack currentStack = battle.GetCurrentStack();
            
            Assert.AreEqual(3,battle.GetCurrentStacks().Count);
            battle.Defend();
            Assert.IsTrue(currentStack.IsDefended());
            Assert.AreEqual(35, currentStack.GetDefence());
            Assert.AreEqual(2,battle.GetCurrentStacks().Count);
        }

        [Test]
        public void Battle_Surrender()
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
            
            battle.Start();
            Assert.AreEqual(BattleState.InGame, battle.GetBattleState());
            battle.Surrender();
            Assert.AreEqual(army2, battle.GetWinner().GetBaseArmy());
            Assert.AreEqual(BattleState.Ended, battle.GetBattleState());

            battle.Start();
            Assert.AreEqual(BattleState.InGame, battle.GetBattleState());
            battle.Wait();
            battle.Surrender();
            Assert.AreEqual(army1, battle.GetWinner().GetBaseArmy());
            Assert.AreEqual(BattleState.Ended, battle.GetBattleState());
        }

        [Test]
        public void Battle_TestBattle()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);
            UnitsStack stack3 = new UnitsStack(angel, 3);

            List<UnitsStack> stacks1 = new List<UnitsStack> {stack1, stack2};
            List<UnitsStack> stacks2 = new List<UnitsStack> {stack3};

            Army army1 = new Army(stacks1);
            Army army2 = new Army(stacks2);
            
            Battle battle = new Battle(army1, army2);
            battle.Start();
            
            BattleUnitsStack target1 = battle.GetStacks()[0];
            BattleUnitsStack target2 = battle.GetStacks()[1];
            BattleUnitsStack target3 = battle.GetStacks()[2];
            
            battle.Attack(target3);
            Assert.AreEqual(1755, target1.GetHitPoints());
            Assert.AreEqual(90, target3.GetHitPoints());
            battle.Attack(target2);
            Assert.AreEqual(90, target3.GetHitPoints());
            Assert.AreEqual(109, target2.GetHitPoints());
            battle.Attack(target3);
            Assert.AreEqual(109, target2.GetHitPoints());
            Assert.AreEqual(80, target3.GetHitPoints());
            Assert.AreEqual(2, battle.GetRound());
            
            Assert.AreEqual(BattleState.InGame, battle.GetBattleState());
            Assert.IsNull(battle.GetWinner());
            Assert.IsTrue(target3.IsAlive());
            battle.Attack(target3);
            Assert.IsTrue(target3.IsDead());
            Assert.AreEqual(army1, battle.GetWinner().GetBaseArmy());
            Assert.AreEqual(BattleState.Ended, battle.GetBattleState());
        }
    }
}