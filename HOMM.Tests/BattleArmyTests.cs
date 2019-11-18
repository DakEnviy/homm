using System;
using System.Collections.Generic;
using HOMM.BattleObjects;
using HOMM.Objects;
using HOMM.Units;
using NUnit.Framework;

namespace HOMM.Tests
{
    [TestFixture]
    public class BattleArmyTests
    {
        [Test]
        public void CreateBattleArmy()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);

            List<UnitsStack> stacks = new List<UnitsStack> {stack1, stack2};

            Army baseArmy = new Army(stacks);
            BattleArmy army = new BattleArmy(baseArmy);

            Assert.AreEqual(baseArmy, army.GetBaseArmy());
            Assert.AreEqual(stack1, army.GetBaseArmy().GetStacks()[0]);
            Assert.AreEqual(stack2, army.GetBaseArmy().GetStacks()[1]);
            Assert.AreEqual(stack1, army.GetStack(0).GetBaseStack());
            Assert.AreEqual(stack2, army.GetStack(1).GetBaseStack());
        }

        [Test]
        public void BattleArmy_GetStackByUnitType()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(angel, 5);
            UnitsStack stack3 = new UnitsStack(skeleton, 42);

            List<UnitsStack> stacks = new List<UnitsStack> {stack1, stack2, stack3};

            Army baseArmy = new Army(stacks);
            BattleArmy army = new BattleArmy(baseArmy);

            IList<BattleUnitsStack> angels = army.GetStacksByUnitType(UnitType.Angel);
            IList<BattleUnitsStack> skeletons = army.GetStacksByUnitType(UnitType.Skeleton);

            Assert.AreEqual(2, angels.Count);
            Assert.AreEqual(stack1, angels[0].GetBaseStack());
            Assert.AreEqual(stack2, angels[1].GetBaseStack());
            Assert.AreEqual(1, skeletons.Count);
            Assert.AreEqual(stack3, skeletons[0].GetBaseStack());
        }

        [Test]
        public void BattleArmy_AddStack()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(angel, 5);
            BattleUnitsStack stack3 = new BattleUnitsStack(new UnitsStack(skeleton, 42));
            BattleUnitsStack stack4 = new BattleUnitsStack(new UnitsStack(skeleton, 42));
            BattleUnitsStack stack5 = new BattleUnitsStack(new UnitsStack(skeleton, 42));
            BattleUnitsStack stack6 = new BattleUnitsStack(new UnitsStack(skeleton, 42));
            BattleUnitsStack stack7 = new BattleUnitsStack(new UnitsStack(skeleton, 42));
            BattleUnitsStack stack8 = new BattleUnitsStack(new UnitsStack(skeleton, 42));
            BattleUnitsStack stack9 = new BattleUnitsStack(new UnitsStack(skeleton, 42));
            BattleUnitsStack stack10 = new BattleUnitsStack(new UnitsStack(skeleton, 42));

            List<UnitsStack> stacks = new List<UnitsStack> {stack1, stack2};

            Army baseArmy = new Army(stacks);
            BattleArmy army = new BattleArmy(baseArmy);

            Assert.AreEqual(2, army.GetStacks().Count);
            army.AddStack(stack3);
            Assert.AreEqual(3, army.GetStacks().Count);
            Assert.AreEqual(stack3, army.GetStack(2));
            army.AddStack(stack4);
            army.AddStack(stack5);
            army.AddStack(stack6);
            army.AddStack(stack7);
            army.AddStack(stack8);
            army.AddStack(stack9);
            Assert.AreEqual(9, army.GetStacks().Count);
            Assert.Throws<ArgumentOutOfRangeException>(() => army.AddStack(stack10));
        }

        [Test]
        public void BattleArmy_RemoveStack()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);

            List<UnitsStack> stacks = new List<UnitsStack> {stack1, stack2};

            Army baseArmy = new Army(stacks);
            BattleArmy army = new BattleArmy(baseArmy);

            BattleUnitsStack stack = army.GetStack(0);
            Assert.AreEqual(2, army.GetStacks().Count);
            Assert.IsTrue(army.RemoveStack(stack));
            Assert.AreEqual(1, army.GetStacks().Count);
            Assert.IsFalse(army.RemoveStack(stack));
        }
        
        [Test]
        public void BattleArmy_ContainsStack()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();

            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);
            BattleUnitsStack stack3 = new BattleUnitsStack(new UnitsStack(skeleton, 42));

            List<UnitsStack> stacks = new List<UnitsStack> {stack1, stack2};

            Army baseArmy = new Army(stacks);
            BattleArmy army = new BattleArmy(baseArmy);

            army.AddStack(stack3);
            Assert.IsTrue(army.ContainsStack(stack3));
        }
    }
}