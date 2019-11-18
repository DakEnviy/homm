using System;
using System.Collections.Generic;
using HOMM.Objects;
using HOMM.Units;
using NUnit.Framework;

namespace HOMM.Tests
{
    [TestFixture]
    public class ArmyTests
    {
        [Test]
        public void CreateArmy()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();
            
            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);

            List<UnitsStack> stacks = new List<UnitsStack> {stack1, stack2};
            
            Army army = new Army(stacks);
            
            Assert.AreEqual(stacks, army.GetStacks());
        }

        [Test]
        public void CreateArmy_OutOfRange()
        {
            Unit angel = new UnitAngel();
            Unit skeleton = new UnitSkeleton();
            
            UnitsStack stack1 = new UnitsStack(angel, 10);
            UnitsStack stack2 = new UnitsStack(skeleton, 42);
            UnitsStack stack3 = new UnitsStack(skeleton, 11);
            UnitsStack stack4 = new UnitsStack(angel, 12);
            UnitsStack stack5 = new UnitsStack(angel, 1);
            UnitsStack stack6 = new UnitsStack(skeleton, 133);
            UnitsStack stack7 = new UnitsStack(skeleton, 977);

            List<UnitsStack> stacks = new List<UnitsStack> {stack1, stack2, stack3, stack4, stack5, stack6};
            
            Army army1 = new Army(stacks);
            
            Assert.AreEqual(stacks, army1.GetStacks());
            
            stacks.Add(stack7);

            Assert.Throws<ArgumentOutOfRangeException>(() => new Army(stacks));
        }
    }
}