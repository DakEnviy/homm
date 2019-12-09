using System;
using System.Collections.Generic;
using OneOf;

namespace HOMM.BattleObjects
{
    using SkillSource = OneOf<BattleArmy, BattleUnitsStack>;
    using SkillTarget = OneOf<Battle, BattleArmy, IList<BattleUnitsStack>, BattleUnitsStack>;

    public abstract class Skill
    {
        public void Use(SkillSource source, SkillTarget target)
        {
            source.Switch(
                army => Use(army, target),
                stack => Use(stack, target)
            );
        }

        public void Use(BattleArmy source, SkillTarget target)
        {
            target.Switch(
                battle => Use(source, battle),
                army => Use(source, army),
                list => Use(source, list),
                stack => Use(source, stack)
            );
        }

        public void Use(BattleUnitsStack source, SkillTarget target)
        {
            target.Switch(
                battle => Use(source, battle),
                army => Use(source, army),
                list => Use(source, list),
                stack => Use(source, stack)
            );
        }

        public virtual void Use(BattleArmy source, Battle target)
        {
            throw new NotImplementedException();
        }

        public virtual void Use(BattleArmy source, BattleArmy target)
        {
            throw new NotImplementedException();
        }

        public virtual void Use(BattleArmy source, IList<BattleUnitsStack> targets)
        {
            throw new NotImplementedException();
        }

        public virtual void Use(BattleArmy source, BattleUnitsStack target)
        {
            throw new NotImplementedException();
        }

        public virtual void Use(BattleUnitsStack source, Battle target)
        {
            throw new NotImplementedException();
        }

        public virtual void Use(BattleUnitsStack source, BattleArmy target)
        {
            throw new NotImplementedException();
        }

        public virtual void Use(BattleUnitsStack source, IList<BattleUnitsStack> targets)
        {
            throw new NotImplementedException();
        }

        public virtual void Use(BattleUnitsStack source, BattleUnitsStack target)
        {
            throw new NotImplementedException();
        }
    }
}